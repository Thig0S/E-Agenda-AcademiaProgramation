using EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;
using FluentResults;

namespace EAgendaWeb.WebApp.Modulos.ModuloContato.Aplicacao;

public class ServicoContato
{
    private readonly IRepositorioContato repositorioContato;

    public ServicoContato(IRepositorioContato repositorioContato)
    {
        this.repositorioContato = repositorioContato;
    }

    public List<DetalhesContatoDto> SelecionarTodos()
    {
        return repositorioContato.SelecionarTodos().Select(c => new DetalhesContatoDto(
            c.Id.ToString(),
            c.Nome,
            c.Telefone,
            c.Empresa,
            c.Cargo, c.Email)).ToList();
    }

    internal Result Cadastrar(CadastroContatoDto dto)
    {
        Contato novoContato = new(dto.Nome, dto.Email, dto.Telefone, dto.Cargo, dto.Empresa);

        Result resultadoValidacao = ValidarEntidade(novoContato);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioContato.Cadastrar(novoContato);

        return Result.Ok();
    }
    private static Result ValidarEntidade(Contato novoContato)
    {
        List<string> erros = novoContato.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new FluentResults.Error(erros.First()).WithMetadata("Campo", string.Empty));
    }
}
