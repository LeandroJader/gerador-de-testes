using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GeradorDeTestes.WebApp.Models;

public abstract class FormularioQuestaoViewModel
{
    [Required(ErrorMessage = "O campo \"Matéria\" é obrigatório.")]
    public Guid MateriaId { get; set; }
    public List<SelectListItem> MateriasDisponiveis { get; set; } = new List<SelectListItem>();

    [Required(ErrorMessage = "O campo \"Enunciado\" é obrigatório.")]
    [MinLength(8, ErrorMessage = "O campo \"Enunciado\" precisa conter ao menos 8 caracteres.")]
    [MaxLength(300, ErrorMessage = "O campo \"Enunciado\" precisa conter no máximo 300 caracteres.")]
    public string Enunciado { get; set; }
}

public class CadastrarQuestaoViewModel : FormularioQuestaoViewModel
{
    public List<AlternativaViewModel> Alternativas { get; set; } = new List<AlternativaViewModel>();

    public CadastrarQuestaoViewModel() { }

    public CadastrarQuestaoViewModel(List<Materia> materias) : this()
    {
        foreach (var m in materias)
        {
            var materiaDisponivel = new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nome
            };

            MateriasDisponiveis.Add(materiaDisponivel);
        }
    }

    public CadastrarQuestaoViewModel(
        List<Materia> materias,
        string enunciado,
        List<Alternativa> alternativas) : this()
    {
        Enunciado = enunciado;

        foreach (var m in materias)
        {
            var materiaDisponivel = new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nome
            };

            MateriasDisponiveis.Add(materiaDisponivel);
        }

        if (alternativas is null) return;
        
        for (int i = 0; i < alternativas.Count; i++)
        {
            var letraEscolhida = (char)('a' + i);

            var alternativaVM = new AlternativaViewModel(
                letraEscolhida,
                alternativas[i].Resposta,
                alternativas[i].Correta
            );

            Alternativas.Add(alternativaVM);
        }
    }
}

public class EditarQuestaoViewModel : FormularioQuestaoViewModel
{
    public Guid Id { get; set; }

    public List<AlternativaViewModel> Alternativas { get; set; } = new List<AlternativaViewModel>();

    public EditarQuestaoViewModel() { }

    public EditarQuestaoViewModel(
        Guid id,
        List<Materia> materias,
        Guid materiaId,
        string enunciado,
        List<Alternativa> alternativas) : this()
    {
        Id = id;
        Enunciado = enunciado;
        MateriaId = materiaId;

        foreach (var m in materias)
        {
            var materiaDisponivel = new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nome
            };

            MateriasDisponiveis.Add(materiaDisponivel);
        }

        if (alternativas is null) return;
        
        for (int i = 0; i < alternativas.Count; i++)
        {
            var letraEscolhida = (char)('a' + i);
            var alternativaVM = new AlternativaViewModel(
                letraEscolhida,
                alternativas[i].Resposta,
                alternativas[i].Correta
            );
            Alternativas.Add(alternativaVM);
        }
    }
}

public class ExcluirQuestaoViewModel
{
    public Guid Id { get; set; }
    public string Enunciado { get; set; }

    public ExcluirQuestaoViewModel() { }

    public ExcluirQuestaoViewModel(Guid id, string enunciado) : this()
    {
        Id = id;
        Enunciado = enunciado;
    }
}

public class VisualizarQuestaoViewModel
{
    public List<DetalhesQuestaoViewModel> Registros { get; }

    public VisualizarQuestaoViewModel(List<Questao> questoes)
    {
        Registros = new List<DetalhesQuestaoViewModel>();

        foreach (var i in questoes)
        {
            var detalhesVM = i.ParaDetalhesVM();
            
            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesQuestaoViewModel
{
    public Guid Id { get; set; }
    public string Materia { get; set; }
    public string Enunciado { get; set; }
    public List<AlternativaViewModel> Alternativas { get; set; } = new List<AlternativaViewModel>();

    public DetalhesQuestaoViewModel(
        Guid id,
        string materia,
        string enunciado,
        List<Alternativa> alternativas)
    {
        Id = id;
        Materia = materia;
        Enunciado = enunciado;

        if (alternativas is null) return;

        for (int i = 0; i < alternativas.Count; i++)
        {
            var letraEscolhida = (char)('a' + i);

            var alternativaVM = new AlternativaViewModel(
                letraEscolhida,
                alternativas[i].Resposta,
                alternativas[i].Correta
            );

            Alternativas.Add(alternativaVM);
        }
    }
}

public class AlternativaViewModel
{
    public char Letra { get; set; }
    public string Resposta { get; set; }
    public bool Correta { get; set; }

    public AlternativaViewModel()
    {
    }

    public AlternativaViewModel(char letra, string resposta, bool correta) : this()
    {
        Letra = letra;
        Resposta = resposta;
        Correta = correta;
    }
}

public class AdicionarAlternativaViewModel
{
    public string Resposta { get; set; }
    public bool Correta { get; set; }

    public AdicionarAlternativaViewModel()
    {
    }

    public AdicionarAlternativaViewModel(string resposta, bool correta) : this()
    {
        Resposta = resposta;
        Correta = correta;
    }
}