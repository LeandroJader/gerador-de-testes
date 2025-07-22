using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using GeradorDeTestes.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers;

[Route("testes")]
public class TesteController : Controller
{
    public GeradorDeTestesDbContext contexto;
    public IRepositorioTeste repositorioTeste;
    public IRepositorioDisciplina repositorioDisciplina;
    public IRepositorioMateria repositorioMateria;
    public IRepositorioQuestao repositorioQuestao;

    public TesteController (
        GeradorDeTestesDbContext contexto, 
        IRepositorioTeste repositorioTeste, 
        IRepositorioDisciplina repoitorioDisciplina,
        IRepositorioMateria repositorioMateria, 
        IRepositorioQuestao repositorioQuestao
    )
    {
        this.contexto = contexto;
        this.repositorioTeste = repositorioTeste;
        this.repositorioDisciplina = repoitorioDisciplina;
        this.repositorioMateria = repositorioMateria;
        this.repositorioQuestao = repositorioQuestao;
    }

    public IActionResult Index()
    {
        var registros = repositorioTeste.SelecionarRegistros();

        var visualizarVM = new VisualizarTestesViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("gerar")]
    public IActionResult Gerar()
    {
        var gerarVM = new GerarTesteViewModel
        {
            Disciplinas = repositorioDisciplina.SelecionarRegistros(),
            Materias = repositorioMateria.SelecionarRegistros()
        };

        return View(gerarVM);
    }

