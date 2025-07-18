using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers;

[Route("materias")]
public class MateriaController : Controller
{
    private readonly GeradorDeTestesDbContext contexto;
    private readonly IRepositorioMateria repositorioMateria;
    
    public MateriaController(GeradorDeTestesDbContext contexto, IRepositorioMateria repositorioMateria)
    {
        this.contexto = contexto;
        this.repositorioMateria = repositorioMateria;
    }

    public IActionResult Index()
    {
        var registros = repositorioMateria.SelecionarRegistros();

        var visualizarVM = new VisualizarMateriasViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarMateriaViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public ActionResult Cadastrar(CadastrarMateriaViewModel cadastrarVM)
    {
        var registros = repositorioMateria.SelecionarRegistros();

        if (registros.Any(x => x.Nome.Equals(cadastrarVM.Nome)))
            ModelState.AddModelError("CadastroUnico", "Já existe um registro registrado com este nome.");

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var entidade = cadastrarVM.ParaEntidade();

        var transacao = contexto.Database.BeginTransaction();

        try
        {
            repositorioMateria.CadastrarRegistro(entidade);

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
