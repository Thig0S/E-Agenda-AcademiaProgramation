using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloCategoria.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Apresentacao;
using EAgendaWeb.WebApp.Modulos.ModuloDespesa.Dominio;

namespace EAgendaWeb.WebApp.Modulos.ModuloDespesa.Aplicacao;

public class ServicoDespesa
{
    private readonly IRepositorioDespesa repositorioDespesa;
    private readonly IRepositorioCategoria repositorioCategoria;
    private readonly IMapper mapper;

    public ServicoDespesa(IRepositorioDespesa repositorioDespesa, IRepositorioCategoria repositorioCategoria, IMapper mapper)
    {
        this.repositorioDespesa = repositorioDespesa;
        this.repositorioCategoria = repositorioCategoria;
        this.mapper = mapper;
    }

    public List<DetalheDespesaDto> SelecionarTodos()
    {
        return repositorioDespesa.SelecionarTodos().Select(e => new DetalheDespesaDto(
            e.Id.ToString(),
            e.Descricao,
            e.DataOcorrencia.ToString(),
            e.Valor,
            e.FormaPagamento,
            e.Categoria?.ToString() ?? "Sem Categoria")).ToList();
    }
}
