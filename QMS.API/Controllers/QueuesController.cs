using QMS.BL.DTOs.QueueDTOs;

namespace QMS.API.Controllers;

[Route("api/[controller]/[Action]")]
[ApiController]
public class QueuesController : ControllerBase
{
    private readonly IQueueService _queueService;
    public QueuesController(IQueueService queueService)
    {
        _queueService = queueService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllQueuesAsync()
    {
        var result = await _queueService.GetAllQueueAsync();

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status201Created, result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQueueByIdAsync(int id)
    {
        var result = await _queueService.GetQueueByIdAsync(id);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status201Created, result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQueueAsync(QueueRequestDTO dto)
    {
        if (!ModelState.IsValid)
            return this.HandleInvalidModelState();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString() ?? "System";
        var result = await _queueService.CreateQueueAsync(dto, userId);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status201Created, result.Value);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQueueAsync(int id, [FromBody] QueueRequestDTO dto)
    {
        if (!ModelState.IsValid)
            return this.HandleInvalidModelState();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString() ?? "System";
        var result = await _queueService.UpdateQueueAsync(id, dto, userId);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status201Created, result.Value);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQueueAsync(int id)
    {
        var result = await _queueService.DeleteQueueAsync(id);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status201Created, result.Value);
    }
}
