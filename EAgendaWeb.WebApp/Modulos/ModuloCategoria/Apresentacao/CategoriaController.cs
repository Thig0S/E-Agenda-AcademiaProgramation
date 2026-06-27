using EAgendaWeb.WebApp.Modulos.ModuloCategoria.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace EAgendaWeb.WebApp.Modulos.ModuloCategoria.Apresentacao;

public class CategoriaController : Controller
{
    private readonly IRepositorioCategoria repositorioCategoria;

    public CategoriaController(IRepositorioCategoria repositorioCategoria)
    {
        this.repositorioCategoria = repositorioCategoria;
    }

    public ActionResult Listar()
    {
        List<Categoria> listaCategorias = repositorioCategoria.SelecionarTodos();
        List<ListarCategoriaViewModel> listarVms = [];
        foreach (var item in listaCategorias)
        {
            ListarCategoriaViewModel vm = new(item.Id.ToString(), item.Titulo);

            listarVms.Add(vm);
        }
        return View(listarVms);
    }

    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]

    public ActionResult Cadastrar(CadastrarCategoriaViewModel vm)
    {
        Categoria novaCategoria = new(vm.Titulo);
        repositorioCategoria.Cadastrar(novaCategoria);
        return RedirectToAction(nameof(Listar));
    }

    public ActionResult Excluir(string Id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(new Guid(Id));
        repositorioCategoria.Excluir(new Guid(Id));
        return RedirectToAction(nameof(Listar));
    }

    public ActionResult Editar(string Id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(new Guid(Id));
        EditarCategoriaViewModel vm = new(Id, categoria.Titulo);
        return View(vm);
    }

    [HttpPost]

    public ActionResult Editar(EditarCategoriaViewModel vm)
    {
        Categoria categoria = new(vm.Titulo);
        repositorioCategoria.Editar(new Guid(vm.Id), categoria);
        return RedirectToAction(nameof(Listar));
    }

}
