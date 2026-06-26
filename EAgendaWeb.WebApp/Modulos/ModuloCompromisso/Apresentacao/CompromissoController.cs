using Microsoft.AspNetCore.Mvc;

namespace EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Apresentacao;

public class CompromissoController : Controller
{
    public ActionResult Listar()
    {
        List<ListarCompromissoViewModel> vms = [];

        return View(vms);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }
}
