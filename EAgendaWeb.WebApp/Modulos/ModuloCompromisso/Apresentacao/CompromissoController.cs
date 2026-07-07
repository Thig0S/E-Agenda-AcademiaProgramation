using AutoMapper;
using EAgendaWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Apresentacao;

public class CompromissoController : Controller
{
    private readonly ServicoCompromisso servicoCompromisso;
    private readonly ServicoContato servicoContato;
    private readonly IMapper mapper;

    public CompromissoController(
        ServicoCompromisso servicoCompromisso,
        IMapper mapper,
        ServicoContato servicoContato
        )
    {
        this.servicoCompromisso = servicoCompromisso;
        this.mapper = mapper;
        this.servicoContato = servicoContato;
    }

    public ActionResult Listar()
    {
        List<DetalhesCompromissoDto> dtos = servicoCompromisso.SelecionarTodos();

        List<ListarCompromissoViewModel> vms = mapper.Map<List<ListarCompromissoViewModel>>(dtos);

        return View(vms);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        List<DetalhesContatoDto> dtosContato = servicoContato.SelecionarTodos();

        ViewBag.Contatos = dtosContato.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Nome
        }).ToList();

        return View();
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastrarCompromissoViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        CadastrarCompromissoDto dto = mapper.Map<CadastrarCompromissoDto>(vm);

        Result resultado = servicoCompromisso.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Result<DetalhesCompromissoDto> dto = servicoCompromisso.SelecionarPorId(new Guid(id));

        if (dto.IsFailed)
            return RedirectToAction(nameof(Listar));

        ExcluirCompromissoViewModel vm = mapper.Map<ExcluirCompromissoViewModel>(dto.Value);

        return View(vm);

    }
    [HttpPost]
    public ActionResult Excluir(ExcluirCompromissoViewModel vm)
    {
        ExcluirCompromissoDto dto = mapper.Map<ExcluirCompromissoDto>(vm);

        Result resultado = servicoCompromisso.Excluir(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
    public ActionResult Editar(string id)
    {
        Result<DetalhesCompromissoDto> dto = servicoCompromisso.SelecionarPorId(new Guid(id));

        if (dto.IsFailed)
            return RedirectToAction(nameof(Listar));

        List<DetalhesContatoDto> dtosContato = servicoContato.SelecionarTodos();

        ViewBag.Contatos = dtosContato.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Nome
        }).ToList();

        EditarCompromissoViewModel vm = mapper.Map<EditarCompromissoViewModel>(dto.Value);

        return View(vm);
    }
    [HttpPost]
    public ActionResult Editar(EditarCompromissoViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        EditarCompromissoDto dto = mapper.Map<EditarCompromissoDto>(vm);

        Result resultado = servicoCompromisso.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
}


