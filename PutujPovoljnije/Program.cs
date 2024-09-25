using Microsoft.EntityFrameworkCore;
using PutujPovoljnije.Application.Interfaces;
using PutujPovoljnije.Application.Mapper;
using PutujPovoljnije.Application.Services;
using PutujPovoljnije.Domain.Interfaces;
using PutujPovoljnije.Domain.Settings;
using PutujPovoljnije.Infrastructure.Data;
using PutujPovoljnije.Infrastructure.DataScraper;
using PutujPovoljnije.Infrastructure.ExternalApiClients;
using PutujPovoljnije.Infrastructure.Repositories;
using PutujPovoljnije.Infrastructure.Seeder;
using SQLitePCL;

namespace PutujPovoljnije
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Batteries.Init();
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IFlightSearchService, FlightSearchService>();
            builder.Services.AddScoped<IFlightSearchRepository, FlightSearchRepository>();
            builder.Services.AddScoped<IAirportRepository, AirportRepository>();
            builder.Services.AddScoped<IAirportsService, AirportsService>();

            builder.Services.Configure<WebScrapingSettings>(builder.Configuration.GetSection("WebScrapingSettings"));
            builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));

            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddScoped<IRefreshDataService, RefreshDataService>();
            builder.Services.AddScoped<IDataScrapeService, DataScrapeService>();
            builder.Services.AddTransient<TokenHandler>();


            builder.Services.AddHttpClient<IExternalFlightApiClient, AmadeusApiClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["AmadeusBaseUrl"]);
            })
            .AddHttpMessageHandler<TokenHandler>();


            builder.Services.AddDbContext<FlightSearchDbContext>(options =>
                options.UseSqlite(builder.Configuration["ConnectionStringDb"],
                b => b.MigrationsAssembly("PutujPovoljnije.Infrastructure")));

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddControllers();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            WebApplication app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.UseCors("AllowAllOrigins");


            app.MapControllers();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                FlightSearchDbContext context = services.GetRequiredService<FlightSearchDbContext>();

                context.Database.Migrate();

                if (builder.Environment.IsDevelopment())
                {
                    DbInitializer dbInitializer = new DbInitializer(context);
                    await dbInitializer.SeedAsync();
                }

            }

            app.Run();
        }
    }
}
