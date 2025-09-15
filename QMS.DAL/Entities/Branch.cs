using QMS.DAL.Entities.EntityBase;
using System.ComponentModel.DataAnnotations;

namespace QMS.DAL.Models;

public class Branch : Entity
{
    public int BranchID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(20)]
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

    public decimal Longitude { get; set; }

    public decimal Latitude { get; set; }

    public int CompanyID { get; set; }

    //Navigation Properties
    public Company Company { get; set; }

    public IEnumerable<User> Users { get; set; } = new HashSet<User>();
    public IEnumerable<Queue> Queues { get; set; } = new HashSet<Queue>();
}
