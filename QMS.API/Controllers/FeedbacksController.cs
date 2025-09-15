using QMS.BL.DTOs.FeedbackDTOs;
using QMS.DAL.UnitOfWork;

namespace QMS.API.Controllers;

[Route("api/[controller]/[Action]")]
[ApiController]
public class FeedbacksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<FeedbacksController> _logger;
    public FeedbacksController(IMapper mapper, 
                               IUnitOfWork unitOfWork,
                               ILogger<FeedbacksController> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllFeedbacksAsync()
    {
        try
        {
            return Ok(await _unitOfWork.Feedbacks.GetAllAsync());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Getting All Feedbacks");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFeedbackByIdAsync(int id)
    {
        try
        {
            var feedback = await _unitOfWork.Feedbacks.GetByIdAsync(id);

            if (feedback is null)
                return NotFound();

            return Ok(feedback);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting Feedback with ID {id}");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpPost()]
    public async Task<IActionResult> CreateFeedbackAsync(FeedbackDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var feedbacks = _mapper.Map<Feedback>(dto);

            await _unitOfWork.Feedbacks.AddAsync(feedbacks);

            return StatusCode(201, feedbacks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating Feedback");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFeedbackAsync(int id, [FromBody] FeedbackDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var feedback = await _unitOfWork.Feedbacks.GetByIdAsync(id);

            if (feedback == null)
                return NotFound();

            _mapper.Map(dto, feedback);
            _unitOfWork.Feedbacks.Update(feedback);
            await _unitOfWork.CompleteAsync();

            return Ok(feedback);
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, $"Error updating Feedback with ID {id}");
            return StatusCode(500, "An error occurred while processing your request");

        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeedbackAsync(int id)
    {
        try
        {
            var feedback = await _unitOfWork.Feedbacks.GetByIdAsync(id);

            if (feedback is null)
                return NotFound();

            _unitOfWork.Feedbacks.Delete(feedback);

            return Ok(feedback);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting Feedback with ID {id}");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

}
