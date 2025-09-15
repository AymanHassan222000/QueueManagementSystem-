using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.RoleDTOs;

public class RoleDTO : RoleBaseDTO
{
    public byte RoleID { get; set; }
    public DateTime CreatedOn { get; set; } 
    public string CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; } 
    public string ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}
