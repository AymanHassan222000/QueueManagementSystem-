using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.QueueDTOs;

public class QueueResponseDTO : QueueBaseDTO
{
    public TimeSpan ExpectedServiceTime { get; set; }

    public DateTime CreatedOn { get; set; } 

    [Required]
    [MaxLength(100)]
    public string CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; } 

    [Required]
    [MaxLength(100)]
    public string ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
    
}
