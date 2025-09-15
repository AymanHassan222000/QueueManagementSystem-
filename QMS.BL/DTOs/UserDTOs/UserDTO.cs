using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.UserDTOs;

public class UserDTO:UserBaseDTO
{
    public int UserID { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public string CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; } = DateTime.Now;
    public string ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}
