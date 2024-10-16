using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.Models
{
	public class Role
	{
		public byte Id { get; set; }

		public required string Name { get; set; }

		public DateTime CreatedOn { get; set; } = DateTime.Now;

		public required string CreatedBy { get; set; }

		public DateTime ModifiedOn { get; set; } = DateTime.Now;

		public required string ModifiedBy { get; set; }

	}
}
