using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloTarefa.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Apresentacao;

public class TarefaController : Controller
{
    private readonly ServicoTarefa servicoTarefa;
    private readonly IMapper mapper;

    public TarefaController(ServicoTarefa servicoTarefa, IMapper mapper)
    {
        this.servicoTarefa = servicoTarefa;
        this.mapper = mapper;
    }

    public ActionResult Listar()
    {
        List<DetalhesTarefaDto> dtos = servicoTarefa.SelecionarTodos();

        List<ListarTarefaViewModel> vms = mapper.Map<List<ListarTarefaViewModel>>(dtos);

        return View(vms);
    }

    public ActionResult Cadastrar()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastroTarefaViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        CadastroTarefaDto dto = mapper.Map<CadastroTarefaDto>(vm);

        Result resultado = servicoTarefa.Cadastrar(dto);

        return RedirectToAction(nameof(Listar));
    }
}
