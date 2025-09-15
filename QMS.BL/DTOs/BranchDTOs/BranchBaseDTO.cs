using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.BranchDTOs;

public class BranchBaseDTO
{
    [Required]
    [StringLength(100,MinimumLength = 2)]
    public string Name { get; set; } 

    [Required]
    [RegularExpression(@"^\+?[0-9\s\-()]{7,20}$", ErrorMessage = "Invalid phone number format.")]
    [Phone]
    public string Phone { get; set; } 

    [Required]
    [StringLength(50,MinimumLength = 2)]
    public string Street { get; set; } 

    [Required]
    [StringLength(50,MinimumLength = 2)]
    public string City { get; set; }

    [StringLength(50, MinimumLength = 2)]
    public string? State { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Country { get; set; }

    [Required]
    [Range(-180, 180)]
    public decimal Longitude { get; set; }

    [Required]
    [Range(-90, 90)]
    public decimal Latitude { get; set; }

    [Required]
    public int CompanyID { get; set; }
}
