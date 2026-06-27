using EAgendaWeb.WebApp.Compartilhado.Dominio;

namespace EAgendaWeb.WebApp.Modulos.ModuloCategoria.Dominio;

public class Categoria : EntidadeBase<Categoria>
{
    public string Titulo { get; set; }

    public Categoria()
    {

    }
    public Categoria(string titulo)
    {
        Titulo = titulo;
    }

    public override void Atualizar(Categoria entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (String.IsNullOrWhiteSpace(Titulo))
            erros.Add("O campo \"Titulo\" não pode ser vazio");

        if (Titulo.Length < 2 || Titulo.Length > 100)
            erros.Add("O campo \"Titulo\" deve conter entre 2 a 100 caracteres");

        return erros;
    }
}
