using Dapper;
using EAgendaWeb.WebApp.Compartilhado.Infra.Sql;
using EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Infra;
using EAgendaWeb.WebApp.Modulos.ModuloCategoria.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloCategoria.Infra;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Infra;
using EAgendaWeb.WebApp.Modulos.ModuloDespesa.Dominio;

namespace EAgendaWeb.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IRepositorioContato, RepositorioContatoEmSql>();
        services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmSql>();
        services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmSql>();
        services.AddScoped<IRepositorioDespesa, RepositorioDespesaEmSql>();
        SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());
    }
}
