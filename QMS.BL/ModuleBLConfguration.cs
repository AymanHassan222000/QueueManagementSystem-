using Microsoft.Extensions.DependencyInjection;
using QMS.API.Helper;
using QMS.BL.Helper;
using QMS.BL.Services;
using QMS.BL.Services.Implementations;

namespace QMS.DAL;

public static class ModuleBLConfguration
{
    public static IServiceCollection BLConfguration(this IServiceCollection services)
    {

        // Add Auto Mapper
        services.AddAutoMapper(typeof(CompanyProfile));
        services.AddAutoMapper(typeof(BranchProfile));
        services.AddAutoMapper(typeof(RoleProfile));
        services.AddAutoMapper(typeof(SubscriptionProfile));
        services.AddAutoMapper(typeof(QueueProfile));
        services.AddAutoMapper(typeof(FeedbackProfile));
        services.AddAutoMapper(typeof(UserProfile));
        services.AddAutoMapper(typeof(IdentityTypeProfile));

        //services.AddScoped<IAuthService, AuthService>();
        //services.AddScoped<UserRepository>();

        services.AddTransient<ICompanyService, CompanyService>();
        services.AddTransient<IBranchService, BranchService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ISubscriptionService, SubscriptionService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IQueueService, QueueService>();
        services.AddTransient<IIdentityTypeService, IdentityTypeService>();
            
        return services;
    }
}
