using Microsoft.AspNetCore.Mvc;

namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Apresentacao;

public class TarefaController : Controller
{
    public ActionResult Listar()
    {
        return View();
    }
}