    [HttpPost("gerar")]
    [ValidateAntiForgeryToken]
    public IActionResult Gerar(GerarTesteViewModel gerarVM)
    {
        var registros = repositorioTeste.SelecionarRegistros();

        if (registros.Any(x => x.Titulo.Equals(gerarVM.Titulo)))
            ModelState.AddModelError("CadastroUnico", "Já existe um registro registrado com este título.");

        var disciplinas = repositorioDisciplina.SelecionarRegistros();
        var materias = repositorioMateria.SelecionarRegistros();

        if (!ModelState.IsValid)
        {
            gerarVM.Disciplinas = disciplinas;
            gerarVM.Materias = materias;
            return View(gerarVM);
        }

        var entidade = gerarVM.ParaEntidade(disciplinas, materias);

        var transacao = contexto.Database.BeginTransaction();

        try
        {
            repositorioTeste.CadastrarRegistro(entidade);

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


    [HttpPost("preview")]
    [ValidateAntiForgeryToken]
    public IActionResult Preview(GerarTesteViewModel gerarVM)
    {
        var questoes = repositorioQuestao.SelecionarRegistros()
            .Where(q =>
                q.Materia.Id == gerarVM.MateriaId &&
                q.Materia.Serie == gerarVM.Serie)
            .ToList();

        if (gerarVM.QuantidadeQuestoes > questoes.Count)
        {
            ModelState.AddModelError(nameof(gerarVM.QuantidadeQuestoes), "Quantidade maior do que o número de questões disponíveis.");
        }

        gerarVM.Disciplinas = repositorioDisciplina.SelecionarRegistros();
        gerarVM.Materias = repositorioMateria.SelecionarRegistros();

        gerarVM.QuestoesSorteadas = questoes.OrderBy(_ => Guid.NewGuid())
                                            .Take(gerarVM.QuantidadeQuestoes)
                                            .ToList();

        return View("Gerar", gerarVM);
    }

    [HttpGet("duplicar/{id:guid}")]
    public IActionResult Duplicar(Guid id)
    {
        Teste? registro = repositorioTeste.SelecionarRegistroPorId(id);

        var disciplinas = repositorioDisciplina.SelecionarRegistros();
        var materias = repositorioMateria.SelecionarRegistros();

        var duplicarVM = new DuplicarTesteViewModel(registro);

        duplicarVM.Disciplinas = disciplinas;
        duplicarVM.Materias = materias;

        return View(duplicarVM);
    }

    [HttpPost("duplicar/preview")]
    [ValidateAntiForgeryToken]
    public IActionResult Preview(DuplicarTesteViewModel duplicarVM)
    {
        var questoes = repositorioQuestao.SelecionarRegistros()
            .Where(q =>
                q.Materia.Id == duplicarVM.MateriaId &&
                q.Materia.Serie == duplicarVM.Serie)
            .ToList();

        if (duplicarVM.QuantidadeQuestoes > questoes.Count)
        {
            ModelState.AddModelError(nameof(duplicarVM.QuantidadeQuestoes), "Quantidade maior do que o número de questões disponíveis.");
        }

        duplicarVM.Disciplinas = repositorioDisciplina.SelecionarRegistros();
        duplicarVM.Materias = repositorioMateria.SelecionarRegistros();

        duplicarVM.QuestoesSorteadas = questoes.OrderBy(_ => Guid.NewGuid())
                                            .Take(duplicarVM.QuantidadeQuestoes)
                                            .ToList();

        return View("Duplicar", duplicarVM);
    }

    [HttpPost("duplicar")]
    [ValidateAntiForgeryToken]
    public IActionResult Duplicar(DuplicarTesteViewModel duplicarVM)
    {
        var registros = repositorioTeste.SelecionarRegistros();

        if (registros.Any(x => x.Titulo.Equals(duplicarVM.Titulo)))
            ModelState.AddModelError("CadastroUnico", "Já existe um registro registrado com este título.");

        var disciplinas = repositorioDisciplina.SelecionarRegistros();
        var materias = repositorioMateria.SelecionarRegistros();

        if (!ModelState.IsValid)
        {
            duplicarVM.Disciplinas = disciplinas;
            duplicarVM.Materias = materias;
            return View(duplicarVM);
        }

        var entidade = duplicarVM.ParaEntidade(disciplinas, materias);

        var transacao = contexto.Database.BeginTransaction();

        try
        {
            repositorioTeste.CadastrarRegistro(entidade);

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
        var registro = repositorioTeste.SelecionarRegistroPorId(id);

        if (registro is null)
            return RedirectToAction(nameof(Index));

        var excluirVM = new ExcluirTesteViewModel(registro.Id, registro.Titulo);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Excluir(Guid id, ExcluirTesteViewModel excluirVM)
    {
        var transacao = contexto.Database.BeginTransaction();

        try
        {
            repositorioTeste.ExcluirRegistro(id);

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
        var registro = repositorioTeste.SelecionarRegistroPorId(id);

        var questoes = repositorioQuestao.SelecionarRegistros()
            .Where(q =>
                q.Materia.Id == registro.Materia.Id &&
                q.Materia.Serie == registro.Materia.Serie)
            .ToList();

        if (registro is null)
            return RedirectToAction(nameof(Index));

        var detalhesVM = new DetalhesTesteViewModel(
            registro.Id,
            registro.Titulo,
            registro.Disciplina.Nome,
            registro.Materia.Nome,
            registro.Serie,
            registro.TipoTeste,
            registro.QuantidadeQuestoes
        );

        detalhesVM.QuestoesSorteadas = questoes;

        return View(detalhesVM);
    }

    [HttpGet("gerar-pdf/{id:guid}")]
    public IActionResult GerarPdf(Guid id)
    {
        var teste = repositorioTeste.SelecionarRegistroPorId(id);
        if (teste == null)
            return NotFound();

        var questoes = repositorioQuestao.SelecionarRegistros()
            .Where(q =>
                q.Materia.Id == teste.Materia.Id &&
                q.Materia.Serie == teste.Serie)
            .ToList();

        var detalhesVM = new DetalhesTesteViewModel(
            teste.Id,
            teste.Titulo,
            teste.Disciplina.Nome,
            teste.Materia.Nome,
            teste.Serie,
            teste.TipoTeste,
            teste.QuantidadeQuestoes
        )
        {
            QuestoesSorteadas = questoes
        };

        PdfGenerator.GerarPdfSemGabarito(detalhesVM);

        var nomeArquivo = $"{detalhesVM.Titulo}.pdf";
        var caminhoCompleto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "testes", nomeArquivo);

        var bytes = System.IO.File.ReadAllBytes(caminhoCompleto);
        return File(bytes, "application/pdf", nomeArquivo);
    }

    [HttpGet("gerar-pdf-com-gabarito/{id:guid}")]
    public IActionResult GerarPdfComGabarito(Guid id)
    {
        var teste = repositorioTeste.SelecionarRegistroPorId(id);
        if (teste == null)
            return NotFound();

        var questoes = repositorioQuestao.SelecionarRegistros()
            .Where(q =>
                q.Materia.Id == teste.Materia.Id &&
                q.Materia.Serie == teste.Serie)
            .ToList();

        var detalhesVM = new DetalhesTesteViewModel(
            teste.Id,
            teste.Titulo,
            teste.Disciplina.Nome,
            teste.Materia.Nome,
            teste.Serie,
            teste.TipoTeste,
            teste.QuantidadeQuestoes
        )
        {
            QuestoesSorteadas = questoes
        };

        PdfGenerator.GerarPdfComGabarito(detalhesVM);

        var nomeArquivo = $"{detalhesVM.Titulo} - Gabarito.pdf";
        var caminhoCompleto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "testes", nomeArquivo);

        var bytes = System.IO.File.ReadAllBytes(caminhoCompleto);
        return File(bytes, "application/pdf", nomeArquivo);
    }

}
