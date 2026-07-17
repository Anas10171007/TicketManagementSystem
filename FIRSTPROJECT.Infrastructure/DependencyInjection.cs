using FIRSTPROJECT.Application.Authentication.Interfaces;
using FIRSTPROJECT.Application.Categories.Interfaces;
using FIRSTPROJECT.Application.Categories.Services;
using FIRSTPROJECT.Infrastructure.Identity;
using FIRSTPROJECT.Infrastructure.Persistence;
using FIRSTPROJECT.Infrastructure.Persistence.Repositories;
using FIRSTPROJECT.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FIRSTPROJECT.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
    configuration.GetConnectionString("DefaultConnection")));
        services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}