using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.WebApp.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace GeradorDeTestes.WebApp.Models
{
    public class FormularioDisciplinaViewModel
    {
        [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
        [MinLength(3, ErrorMessage = "O campo \"Nome\" precisa conter ao menos 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O campo \"Nome\" precisa conter no máximo 100 caracteres.")]
        public string Nome { get; set; }

    }

    public class CadastrarDisciplinaViewModel : FormularioDisciplinaViewModel
    {
        public CadastrarDisciplinaViewModel() { }

        public CadastrarDisciplinaViewModel(string nome) : this()
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
