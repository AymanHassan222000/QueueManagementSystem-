using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.DTOs
{
	public class QueueDTO
	{
		[MaxLength(40)]
		public required string Name { get; set; }

		public required string Description { get; set; }

		[MaxLength(20)]
		public string? StartWithCharacter { get; set; }

		public TimeSpan ExpectedServiceTime { get; set; }

		public DateTime? CreatedOn { get; set; }

		[MaxLength(100)]
		public required string CreatedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		[MaxLength(100)]
		public required string ModifiedBy { get; set; }

		public long BranchId { get; set; }

	}
}
