using QMS.DAL.Entities.EntityBase;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QMS.DAL.Models;

public class Queue : Entity
{
	public int QueueID { get; set; }

	[Required]
	[MaxLength(40)]
	public string Name { get; set; } = null;

	[Required]
	public string Description { get; set; } = null;

	[MaxLength(20)]
	public string? StartWithCharacter { get; set; }

	public TimeSpan ExpectedServiceTime { get; set; }

	public int BranchId { get; set; }

	public Branch Branch { get; set; } 

	public IEnumerable<UserQueue> UserQueues { get; set; }
}
