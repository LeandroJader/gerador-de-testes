using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace GeradorDeTestes.WebApp.Models;

public class FormularioMateriaViewModel
{
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [MinLength(3, ErrorMessage = "O campo \"Nome\" precisa conter ao menos 3 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Nome\" precisa conter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo \"Disciplina\" é obrigatório.")]
    [MinLength(3, ErrorMessage = "O campo \"Disciplina\" precisa conter ao menos 3 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Disciplina\" precisa conter no máximo 100 caracteres.")]
    public string Disciplina { get; set; }

    [Required(ErrorMessage = "O campo \"Série\" é obrigatório.")]
    public Serie Serie { get; set; }
}

public class CadastrarMateriaViewModel : FormularioMateriaViewModel
{
    public CadastrarMateriaViewModel() { }

    public CadastrarMateriaViewModel(string nome, string disciplina, Serie serie) : this()
    {
        Nome = nome;
        Disciplina = disciplina;
        Serie = serie;
    }
}

public class EditarMateriaViewModel : FormularioMateriaViewModel
{
    public Guid Id { get; set; }

    public EditarMateriaViewModel() { }

    public EditarMateriaViewModel(
        Guid id,
        string nome,
        string disciplina,
        Serie serie
        ) : this()
    {
        Id = id;
        Nome = nome;
        Disciplina = disciplina;
        Serie = serie;
    }

    public Materia ParaEntidade()
    {
        return new Materia(
            Id,
            Nome,
            Disciplina,
            Serie
        );
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
