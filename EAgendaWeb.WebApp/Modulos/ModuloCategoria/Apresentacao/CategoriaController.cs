using EAgendaWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using EAgendaWeb.WebApp.Modulos.ModuloCategoria.Dominio;
using FluentResults;
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

        Result resultado = ValidarEntidade(novaCategoria);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

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

        Result resultado = ValidarEntidade(categoria);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

        repositorioCategoria.Editar(new Guid(vm.Id), categoria);
        return RedirectToAction(nameof(Listar));
    }
    private static Result ValidarEntidade(Categoria entidade)
    {
        List<string> erros = entidade.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        var resultado = new Result();

        foreach (string erro in erros)
        {
            string campo = string.Empty;
            string mensagem = erro;

            if (erro.Contains('|'))
            {
                var partes = erro.Split('|', 2);
                campo = partes[0];
                mensagem = partes[1];
            }

            resultado.WithError(new Error(mensagem).WithMetadata("Campo", campo));
        }

        return resultado; // Agora todos os erros vão para o ModelState!
    }

}
