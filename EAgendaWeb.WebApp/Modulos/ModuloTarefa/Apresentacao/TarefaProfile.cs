using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloTarefa.Aplicacao;

namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Apresentacao;

public class TarefaProfile : Profile
{
    public TarefaProfile()
    {
        CreateMap<DetalhesTarefaDto, ListarTarefaViewModel>();
        CreateMap<CadastroTarefaViewModel, CadastroTarefaDto>();
        CreateMap<DetalhesTarefaDto, ExcluirTarefaViewModel>();
        CreateMap<ExcluirTarefaViewModel, ExcluirTarefaDto>();
        CreateMap<DetalhesTarefaDto, EditarTarefaViewModel>();
    }
}
