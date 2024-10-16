using QMS.BL.AuthenticationModels;
using QMS.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.Interfaces
{
	public interface IAuthService
	{
		Task<AuthModel> RegisterAsync(UserDTO dto);
		Task<AuthModel> GetTokenAsync(TokenRequestModel model);

	}
}
