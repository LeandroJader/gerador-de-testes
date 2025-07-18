using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloMateria;

public class RepositorioMateriaEmOrm : RepositorioBaseEmOrm<Materia>, IRepositorioMateria
{
    public RepositorioMateriaEmOrm(GeradorDeTestesDbContext contexto) : base(contexto)
    {
    }
}
