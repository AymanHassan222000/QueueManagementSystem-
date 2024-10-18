using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.Models
{
	public class UserQueueNeededData
	{
		public int Id { get; set; }

		[MaxLength(200)]
		public required string Value { get; set; }

		public DateTime CreatedOn { get; set; } = DateTime.Now;

		[MaxLength(100)]
		public required string CreatedBy { get; set; }

		public DateTime ModifiedOn { get; set; } = DateTime.Now;

		[MaxLength(100)]
		public required string ModifiedBy { get; set; }

		public int ServiceNeededDataId { get; set; }

		public int UserQueueId { get; set; }

		public bool IsDeleted { get; set; }

	}
}
