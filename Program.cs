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

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            // Default Lifetime for AddDbContext => Scoped
            builder.Services.AddDbContext<UniversityMvcContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                //ServiceLifetime.Transient,
                //ServiceLifetime.Singleton
            );



            // Configure file upload limits
            builder.Services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = 10 * 1024 * 1024;
                options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10MB
                options.MultipartHeadersLengthLimit = 10 * 1024 * 1024;
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
