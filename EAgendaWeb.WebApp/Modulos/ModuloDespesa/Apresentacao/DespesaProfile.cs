using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloDespesa.Aplicacao;

namespace EAgendaWeb.WebApp.Modulos.ModuloDespesa.Apresentacao;

public class DespesaProfile : Profile
{
    public DespesaProfile()
    {
        CreateMap<DetalheDespesaDto, ListarDespesaViewModel>();
        CreateMap<CadastrarDespesaViewModel, CadastrarDespesaDto>();
        CreateMap<DetalheDespesaDto, ExcluirDespesaViewModel>();
    }
}
