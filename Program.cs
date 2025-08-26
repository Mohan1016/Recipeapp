
//using RecipeApp.Data;
//using Microsoft.EntityFrameworkCore;
//using RecipeApp.Repositories;
//namespace RecepieApp
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            builder.Services.AddDbContext<RecipeDbContext>(options =>
//               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();

//            builder.Services.AddCors(options =>
//            {
//                options.AddPolicy("AllowAll", policy =>
//                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
//            });

//            // Add services to the container.

//            builder.Services.AddControllers();
//            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();
//            app.UseCors("AllowAll");
//            app.UseAuthorization();


//            app.MapControllers();

//            app.Run();
//        }
//    }
//}


//using RecipeApp.Data;
//using Microsoft.EntityFrameworkCore;
//using RecipeApp.Repositories;

//namespace RecepieApp
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Database
//            builder.Services.AddDbContext<RecipeDbContext>(options =>
//                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//            // Repository
//            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();

//            // CORS (allow all for now; later you can restrict to your frontend URL)
//            builder.Services.AddCors(options =>
//            {
//                options.AddPolicy("AllowAll", policy =>
//                    policy.AllowAnyOrigin()
//                          .AllowAnyHeader()
//                          .AllowAnyMethod());
//            });

//            // Controllers + Swagger
//            builder.Services.AddControllers();
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();

//            var app = builder.Build();

//            // --- 🔹 Render Port Binding ---
//            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
//            app.Urls.Add($"http://*:{port}");

//            // Swagger only in dev
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();
//            app.UseCors("AllowAll");
//            app.UseAuthorization();
//            app.MapControllers();

//            app.Run();
//        }
//    }
//}


using RecipeApp.Data;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Repositories;

namespace RecepieApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // --- Database ---
            builder.Services.AddDbContext<RecipeDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // --- Repository ---
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();

            // --- CORS ---
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                    policy.WithOrigins("https://recipeapp-frontend-yh7q.onrender.com") // Frontend URL
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            // --- Controllers & Swagger ---
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // --- Render Port Binding ---
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            app.Urls.Add($"http://*:{port}");

            // --- Swagger (dev only) ---
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHttpsRedirection(); // Only in dev
            }

            // --- Middleware ---
            app.UseCors("AllowFrontend");
            app.UseAuthorization();
            app.MapControllers();

            // --- Run App ---
            app.Run();
        }
    }
}
