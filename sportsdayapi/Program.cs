namespace sportsdayapi
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using sportsdayapi.Data;
    using sportsdayapi.Services;

    /// <summary>
    /// The program class for the web server
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {
        /// <summary>
        /// Driver method for the web server
        /// </summary>
        /// <param name="args">Arguments related to web server initialization</param>
        public static void Main(string[] args)
        {
            var corsPolicy = "_myCorsPolicy";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("SportsDay"));

            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IEventService, EventService>();

            builder.Services.AddScoped<IUserEventService, UserEventService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    name: corsPolicy,
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:3000", "http://localhost:3000");
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

            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<DataContext>();
                    DataInitializer.Initialize(context);
                }
            }

            app.MapControllers();

            app.UseCors(corsPolicy);

            app.Run();
        }
    }
}