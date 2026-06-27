using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;
using FluentResults;
using Microsoft.AspNetCore.Http.Features;

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
        Contato? contato = null;

        if (!string.IsNullOrWhiteSpace(dto.Contato))
            contato = repositorioContato.SelecionarPorId(new Guid(dto.Contato!));

        Compromisso novoCompromisso = new(dto.Assunto, DateTime.Parse(dto.DataOcorrencia),
            TimeOnly.Parse(dto.HoraInicio), TimeOnly.Parse(dto.HoraTermino),
            dto.TipoDeCompromisso, dto.Local, dto.Link, contato
            );

        Result resultadoValidacao = ValidarEntidade(novoCompromisso);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioCompromisso.Cadastrar(novoCompromisso);

        return Result.Ok();
    }
    private static Result ValidarEntidade(Compromisso compromisso)
    {
        List<string> erros = compromisso.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new FluentResults.Error(erros.First()).WithMetadata("Campo", string.Empty));
    }
    internal List<DetalhesCompromissoDto> SelecionarTodos()
    {
        return repositorioCompromisso.SelecionarTodos().Select(c => new DetalhesCompromissoDto(
            c.Id.ToString(),
            c.Assunto,
            c.DataOcorrencia.ToShortDateString(),
            c.HoraInicio.ToShortTimeString(),
            c.HoraTermino.ToShortTimeString(),
            c.TipoDeCompromisso,
            c.Local, c.Link,
            c.Contato?.Nome ?? "Sem Contato")).ToList();
    }
    internal Result<DetalhesCompromissoDto> SelecionarPorId(Guid id)
    {
        Compromisso? compromisso = repositorioCompromisso.SelecionarPorId(id);

        if (compromisso == null)
            return Result.Fail("Fornecedor não encontrado!");

        return Result.Ok(
            new DetalhesCompromissoDto(
            compromisso.Id.ToString(),
            compromisso.Assunto,
            compromisso.DataOcorrencia.ToShortDateString(),
            compromisso.HoraInicio.ToShortTimeString(),
            compromisso.HoraTermino.ToShortTimeString(),
            compromisso.TipoDeCompromisso,
            compromisso.Local, compromisso.Link,
            compromisso.Contato.Nome)
            );
    }
}
