using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GeradorDeTestes.WebApp.Controllers;

[Route("questoes")]
public class QuestaoController : Controller
{
    private readonly GeradorDeTestesDbContext contexto;
    private readonly IRepositorioQuestao repositorioQuestao;
    private readonly IRepositorioMateria repositorioMateria;

    public QuestaoController(
        GeradorDeTestesDbContext contexto,
        IRepositorioQuestao repositorioQuestao,
        IRepositorioMateria repositorioMateria)
    {
        this.contexto = contexto;
        this.repositorioMateria = repositorioMateria;
        this.repositorioQuestao = repositorioQuestao;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var registros = repositorioQuestao.SelecionarRegistros();

        var vizualizarVM = new VisualizarQuestaoViewModel(registros);

        return View(vizualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var materias = repositorioMateria.SelecionarRegistros();

        var cadastrarVM = new CadastrarQuestaoViewModel(materias);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarQuestaoViewModel cadastrarVM)
    {
        var registros = repositorioQuestao.SelecionarRegistros();

        foreach (var item in registros)
        {
            if (item.Enunciado.Equals(cadastrarVM.Enunciado))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe uma questão registrada com este enunciado.");
                break;
            }
        }

        if (cadastrarVM.Alternativas.Count < 2)
            ModelState.AddModelError("CadastroUnico", "É necessário cadastrar ao menos duas alternativas.");

        if (cadastrarVM.Alternativas.Count(a => a.Correta) != 1)
            ModelState.AddModelError("CadastroUnico", "É necessário cadastrar exatamente uma alternativa correta.");

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var transacao = contexto.Database.BeginTransaction();

        try
        {
            var alternativas = cadastrarVM.Alternativas
                .Select(a => a.ParaEntidade())
                .ToList();

            var materias = repositorioMateria.SelecionarRegistros();

            var entidade = cadastrarVM.ParaEntidade(alternativas, materias);

            repositorioQuestao.CadastrarRegistro(entidade);

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
        var registro = repositorioQuestao.SelecionarRegistroPorId(id);

        var materias = repositorioMateria.SelecionarRegistros();

        if (registro is null)
            return RedirectToAction(nameof(Index));

        var editarVM = new EditarQuestaoViewModel(
            registro.Id,
            materias,
            registro.Materia.Id,
            registro.Enunciado,
            registro.Alternativas);

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarQuestaoViewModel editarVM)
    {
        var registros = repositorioQuestao.SelecionarRegistros();

        foreach (var item in registros)
        {
            if (item.Enunciado.Equals(editarVM.Enunciado) && !item.Id.Equals(id))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe uma questão registrada com este enunciado.");
                break;
            }
        }

        if (editarVM.Alternativas.Count < 2)
            ModelState.AddModelError("CadastroUnico", "É necessário cadastrar ao menos duas alternativas.");

        if (editarVM.Alternativas.Count(a => a.Correta) != 1)
            ModelState.AddModelError("CadastroUnico", "É necessário cadastrar exatamente uma alternativa correta.");

        if (!ModelState.IsValid)
            return View(editarVM);


        var transacao = contexto.Database.BeginTransaction();

        try
        {
            var alternativas = editarVM.Alternativas
                .Select(a => a.ParaEntidade())
                .ToList();

            var materias = repositorioMateria.SelecionarRegistros();

            var entidade = editarVM.ParaEntidade(alternativas, materias);

            repositorioQuestao.EditarRegistro(id, entidade);

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
    public IActionResult Excluir(Guid id)
    {
        var registro = repositorioQuestao.SelecionarRegistroPorId(id);
        
        if (registro is null)
            return RedirectToAction(nameof(Index));
        
        var excluirVM = new ExcluirQuestaoViewModel(registro.Id, registro.Enunciado);
        
        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Excluir(Guid id, ExcluirQuestaoViewModel excluirVM)
    {
        var transacao = contexto.Database.BeginTransaction();

        try
        {
            repositorioQuestao.ExcluirRegistro(id);

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

    [HttpGet("detalhes/{id:guid}")]
    public IActionResult Detalhes(Guid id)
    {
        var registro = repositorioQuestao.SelecionarRegistroPorId(id);
        
        if (registro is null)
            return RedirectToAction(nameof(Index));
        
        var detalhesVM = new DetalhesQuestaoViewModel(
            id,
            registro.Materia.Nome,
            registro.Enunciado,
            registro.Alternativas);
        
        return View(detalhesVM);
    }

    [HttpPost, Route("/questoes/cadastrar/adicionar-alternativa")]
    public IActionResult AdicionarAlternativa(
        CadastrarQuestaoViewModel cadastrarVM, 
        AdicionarAlternativaViewModel adicionarVM)
    {
        var materias = repositorioMateria.SelecionarRegistros();

        var letraEscolhida = (char)('a' + cadastrarVM.Alternativas.Count);

        var alternativa = new AlternativaViewModel(
            letraEscolhida, 
            adicionarVM.Resposta, 
            adicionarVM.Correta);

        cadastrarVM.Alternativas.Add(alternativa);

        cadastrarVM.MateriasDisponiveis = materias
            .Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nome
            }).ToList();

        return View(nameof(Cadastrar), cadastrarVM);
    }

    [HttpPost, Route("/questoes/cadastrar/remover-alternativa/{letra}")]
    public IActionResult RemoverAlternativa(
        CadastrarQuestaoViewModel cadastrarVM, 
        string letra)
    {
        var materias = repositorioMateria.SelecionarRegistros();

        char letraChar = letra[0];

        var alternativaParaRemover = cadastrarVM.Alternativas
            .FirstOrDefault(a => a.Letra == letraChar);

        if (alternativaParaRemover is not null)
            cadastrarVM.Alternativas.Remove(alternativaParaRemover);

        for (int i = 0; i < cadastrarVM.Alternativas.Count; i++)
            cadastrarVM.Alternativas[i].Letra = (char)('a' + i);

        cadastrarVM.MateriasDisponiveis = materias
            .Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nome
            }).ToList();

        return View(nameof(Cadastrar), cadastrarVM);
    }

    [HttpPost, Route("/questoes/editar/adicionar-alternativa")]
    public IActionResult AdicionarAlternativa(
        EditarQuestaoViewModel editarVM,
        AdicionarAlternativaViewModel adicionarVM)
    {
        var materias = repositorioMateria.SelecionarRegistros();

        var letraEscolhida = (char)('a' + editarVM.Alternativas.Count);

        var alternativa = new AlternativaViewModel(
            letraEscolhida,
            adicionarVM.Resposta,
            adicionarVM.Correta);

        editarVM.Alternativas.Add(alternativa);

        editarVM.MateriasDisponiveis = materias
            .Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nome
            }).ToList();

        return View(nameof(Editar), editarVM);
    }

    [HttpPost, Route("/questoes/editar/remover-alternativa/{letra}")]
    public IActionResult RemoverAlternativa(
        EditarQuestaoViewModel editarVM,
        string letra)
    {
        var materias = repositorioMateria.SelecionarRegistros();

        char letraChar = letra[0];

        var alternativaParaRemover = editarVM.Alternativas
            .FirstOrDefault(a => a.Letra == letraChar);

        if (alternativaParaRemover is not null)
            editarVM.Alternativas.Remove(alternativaParaRemover);

        for (int i = 0; i < editarVM.Alternativas.Count; i++)
            editarVM.Alternativas[i].Letra = (char)('a' + i);

        editarVM.MateriasDisponiveis = materias
            .Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nome
            }).ToList();

        return View(nameof(Editar), editarVM);
    }

}
