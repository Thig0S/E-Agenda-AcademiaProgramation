using EAgendaWeb.WebApp.Compartilhado.Dominio;

namespace EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;

public class Contato : EntidadeBase<Contato>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cargo { get; set; }
    public string Empresa { get; set; }

    public Contato()
    {
        
    }

    public Contato(string nome, string email, string telefone, string cargo, string empresa)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Cargo = cargo;
        Empresa = empresa;
    }


    public override void Atualizar(Contato entidadeAtualizada)
    {
        throw new NotImplementedException();
    }

    public override List<string> Validar()
    {
        throw new NotImplementedException();
    }
}
