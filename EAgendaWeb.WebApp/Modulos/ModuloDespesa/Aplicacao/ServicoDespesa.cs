using AutoMapper;
using AutoMapper.Internal.Mappers;
using EAgendaWeb.WebApp.Modulos.ModuloCategoria.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Apresentacao;
using EAgendaWeb.WebApp.Modulos.ModuloDespesa.Dominio;
using FluentResults;

namespace EAgendaWeb.WebApp.Modulos.ModuloDespesa.Aplicacao;

public class ServicoDespesa
{
    private readonly IRepositorioDespesa repositorioDespesa;
    private readonly IRepositorioCategoria repositorioCategoria;
    private readonly IMapper mapper;

    public ServicoDespesa(IRepositorioDespesa repositorioDespesa, IRepositorioCategoria repositorioCategoria, IMapper mapper)
    {
        this.repositorioDespesa = repositorioDespesa;
        this.repositorioCategoria = repositorioCategoria;
        this.mapper = mapper;
    }

    public List<DetalheDespesaDto> SelecionarTodos()
    {
        return repositorioDespesa.SelecionarTodos().Select(e => new DetalheDespesaDto(
            e.Id.ToString(),
            e.Descricao,
            e.DataOcorrencia.ToString(),
            e.Valor,
            e.FormaPagamento,
            e.Categoria?.ToString() ?? "Sem Categoria")).ToList();
    }

    internal Result Cadastrar(CadastrarDespesaDto dto)
    {
        Categoria? categoria = null;

        if (dto.Categoria != null)
            categoria = repositorioCategoria.SelecionarPorId(new Guid(dto.Categoria));

        Despesa novaDespesa = new(dto.Descricao, dto.Valor, dto.FormaPagamento, categoria,
        (dto.DataOcorrencia == null) ? null : DateTime.Parse(dto.DataOcorrencia));

        Result errosValidacao = ValidarEntidade(novaDespesa);

        if (errosValidacao.IsFailed)
            return errosValidacao;

        repositorioDespesa.Cadastrar(novaDespesa);

        return Result.Ok();

    }
    private Result ValidarEntidade(Despesa entidade)
    {
        List<string> erros = entidade.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new FluentResults.Error(erros.First()).WithMetadata("Campo", string.Empty));
    }
}
