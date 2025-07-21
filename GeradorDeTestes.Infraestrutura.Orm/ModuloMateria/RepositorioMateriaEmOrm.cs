using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;

public class RepositorioMateriaEmOrm : RepositorioBaseEmOrm<Materia>, IRepositorioMateria
{
    public RepositorioMateriaEmOrm(GeradorDeTestesDbContext contexto) : base(contexto)
    {
    }

    public override Materia? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros
            .Include(q => q.Disciplina)
            .FirstOrDefault(q => q.Id.Equals(idRegistro));
    }

    public override List<Materia> SelecionarRegistros()
    {
        return registros
            .Include(q => q.Disciplina)
            .ToList();
    }
}
