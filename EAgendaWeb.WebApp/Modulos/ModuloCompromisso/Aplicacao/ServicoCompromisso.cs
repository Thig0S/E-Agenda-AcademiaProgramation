using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;
using FluentResults;

namespace EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Aplicacao;

public class ServicoCompromisso
{
    private readonly IRepositorioCompromisso repositorioCompromisso;
    private readonly IRepositorioContato repositorioContato;
    private readonly IMapper mapper;

    public ServicoCompromisso(IRepositorioCompromisso repositorioCompromisso, IMapper mapper, IRepositorioContato repositorioContato)
    {
        this.repositorioCompromisso = repositorioCompromisso;
        this.mapper = mapper;
        this.repositorioContato = repositorioContato;
    }

    public Result Cadastrar(CadastrarCompromissoDto dto)
    {
        Contato? contato = repositorioContato.SelecionarPorId(new Guid(dto.Contato!));

        Compromisso novoCompromisso = new(dto.Assunto, DateTime.Parse(dto.DataOcorrencia),
            TimeOnly.Parse(dto.HoraInicio), TimeOnly.Parse(dto.HoraTermino),
            dto.TipoDeCompromisso, dto.Local, dto.Link, contato
            );

        return Result.Ok();
    }

    public List<ListarCompromissoDto> SelecionarTodos()
    {
        throw new NotImplementedException();
    }
    public ListarCompromissoDto SelecionarPorId()
    {
        throw new NotImplementedException();
    }
}
