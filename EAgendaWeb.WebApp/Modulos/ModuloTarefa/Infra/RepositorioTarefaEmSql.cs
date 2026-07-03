using Dapper;
using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.Modulos.ModuloTarefa.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloTarefa.Dominio;
using Microsoft.Data.SqlClient;

namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Infra;

public class RepositorioTarefaEmSql(ISqlConnectionFactory connectionFactory) : IRepositorioTarefa
{
    private const string InserirSql = """
        INSERT INTO dbo.TBTarefas (Id, Titulo, Prioridade, DataCriacao, DataConclusao, StatusDeConclusao)
        VALUES (@Id, @Titulo, @Prioridade, @DataCriacao, @DataConclusao, @StatusDeConclusao);
    """;

    private const string InserirItemSql = """
        INSERT INTO dbo.TBItensTarefa (Id, Titulo, Concluido, TarefaId)
        VALUES (@Id, @Titulo, @Concluido, @TarefaId);
    """;

    private const string ExcluirItemSql = """
        DELETE FROM dbo.TBItensTarefa
        WHERE Id = @Id;
    """;

    private const string ConcluirItemSql = """
        UPDATE dbo.TBItensTarefa
        SET Concluido = 1
        WHERE Id = @Id;
    """;

    private const string AtualizarSql = """
        UPDATE dbo.TBTarefas
        SET Titulo = @Titulo,
        Prioridade = @Prioridade,
        DataConclusao = @DataConclusao
        WHERE Id = @Id;
    """;

    private const string ExcluirSql = """
        DELETE FROM dbo.TBTarefas
        WHERE Id = @Id;
    """;

    private const string SelecionarPorIdSql = """
        SELECT 
            t.Id, t.Titulo, t.Prioridade, t.DataCriacao, t.DataConclusao, t.StatusDeConclusao,
            i.Id, i.Titulo, i.Concluido, i.TarefaId
        FROM dbo.TBTarefas t
        LEFT JOIN dbo.TBItensTarefa i ON t.Id = i.TarefaId
        WHERE t.Id = @Id;
    """;

    private const string SelecionarTodosSql = """
        SELECT Id, Titulo, Prioridade, DataCriacao, DataConclusao, StatusDeConclusao
        FROM dbo.TBTarefas
        ORDER BY Titulo;
    """;

    public void Cadastrar(Tarefa entidade)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        conexao.Execute(InserirSql, entidade);
    }

    public void AdicionarItem(ItensTarefa novoItem)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Execute(InserirItemSql, novoItem);
    }

    public bool Editar(Guid idSelecionado, Tarefa entidadeAtualizada)
    {
        entidadeAtualizada.Id = idSelecionado;

        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Execute(AtualizarSql, entidadeAtualizada) == 1;
    }

    public bool ExcluirItem(ExcluirItemDto dto)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Execute(ExcluirItemSql, new { Id = dto.Id }) == 1;
    }

    public bool Excluir(Guid idSelecionado)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Execute(ExcluirSql, new { Id = idSelecionado }) == 1;
    }

    public Tarefa? SelecionarPorId(Guid id)
    {
        //aqui foi IA dmssssss
        using SqlConnection conexao = connectionFactory.CreateConnection();

        Tarefa? tarefaResult = null;

        // Indicamos os tipos: <Pai, Filho, Retorno>
        conexao.Query<Tarefa, ItensTarefa, Tarefa>(
            SelecionarPorIdSql,
            (tarefa, item) =>
            {
                // Se for a primeira linha do resultado, guardamos a Tarefa principal
                if (tarefaResult == null)
                {
                    tarefaResult = tarefa;
                    tarefaResult.Tarefas = new List<ItensTarefa>(); // Garanta que a lista está inicializada
                }

                // Se a tarefa tiver itens (o LEFT JOIN não veio nulo), adicionamos à lista
                if (item != null)
                {
                    tarefaResult.Tarefas.Add(item);
                }

                return tarefa;
            },
            new { Id = id },
            splitOn: "Id" // O Dapper usa isso para saber onde termina a tabela Tarefas e começa a TBItensTarefa
        );

        return tarefaResult;
    }

    public List<Tarefa> SelecionarTodos()
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Query<Tarefa>(SelecionarTodosSql).ToList();
    }

    public List<Tarefa> Filtrar(Predicate<Tarefa> filtro)
    {
        return SelecionarTodos().FindAll(filtro);
    }

    public void ConcluirItem(ConcluirItemDto dto)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        conexao.Execute(ConcluirItemSql, dto);
    }
}