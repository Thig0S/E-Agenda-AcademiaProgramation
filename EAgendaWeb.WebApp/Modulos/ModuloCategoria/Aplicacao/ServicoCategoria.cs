using EAgendaWeb.WebApp.Modulos.ModuloCategoria.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Apresentacao;

namespace EAgendaWeb.WebApp.Modulos.ModuloCategoria.Aplicacao;

public class ServicoCategoria
{
    private readonly IRepositorioCategoria repositorioCategoria;

    public ServicoCategoria(IRepositorioCategoria repositorioCategoria)
    {
        this.repositorioCategoria = repositorioCategoria;
    }

    public List<Categoria> SelecionarTodos()
    {
        return repositorioCategoria.SelecionarTodos();
    }
}
