using QMS.DAL.Entities.EntityBase;
using System.ComponentModel.DataAnnotations;

namespace QMS.DAL.Models;

public class User : Entity
{
    public int UserID { get; set; }

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

    [Required]
    [MaxLength(100)]
    public string UserName { get; set; } 

    [Required]
    [MaxLength(100)]
    public string Password { get; set; } 

    public int? BranchID { get; set; }

    public IEnumerable<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    public IEnumerable<UserIdentity> UserIdentities { get; set; } = new HashSet<UserIdentity>();
}
