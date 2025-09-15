using System.ComponentModel.DataAnnotations;

namespace QMS.DAL.Entities.EntityBase;

public class Entity
{
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    [Required]
    [MaxLength(100)]
    public string CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; } = DateTime.Now;

    [Required]
    [MaxLength(100)]
    public string ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}
