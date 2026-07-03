namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Aplicacao;


public record DetalhesTarefaDto(
    string Id,
    string Titulo,
    string Prioridade,
    string DataCricao,
    string DataConclusao,
    string StatusDeConclusao,
    int PercentualConcluido
);
public record CadastroTarefaDto(
    string Titulo,
    string Prioridade,
    string DataConclusao
);
public record ExcluirTarefaDto(
    string Id,
    string Titulo,
    string Prioridade,
    string DataCricao,
    string DataConclusao,
    string StatusDeConclusao,
    int PercentualConcluido
);