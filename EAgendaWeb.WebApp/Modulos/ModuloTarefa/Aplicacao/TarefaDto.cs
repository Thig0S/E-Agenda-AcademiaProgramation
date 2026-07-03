namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Aplicacao;


public record DetalhesTarefaDto(
    string Id,
    string Titulo,
    string Prioridade,
    string DataCriacao,
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
    string DataCriacao,
    string DataConclusao,
    string StatusDeConclusao,
    int PercentualConcluido
);

public record EditarTarefaDto(
    string Id,
    string Titulo,
    string Prioridade,
    string DataCriacao,
    string DataConclusao,
    string StatusDeConclusao,
    int PercentualConcluido
);