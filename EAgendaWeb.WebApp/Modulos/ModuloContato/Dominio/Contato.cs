using System.ComponentModel.DataAnnotations;
using EAgendaWeb.WebApp.Compartilhado.Dominio;

namespace EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;

public class Contato : EntidadeBase<Contato>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string? Cargo { get; set; }
    public string? Empresa { get; set; }

    public Contato()
    {
    }

    public Contato(string nome, string email, string telefone, string cargo = "", string empresa = "")
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Cargo = cargo;
        Empresa = empresa;
    }

    public override void Atualizar(Contato entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Email = entidadeAtualizada.Email;
        Telefone = entidadeAtualizada.Telefone;
        Cargo = entidadeAtualizada.Cargo;
        Empresa = entidadeAtualizada.Empresa;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Nome.Length < 2 && Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 a 100 caracteres");

        bool isValid = new EmailAddressAttribute().IsValid(Email);

        if (!isValid)
            erros.Add("O campo \"Email\" deve conter um Email válido!");

        if (Email.Length < 2 && Email.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 a 100 caracteres");

        string telefoneEncurtado = Telefone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
        bool contemLetraOuSimbolo = false;

        int contadorDigitos = 0;

        for (int i = 0; i < telefoneEncurtado.Length; i++)
        {
            char c = telefoneEncurtado[i];
            if (char.IsDigit(c))
                contadorDigitos++;
            else
            {
                contemLetraOuSimbolo = true;
                break;
            }
        }

        if (contadorDigitos < 10 || contadorDigitos > 11)
            erros.Add("O campo \"Telefone\" deve conter entre 10 e 11 dígitos;");

        if (contemLetraOuSimbolo)
            erros.Add("O campo \"Telefone\" deve conter apenas dígitos;");

        return erros;
    }
}
