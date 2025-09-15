using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.QueueDTOs;

public class QueueBaseDTO
{
    [StringLength(40, MinimumLength = 2)]
    public string? Name { get; set; }
    public string? Description { get; set; }

    [StringLength(20, MinimumLength = 1)]
    public string? StartWithCharacter { get; set; }
    public int? BranchId { get; set; }
}
