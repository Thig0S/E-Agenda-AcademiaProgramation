
using Dapper;
using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;
using Microsoft.Data.SqlClient;

namespace EAgendaWeb.WebApp.Modulos.ModuloContato.Infra
{
    public sealed class RepositorioContatoEmSql(ISqlConnectionFactory connectionFactory) : IRepositorioContato
    {
        private const string InserirSql = """
        INSERT INTO dbo.TBContatos (Id, Nome, Telefone, Email, Cargo, Empresa)
        VALUES (@Id, @Nome, @Telefone, @Email, @Cargo, @Empresa);
    """;

        private const string AtualizarSql = """
        UPDATE dbo.TBContatos
        SET
            Nome = @Nome,
            Telefone = @Telefone,
            Email = @Email,
            Cargo = @Cargo
        WHERE Id = @Id;
    """;

        private const string ExcluirSql = """
        DELETE FROM dbo.TBContatos
        WHERE Id = @Id;
    """;

        private const string SelecionarPorIdSql = """
        SELECT Id, Nome, Telefone, Email, Cargo, Empresa
        FROM dbo.TBContatos
        WHERE Id = @Id;
    """;

        private const string SelecionarTodosSql = """
        SELECT Id, Nome, Telefone, Email, Cargo, Empresa
        FROM dbo.TBContatos
        ORDER BY Nome;
    """;

        public void Cadastrar(Contato entidade)
        {
            using SqlConnection conexao = connectionFactory.CreateConnection();

            conexao.Open();

            conexao.Execute(InserirSql, entidade);
        }

        public bool Editar(Guid idSelecionado, Contato entidadeAtualizada)
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

        public Contato? SelecionarPorId(Guid idSelecionado)
        {
            using SqlConnection conexao = connectionFactory.CreateConnection();

            conexao.Open();

            return conexao.QuerySingleOrDefault<Contato>(SelecionarPorIdSql, new { Id = idSelecionado });
        }

        public List<Contato> SelecionarTodos()
        {
            using SqlConnection conexao = connectionFactory.CreateConnection();

            conexao.Open();

            return conexao.Query<Contato>(SelecionarTodosSql).ToList();
        }

        public List<Contato> Filtrar(Predicate<Contato> filtro)
        {
            return SelecionarTodos().FindAll(filtro);
        }
    }
}