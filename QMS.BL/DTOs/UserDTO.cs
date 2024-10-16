﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.DTOs
{
	public class UserDTO
	{
		[Required, MaxLength(30)]
		public required string FirstName { get; set; }

		[Required, MaxLength(30)]
		public required string LastName { get; set; }

		[Required, MaxLength(10)]
		public required string Gender { get; set; }

		[Required]
		public DateTime DateOfBirth { get; set; }

		[Required, MaxLength(100), EmailAddress]
		public required string Email { get; set; }

		[Required, MaxLength(15)]
		public required string Phone { get; set; }

		[Required, MaxLength(50)]
		public required string Street { get; set; }

		[Required, MaxLength(50)]
		public required string City { get; set; }

		[MaxLength(50)]
		public string? State { get; set; }

		[MaxLength(50)]
		public required string Country { get; set; }

		[MaxLength(100)]
		public required string UserName { get; set; }

		[MaxLength(100)]
		public required string Password { get; set; }

		public DateTime? CreatedOn { get; set; }

		[MaxLength(100)]
		public required string CreatedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		[MaxLength(100)]
		public required string ModifiedBy { get; set; }

		public long? BranchId { get; set; }

	}
}