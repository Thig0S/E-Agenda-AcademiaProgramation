namespace EAgendaWeb.WebApp.Modulos.ModuloDespesa.Apresentacao;

public record ListarDespesaViewModel(
    string Id,
    string Descricao,
    string DataOcorrencia,
    decimal Valor,
    string FormaPagamento,
    string Categoria
);
