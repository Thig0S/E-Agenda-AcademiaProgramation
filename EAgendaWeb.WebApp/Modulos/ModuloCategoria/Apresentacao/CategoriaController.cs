using Microsoft.AspNetCore.Mvc;

namespace EAgendaWeb.WebApp.Modulos.ModuloCategoria.Apresentacao;

public class CategoriaController : Controller
{
    public ActionResult Listar()
    {
        return View();
    }
}
