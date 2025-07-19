using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.WebApp.Models;

namespace GeradorDeTestes.WebApp.Extensions;

public static class QuestaoExtensions
{
    public static Questao ParaEntidade(this FormularioQuestaoViewModel formularioVM, List<Alternativa> alternativas)
    {
        var questao = new Questao(
            formularioVM.Materia, 
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
            questao.Materia,
            questao.Enunciado,
            questao.Alternativas);
    }
}
