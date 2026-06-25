using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Aplicacao;
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
}
