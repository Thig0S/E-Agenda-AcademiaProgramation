using EAgendaWeb.WebApp.Compartilhado.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloTarefa.Aplicacao;

namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Dominio;

public interface IRepositorioTarefa : IRepositorio<Tarefa>
{
    void AdicionarItem(ItensTarefa novoItem);
    bool ExcluirItem(ExcluirItemDto dto);
}