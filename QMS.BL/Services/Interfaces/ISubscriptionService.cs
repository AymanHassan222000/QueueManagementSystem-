using QMS.BL.DTOs.SubscriptionDTOs;

namespace QMS.BL.Services.Interfaces;

public interface ISubscriptionService
{
    Task<Result<SubscriptionDTO>> CreateSubscriptionAsync(CreateSubscriptionDTO dto, string userId);
    Task<Result<IEnumerable<SubscriptionDTO>>> GetAllSubscriptionsAsync();
    Task<Result<SubscriptionDTO>> GetSubscriptionByIdAsync(int id);
    Task<Result<SubscriptionDTO>> UpdateSubscriptionAsync(int id, UpdateSubscriptionDTO dto, string userId);
    Task<Result<SubscriptionDTO>> DeleteSubscriptionAsync(int id);

}
