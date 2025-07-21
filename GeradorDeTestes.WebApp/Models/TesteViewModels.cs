using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GeradorDeTestes.WebApp.Models;
public class FormularioTesteViewModel
{
    [Required(ErrorMessage = "O campo \"Título\" é obrigatório.")]
    [MinLength(3, ErrorMessage = "O campo \"Título\" deve ter ao menos 3 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Título\" deve ter no máximo 100 caracteres.")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O campo \"Série\" é obrigatório.")]
    public Serie Serie { get; set; }

    [Required(ErrorMessage = "O campo \"Tipo de Teste\" é obrigatório.")]
    public TipoTeste TipoTeste { get; set; }

    [Required(ErrorMessage = "O campo \"Quantidade de Questões\" é obrigatório.")]
    [Range(1, 50, ErrorMessage = "A quantidade de questões deve estar entre 1 e 50.")]
    public int QuantidadeQuestoes { get; set; }

    [Required(ErrorMessage = "A disciplina é obrigatória.")]
    public Guid? DisciplinaId { get; set; }

    public Disciplina? Disciplina { get; set; }

    [ValidateNever]
    public List<Disciplina> Disciplinas { get; set; }

    public Guid? MateriaId { get; set; }

    public Materia? Materia { get; set; }

    [ValidateNever]
    public List<Materia> Materias { get; set; }
    [ValidateNever]
    public List<Questao> QuestoesSorteadas { get; set; }
}

public class GerarTesteViewModel : FormularioTesteViewModel
{
    public GerarTesteViewModel() { }

    public GerarTesteViewModel(
        string titulo,
        Serie serie,
        TipoTeste tipoTeste,
        int quantidadeQuestoes,
        Guid? disciplinaId,
        Disciplina? disciplina,
        List<Disciplina> disciplinas,
        Guid? materiaId,
        Materia? materia,
        List<Materia> materias
    )
    {
        Titulo = titulo;
        Serie = serie;
        TipoTeste = tipoTeste;
        QuantidadeQuestoes = quantidadeQuestoes;
        DisciplinaId = disciplinaId;
        Disciplina = disciplina;
        Disciplinas = disciplinas;
        MateriaId = materiaId;
        Materia = materia;
        Materias = materias;
    }
}

public class DuplicarTesteViewModel : FormularioTesteViewModel
{
    public Guid? TesteOriginalId { get; set; }

    public DuplicarTesteViewModel() { }

    public DuplicarTesteViewModel(Teste teste)
    {
        TesteOriginalId = teste.Id;
        Titulo = teste.Titulo + " (Cópia)";
        Serie = teste.Serie;
        TipoTeste = teste.TipoTeste;
        QuantidadeQuestoes = teste.QuantidadeQuestoes;
        DisciplinaId = teste.Disciplina?.Id;
        Disciplina = teste.Disciplina;
        MateriaId = teste.Materia?.Id;
        Materia = teste.Materia;
    }
}

public class ExcluirTesteViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }

    public ExcluirTesteViewModel() { }

    public ExcluirTesteViewModel(Guid id, string titulo) : this()
    {
        Id = id;
        Titulo = titulo;
    }
}

public class VisualizarTestesViewModel
{
    public List<DetalhesTesteViewModel> Registros { get; set; }

    public VisualizarTestesViewModel(List<Teste> testes)
    {
        Registros = new List<DetalhesTesteViewModel>();

        foreach (var t in testes)
            Registros.Add(t.ParaDetalhes());
    }
}

public class DetalhesTesteViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Disciplina { get; set; }
    public string Materia { get; set; }
    public Serie Serie { get; set; }
    public TipoTeste TipoTeste { get; set; }
    public int QuantidadeQuestoes { get; set; }
    public List<Questao> QuestoesSorteadas { get; set; }

    public DetalhesTesteViewModel(
        Guid id,
        string titulo,
        string disciplina,
        string materia,
        Serie serie,
        TipoTeste tipoTeste,
        int quantidadeQuestoes
    )
    {
        Id = id;
        Titulo = titulo;
        Disciplina = disciplina;
        Materia = materia;
        Serie = serie;
        TipoTeste = tipoTeste;
        QuantidadeQuestoes = quantidadeQuestoes;
    }
}