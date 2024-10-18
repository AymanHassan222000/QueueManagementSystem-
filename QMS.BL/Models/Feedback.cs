using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.Models
{
	public class Feedback
	{
		public long Id { get; set; }

		public required string Comment { get; set; }

		public decimal? RateValue { get; set; }

		public DateTime CreatedOn { get; set; } = DateTime.Now;

		[MaxLength(100)]
		public required string CreatedBy { get; set; }

		public DateTime ModifiedOn { get; set; } = DateTime.Now;

		[MaxLength(100)]
		public required string ModifiedBy { get; set; }

		public int UserQueueId { get; set; }

		public bool IsDeleted { get; set; }
	}
}
