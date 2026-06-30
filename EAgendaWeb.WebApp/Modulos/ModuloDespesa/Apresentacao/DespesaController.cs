using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloDespesa.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace EAgendaWeb.WebApp.Modulos.ModuloDespesa.Apresentacao;

public class DespesaController : Controller
{
    private readonly ServicoDespesa servicoDespesa;
    private readonly IMapper mapper;

    public DespesaController(ServicoDespesa servicoDespesa, IMapper mapper)
    {
        this.servicoDespesa = servicoDespesa;
        this.mapper = mapper;
    }

    public ActionResult Listar()
    {
        List<DetalheDespesaDto> dtos = servicoDespesa.SelecionarTodos();

        List<ListarDespesaViewModel> vms = mapper.Map<List<ListarDespesaViewModel>>(dtos);

        return View(vms);
    }
}
