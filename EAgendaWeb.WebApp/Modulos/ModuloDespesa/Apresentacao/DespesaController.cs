using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloCategoria.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloCategoria.Dominio;
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

        return RedirectToAction(nameof(Listar));
    }
    public ActionResult Excluir(string id)
    {
        DetalheDespesaDto dto = servicoDespesa.SelecionarPorId(id);

        ExcluirDespesaViewModel vm = mapper.Map<ExcluirDespesaViewModel>(dto);

        return View(vm);
    }
}
