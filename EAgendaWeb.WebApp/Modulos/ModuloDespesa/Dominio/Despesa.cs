using System.Runtime.Serialization;
using EAgendaWeb.WebApp.Compartilhado.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloCategoria.Dominio;

namespace EAgendaWeb.WebApp.Modulos.ModuloDespesa.Dominio;

public class Despesa : EntidadeBase<Despesa>
{
    public string Descricao { get; set; }
    public DateTime? DataOcorrencia { get; set; }
    public decimal Valor { get; set; }
    public string FormaPagamento { get; set; }
    public Categoria? Categoria { get; set; }

    public Despesa()
    {

    }
    public Despesa(string descricao, decimal valor, string formaPagamento, Categoria? categorias, DateTime? dataOcorrencia = null)
    {
        Descricao = descricao;
        DataOcorrencia = (dataOcorrencia != null) ? dataOcorrencia : DateTime.Now;
        FormaPagamento = formaPagamento;
        Valor = valor;
        Categoria = categorias;
    }

    public override void Atualizar(Despesa entidadeAtualizada)
    {
        Descricao = entidadeAtualizada.Descricao;
        DataOcorrencia = entidadeAtualizada.DataOcorrencia;
        Valor = entidadeAtualizada.Valor;
        Categoria = entidadeAtualizada.Categoria;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Descricao.Length < 2 || Descricao.Length > 100)
            erros.Add("O campo \"Descrição\" deve conter entre 2 à 100 caracteres!");

        if (Valor <= 0)
            erros.Add("O campo \"Valor\" deve conter uma valor positivo!");

        return erros;
    }
}
