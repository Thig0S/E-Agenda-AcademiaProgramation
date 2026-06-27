using System.ComponentModel.DataAnnotations;

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
public record CadastrarCompromissoViewModel(
    [Required (ErrorMessage = "O campo \"Assunto\" é obrigatório!")]
    string Assunto,
    [Required (ErrorMessage = "O campo \"Data de Ocorrencia\" é obrigatório!")]
    string DataOcorrencia,
    [Required (ErrorMessage = "O campo \"Hora de Inicio\" é obrigatório!")]
    string HoraInicio,
    [Required (ErrorMessage = "O campo \"Hora de Termino\" é obrigatório!")]
    string HoraTermino,
    [Required (ErrorMessage = "O campo \"Tipo de Compromisso\" é obrigatório!")]
    string TipoDeCompromisso,
    string? Local,
    string? Link,
    string? Contato
);