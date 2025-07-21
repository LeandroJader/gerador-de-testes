using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeTestes.WebApp.Controllers;

public class TesteController : Controller
{
    public GeradorDeTestesDbContext contexto;
    public IRepositorioTeste repositorioTeste;
    public IRepositorioMateria repositorioMateria;
    public IRepositorioQuestao repositorioQuestao;

    public TesteController (
        GeradorDeTestesDbContext contexto, 
        IRepositorioTeste repositorioTeste, 
        IRepositorioMateria repositorioMateria, 
        IRepositorioQuestao repositorioQuestao
    )
    {
        this.contexto = contexto;
        this.repositorioTeste = repositorioTeste;
        this.repositorioMateria = repositorioMateria;
        this.repositorioQuestao = repositorioQuestao;
    }

    public IActionResult Index()
    {
        return View();
    }
}
