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
    [HttpGet("Cadastrar")]

    public IActionResult Cadastrar()
    {
        var CadastarVM = new CadastrarDisciplinaViewModel();

        return View(CadastarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarDisciplinaViewModel cadastrarVM)
    {
        var registros = repositorioDisciplina.SelecionarRegistros();

        if (registros.Any(x => x.Nome.Equals(cadastrarVM.Nome)))
            ModelState.AddModelError("CadastroUnico", "Já existe um registro registrado com este nome.");

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var entidade = cadastrarVM.ParaEntidade();

        var transacao = contexto.Database.BeginTransaction();

        try
        {
            repositorioDisciplina.CadastrarRegistro(entidade);

            contexto.SaveChanges();

            transacao.Commit();
        }
        catch (Exception)
        {
            transacao.Rollback();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }
}

 

