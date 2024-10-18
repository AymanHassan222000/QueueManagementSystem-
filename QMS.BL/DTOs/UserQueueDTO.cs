using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.DTOs
{
	public class UserQueueDTO
	{
		public DateTime? StartedIn { get; set; }

		public DateTime? EndedIn { get; set; }

		public DateTime? CreatedOn { get; set; }

		[MaxLength(100)]
		public required string CreatedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		[MaxLength(100)]
		public required string ModifiedBy { get; set; }

		public long UserId { get; set; }

		public int QueueId { get; set; }

		public byte ServiceId { get; set; }

		public bool? IsDeleted { get; set; }

	}
}
