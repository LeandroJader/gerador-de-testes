using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.WebApp.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GeradorDeTestes.WebApp.Models
{
    public class FormularioDisciplinaViewModel
    {
        public string Nome { get; set; }


        public FormularioDisciplinaViewModel(string nome )
        {
            Nome = nome;
        }
    }
    public class VisualizarDisciplinaViewModel
    {
        public List<DetalhesDisciplinaViewModel> Registros { get; set; }

        public VisualizarDisciplinaViewModel(List<Disciplina> categorias)
        {
            Registros = new List<DetalhesDisciplinaViewModel>();

            foreach (var c in categorias)
                Registros.Add(c.ParaDetalhesVM());
        }
    }
    public class DetalhesDisciplinaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }


        public DetalhesDisciplinaViewModel(
            Guid id,
            string nome

        )
        {
            Id = id;
            Nome = nome;


        }

    }
}
