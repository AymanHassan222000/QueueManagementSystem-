using QMS.DAL.Entities.EntityBase;
using System.ComponentModel.DataAnnotations;

namespace QMS.DAL.Models;

public class ServiceNeededData : Entity
{
    public int ServiceNeededDataID { get; set; }

    [Required]
    [MaxLength(100)]
    public string DataType { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string TitleData { get; set; } = null!;

    public byte ServiceId { get; set; }
    public Service Service { get; set; }
}
