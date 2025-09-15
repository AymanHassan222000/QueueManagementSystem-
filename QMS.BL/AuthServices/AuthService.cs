using QMS.DAL.Repositories.Interfaces;

namespace QMS.API.AuthenticationServices;

//public class AuthService : IAuthService
//{
//	private readonly UserRepository _userRepository;
//	private readonly IBaseRepository<UserRole> _userRolesRepository;
//	private readonly IMapper _mapper;
//	private readonly JWT _jwt;

//	public AuthService(UserRepository userRepository, IMapper mapper,
//		JWT jwt, IBaseRepository<UserRole> userRolesRepository)
//	{
//		_userRepository = userRepository;
//		_mapper = mapper;
//		_jwt = jwt;
//		_userRolesRepository = userRolesRepository;
//	}

//	public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
//	{
//		var user = await _userRepository.Find(m => m.UserName == model.UserName && m.Password == model.Password);

//		if (user is null)
//			return new AuthModel { Message = "Email or Password is incorrect." };

//		var token = await CreateJwtToken(user);

//		var roles = await _userRepository.GetRoles(user);

//		return new AuthModel
//		{
//			Message = string.Empty,
//			IsAuthenticated = true,
//			UserName = user.UserName,
//			Email = user.Email,
//			Roles = roles,
//			Token = token,
//			ExpiresOn = DateTime.UtcNow.AddMinutes(_jwt.Lifetime)
//		};
//	}

//	public async Task<AuthModel> RegisterAsync(UserDTO dto)
//	{
//		if (await _userRepository.Find(m => m.Email == dto.Email) is not null)
//			return new AuthModel { Message = "Email is already registered!" };

//		if (await _userRepository.Find(m => m.UserName == dto.UserName) is not null)
//			return new AuthModel { Message = "UserName is already registered!" };

//		if (dto.DateOfBirth < new DateTime(1900, 1, 1))
//			return new AuthModel { Message = "Invalid Birthdate" };

//		var user = _mapper.Map<User>(dto);

//		await _userRepository.Add(user);
//		await _userRolesRepository.Add(new UserRole { UserId = user.Id, RoleId = 1 });

//		var token = await CreateJwtToken(user);

//		return new AuthModel
//		{
//			Message = string.Empty,
//			IsAuthenticated = true,
//			UserName = dto.UserName,
//			Email = user.Email,
//			Roles = new List<string>() { "LinePerson" },
//			Token = token,
//			ExpiresOn = DateTime.UtcNow.AddMinutes(_jwt.Lifetime)
//		};

//	}

//	private async Task<string> CreateJwtToken(User user)
//	{
//		var userRoles = new List<Claim>();

//		var roles = await _userRepository.GetRoles(user);

//		foreach (var role in roles)
//			userRoles.Add(new Claim(ClaimTypes.Role, role));

//		var tokenHandler = new JwtSecurityTokenHandler();

//		var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

//		var tokenDescriptor = new SecurityTokenDescriptor
//		{
//			Issuer = _jwt.Issuer,
//			Audience = _jwt.Audience,
//			Expires = DateTime.UtcNow.AddMinutes(_jwt.Lifetime),
//			IssuedAt = DateTime.UtcNow,
//			NotBefore = DateTime.UtcNow,
//			SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256),
//			Subject = new ClaimsIdentity(new Claim[]
//			{
//				new(ClaimTypes.NameIdentifier,user.Id.ToString()),
//				new(ClaimTypes.Email,user.UserName),
//				new(ClaimTypes.Expiration,DateTime.UtcNow.AddMinutes(_jwt.Lifetime).ToString())
//			}
//			.Union(userRoles))
//		};

//		var securityToken = tokenHandler.CreateToken(tokenDescriptor);
//		var accessToken = tokenHandler.WriteToken(securityToken);

//		return accessToken;
//	}

//}
