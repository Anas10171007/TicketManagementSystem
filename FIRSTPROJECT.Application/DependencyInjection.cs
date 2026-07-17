using FIRSTPROJECT.Application.Tickets.Interfaces;
using FIRSTPROJECT.Application.Tickets.Services;

namespace FIRSTPROJECT.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CategoryService>();
        services.AddScoped<ITicketService, TicketService>();

        return services;
    }
}