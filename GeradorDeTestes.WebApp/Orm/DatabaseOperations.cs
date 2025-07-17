using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace GeradorDeTestes.WebApp.Orm;

public static class DatabaseOperations
{
    public static void ApplyMigrations(this IHost app)
    {
        var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<GeradorDeTestesDbContext>();

        dbContext.Database.Migrate();
    }
}
