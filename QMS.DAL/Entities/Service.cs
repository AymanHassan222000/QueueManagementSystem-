using QMS.DAL.Entities.EntityBase;
using System.ComponentModel.DataAnnotations;

namespace QMS.DAL.Models;

public class Service : Entity
{
	public byte ServiceID { get; set; }

	[Required]
	[MaxLength(100)]
	public string Name { get; set; } = null!;

	[Required]
	public string Description { get; set; } = null!;

	public TimeSpan EstimatedTime { get; set; }

	public IEnumerable<UserQueue> UserQueues { get; set; }
	public IEnumerable<ServiceNeededData> ServiceNeededData { get; set; }
}
