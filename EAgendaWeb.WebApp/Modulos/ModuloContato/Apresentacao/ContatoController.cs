using AutoMapper;
using EAgendaWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace EAgendaWeb.WebApp.Modulos.ModuloContato.Apresentacao;

public class ContatoController : Controller
{
    private readonly ServicoContato servicoContato;
    private readonly IMapper mapper;

    public ContatoController(ServicoContato servicoContato, IMapper mapper)
    {
        this.servicoContato = servicoContato;
        this.mapper = mapper;
    }

    public ActionResult Listar()
    {
        List<DetalhesContatoDto> detalhesContatoDtos = servicoContato.SelecionarTodos();
        List<ListarContatoViewModel> listarContatoViewModels = mapper.Map<List<ListarContatoViewModel>>(detalhesContatoDtos);
        return View(listarContatoViewModels);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastroContatoViewModel vm)
    {
        if (!ModelState.IsValid)
            return View();

        CadastroContatoDto dto = mapper.Map<CadastroContatoDto>(vm);

        Result resultado = servicoContato.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesContatoDto> resultado = servicoContato.SelecionarPorId(id);

        ExcluirContatoViewModel vm = mapper.Map<ExcluirContatoViewModel>(resultado.Value);

        return View(vm);
    }
    [HttpPost]
    public ActionResult Excluir(ExcluirContatoViewModel vm)
    {
        Result resultado = servicoContato.Excluir(vm.Id);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesContatoDto> dto = servicoContato.SelecionarPorId(id);

        EditarContatoViewModel vm = mapper.Map<EditarContatoViewModel>(dto.Value);

        return View(vm);
    }
    [HttpPost]
    public ActionResult Editar(EditarContatoViewModel vm)
    {
        EditarContatoDto dto = mapper.Map<EditarContatoDto>(vm);
        Result resultado = servicoContato.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }
        return RedirectToAction(nameof(Listar));
    }
}
