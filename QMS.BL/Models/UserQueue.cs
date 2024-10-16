using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.Models
{
	public class UserQueue
	{
		public int Id { get; set; }

		public DateTime StartedIn { get; set; } = DateTime.Now;

		public DateTime EndedIn { get; set; } = DateTime.Now;

		public int? TimeTaken { get; set; }

		public DateTime CreatedOn { get; set; } = DateTime.Now;

		public required string CreatedBy { get; set; }

		public DateTime ModifiedOn { get; set; } = DateTime.Now;

		public required string ModifiedBy { get; set; }

		public long UserId { get; set; }

		public int QueueId { get; set; }

		public byte ServiceId { get; set; }

	}
}
