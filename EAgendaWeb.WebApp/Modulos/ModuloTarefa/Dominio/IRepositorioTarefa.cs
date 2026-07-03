using EAgendaWeb.WebApp.Compartilhado.Dominio;

namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Dominio;

public interface IRepositorioTarefa : IRepositorio<Tarefa>
{
    void AdicionarItem(ItensTarefa novoItem);

}