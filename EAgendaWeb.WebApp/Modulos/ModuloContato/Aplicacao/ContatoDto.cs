namespace EAgendaWeb.WebApp.Modulos.ModuloContato.Aplicacao;

public record DetalhesContatoDto(
    string Id,
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo
);
public record CadastroContatoDto(
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo
);
public record EditarContatoDto(
    string Id,
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo
);