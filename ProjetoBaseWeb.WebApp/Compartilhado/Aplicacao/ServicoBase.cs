using ProjetoBaseWeb.WebApp.Compartilhado.Dominio;
using FluentResults;

namespace ProjetoBaseWeb.WebApp.Compartilhado.Aplicacao
{
    public abstract class ServicoBase<T> where T : EntidadeBase<T>
    {
        protected static Result ValidarEntidade(T entidade)
        {
            List<string> erros = entidade.Validar();

            if (erros.Count == 0)
                return Result.Ok();

            var resultado = new Result();

            foreach (string erro in erros)
            {
                string campo = string.Empty;
                string mensagem = erro;

                if (erro.Contains('|'))
                {
                    var partes = erro.Split('|', 2);
                    campo = partes[0];
                    mensagem = partes[1];
                }

                resultado.WithError(new Error(mensagem).WithMetadata("Campo", campo));
            }

            return resultado; // Agora todos os erros vão para o ModelState!
        }

        protected static Result Falha(string campo, string mensagem)
        {
            return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
        }
    }
}