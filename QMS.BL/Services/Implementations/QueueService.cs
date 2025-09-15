using QMS.BL.DTOs.BranchDTOs;
using QMS.BL.DTOs.QueueDTOs;

namespace QMS.BL.Services.Implementations;

public class QueueService : IQueueService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<QueueService> _logger;
    private readonly IMapper _mapper;

    public QueueService(IUnitOfWork unitOfWork,
                       ILogger<QueueService> logger,
                       IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<QueueResponseDTO>> CreateQueueAsync(QueueRequestDTO dto, string userId)
    {
        try
        {
            _logger.LogDebug("Mapping DTO to queue entity");
            var queue = _mapper.Map<Queue>(dto);

            queue.CreatedBy = userId;
            queue.ModifiedBy = userId;


            _logger.LogDebug("Add new queue to database");

            var stopwatch = Stopwatch.StartNew();
            await _unitOfWork.Queues.AddAsync(queue);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database operation completed in {stopwatch.ElapsedMilliseconds}ms");

            _logger.LogInformation($"Successful created queue `{queue.Name}` with ID `{queue.QueueID}`");
            return Result<QueueResponseDTO>.Success(_mapper.Map<QueueResponseDTO>(queue), StatusCodes.Status201Created);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error while creating queue `{dto.Name}`");
            return Result<QueueResponseDTO>.Failure("Failed to save queue due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error while creating queue `{dto.Name}`");
            return Result<QueueResponseDTO>.Failure("Failed to process queue data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error create queue");
            return Result<QueueResponseDTO>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }

    }


    public async Task<Result<IEnumerable<QueueResponseDTO>>> GetAllQueueAsync()
    {
        _logger.LogInformation("Starting retrieval of all queues.");

        try
        {
            _logger.LogDebug("Querying database for queues");

            var orderBy = new List<(Expression<Func<Queue, object>> keySlector, bool ascending)>
            {
                (b => b.Name,true)
            };

            var stopwatch = Stopwatch.StartNew();
            var queues = await _unitOfWork.Queues.GetAllAsync(orderBy: orderBy);
            stopwatch.Stop();
            _logger.LogDebug($"Database query completed in {stopwatch.ElapsedMilliseconds}ms");

            if (!queues.Any())
            {
                _logger.LogInformation("No queues found in database");
                return Result<IEnumerable<QueueResponseDTO>>.Failure("No branches found", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Retrieved {queues.Count()} branches");
            var queueDto = _mapper.Map<IEnumerable<QueueResponseDTO>>(queues);

            return Result<IEnumerable<QueueResponseDTO>>.Success(queueDto, StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving queues data.");
            return Result<IEnumerable<QueueResponseDTO>>.Failure("An error occurred while mapping queue data.", StatusCodes.Status500InternalServerError);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Database error while retrieving queues");
            return Result<IEnumerable<QueueResponseDTO>>.Failure("Unable to retrieve queues at this time", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all queues");
            return Result<IEnumerable<QueueResponseDTO>>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<Result<QueueResponseDTO>> GetQueueByIdAsync(int id)
    {
        _logger.LogInformation($"Starting queue retrieval for ID {id}");
        try
        {
            _logger.LogDebug($"Qeurying database for queue {id}");

            var stopwatch = Stopwatch.StartNew();
            var queue = await _unitOfWork.Queues.GetByIdAsync(id);
            stopwatch.Stop();

            _logger.LogDebug($"Database query completed in {stopwatch.ElapsedMilliseconds}ms");

            if (queue is null)
            {
                _logger.LogWarning($"Queue not found for ID {id}");
                return Result<QueueResponseDTO>.Failure($"Queue with ID {id} not found", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Successful retrieval queue `{queue.Name}` (ID: {queue.QueueID})");

            return Result<QueueResponseDTO>.Success(_mapper.Map<QueueResponseDTO>(queue), StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving queue data.");
            return Result<QueueResponseDTO>.Failure("An error occurred while mapping queue data.", StatusCodes.Status500InternalServerError);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Database error while retrieving queue");
            return Result<QueueResponseDTO>.Failure("Unable to retrieve queue at this time", StatusCodes.Status503ServiceUnavailable);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting queue with ID {id}");
            return Result<QueueResponseDTO>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<Result<QueueResponseDTO>> UpdateQueueAsync(int id, QueueRequestDTO dto, string userId)
    {
        _logger.LogInformation($"Starting queue update for ID {id}.");
        _logger.LogDebug("Incoming queue data {@BranchData}", dto);

        try
        {
            _logger.LogDebug($"Retrieving queue `{id}` for update");

            var queue = await _unitOfWork.Queues.GetByIdAsync(id);

            if (queue is null)
            {
                _logger.LogWarning($"Queue not found for update: {id}");
                return Result<QueueResponseDTO>.Failure($"Queue with ID `{id}` not found", StatusCodes.Status404NotFound);
            }

            _logger.LogDebug("Mapping DTO to Queue entity");
            _mapper.Map(dto, queue);
            queue.ModifiedBy = userId;

            _logger.LogInformation($"Updating queue `{id}`");

            var stopwatch = Stopwatch.StartNew();
            _unitOfWork.Queues.Update(queue);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database update completed in {stopwatch.ElapsedMilliseconds}ms");

            return Result<QueueResponseDTO>.Success(_mapper.Map<QueueResponseDTO>(queue), StatusCodes.Status200OK);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict updating queue {id}");
            return Result<QueueResponseDTO>.Failure("The queue was modified by another user. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error updating queue {id}");
            return Result<QueueResponseDTO>.Failure($"Failed to update queue due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error updating queue {id}");
            return Result<QueueResponseDTO>.Failure("Failed to process queue data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when update queue");
            return Result<QueueResponseDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }

    }
    public async Task<Result<QueueResponseDTO>> DeleteQueueAsync(int id)
    {
        _logger.LogInformation($"Starting queue deletion for ID {id}");
        try
        {
            _logger.LogDebug($"Fetching branch with ID {id}");
            var queue = await _unitOfWork.Queues.GetByIdAsync(id);

            if (queue is null)
            {
                _logger.LogWarning($"Queue not found for deletion: {id}");
                return Result<QueueResponseDTO>.Failure($"No queue found with ID {id}", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Deleting queue `{queue.Name}` (ID: {id})");

            var stopwatch = Stopwatch.StartNew();
            _unitOfWork.Queues.Delete(queue);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Delete operation completed in {stopwatch.ElapsedMilliseconds}ms");

            return Result<QueueResponseDTO>.Success(_mapper.Map<QueueResponseDTO>(queue), StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error deleting queue {id}");
            return Result<QueueResponseDTO>.Failure("An error occurred while deleting the queue", StatusCodes.Status500InternalServerError);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict deleting queue {id}");
            return Result<QueueResponseDTO>.Failure("The queue was deleted by another user. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error deleting queue {id}");
            return Result<QueueResponseDTO>.Failure("Failed to delete queue due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting queue with ID {id}");
            return Result<QueueResponseDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }

    }

}
