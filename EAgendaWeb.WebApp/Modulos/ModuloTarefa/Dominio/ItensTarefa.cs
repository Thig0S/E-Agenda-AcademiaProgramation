using EAgendaWeb.WebApp.Compartilhado.Dominio;

namespace EAgendaWeb.WebApp.Modulos.ModuloTarefa.Dominio;

public class ItensTarefa : EntidadeBase<ItensTarefa>
{
    public string Titulo { get; set; } = string.Empty;
    public bool Concluido { get; set; }

    public ItensTarefa()
    {
    }

    public ItensTarefa(string titulo) : this()
    {
        Titulo = titulo;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Titulo) || Titulo.Length < 2 || Titulo.Length > 100)
            erros.Add("O campo \"Título\" deve conter entre 2 e 100 caracteres.");

        return erros;
    }

    public override void Atualizar(ItensTarefa entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
        Concluido = entidadeAtualizada.Concluido;
    }
}
