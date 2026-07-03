namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Apresentacao;

public record ListarTarefaViewModel(
    string Id,
    string Titulo,
    string Prioridade,
    string DataCricao,
    string DataConclusao,
    string StatusDeConclusao,
    int PercentualConcluido
);