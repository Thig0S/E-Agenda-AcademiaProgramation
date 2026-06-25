using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace EAgendaWeb.WebApp.Modulos.ModuloContato.Apresentacao;

public record ListarContatoViewModel(
    string Id,
    string Nome,
    string Telefone,
    string Empresa,
    string? Cargo,
    string? Email
);
public record CadastroContatoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório!")]
    string Nome,
    [Required(ErrorMessage = "O campo \"Telefone\" é obrigatório!")]
    string Telefone,
    [Required(ErrorMessage = "O campo \"Email\" é obrigatório!")]
    string Email,
    [Optional]
    string? Empresa,
    [Optional]
    string? Cargo
);