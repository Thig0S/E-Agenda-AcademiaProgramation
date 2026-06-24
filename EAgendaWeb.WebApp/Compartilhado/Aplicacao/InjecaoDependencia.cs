using EAgendaWeb.WebApp.Compartilhado.Aplicacao.Logging;
using EAgendaWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloFuncionario.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;

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

        services.AddScoped<ServicoEstoque>();
        services.AddScoped<ServicoFornecedor>();
        services.AddScoped<ServicoFuncionario>();
        services.AddScoped<ServicoMedicamento>();
        services.AddScoped<ServicoPaciente>();
    }
}
