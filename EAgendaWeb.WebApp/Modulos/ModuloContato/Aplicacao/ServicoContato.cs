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

        if (ExisteContatoComMesmoTelefone(novoContato.Telefone))
            return Falha(nameof(dto.Telefone), "Já existe um Contato com este Telefone.");

        if (ExiteContatoComMesmoEmail(novoContato.Email))
            return Falha(nameof(dto.Email), "Já existe um Contato com este Email.");

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

    private bool ExisteContatoComMesmoTelefone(string telefone, Guid? idIgnorado = null)
    {
        return repositorioContato.SelecionarTodos().Any(c => c.Id != idIgnorado &&
        string.Equals(c.Telefone, telefone, StringComparison.OrdinalIgnoreCase)
        );
    }
    private bool ExiteContatoComMesmoEmail(string email, Guid? idIgnorado = null)
    {
        return repositorioContato.SelecionarTodos().Any(c => c.Id != idIgnorado &&
        string.Equals(c.Email, email, StringComparison.OrdinalIgnoreCase)
        );
    }
    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new FluentResults.Error(mensagem).WithMetadata("Campo", campo));
    }
}
