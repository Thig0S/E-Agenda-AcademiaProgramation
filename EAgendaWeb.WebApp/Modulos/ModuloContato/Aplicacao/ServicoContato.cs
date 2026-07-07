using AutoMapper;
using EAgendaWeb.WebApp.Compartilhado.Aplicacao;
using EAgendaWeb.WebApp.Modulos.ModuloContato.Dominio;
using FluentResults;

namespace EAgendaWeb.WebApp.Modulos.ModuloContato.Aplicacao;

public class ServicoContato : ServicoBase<Contato>
{
    private readonly IRepositorioContato repositorioContato;
    private readonly IMapper mapper;

    public ServicoContato(IRepositorioContato repositorioContato, IMapper mapper)
    {
        this.repositorioContato = repositorioContato;
        this.mapper = mapper;
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
    internal List<DetalhesContatoDto> SelecionarTodos()
    {
        return repositorioContato.SelecionarTodos().Select(c => new DetalhesContatoDto(
            c.Id.ToString(),
            c.Nome,
            c.Telefone,
            c.Email,
            c.Empresa,
            c.Cargo)).ToList();
    }
    internal Result<DetalhesContatoDto> SelecionarPorId(Guid id)
    {
        Contato? contato = repositorioContato.SelecionarPorId(id);

        if (contato == null)
            return Result.Fail("Fornecedor não encontrado!");

        return Result.Ok(
            new DetalhesContatoDto(contato.Id.ToString(), contato.Nome, contato.Telefone, contato.Email,
            contato.Empresa!, contato.Cargo!)
            );
    }

    internal Result Excluir(string id)
    {
        Guid guid = new(id);

        Result<DetalhesContatoDto> selecionarContato = SelecionarPorId(guid);

        if (selecionarContato.IsFailed)
            return Result.Fail("Contato Não Encontrado!");

        repositorioContato.Excluir(guid);

        return Result.Ok();
    }

    internal Result Editar(EditarContatoDto dto)
    {
        Guid guid = new(dto.Id);
        Contato contatoAtualizado = new(dto.Nome, dto.Email, dto.Telefone, dto.Cargo!, dto.Empresa!);

        Result resultadoValidacao = ValidarEntidade(contatoAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        if (ExisteContatoComMesmoTelefone(contatoAtualizado.Telefone, guid))
            return Falha(nameof(dto.Telefone), "Já existe um Contato com este Telefone.");

        if (ExiteContatoComMesmoEmail(contatoAtualizado.Email, guid))
            return Falha(nameof(dto.Email), "Já existe um Contato com este Email.");

        repositorioContato.Editar(guid, contatoAtualizado);

        return Result.Ok();
    }
}
