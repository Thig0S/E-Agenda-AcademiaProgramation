using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloTarefa.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloTarefa.Dominio;

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
        CreateMap<EditarTarefaViewModel, EditarTarefaDto>();

        // Mapeamento do filho
        CreateMap<ItensTarefa, ItensDaTarefaViewModel>()
            .ForMember(dest => dest.Concluido, opt => opt.MapFrom(src => src.Concluido));
        // ^ Se os nomes (Concluido vs StatusConclusao) forem diferentes, use o ForMember para ensinar o AutoMapper

        // Mapeamento do pai (ele vai ver a lista e usar o mapeamento de cima sozinho!)
        CreateMap<Tarefa, MostrarItensTarefa>();
    }
}
