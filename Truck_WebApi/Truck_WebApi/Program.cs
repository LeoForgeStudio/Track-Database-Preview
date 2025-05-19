
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Reflection;
using Truck_BusnessLogic.Services;
using Truck_DataAccess.Entities;
using Truck_DataAccess.Entities.Filters;
using Truck_DataAccess.Repositories;


namespace Truck_WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                return new MongoClient(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IRepository<Truck, TruckFilter>, TruckRepository>();
            builder.Services.AddScoped<IRepository<Gearbox, GearboxFilter>, GearboxRepository>();
            builder.Services.AddScoped<IRepository<Engine, EngineFilter>, EngineRepository>();
            builder.Services.AddScoped<IRepository<Manufacturer, ManufacturerFilter>, ManufacturerRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            builder.Services.AddTransient<ITruckService, TruckService>();
            builder.Services.AddTransient<IUserService, UserService>();

            builder.Services.AddTransient<IEngineService, EngineService>();
            builder.Services.AddTransient<IGearboxService, GearboxService>();
            builder.Services.AddTransient<IManufacturerService, ManufacturerService>();

            builder.Services.AddAuthentication("BaseAuth")
                .AddScheme<AuthenticationSchemeOptions, DummyAuthHandler>("BaseAuth", null);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Truck Api",
                    Version = "v1",
                    Description = "Truck database entity CRUD methods.<br/> How to use:<br/>1. Launch Truck_ConsoleApp to create database entrys. <br/>2. Log in through Authorize using the created user. <br/>2 Basic  LOGIN:admin PASS:admin",
                    Contact = new OpenApiContact
                    {
                        Name = "Liutauras Cicinskas",
                        Email = "twinpiligrim@gmail.com",
                    },
                });

                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic auth header"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMiddleware<BaseAuthMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
