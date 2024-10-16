﻿using System;
using System.Collections.Generic;
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

		public required string CreatedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public required string ModifiedBy { get; set; }

		public long UserId { get; set; }

		public int QueueId { get; set; }

		public byte ServiceId { get; set; }


	}
}