using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.UserDTOs;

public class UserBaseDTO
{

    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(30)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(10)]
    public string Gender { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(15)]
    public string Phone { get; set; }

    [Required]
    [MaxLength(50)]
    public string Street { get; set; }

    [Required]
    [MaxLength(50)]
    public string City { get; set; }

    [MaxLength(50)]
    public string? State { get; set; }

    [Required]
    [MaxLength(50)]
    public string Country { get; set; }

    public int? BranchID { get; set; }

}
