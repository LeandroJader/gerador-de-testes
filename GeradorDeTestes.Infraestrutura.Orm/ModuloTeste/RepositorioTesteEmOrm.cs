using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloTeste;

public class RepositorioTesteEmOrm : RepositorioBaseEmOrm<Teste>, IRepositorioTeste
{
    public RepositorioTesteEmOrm(GeradorDeTestesDbContext contexto) : base(contexto)
    {
    }

    public override Teste? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros
            .Include(q => q.Disciplina)
            .Include(q => q.Materia)
            .FirstOrDefault(q => q.Id.Equals(idRegistro));
    }

    public override List<Teste> SelecionarRegistros()
    {
        return registros
            .Include(q => q.Disciplina)
            .Include(q => q.Materia)
            .ToList();
    }
}
