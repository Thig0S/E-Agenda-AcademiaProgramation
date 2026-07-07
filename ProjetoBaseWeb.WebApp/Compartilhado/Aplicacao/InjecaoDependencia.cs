using ProjetoBaseWeb.WebApp.Compartilhado.Aplicacao.Logging;

namespace ProjetoBaseWeb.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ILoggingBuilder logging
    )
    {
        services.AddSerilogLogger(configuration, logging);
        // services.AddScoped<ServicoContato>();
    }
}
