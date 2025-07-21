using GeradorDeTestes.Dominio.ModuloDisciplina;
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
    private readonly IRepositorioDisciplina repositorioDisciplina;

    public MateriaController(GeradorDeTestesDbContext contexto, IRepositorioMateria repositorioMateria, IRepositorioDisciplina repositorioDisciplina)
    {
        this.contexto = contexto;
        this.repositorioMateria = repositorioMateria;
        this.repositorioDisciplina = repositorioDisciplina;
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
        var cadastrarVM = new CadastrarMateriaViewModel
        {
            Disciplinas = repositorioDisciplina.SelecionarRegistros()
        };

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarMateriaViewModel cadastrarVM)
    {
        var registros = repositorioMateria.SelecionarRegistros();

        if (registros.Any(x => x.Nome.Equals(cadastrarVM.Nome)))
            ModelState.AddModelError("CadastroUnico", "Já existe um registro registrado com este nome.");

        if (!ModelState.IsValid)
        {
            cadastrarVM.Disciplinas = repositorioDisciplina.SelecionarRegistros();
            return View(cadastrarVM);
        }

        var disciplinas = repositorioDisciplina.SelecionarRegistros();

        var entidade = cadastrarVM.ParaEntidade(disciplinas);

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

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        var registroSelecionado = repositorioMateria.SelecionarRegistroPorId(id);

        if (registroSelecionado == null)
            return NotFound();

        var disciplinas = repositorioDisciplina.SelecionarRegistros();

        var editarVM = new EditarMateriaViewModel(
            registroSelecionado.Id,
            registroSelecionado.Nome,
            registroSelecionado.Serie,
            registroSelecionado.Disciplina.Id,
            disciplinas
        );

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    public IActionResult Editar(Guid id, EditarMateriaViewModel editarVM)
    {
        var registros = repositorioMateria
            .SelecionarRegistros()
            .Where(r => r.Id != id);

        if (registros.Any(x => x.Nome.Equals(editarVM.Nome)))
            ModelState.AddModelError("CadastroUnico", "Já existe um registro registrado com este nome.");

        if (!ModelState.IsValid)
        {
            editarVM.Disciplinas = repositorioDisciplina.SelecionarRegistros();
            return View(editarVM);
        }

        var disciplinas = repositorioDisciplina.SelecionarRegistros();

        var entidade = editarVM.ParaEntidade(disciplinas);

        if (editarVM.DisciplinaId != null)
            entidade.Disciplina = repositorioDisciplina.SelecionarRegistroPorId(id);

        var transacao = contexto.Database.BeginTransaction();

        try
        {
            repositorioMateria.EditarRegistro(id, entidade);

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

    [HttpGet("excluir/{id:guid}")]
    public ActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioMateria.SelecionarRegistroPorId(id);

        if (registroSelecionado == null)
            return RedirectToAction(nameof(Index));

        var excluirVM = new ExcluirMateriaViewModel(registroSelecionado.Id, registroSelecionado.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public ActionResult ExcluirConfirmado(Guid id)
    {
        var transacao = contexto.Database.BeginTransaction();

        try
        {
            repositorioMateria.ExcluirRegistro(id);

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
