using AutoMapper;
using EAgendaWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Aplicacao;
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
}
