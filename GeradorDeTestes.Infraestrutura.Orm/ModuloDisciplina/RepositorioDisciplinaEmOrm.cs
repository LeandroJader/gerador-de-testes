using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina
{
    public class RepositorioDisciplinaEmOrm : RepositorioBaseEmOrm<Disciplina>, IRepositorioDisciplina
    {
        public RepositorioDisciplinaEmOrm(GeradorDeTestesDbContext contexto) : base(contexto)
        {
        }
    }
}
