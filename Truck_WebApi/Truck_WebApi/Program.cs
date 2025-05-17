
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Truck_BusnessLogic.Services;
using Truck_DataAccess.Entities;
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
            builder.Services.AddScoped<ITruckService, TruckService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Truck Api",
                    Version = "v1",
                    Description = "Truck database entity Crud metods",
                    Contact = new OpenApiContact
                    {
                        Name = "Liutauras Cicinskas",
                        Email = "twinpiligrim@gmail.com",
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
