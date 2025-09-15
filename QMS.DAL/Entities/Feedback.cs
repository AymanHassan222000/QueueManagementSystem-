using QMS.DAL.Entities.EntityBase;
using System.ComponentModel.DataAnnotations;

namespace QMS.DAL.Models;

public class Feedback : Entity
{
	public int FeedbackID { get; set; }

	[Required]
	public string Comment { get; set; } = null!;

	public decimal? Rate { get; set; }

	public int UserQueueID { get; set; }
	public UserQueue UserQueue { get; set; }
}
