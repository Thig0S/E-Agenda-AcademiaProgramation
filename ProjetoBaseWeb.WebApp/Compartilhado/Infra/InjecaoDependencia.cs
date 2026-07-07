using Dapper;

namespace ProjetoBaseWeb.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        // services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());
    }
}
