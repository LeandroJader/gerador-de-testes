using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.WebApp.Models;

namespace GeradorDeTestes.WebApp.Extensions;

public static class TesteExtensions
{
    public static Teste ParaEntidade(this FormularioTesteViewModel formularioVM)
    {
        return new Teste(
            formularioVM.Titulo,
            formularioVM.Disciplina,
            formularioVM.Materia,
            formularioVM.Serie,
            formularioVM.QuantidadeQuestoes,
            formularioVM.TipoTeste
        );
    }

    public static DetalhesTesteViewModel ParaDetalhes(this Teste teste)
    {
        return new DetalhesTesteViewModel(
                teste.Id,
                teste.Titulo,
                teste.Disciplina?.Nome,
                teste.Materia?.Nome,
                teste.Serie,
                teste.TipoTeste,
                teste.QuantidadeQuestoes
        );
    }
}
