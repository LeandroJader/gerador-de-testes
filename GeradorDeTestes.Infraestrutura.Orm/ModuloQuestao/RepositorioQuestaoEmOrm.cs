using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestao;

public class RepositorioQuestaoEmOrm : RepositorioBaseEmOrm<Questao>, IRepositorioQuestao
{
    public RepositorioQuestaoEmOrm(GeradorDeTestesDbContext contexto) : base(contexto) { }

    public override Questao? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros
            .Include(q => q.Alternativas)
            .FirstOrDefault(q => q.Id.Equals(idRegistro));
    }

    public override List<Questao> SelecionarRegistros()
    {
        return registros
            .Include(q => q.Alternativas)
            .ToList();
    }
}
