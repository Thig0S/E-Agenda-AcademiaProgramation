using AutoMapper;
using EAgendaWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using EAgendaWeb.WebApp.Modulos.ModuloCategoria.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloCategoria.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Apresentacao;
using EAgendaWeb.WebApp.Modulos.ModuloDespesa.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EAgendaWeb.WebApp.Modulos.ModuloDespesa.Apresentacao;

public class DespesaController : Controller
{
    private readonly ServicoDespesa servicoDespesa;
    private readonly ServicoCategoria servicoCategoria;

    private readonly IMapper mapper;

    public DespesaController(ServicoDespesa servicoDespesa, IMapper mapper, ServicoCategoria servicoCategoria)
    {
        this.servicoDespesa = servicoDespesa;
        this.mapper = mapper;
        this.servicoCategoria = servicoCategoria;
    }

    public ActionResult Listar()
    {
        List<DetalheDespesaDto> dtos = servicoDespesa.SelecionarTodos();

        List<ListarDespesaViewModel> vms = mapper.Map<List<ListarDespesaViewModel>>(dtos);

        return View(vms);
    }
    public ActionResult Cadastrar()
    {
        List<Categoria> categoria = servicoCategoria.SelecionarTodos();

        ViewBag.Categorias = categoria.Select(e => new SelectListItem
        {
            Value = e.Id.ToString(),
            Text = e.Titulo
        }
        );

        return View();
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastrarDespesaViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        CadastrarDespesaDto dto = mapper.Map<CadastrarDespesaDto>(vm);

        Result resultado = servicoDespesa.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
    public ActionResult Excluir(string id)
    {
        DetalheDespesaDto dto = servicoDespesa.SelecionarPorId(id);

        ExcluirDespesaViewModel vm = mapper.Map<ExcluirDespesaViewModel>(dto);

        return View(vm);
    }
    [HttpPost]
    public ActionResult Excluir(ExcluirCompromissoViewModel vm)
    {
        ExcluirDespesaDto dto = new(vm.Id);

        Result resultado = servicoDespesa.Excluir(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
    public ActionResult Editar(string id)
    {
        List<Categoria> categoria = servicoCategoria.SelecionarTodos();

        ViewBag.Categorias = categoria.Select(e => new SelectListItem
        {
            Value = e.Id.ToString(),
            Text = e.Titulo
        });

        DetalheDespesaDto dto = servicoDespesa.SelecionarPorId(id);

        EditarDespesaViewModel vm = mapper.Map<EditarDespesaViewModel>(dto);

        return View(vm);
    }
    [HttpPost]
    public ActionResult Editar(EditarDespesaViewModel vm)
    {
        EditarDespesaDto dto = mapper.Map<EditarDespesaDto>(vm);

        Result resultado = servicoDespesa.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
}
