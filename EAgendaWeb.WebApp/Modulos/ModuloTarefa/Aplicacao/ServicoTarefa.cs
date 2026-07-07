using AutoMapper;
using EAgendaWeb.WebApp.Compartilhado.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloTarefa.Apresentacao;
using EAgendaWeb.WebApp.Modulos.ModuloTarefa.Dominio;
using FluentResults;

namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Aplicacao;

public class ServicoTarefa : ServicoBase<Tarefa>
{
    private readonly IRepositorioTarefa repositorioTarefa;

    public ServicoTarefa(IRepositorioTarefa repositorioTarefa, IMapper mapper)
    {
        this.repositorioTarefa = repositorioTarefa;
    }

    internal Result Cadastrar(CadastroTarefaDto dto)
    {
        Tarefa novaTarefa = new(dto.Titulo, dto.Prioridade, Convert.ToDateTime(dto.DataConclusao));

        Result resultadoValidacao = ValidarEntidade(novaTarefa);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioTarefa.Cadastrar(novaTarefa);

        return Result.Ok();
    }
    public List<DetalhesTarefaDto> SelecionarTodos()
    {
        return repositorioTarefa.SelecionarTodos().Select(t => new DetalhesTarefaDto(
            t.Id.ToString(),
            t.Titulo,
            t.Prioridade,
            t.DataCriacao.ToShortDateString(),
            t.DataConclusao.ToShortDateString(),
            t.StatusDeConclusao!.ToString(),
            t.PercentualConcluido
        )).ToList();
    }
    public MostrarItensTarefaDto? SelecionarPorIdTarefaEItensTarefa(string id)
    {
        // 1. Busca o objeto único do banco através do repositório
        Tarefa? tarefa = repositorioTarefa.SelecionarPorId(new Guid(id));

        if (tarefa == null) return null;

        return new MostrarItensTarefaDto(
            Id: tarefa.Id.ToString(),
            Titulo: tarefa.Titulo,
            Prioridade: tarefa.Prioridade.ToString(),
            StatusDeConclusao: tarefa.StatusDeConclusao.ToString(),
            PercentualConcluido: tarefa.PercentualConcluido,

            ListaDeItens: tarefa.Tarefas.Select(item => new ItensDaTarefaDto(
                Id: item.Id.ToString(),
                Titulo: item.Titulo,
                Concluido: item.Concluido
            )).ToList()
        );

        ;
    }
    public DetalhesTarefaDto SelecionarPorId(string Id)
    {
        Tarefa? t = repositorioTarefa.SelecionarPorId(new Guid(Id));

        if (t == null)
            throw new Exception("Tarefa não encontrada!");

        return new DetalhesTarefaDto(t.Id.ToString(), t.Titulo,
         t.Prioridade, t.DataCriacao.ToShortDateString(),
         t.DataConclusao.ToShortDateString(), t.StatusDeConclusao.ToString(),
          t.PercentualConcluido);
    }

    internal void Excluir(ExcluirTarefaDto dto)
    {
        Tarefa? tarefa = repositorioTarefa.SelecionarPorId(new Guid(dto.Id));

        if (tarefa == null)
            throw new Exception("Tarefa não encotrada!");

        repositorioTarefa.Excluir(new Guid(dto.Id));
    }

    internal Result Editar(EditarTarefaDto dto)
    {
        Tarefa novaTarefa = new(dto.Titulo, dto.Prioridade, Convert.ToDateTime(dto.DataConclusao));

        Result resultadoValidacao = ValidarEntidade(novaTarefa);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioTarefa.Editar(new Guid(dto.Id), novaTarefa);

        return Result.Ok();
    }

    internal void AdicionarTarefa(CadastrarItemDto dto)
    {

        Tarefa? tarefaSelecionada = repositorioTarefa.SelecionarPorId(new Guid(dto.TarefaId));

        if (tarefaSelecionada == null)
            throw new Exception("Tarefa não encontrada!");

        ItensTarefa novoItensTarefa = new(dto.Titulo, tarefaSelecionada);

        repositorioTarefa.AdicionarItem(novoItensTarefa);

    }

    internal void ExcluirTarefa(ExcluirItemDto dto)
    {
        if (dto.Id == null)
            throw new Exception("Item Tarefa não encontrado!");

        if (dto.TarefaId == null)
            throw new Exception("Tarefa não encontrada!");

        repositorioTarefa.ExcluirItem(dto);
    }

    internal void ConcluirTarefa(ConcluirItemDto dto)
    {
        if (dto.Id == null)
            throw new Exception("Item Tarefa não encontrado!");

        if (dto.TarefaId == null)
            throw new Exception("Tarefa não encontrada!");

        repositorioTarefa.ConcluirItem(dto);
    }

    internal void ReabrirTarefa(ReabrirIteDto dto)
    {
        if (dto.Id == null)
            throw new Exception("Item Tarefa não encontrado!");

        if (dto.TarefaId == null)
            throw new Exception("Tarefa não encontrada!");

        repositorioTarefa.ReabrirItem(dto);
    }
}
