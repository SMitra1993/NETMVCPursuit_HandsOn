using Microsoft.EntityFrameworkCore;
using MVCWebAppHandsOn.Data;
using MVCWebAppHandsOn.Services;

namespace MVCWebAppHandsOn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<StudentContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("MVCWebAppContext") ??
                throw new InvalidOperationException("Connection string 'MVCWebAppContext' not found.")));
            // Add services to the container.
            builder.Services.AddScoped<IStudentDetailService, StudentDetailService>();
            builder.Services.AddControllersWithViews();
            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}