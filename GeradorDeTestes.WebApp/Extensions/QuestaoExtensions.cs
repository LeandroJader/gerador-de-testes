using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.WebApp.Models;

namespace GeradorDeTestes.WebApp.Extensions;

public static class QuestaoExtensions
{
    public static Questao ParaEntidade(
        this FormularioQuestaoViewModel formularioVM, 
        List<Alternativa> alternativas,
        List<Materia> materias)
    {
        Materia? materiaSelecionada = materias
            .FirstOrDefault(m => m.Id == formularioVM.MateriaId);

        var questao = new Questao(
            materiaSelecionada, 
            formularioVM.Enunciado);

        foreach (var alternativa in alternativas)
            questao.AdicionarAlternativa(alternativa);

        return questao;
    }

    public static Alternativa ParaEntidade(this AlternativaViewModel alternativaVM)
    {
        return new Alternativa(alternativaVM.Resposta, alternativaVM.Correta);
    }

    public static DetalhesQuestaoViewModel ParaDetalhesVM(this Questao questao)
    {
        return new DetalhesQuestaoViewModel(
            questao.Id,
            questao.Materia.Nome,
            questao.Enunciado,
            questao.Alternativas);
    }
}
