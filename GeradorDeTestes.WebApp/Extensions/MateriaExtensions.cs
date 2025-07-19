using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.WebApp.Models;

namespace GeradorDeTestes.WebApp.Extensions;

public static class MateriaExtensions
{
    public static Materia ParaEntidade(this FormularioMateriaViewModel formularioVM)
    {
        return new Materia(formularioVM.Nome, formularioVM.Disciplina, formularioVM.Serie);
    }

    public static DetalhesMateriaViewModel ParaDetalhesVM(this Materia categoria)
    {
        return new DetalhesMateriaViewModel(
                categoria.Id,
                categoria.Nome,
                categoria.Disciplina,
                categoria.Serie
        );
    }
}