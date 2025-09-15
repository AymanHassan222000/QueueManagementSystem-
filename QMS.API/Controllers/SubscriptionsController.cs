using QMS.BL.DTOs.SubscriptionDTOs;

namespace QMS.API.Controllers;

[Route("api/[controller]/[Action]")]
[ApiController]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;
    public SubscriptionsController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllSubscriptionsAsync()
    {
        var result = await _subscriptionService.GetAllSubscriptionsAsync();

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubscriptionByIdAsync(int id)
    {
        var result = await _subscriptionService.GetSubscriptionByIdAsync(id);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscriptionAsync([FromBody] CreateSubscriptionDTO dto)
    {
        if (!ModelState.IsValid)
            return this.HandleInvalidModelState();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString() ?? "System";

        var result = await _subscriptionService.CreateSubscriptionAsync(dto, userId);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubscriptionAsync(int id, [FromBody] UpdateSubscriptionDTO dto)
    {
        if (!ModelState.IsValid)
            return this.HandleInvalidModelState();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString() ?? "System";

        var result = await _subscriptionService.UpdateSubscriptionAsync(id,dto, userId);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubscriptionAsync(int id)
    {
        var result = await _subscriptionService.DeleteSubscriptionAsync(id);

        if (result.IsFailure)
            return StatusCode(result.StatusCode ?? StatusCodes.Status500InternalServerError, result.Errors);

        return StatusCode(result.StatusCode ?? StatusCodes.Status200OK, result.Value);
    }
}
