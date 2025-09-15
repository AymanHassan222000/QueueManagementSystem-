using QMS.DAL.Entities.EntityBase;
using System.ComponentModel.DataAnnotations;

namespace QMS.DAL.Models;

public class Role : Entity
{
	public byte RoleID { get; set; }

	[Required]
	[MaxLength(40)]
	public string Name { get; set; }
}
