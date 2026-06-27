using Dapper;
using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Dominio;
using Microsoft.Data.SqlClient;

namespace EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Infra;

public class RepositorioCompromissoEmSql(ISqlConnectionFactory connectionFactory) : IRepositorioCompromisso
{
    private const string InserirSql = """
    INSERT INTO dbo.TBCompromissos 
    (Id, Assunto, DataOcorrencia, HoraInicio, HoraTermino, TipoDeCompromisso, Local, Link, ContatoId)
    VALUES 
    (@Id, @Assunto, @DataOcorrencia, @HoraInicio, @HoraTermino, @TipoDeCompromisso, @Local, @Link, @ContatoId);
""";

    private const string AtualizarSql = """
        UPDATE dbo.TBCompromissos
        SET
            Assunto = @Assunto,
            DataAcorrencia = @DataOcorrencia,
            HoraInicio = @HoraInicio,
            HoraTermino = @HoraTermino,
            TipoDeCompromisso = @TipoDeCompromisso,
            Local = @Local,
            ContatoId = @ContatoId
        WHERE Id = @Id;
    """;

    private const string ExcluirSql = """
        DELETE FROM dbo.TBCompromissos
        WHERE Id = @Id;
    """;

    private const string SelecionarPorIdSql = """
        SELECT Id, Assunto, DataOcorrencia, HoraInicio, HoraTermino, TipoDeCompromisso, Local, Link, ContatoId
        FROM dbo.TBCompromissos
        WHERE Id = @Id;
    """;

    private const string SelecionarTodosSql = """
    SELECT 
        c.Id, 
        c.Assunto, 
        c.DataOcorrencia, 
        c.HoraInicio, 
        c.HoraTermino, 
        c.TipoDeCompromisso, 
        c.Local, 
        c.Link, 
        c.ContatoId,
        ct.Nome AS NomeContato
    FROM dbo.TBCompromissos c
    LEFT JOIN dbo.TBContatos ct ON c.ContatoId = ct.Id
    ORDER BY c.Assunto;
""";

    public void Cadastrar(Compromisso entidade)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        conexao.Execute(InserirSql, entidade);
    }

    public bool Editar(Guid idSelecionado, Compromisso entidadeAtualizada)
    {
        entidadeAtualizada.Id = idSelecionado;

        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Execute(AtualizarSql, entidadeAtualizada) == 1;
    }

    public bool Excluir(Guid idSelecionado)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Execute(ExcluirSql, new { Id = idSelecionado }) == 1;
    }

    public Compromisso? SelecionarPorId(Guid idSelecionado)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.QuerySingleOrDefault<Compromisso>(SelecionarPorIdSql, new { Id = idSelecionado });
    }

    public List<Compromisso> SelecionarTodos()
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Query<Compromisso>(SelecionarTodosSql).ToList();
    }

    public List<Compromisso> Filtrar(Predicate<Compromisso> filtro)
    {
        return SelecionarTodos().FindAll(filtro);
    }
}
