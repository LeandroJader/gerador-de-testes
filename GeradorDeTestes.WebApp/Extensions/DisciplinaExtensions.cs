using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.WebApp.Models;

namespace GeradorDeTestes.WebApp.Extensions
{
    public static class DisciplinaExtensions
    {
        public static DetalhesDisciplinaViewModel ParaDetalhesVM(this Disciplina categoria)
        {
            return new DetalhesDisciplinaViewModel(
                    categoria.Id,
                    categoria.Nome

            );
        }
    }
}
