using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.IdentityTypesDTOs;

public class IdentityTypeResponseDTO:IdentityTypeBaseDTO
{
	public byte IdentityTypeID { get; set; }
	public DateTime? CreatedOn { get; set; }

	[MaxLength(100)]
	public required string CreatedBy { get; set; }

	public DateTime? ModifiedOn { get; set; }

	[MaxLength(100)]
	public required string ModifiedBy { get; set; }

	public bool? IsDeleted { get; set; }
}
