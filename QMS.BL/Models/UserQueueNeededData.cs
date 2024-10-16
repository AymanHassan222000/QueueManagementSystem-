using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.Models
{
	public class UserQueueNeededData
	{
		public int Id { get; set; }

		public required string Value { get; set; }

		public DateTime CreatedOn { get; set; } = DateTime.Now;

		public required string CreatedBy { get; set; }

		public DateTime ModifiedOn { get; set; } = DateTime.Now;

		public required string ModifiedBy { get; set; }

		public int ServiceNeededDataId { get; set; }

		public int UserQueueId { get; set; }

	}
}
