using System.ComponentModel.DataAnnotations;

namespace QMS.DAL.Models;

public class UserQueueNeededData
{

	[Required]
	[MaxLength(200)]
	public string Value { get; set; } = null!;

	[Required]
	public int ServiceNeededDataID { get; set; }
	public ServiceNeededData ServiceNeededData { get; set; }


	[Required]
	public int UserQueueID { get; set; }
	public UserQueue UserQueue { get; set; }

}
