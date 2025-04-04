
using BookingSystem.Data;
using BookingSystem.Repository;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace BookingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var Configuration = builder.Configuration;

            builder.Services.AddControllers();


            builder.Services.AddDbContext<CombinedDbContext>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<PackageRepository>();
            builder.Services.AddScoped<BookandPaymentRepository>();
            builder.Services.AddScoped<InsuranceRepository>();
            builder.Services.AddScoped<AssistanceRepository>();
            builder.Services.AddScoped<ReviewRepository>(provider => new ReviewRepository(Configuration.GetConnectionString("DefaultConnection")));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
