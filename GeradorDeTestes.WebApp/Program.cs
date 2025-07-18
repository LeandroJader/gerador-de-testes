using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;
using GeradorDeTestes.WebApp.ActionFilters;
using GeradorDeTestes.WebApp.DependencyInjection;
using GeradorDeTestes.WebApp.Orm;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeTestes.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<LogarAcaoAttribute>();
            });

            builder.Services.AddScoped<IRepositorioMateria, RepositorioMateriaEmOrm>();

            builder.Services.AddEntityFrameworkConfig(builder.Configuration);

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.ApplyMigrations();
            app.UseAntiforgery();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
