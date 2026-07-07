using AutoMapper;
using EAgendaWeb.WebApp.Compartilhado.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;
using FluentResults;

namespace EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Aplicacao;

public class ServicoCompromisso : ServicoBase<Compromisso>
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
            c.Contato ?? "Sem Contato")).ToList();
    }
    internal Result<DetalhesCompromissoDto> SelecionarPorId(Guid id)
    {
        Compromisso? compromisso = repositorioCompromisso.SelecionarPorId(id);

        if (compromisso == null)
            return Result.Fail("Contato não encontrado!");

        return Result.Ok(
            new DetalhesCompromissoDto(
            compromisso.Id.ToString(),
            compromisso.Assunto,
            compromisso.DataOcorrencia.ToShortDateString(),
            compromisso.HoraInicio.ToShortTimeString(),
            compromisso.HoraTermino.ToShortTimeString(),
            compromisso.TipoDeCompromisso,
            compromisso.Local, compromisso.Link,
            compromisso.Contato ?? "Não informado")
            );
    }

    internal Result Excluir(ExcluirCompromissoDto dto)
    {
        Compromisso? compromissoSelecionado = repositorioCompromisso.SelecionarPorId(new Guid(dto.Id));

        if (compromissoSelecionado == null)
            return Result.Fail("Compromisso não encontrado");

        repositorioCompromisso.Excluir(new Guid(dto.Id));

        return Result.Ok();
    }

    internal Result Editar(EditarCompromissoDto dto)
    {
        Contato? contato = null;

        if (!string.IsNullOrWhiteSpace(dto.Contato))
            contato = repositorioContato.SelecionarPorId(new Guid(dto.Contato!));

        Compromisso CompromissoEditado = new(dto.Assunto, DateTime.Parse(dto.DataOcorrencia),
            TimeOnly.Parse(dto.HoraInicio), TimeOnly.Parse(dto.HoraTermino),
            dto.TipoDeCompromisso, dto.Local, dto.Link, contato
            );

        Result resultadoValidacao = ValidarEntidade(CompromissoEditado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioCompromisso.Editar(new Guid(dto.Id), CompromissoEditado);
        return Result.Ok();
    }
}
