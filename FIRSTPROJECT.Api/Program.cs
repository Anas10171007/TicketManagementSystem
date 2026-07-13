using FIRSTPROJECT.Application;
using FIRSTPROJECT.Application.Categories.Interfaces;
using FIRSTPROJECT.Infrastructure;
using FluentValidation;
using FIRSTPROJECT.Application.Categories.Validators;
namespace FIRSTPROJECT.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddApplication();

            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDtoValidator>();//now we are goint to use fluent validation instead of using services.

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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
        }
    }
}
