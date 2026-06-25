using EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;

namespace EAgendaWeb.WebApp.Modulos.ModuloContato.Aplicacao;

public class ServicoContato
{
    private readonly IRepositorioContato repositorioContato;

    public ServicoContato(IRepositorioContato repositorioContato)
    {
        this.repositorioContato = repositorioContato;
    }

    public List<DetalhesContatoDto> SelecionarTodos()
    {
        return repositorioContato.SelecionarTodos().Select(c => new DetalhesContatoDto(
            c.Id.ToString(),
            c.Nome,
            c.Telefone,
            c.Empresa,
            c.Cargo)).ToList();
    }
}
