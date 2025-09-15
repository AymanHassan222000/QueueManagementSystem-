using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.CompanyDTOs;

public class CompanyBaseDTO 
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

}
