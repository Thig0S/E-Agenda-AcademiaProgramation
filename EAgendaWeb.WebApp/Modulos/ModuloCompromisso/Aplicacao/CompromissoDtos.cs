namespace EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Aplicacao;

public record ListarCompromissoDto(
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
public record CadastrarCompromissoDto(
    string Assunto,
    string DataOcorrencia,
    string HoraInicio,
    string HoraTermino,
    string TipoDeCompromisso,
    string? Local,
    string? Link,
    string? Contato
);
public record ExcluirCompromissoDto(
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
public record EditarCompromissoDto(
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
public record DetalhesCompromissoDto(
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