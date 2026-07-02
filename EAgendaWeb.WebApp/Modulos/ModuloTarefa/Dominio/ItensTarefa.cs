namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Dominio;

public enum StatusConclusao
{
    Aberto,
    Concluido
}
public class ItensTarefa
{
    public string Titulo { get; set; }
    public StatusConclusao StatusConclusao = StatusConclusao.Aberto;
    public Tarefa TarefaId { get; set; }

    public ItensTarefa(Tarefa tarefa, string titulo)
    {
        TarefaId = tarefa;
        Titulo = titulo;
    }
}
