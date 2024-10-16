using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.DTOs
{
	public class ServiceDTO
	{
		public required string Name { get; set; }

		public required string Description { get; set; }

		public TimeSpan EstimatedTime { get; set; }

		public DateTime? CreatedOn { get; set; }

		public required string CreatedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public required string ModifiedBy { get; set; }

	}
}
