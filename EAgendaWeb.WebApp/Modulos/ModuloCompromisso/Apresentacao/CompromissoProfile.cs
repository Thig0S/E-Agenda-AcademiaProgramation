using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Aplicacao;

namespace EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Apresentacao;

public class CompromissoProfile : Profile
{
    public CompromissoProfile()
    {
        CreateMap<ListarCompromissoViewModel, ListarCompromissoDto>();
        CreateMap<CadastrarCompromissoViewModel, CadastrarCompromissoDto>();
        CreateMap<DetalhesCompromissoDto, ListarCompromissoViewModel>();
    }
}
