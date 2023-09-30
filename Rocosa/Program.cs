using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Rocosa_AccesoDatos.Datos;
using Rocosa_AccesoDatos.Datos.Repository.IRepository;
using Rocosa_Utilidades;

namespace Rocosa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Agregar conexion a la base de datos
            //const string connectionName = "DefaultConnection";
            //var connection = builder.Configuration.GetConnectionString(connectionName);
            //builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connection));

            builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // fin conexion a la base de datos

            // Add services Identity
            builder.Services.AddIdentity<IdentityUser ,IdentityRole>()
                            .AddDefaultTokenProviders()
                            .AddDefaultUI()
                            .AddEntityFrameworkStores<ApplicationDbContext>();
            // Fin Identity implementetion

            builder.Services.AddScoped<ICategoriaRepository, ICategoriaRepository>();

            // Implementar Email Sender
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            // Fin

            builder.Services.AddControllersWithViews();

            // Agregar Sesiones
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession(Options =>
            {
                Options.IdleTimeout = TimeSpan.FromMinutes(10);
                Options.Cookie.HttpOnly = true;
                Options.Cookie.IsEssential = true;
            });
            // fin de Sesiones

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

            // Add Aunthe
            app.UseAuthentication();
            app.UseAuthorization();

            // Uso de Sesiones
            app.UseSession();

            // Add vistas Razor Pages
            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}