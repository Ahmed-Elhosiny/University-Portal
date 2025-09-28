using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using University_Portal.Models;

namespace University_Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<UniversityMvcContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = 10 * 1024 * 1024; // 10MB
                options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10MB
                options.MultipartHeadersLengthLimit = 10 * 1024 * 1024; // 10MB
                options.ValueCountLimit = int.MaxValue;
                options.KeyLengthLimit = int.MaxValue;
            });

            builder.Services.AddMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();

                app.Use(async (context, next) =>
                {
                    context.Response.Headers.Append("X-Frame-Options", "DENY");
                    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
                    context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
                    await next();
                });
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.MapStaticAssets();
            }
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.MapControllerRoute(
                name: "studentDetails",
                pattern: "student/{id:int}",
                defaults: new { controller = "Student", action = "Details" });

            app.MapControllerRoute(
                name: "courseDetails",
                pattern: "course/{id:int}",
                defaults: new { controller = "Course", action = "Details" });

            app.Run();
        }
    }
}
