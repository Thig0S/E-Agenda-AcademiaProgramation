
using Dapper;
using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloDespesa.Dominio;
using Microsoft.Data.SqlClient;

namespace EAgendaWeb.WebApp.Modulos.ModuloContato.Infra
{
    public sealed class RepositorioDespesaEmSql(ISqlConnectionFactory connectionFactory) : IRepositorioDespesa
    {
        private const string InserirSql = """
        INSERT INTO dbo.TBDespesa (Id, Descricao, Valor, FormaPagamento)
        VALUES (@Id, @Descricao, @Valor, @FormaPagamento);
    """;

        private const string AtualizarSql = """
        UPDATE dbo.TBDespesa
        SET
            Descricao = @Descricao,
            Valor = @Valor,
            FormaPagamento = @FormaPagamento
        WHERE Id = @Id;
    """;

        private const string ExcluirSql = """
        -- É recomendável excluir o vínculo na tabela associativa antes de excluir a despesa
        -- para evitar erros de Constraint de Chave Estrangeira (FK), a menos que use "ON DELETE CASCADE".
        DELETE FROM dbo.TBCategoria_Despesa WHERE DespesaId = @Id;

        DELETE FROM dbo.TBDespesa
        WHERE Id = @Id;
    """;

        private const string SelecionarPorIdSql = """
        SELECT 
            d.Id, 
            d.Descricao, 
            d.Valor, 
            d.FormaPagamento,
            c.Id AS CategoriaId,
            c.Titulo AS CategoriaTitulo
        FROM dbo.TBDespesa d
        LEFT JOIN dbo.TBCategoria_Despesa cd ON d.Id = cd.DespesaId
        LEFT JOIN dbo.TBCategoria c ON cd.CategoriaID = c.Id
        WHERE d.Id = @Id;
    """;

        private const string SelecionarTodosSql = """
        SELECT 
            d.Id, 
            d.Descricao, 
            d.Valor, 
            d.FormaPagamento,
            c.Id AS CategoriaId,
            c.Titulo AS CategoriaTitulo
        FROM dbo.TBDespesa d
        LEFT JOIN dbo.TBCategoria_Despesa cd ON d.Id = cd.DespesaId
        LEFT JOIN dbo.TBCategoria c ON cd.CategoriaID = c.Id
        ORDER BY d.Descricao;
    """;

        public void Cadastrar(Despesa entidade)
        {
            using SqlConnection conexao = connectionFactory.CreateConnection();

            conexao.Open();

            conexao.Execute(InserirSql, entidade);
        }

        public bool Editar(Guid idSelecionado, Despesa entidadeAtualizada)
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

        public Despesa? SelecionarPorId(Guid idSelecionado)
        {
            using SqlConnection conexao = connectionFactory.CreateConnection();

            conexao.Open();

            return conexao.QuerySingleOrDefault<Despesa>(SelecionarPorIdSql, new { Id = idSelecionado });
        }

        public List<Despesa> SelecionarTodos()
        {
            using SqlConnection conexao = connectionFactory.CreateConnection();

            conexao.Open();

            return conexao.Query<Despesa>(SelecionarTodosSql).ToList();
        }

        public List<Despesa> Filtrar(Predicate<Despesa> filtro)
        {
            return SelecionarTodos().FindAll(filtro);
        }
    }
}