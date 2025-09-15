using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QMS.DAL.Context;
using QMS.DAL.Repositories.Interfaces;
using QMS.DAL.Repositories;
using QMS.DAL.UnitOfWork;

namespace QMS.DAL;

public static class ModuleDALConfiguration
{
    public static IServiceCollection DALConfiguration(this IServiceCollection services
        , string? connectionString = null)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, b =>
                b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            )
        );

        services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

        return services;
    }
}
