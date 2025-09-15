using QMS.BL.DTOs.SubscriptionDTOs;
using QMS.BL.Results;

namespace QMS.BL.Services.Implementations;

public class SubscriptionService : ISubscriptionService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SubscriptionService> _logger;

    public SubscriptionService(IMapper mapper,
        IUnitOfWork unitOfWork,
        ILogger<SubscriptionService> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<SubscriptionDTO>> CreateSubscriptionAsync(CreateSubscriptionDTO dto, string userId)
    {
        _logger.LogInformation("Starting subscription creation process");
        _logger.LogDebug("Incoming subscription data {@SubscriptionData}", dto);

        try
        {
            _logger.LogDebug("Mapping DTO to Subscription entity");
            var subscription = _mapper.Map<Subscription>(dto);

            subscription.CreatedBy = userId;
            subscription.ModifiedBy = userId;

            _logger.LogDebug("Add new Subscription to database");

            var stopwatch = Stopwatch.StartNew();
            await _unitOfWork.Subscriptions.AddAsync(subscription);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database operation completed in {stopwatch.ElapsedMilliseconds}ms");

            _logger.LogInformation($"Successful created Subscription with ID `{subscription.SubscriptionID}`");

            return Result<SubscriptionDTO>.Success(_mapper.Map<SubscriptionDTO>(subscription), StatusCodes.Status201Created);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error while creating subscription");
            return Result<SubscriptionDTO>.Failure("Failed to save subscription due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error while creating subscription");
            return Result<SubscriptionDTO>.Failure("Failed to process subscription data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Create Subscription");
            return Result<SubscriptionDTO>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<Result<SubscriptionDTO>> DeleteSubscriptionAsync(int id)
    {
        _logger.LogInformation($"Starting subscription deletion for ID {id}");

        try
        {
            _logger.LogDebug($"Fetching subscription with ID {id}");
            var subscription = await _unitOfWork.Companies.GetByIdAsync(id);

            if (subscription is null)
            {
                _logger.LogWarning($"Subscription not found for deletion: {id}");
                return Result<SubscriptionDTO>.Failure($"No subscription found with ID {id}", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Deleting subscription ID: {id})");

            var stopwatch = Stopwatch.StartNew();
            _unitOfWork.Companies.Delete(subscription);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Delete operation completed in {stopwatch.ElapsedMilliseconds}ms");


            return Result<SubscriptionDTO>.Success(_mapper.Map<SubscriptionDTO>(subscription), StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error deleting subscription {id}");
            return Result<SubscriptionDTO>.Failure("An error occurred while deleting the subscription", StatusCodes.Status500InternalServerError);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict deleting subscription {id}");
            return Result<SubscriptionDTO>.Failure("The subscription was deleted by another user. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error deleting subscription {id}");
            return Result<SubscriptionDTO>.Failure("Failed to delete subscription due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting subscription with ID {id}");
            return Result<SubscriptionDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<Result<IEnumerable<SubscriptionDTO>>> GetAllSubscriptionsAsync()
    {
        _logger.LogInformation("Starting retrieval of all subscriptions.");

        try
        {
            var orderBy = new List<(Expression<Func<Subscription, object>> keySelector, bool ascending)>
            {
                (c => c.SubscriptionDate,true)
            };

            _logger.LogDebug("Querying database for subscriptions");

            var stopwatch = Stopwatch.StartNew();
            var subscriptions = await _unitOfWork.Subscriptions.GetAllAsync(orderBy: orderBy);
            stopwatch.Stop();

            _logger.LogDebug($"Database query completed in {stopwatch.ElapsedMilliseconds}ms");

            if (!subscriptions.Any())
            {
                _logger.LogInformation("No subscriptions found in database");
                return Result<IEnumerable<SubscriptionDTO>>.Failure("No subscriptions found", StatusCodes.Status204NoContent);
            }

            _logger.LogInformation($"Retrieved {subscriptions.Count()} subscriptions");

            return Result<IEnumerable<SubscriptionDTO>>.Success(_mapper.Map<IEnumerable<SubscriptionDTO>>(subscriptions), StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving subscriptions data");
            return Result<IEnumerable<SubscriptionDTO>>.Failure("An error occurred while mapping subscriptions data.", StatusCodes.Status500InternalServerError);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Database error while retrieving subscriptions");
            return Result<IEnumerable<SubscriptionDTO>>.Failure("Unable to retrieve subscriptions at this time", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Getting All Subscriptions");
            return Result<IEnumerable<SubscriptionDTO>>.Failure("An error occurred while processing your request", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<Result<SubscriptionDTO>> GetSubscriptionByIdAsync(int id)
    {
        _logger.LogInformation($"Starting subscription retrieval for ID {id}");
        try
        {
            _logger.LogDebug($"Qeurying database for subscription {id}");

            var stopwatch = Stopwatch.StartNew();
            var subscription = await _unitOfWork.Subscriptions.GetByIdAsync(id);
            stopwatch.Stop();

            _logger.LogDebug($"Database query completed in {stopwatch.ElapsedMilliseconds}ms");

            if (subscription is null)
            {
                _logger.LogWarning($"Subscription not found for ID {id}");
                Result<SubscriptionDTO>.Failure($"Subscription with ID {id} not found", StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"Successful retrieval subscription (ID: {subscription.SubscriptionID})");


            return Result<SubscriptionDTO>.Success(_mapper.Map<SubscriptionDTO>(subscription), StatusCodes.Status200OK);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, "Mapping error while retrieving subscription data.");
            return Result<SubscriptionDTO>.Failure("An error occurred while mapping subscription data.", StatusCodes.Status500InternalServerError);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Database error while retrieving subscription");
            return Result<SubscriptionDTO>.Failure("Unable to retrieve subscription at this time", StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting subscription with ID {id}");
            return Result<SubscriptionDTO>.Failure("An error occurred while processing your request",StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<Result<SubscriptionDTO>> UpdateSubscriptionAsync(int id, UpdateSubscriptionDTO dto, string userId)
    {
        _logger.LogInformation($"Starting subscription update for ID {id}.");
        _logger.LogDebug("Incoming subscription data {@SubscriptionData}", dto);

        try
        {
            _logger.LogDebug($"Retrieving subscription `{id}` for update");
            var subscription = await _unitOfWork.Subscriptions.GetByIdAsync(id);
            if (subscription is null)
            {
                _logger.LogWarning($"Subscription not found for update: {id}");
                return Result<SubscriptionDTO>.Failure($"Subscription with ID `{id}` not found", StatusCodes.Status404NotFound);
            }

            _logger.LogDebug("Mapping DTO to Subscription entity");
            _mapper.Map(dto, subscription);

            subscription.ModifiedBy = userId;

            _logger.LogInformation($"Updating subscription `{id}`");

            var stopwatch = Stopwatch.StartNew();
            _unitOfWork.Subscriptions.Update(subscription);
            await _unitOfWork.CompleteAsync();
            stopwatch.Stop();

            _logger.LogDebug($"Database update completed in {stopwatch.ElapsedMilliseconds}ms");

            return Result<SubscriptionDTO>.Success(_mapper.Map<SubscriptionDTO>(subscription), StatusCodes.Status200OK);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, $"Concurrency conflict updating subscription {id}");
            return Result<SubscriptionDTO>.Failure("The subscription was modified by another user. Please refresh and try again.", StatusCodes.Status409Conflict);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Database error updating subscription {id}");
            return Result<SubscriptionDTO>.Failure("Failed to update subscription due to database constraints", StatusCodes.Status503ServiceUnavailable);
        }
        catch (AutoMapperMappingException ex)
        {
            _logger.LogError(ex, $"Mapping error updating subscription {id}");
            return Result<SubscriptionDTO>.Failure("Failed to process subscription data", StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when update subscription");
            return Result<SubscriptionDTO>.Failure("An unexpected error occurred", StatusCodes.Status500InternalServerError);
        }
    }
}
