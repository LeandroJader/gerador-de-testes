using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GeradorDeTestes.WebApp.Models;

public class FormularioMateriaViewModel
{
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [MinLength(3, ErrorMessage = "O campo \"Nome\" precisa conter ao menos 3 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Nome\" precisa conter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo \"Série\" é obrigatório.")]
    public Serie Serie { get; set; }

    [Required(ErrorMessage = "O campo \"Disciplina\" é obrigatório.")]
    public Guid DisciplinaId { get; set; }

    [ValidateNever]
    public List<Disciplina> Disciplinas { get; set; }
}

public class CadastrarMateriaViewModel : FormularioMateriaViewModel
{
    public CadastrarMateriaViewModel() { }

    public CadastrarMateriaViewModel(
        string nome, 
        Serie serie,
        Guid disciplinaId,
        List<Disciplina> disciplinas
    ) : this()
    {
        Nome = nome;
        Serie = serie;
        DisciplinaId = disciplinaId;
        Disciplinas = disciplinas;
    }
}

public class EditarMateriaViewModel : FormularioMateriaViewModel
{
    public Guid Id { get; set; }

    public EditarMateriaViewModel() { }

    public EditarMateriaViewModel(
        Guid id,
        string nome,
        Serie serie,
        Guid disciplinaId,
        List<Disciplina> disciplinas
        ) : this()
    {
        Id = id;
        Nome = nome;
        Serie = serie;
        DisciplinaId = disciplinaId;
        Disciplinas = disciplinas;
    }
}

public class ExcluirMateriaViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public ExcluirMateriaViewModel() { }

    public ExcluirMateriaViewModel(Guid id, string nome) : this()
    {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarMateriasViewModel
{
    public List<DetalhesMateriaViewModel> Registros { get; set; }

    public VisualizarMateriasViewModel(List<Materia> categorias)
    {
        Registros = new List<DetalhesMateriaViewModel>();

        foreach (var c in categorias)
            Registros.Add(c.ParaDetalhesVM());
    }
}

public class DetalhesMateriaViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Disciplina { get; set; }
    public Serie Serie { get; set; }

    public DetalhesMateriaViewModel(
        Guid id,
        string nome,
        string disciplina,
        Serie serie
    )
    {
        Id = id;
        Nome = nome;
        Disciplina = disciplina;
        Serie = serie;
    }
}
