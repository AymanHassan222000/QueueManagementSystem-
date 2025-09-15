using QMS.DAL.Entities.EntityBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QMS.DAL.Models;

public class IdentityType : Entity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public byte IdentityTypeID { get; set; }

	[Required]
	[MaxLength(100)]
	public string Name { get; set; } = null!;
}
