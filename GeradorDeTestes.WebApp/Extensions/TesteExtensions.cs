using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.WebApp.Models;

namespace GeradorDeTestes.WebApp.Extensions;

public static class TesteExtensions
{
    public static Teste ParaEntidade(this FormularioTesteViewModel formularioVM, List<Disciplina> disciplinas, List<Materia> materias)
    {
        var disciplina = disciplinas.FirstOrDefault(d => d.Id == formularioVM.DisciplinaId);
        var materia = materias.FirstOrDefault(m => m.Id == formularioVM.MateriaId);

        return new Teste(
            formularioVM.Titulo,
            disciplina!,
            materia!,
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
                teste.Disciplina?.Nome!,
                teste.Materia?.Nome!,
                teste.Serie,
                teste.TipoTeste,
                teste.QuantidadeQuestoes
        );
    }
}
