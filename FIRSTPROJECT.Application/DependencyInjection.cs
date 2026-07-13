namespace FIRSTPROJECT.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CategoryService>();

        return services;
    }
}