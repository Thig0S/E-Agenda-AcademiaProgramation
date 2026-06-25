using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Aplicacao;

namespace EAgendaWeb.WebApp.Modulos.ModuloContato.Apresentacao;

public class ContatoProfile : Profile
{
    public ContatoProfile()
    {
        CreateMap<DetalhesContatoDto, ListarContatoViewModel>();
    }
}
