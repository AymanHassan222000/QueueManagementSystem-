using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.IdentityTypesDTOs;

public class IdentityTypeBaseDTO
{
    [MaxLength(100)]
    public required string Name { get; set; }

}
