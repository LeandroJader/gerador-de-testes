using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.WebApp.Models;

namespace GeradorDeTestes.WebApp.Extensions;

public static class MateriaExtensions
{
    public static Materia ParaEntidade(this FormularioMateriaViewModel formularioVM, List<Disciplina> disciplinas)
    {
        Disciplina? disciplina = disciplinas.FirstOrDefault(m => m.Id == formularioVM.DisciplinaId);

        return new Materia(formularioVM.Nome, disciplina, formularioVM.Serie);
    }

    public static DetalhesMateriaViewModel ParaDetalhesVM(this Materia categoria)
    {
        return new DetalhesMateriaViewModel(
                categoria.Id,
                categoria.Nome,
                categoria.Disciplina.Nome,
                categoria.Serie
        );
    }
}