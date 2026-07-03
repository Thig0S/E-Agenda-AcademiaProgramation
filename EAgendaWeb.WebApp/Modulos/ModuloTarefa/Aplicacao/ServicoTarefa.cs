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

    private Result ValidarEntidade(Tarefa tarefa)
    {
        List<string> erros = tarefa.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        var resultado = new Result();

        foreach (string erro in erros)
        {
            string campo = string.Empty;
            string mensagem = erro;

            if (erro.Contains('|'))
            {
                var partes = erro.Split('|', 2);
                campo = partes[0];
                mensagem = partes[1];
            }

            resultado.WithError(new Error(mensagem).WithMetadata("Campo", campo));
        }

        return resultado; // Agora todos os erros vão para o ModelState!
    }

    internal void Excluir(ExcluirTarefaDto dto)
    {
        Tarefa? tarefa = repositorioTarefa.SelecionarPorId(new Guid(dto.Id));

        if (tarefa == null)
            throw new Exception("Tarefa não encotrada!");

        repositorioTarefa.Excluir(new Guid(dto.Id));
    }
}
