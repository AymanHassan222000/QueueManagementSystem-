using QMS.DAL.Entities.EntityBase;
using System.ComponentModel.DataAnnotations;

namespace QMS.DAL.Models;

public class UserIdentity : Entity
{

	[Required]
	[MaxLength(150)]
	public string IdentityID { get; set; } = null!;

	public byte IdentityTypeID { get; set; }
	public IdentityType IdentityType { get; set; }

	public int UserId { get; set; }
	public User User { get; set; }
}
