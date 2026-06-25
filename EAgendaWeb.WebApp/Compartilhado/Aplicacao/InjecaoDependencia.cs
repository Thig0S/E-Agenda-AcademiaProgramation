using EAgendaWeb.WebApp.Compartilhado.Aplicacao.Logging;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;


namespace EAgendaWeb.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ILoggingBuilder logging
    )
    {
        services.AddSerilogLogger(configuration, logging);
        services.AddScoped<ServicoFornecedor>();
        services.AddScoped<ServicoContato>();
    }
}
