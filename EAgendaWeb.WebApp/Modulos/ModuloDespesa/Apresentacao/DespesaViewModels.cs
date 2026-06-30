using System.ComponentModel.DataAnnotations;

namespace EAgendaWeb.WebApp.Modulos.ModuloDespesa.Apresentacao;

public record ListarDespesaViewModel(
    string Id,
    string Descricao,
    string DataOcorrencia,
    decimal Valor,
    string FormaPagamento,
    string? Categoria
);
public record CadastrarDespesaViewModel(
    [Required(ErrorMessage = "O campo \"Descrição\" é Obrigatório!")]
    string Descricao,
    string? DataOcorrencia,
    [Required(ErrorMessage = "O campo \"Valor\" é Obrigatório!")]
    decimal Valor,
    [Required(ErrorMessage = "O campo \"Forma de Pagamento\" é Obrigatório!")]
    string FormaPagamento,
    string? Categoria
);
