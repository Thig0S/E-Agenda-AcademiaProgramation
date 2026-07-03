using AutoMapper;
using EAgendaWeb.WebApp.Modulos.ModuloTarefa.Dominio;
using FluentResults;

namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Aplicacao;

public class ServicoTarefa
{
    private readonly IRepositorioTarefa repositorioTarefa;
    private readonly IMapper mapper;

    public ServicoTarefa(IRepositorioTarefa repositorioTarefa, IMapper mapper)
    {
        this.repositorioTarefa = repositorioTarefa;
        this.mapper = mapper;
    }
    public List<DetalhesTarefaDto> SelecionarTodos()
    {
        return repositorioTarefa.SelecionarTodos().Select(t => new DetalhesTarefaDto(
            t.Id.ToString(),
            t.Titulo,
            t.Prioridade,
            t.DataCriacao.ToString(),
            t.DataConclusao.ToString(),
            t.StatusDeConclusao!.ToString(),
            t.PercentualConcluido
        )).ToList();
    }

    internal Result Cadastrar(CadastroTarefaDto dto)
    {
        Tarefa novaTarefa = new(dto.Titulo, dto.Prioridade, Convert.ToDateTime(dto.DataConclusao));

        repositorioTarefa.Cadastrar(novaTarefa);

        return Result.Ok();
    }
}
