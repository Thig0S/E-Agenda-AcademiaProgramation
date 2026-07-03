using EAgendaWeb.WebApp.Compartilhado.Dominio;

namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Dominio;

public enum StatusConclusao
{
    Aberto,
    Concluido
}

public class Tarefa : EntidadeBase<Tarefa>
{
    public string Titulo { get; set; }
    public string Prioridade { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime DataConclusao { get; set; }
    public StatusConclusao StatusDeConclusao { get; set; } = StatusConclusao.Aberto;
    public int PercentualConcluido
    {
        get
        {
            int quantidadeItens = Tarefas.Count;
            if (quantidadeItens == 0)
                return 0;

            int itensConcluidos = Tarefas.Count(i => i.Concluido == true);

            return (itensConcluidos * 100) / quantidadeItens;
        }
    }
    public List<ItensTarefa> Tarefas = [];

    public Tarefa()
    {

    }
    public Tarefa(string titulo, string prioridade, DateTime dataConclusao)
    {
        Titulo = titulo;
        Prioridade = prioridade;
        DataConclusao = dataConclusao;
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
        List<string> erros = [];

        if (Titulo.Length < 2 || Titulo.Length > 100)
            erros.Add("Titulo|O campo \"Titulo\" deve conter entre 2 à 100 caracteres.");

        return erros;
    }
    public void AddParaLista(ItensTarefa itenTarefa)
    {
        Tarefas.Add(itenTarefa);
    }
}
