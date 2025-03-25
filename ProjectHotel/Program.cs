
using Hotel.Models.Entities;
using Hotel.Repository.Data;
using Hotel.Repository.Implementations;
using Hotel.Repository.Interfaces;
using Hotel.Services.Implementations;
using Hotel.Services.Interfases;
using Hotel.Services.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProjectHotel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.
            // Add Dependency Injection
            builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            builder.Services.AddScoped<IHotelService, HotelService>();
            builder.Services.AddScoped<IHotelRepository, HotelRepository>();
            builder.Services.AddScoped<IRoomsRepository, RoomsRepository>();
            //builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
            //builder.Services.AddScoped<IGuestRepository, GuestRepository>();
            //builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
