using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers;


    [Route("disciplinas")]
public class Disciplina : Controller
{
    private readonly GeradorDeTestesDbContext contexto;
    private readonly IRepositorioDisciplina repositorioDisciplina;


    public Disciplina(GeradorDeTestesDbContext contexto, IRepositorioDisciplina repositorioDisciplina)
    {
        this.contexto = contexto;
        this.repositorioDisciplina = repositorioDisciplina;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var registros = repositorioDisciplina.SelecionarRegistros();

        var visualizarVM = new VisualizarDisciplinaViewModel(registros);

        return View(visualizarVM);
    }

}
