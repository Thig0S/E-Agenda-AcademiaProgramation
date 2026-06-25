using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Infra;
using EAgendaWeb.WebApp.Modulos.ModuloFornecedor.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloFornecedor.Infra;

namespace EAgendaWeb.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IRepositorioFornecedor, RepositorioFornecedorEmSql>();
        services.AddScoped<IRepositorioContato, RepositorioContatoEmSql>();
    }
}
