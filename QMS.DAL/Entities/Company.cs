using QMS.DAL.Entities.EntityBase;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QMS.DAL.Models;

public class Company : Entity
{
	[Key]
	public int CompanyID { get; set; }

	[Required]
	[MaxLength(100)]
	public string Name { get; set; }

	[Required]
	[MaxLength(100)]
	public string Email { get; set; }

	public IEnumerable<Branch> Branchs { get; set; } = new HashSet<Branch>();

	public IEnumerable<Subscription> Subscriptions { get; set; }

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public IEnumerable<UserRole> UserRoles { get; set; }
}
