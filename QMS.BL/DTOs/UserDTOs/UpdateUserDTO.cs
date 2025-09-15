using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.UserDTOs;

public class UpdateUserDTO : UserBaseDTO
{
    [Required]
    [MaxLength(100)]
    public string Password { get; set; }
}
