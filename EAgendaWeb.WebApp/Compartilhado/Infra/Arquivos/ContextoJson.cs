using System.Text.Json;
using System.Text.Json.Serialization;
using EAgendaWeb.WebApp.Modulos.ModuloEstoque.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloFornecedor.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloFuncionario.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloMedicamento.Dominio;
using EAgendaWeb.WebApp.Modulos.ModuloPaciente.Dominio;

namespace EAgendaWeb.WebApp.Compartilhado.Infra.Arquivos;

public sealed class ContextoJson
{
    private readonly string caminhoArquivo;

    public List<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();
    public List<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
    public List<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();
    public List<Paciente> Pacientes { get; set; } = new List<Paciente>();
    public List<RequisicaoBase> Requisicoes { get; set; } = new List<RequisicaoBase>();

    public ContextoJson()
    {
        string caminhoAppData = Environment
            .GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorio = Path.Combine(caminhoAppData, "EAgendaWeb");

        Directory.CreateDirectory(caminhoDiretorio);

        caminhoArquivo = Path.Combine(caminhoDiretorio, "dados.json");
    }

    public void Salvar()
    {
        JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
        opcoesJson.WriteIndented = true;
        opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;
        opcoesJson.Converters.Add(new JsonStringEnumConverter());

        string jsonString = JsonSerializer.Serialize(this, opcoesJson);

        File.WriteAllText(caminhoArquivo, jsonString);
    }

    public void Carregar()
    {
        if (!File.Exists(caminhoArquivo))
            return;

        string jsonString = File.ReadAllText(caminhoArquivo);

        JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
        opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;
        opcoesJson.Converters.Add(new JsonStringEnumConverter());

        ContextoJson? contextoSalvo = JsonSerializer
            .Deserialize<ContextoJson>(jsonString, opcoesJson);

        if (contextoSalvo == null)
            return;

        Fornecedores = contextoSalvo.Fornecedores ?? new List<Fornecedor>();
        Funcionarios = contextoSalvo.Funcionarios ?? new List<Funcionario>();
        Medicamentos = contextoSalvo.Medicamentos ?? new List<Medicamento>();
        Pacientes = contextoSalvo.Pacientes ?? new List<Paciente>();
        Requisicoes = contextoSalvo.Requisicoes ?? new List<RequisicaoBase>();
    }
}
