using System.ComponentModel.DataAnnotations;

namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Apresentacao;

public record ListarTarefaViewModel(
    string Id,
    string Titulo,
    string Prioridade,
    string DataCriacao,
    string DataConclusao,
    string StatusDeConclusao,
    int PercentualConcluido
);
public record CadastroTarefaViewModel(
    [Required (ErrorMessage = "O campo \"Titulo\" é obrigatório!")]
    string Titulo,
    [Required (ErrorMessage = "O campo \"Prioridade\" é obrigatório!")]
    string Prioridade,
    [Required (ErrorMessage = "O campo \"Data de Conclusão\" é obrigatório!")]
    string DataConclusao
);
public record ExcluirTarefaViewModel(
    string Id,
    string Titulo,
    string Prioridade,
    string DataCriacao,
    string DataConclusao,
    string StatusDeConclusao,
    int PercentualConcluido
);
public record EditarTarefaViewModel(
    string Id,
    [Required (ErrorMessage = "O campo \"Titulo\" é obrigatório!")]
    string Titulo,
    [Required (ErrorMessage = "O campo \"Prioridade\" é obrigatório!")]
    string Prioridade,
    string DataCriacao,
    [Required (ErrorMessage = "O campo \"Data de Conclusão\" é obrigatório!")]
    string DataConclusao,
    string StatusDeConclusao,
    int PercentualConcluido
);
public record MostrarItensTarefaViewModel(
    string Id,
    string Titulo,
    string Prioridade,
    string StatusDeConclusao,
    int PercentualConcluido,
    List<ItensDaTarefaViewModel> ListaDeItens
);
public record CadastrarItemViewModel(
    string TarefaId,
    string Titulo
);
public record ItensDaTarefaViewModel(
    string Id,
    string Titulo,
    bool Concluido
);
public record ExcluirItemViewModel(
    string Id,
    string TarefaId
);