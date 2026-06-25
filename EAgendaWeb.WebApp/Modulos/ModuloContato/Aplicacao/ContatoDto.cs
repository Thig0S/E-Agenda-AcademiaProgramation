namespace EAgendaWeb.WebApp.Modulos.ModuloContato.Aplicacao;

public record DetalhesContatoDto(
    string Id,
    string Nome,
    string Telefone,
    string Empresa,
    string Cargo,
    string Email
);
public record CadastroContatoDto(
    string Nome,
    string Telefone,
    string Email,
    string Empresa,
    string Cargo
);