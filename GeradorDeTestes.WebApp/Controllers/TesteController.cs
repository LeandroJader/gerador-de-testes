using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
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

        if (!ModelState.IsValid)
        {
            gerarVM.Disciplinas = repositorioDisciplina.SelecionarRegistros();
            gerarVM.Materias = repositorioMateria.SelecionarRegistros();
            return View(gerarVM);
        }

        // 🔽 Buscar entidades reais do banco para garantir rastreamento
        var disciplina = contexto.Disciplinas.SingleOrDefault(d => d.Id == gerarVM.DisciplinaId);
        var materia = contexto.Materias.SingleOrDefault(m => m.Id == gerarVM.MateriaId);

        if (disciplina is null)
            ModelState.AddModelError("DisciplinaId", "Disciplina inválida.");

        if (materia is null)
            ModelState.AddModelError("MateriaId", "Matéria inválida.");

        if (!ModelState.IsValid)
        {
            gerarVM.Disciplinas = repositorioDisciplina.SelecionarRegistros();
            gerarVM.Materias = repositorioMateria.SelecionarRegistros();
            return View(gerarVM);
        }

        // 🔽 Agora sim pode criar o Teste com entidades rastreadas
        var entidade = new Teste(
            gerarVM.Titulo,
            disciplina!,
            materia!,
            gerarVM.Serie,
            gerarVM.QuantidadeQuestoes,
            gerarVM.TipoTeste
        );

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

}
