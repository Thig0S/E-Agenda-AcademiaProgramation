using EAgendaWeb.WebApp.Compartilhado.Dominio;

namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Dominio;

public class Tarefa : EntidadeBase<Tarefa>
{
    public string Titulo { get; set; }
    public string Prioridade { get; set; }
    public DateTime DataCricao { get; set; } = DateTime.Now;
    public DateTime DataConclusao { get; set; }
    public string StatusDeConclusao { get; set; }
    public List<ItensTarefa> Tarefas = [];

    public Tarefa(string titulo, string prioridade, DateTime dataConclusao, List<ItensTarefa> tarefas)
    {
        Titulo = titulo;
        Prioridade = prioridade;
        DataConclusao = dataConclusao;
        Tarefas = tarefas;
    }

    public override void Atualizar(Tarefa entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
        Prioridade = entidadeAtualizada.Prioridade;
        DataConclusao = entidadeAtualizada.DataConclusao;
        Tarefas = entidadeAtualizada.Tarefas;
    }

    public override List<string> Validar()
    {
        throw new NotImplementedException();
    }
    public void AddParaLista(ItensTarefa itenTarefa)
    {
        Tarefas.Add(itenTarefa);
    }
}
