namespace EAgendaWeb.WebApp.Modulos.ModuloDespesa.Aplicacao;

public record DetalheDespesaDto(
    string Id,
    string Descricao,
    string DataOcorrencia,
    decimal Valor,
    string FormaPagamento,
    string Categoria
);
public record CadastrarDespesaDto(
    string Descricao,
    string DataOcorrencia,
    decimal Valor,
    string FormaPagamento,
    string Categoria
);
public record ExcluirDespesaDto(
    string Id
);
