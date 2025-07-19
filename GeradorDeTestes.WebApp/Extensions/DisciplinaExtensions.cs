using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.WebApp.Models;

namespace GeradorDeTestes.WebApp.Extensions
{
    public static class DisciplinaExtensions
    {
        public static Disciplina ParaEntidade(this FormularioDisciplinaViewModel formularioVM)
        {
            return new Disciplina(formularioVM.Nome);
        }
        public static DetalhesDisciplinaViewModel ParaDetalhesVM(this Disciplina categoria)
        {
            return new DetalhesDisciplinaViewModel(
                    categoria.Id,
                    categoria.Nome
            );
        }
    }
}
