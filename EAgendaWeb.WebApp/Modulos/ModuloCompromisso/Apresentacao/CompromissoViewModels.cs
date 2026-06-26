namespace EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Apresentacao;

public record ListarCompromissoViewModel(
    string Id,
    string Assunto,
    string DataOcorrencia,
    string HoraInicio,
    string HoraTermino,
    string TipoDeCompromisso,
    string? Local,
    string? Link,
    string? Contato
);