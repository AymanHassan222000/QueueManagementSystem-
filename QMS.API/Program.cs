using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QMS.API;
using QMS.API.AuthenticationServices;
using QMS.BL.Interfaces;
using QMS.DAL.Data;
using QMS.DAL.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registration Connection String
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection"),
		b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
	)
);

//Registration BaseRepository
builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

//Registration AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

//Registration UserRepository
builder.Services.AddScoped<UserRepository>();

//Registration Auto Mapper
builder.Services.AddAutoMapper(typeof(Program));



//Registration Jwt Bearer Token
var jwtOptions = builder.Configuration.GetSection("JWT").Get<JWT>();
builder.Services.AddSingleton(jwtOptions);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
	{
		options.SaveToken = true;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = jwtOptions.Issuer,
			ValidateAudience = true,
			ValidAudience = jwtOptions.Audience,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
			ValidateLifetime = true,
			ClockSkew = TimeSpan.Zero
		};

	});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
