using EAgendaWeb.WebApp.Compartilhado.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;

namespace EAgendaWeb.WebApp.Modulos.ModuloCompromisso.Dominio;

public class Compromisso : EntidadeBase<Compromisso>
{
    public string Assunto { get; set; }
    public DateTime DataOcorrencia { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraTerminio { get; set; }
    public TipoEvento TipoEvento { get; set; }
    public string? Local { get; set; }
    public string? Link { get; set; }
    public Contato? Contato { get; set; }

    public Compromisso(
        string assunto, DateTime dataOcorrencia,
        TimeOnly horaInicio, TimeOnly horaTerminio,
        TipoEvento tipoEvento, string? local = null,
        string? link = null, Contato? contato = null)
    {
        Assunto = assunto;
        DataOcorrencia = dataOcorrencia;
        HoraInicio = horaInicio;
        HoraTerminio = horaTerminio;
        TipoEvento = tipoEvento;
        Local = local;
        Link = link;
        Contato = contato;
    }

    public override void Atualizar(Compromisso entidadeAtualizada)
    {
        Assunto = entidadeAtualizada.Assunto;
        DataOcorrencia = entidadeAtualizada.DataOcorrencia;
        HoraInicio = entidadeAtualizada.HoraInicio;
        HoraTerminio = entidadeAtualizada.HoraTerminio;
        TipoEvento = entidadeAtualizada.TipoEvento;
        Local = entidadeAtualizada.Local;
        Link = entidadeAtualizada.Link;
        Contato = entidadeAtualizada.Contato;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Assunto.Length < 2 || Assunto.Length > 100)
            erros.Add("O campo \"Assunto\" deve conter entre 2 à 100 caracteres");

        if (DataOcorrencia < DateTime.Now)
            erros.Add("A data do Compromisso deve ser posterior a data atual");

        if (HoraTerminio < HoraInicio)
            erros.Add("A Hora de Terminio deve ser POSTERIOR a hora de Inicio");

        return erros;
    }
}
