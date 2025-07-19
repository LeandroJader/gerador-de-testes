using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Infraestrutura.Orm.ModuloQuestao;
using GeradorDeTestes.WebApp.ActionFilters;
using GeradorDeTestes.WebApp.DependencyInjection;
using GeradorDeTestes.WebApp.Orm;

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

            // builder.Services.AddScoped<IRepositorioDeVoces, RepositorioDeVoces>();
            
            builder.Services.AddScoped<IRepositorioQuestao, RepositorioQuestaoEmOrm>();

            builder.Services.AddEntityFrameworkConfig(builder.Configuration);

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
                app.UseExceptionHandler("/erro");
            else
                app.UseDeveloperExceptionPage();

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
