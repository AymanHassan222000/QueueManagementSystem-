using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.RoleDTOs;

public class RoleBaseDTO
{
    [Required]
    [StringLength(40, MinimumLength = 2)]
    public string Name { get; set; } = null!;

}
