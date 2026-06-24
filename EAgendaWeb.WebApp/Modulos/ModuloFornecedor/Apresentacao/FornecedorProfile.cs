using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;

namespace EAgendaWeb.WebApp.Modulos.ModuloFornecedor.Apresentacao;

public class FornecedorProfile : Profile
{
    public FornecedorProfile()
    {
        CreateMap<ListarFornecedoresDto, ListarFornecedoresViewModel>();
        CreateMap<CadastrarFornecedorViewModel, CadastrarFornecedorDto>();
        CreateMap<EditarFornecedorViewModel, EditarFornecedorDto>();
        CreateMap<DetalhesFornecedorDto, EditarFornecedorViewModel>();
        CreateMap<DetalhesFornecedorDto, ExcluirFornecedorViewModel>();
    }
}
