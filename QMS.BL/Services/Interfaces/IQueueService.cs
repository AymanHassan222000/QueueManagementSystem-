using QMS.BL.DTOs.QueueDTOs;

namespace QMS.BL.Services.Interfaces;

public interface IQueueService
{
    Task<Result<QueueResponseDTO>> CreateQueueAsync(QueueRequestDTO dto, string userId);
    Task<Result<IEnumerable<QueueResponseDTO>>> GetAllQueueAsync();
    Task<Result<QueueResponseDTO>> GetQueueByIdAsync(int id);
    Task<Result<QueueResponseDTO>> UpdateQueueAsync(int id, QueueRequestDTO dto, string userId);
    Task<Result<QueueResponseDTO>> DeleteQueueAsync(int id);

}
