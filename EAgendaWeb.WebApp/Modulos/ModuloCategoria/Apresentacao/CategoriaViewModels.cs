namespace EAgendaWeb.WebApp.Modulos.ModuloCategoria.Apresentacao;

public record ListarCategoriaViewModel(
    string Id,
    string Titulo
);
public record CadastrarCategoriaViewModel(
    string Titulo
);
public record ExcluirCategoriaViewModel(
    string Id,
    string Titulo
);
public record EditarCategoriaViewModel(
    string Id,
    string Titulo
);
