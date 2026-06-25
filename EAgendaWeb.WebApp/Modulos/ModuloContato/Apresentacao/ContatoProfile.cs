using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;

namespace EAgendaWeb.WebApp.Modulos.ModuloContato.Apresentacao;

public class ContatoProfile : Profile
{
    public ContatoProfile()
    {
        //Listagem
        CreateMap<DetalhesContatoDto, ListarContatoViewModel>();

        //Cadastro
        CreateMap<CadastroContatoViewModel, CadastroContatoDto>();
        CreateMap<Contato, DetalhesContatoDto>();
        CreateMap<DetalhesContatoDto, ExcluirContatoViewModel>();
        CreateMap<DetalhesContatoDto, EditarContatoViewModel>();
        CreateMap<EditarContatoViewModel, EditarContatoDto>();
    }
}
