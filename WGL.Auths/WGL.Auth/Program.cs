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

            // Logger Configuration.
            builder.Services.AddSerilog(options =>{options.ReadFrom.Configuration(configuration);});

            // Cors Policy-----------------
            // * - represent all origins
            // Can be single or multi i.e. "https://localhost:1010/", "https://locahost:2020"
            builder.Services.AddCors(p => p.AddPolicy("WGLAuthPolicy", build => {
                build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();})); 


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("WGLAuthPolicy");
            app.UseErrorHandlingMiddleware();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}