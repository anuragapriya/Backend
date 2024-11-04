using WGL.Auth.Application;
using WGL.Auth.Persistence;
using WGL.Auth.Domain.Settings;
using Serilog;
using WGL.Auth.Extensions;
using WGL.Auth.Persistence.Contexts;

namespace WGL.Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            var configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.Development.json")
                       .Build();

            // Add services to the container.

            builder.Services.AddApplicationLayer();
            builder.Services.AddPersistenceLayer();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<OneLoginSettings>(builder.Configuration.GetSection("OneLoginSettings"));

            //Caching Details.
            builder.Services.AddMemoryCache();
            builder.Services.AddLazyCache();

            builder.Services.AddSerilog(options =>
            {
                options.ReadFrom.Configuration(configuration);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseErrorHandlingMiddleware();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}