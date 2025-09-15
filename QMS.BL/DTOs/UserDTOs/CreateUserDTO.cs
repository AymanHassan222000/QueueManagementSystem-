using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.UserDTOs;

public class CreateUserDTO : UserBaseDTO
{
    [Required]
    [MaxLength(100)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }

    public List<string> IdentityIDs { get; set; } = new();
    public List<byte> IdentityTypeIDs { get; set; } = new();

}
