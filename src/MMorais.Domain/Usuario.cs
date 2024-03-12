using MMorais.Core.DomainObjects;
using System.Text.Json.Serialization;

namespace MMorais.Domain;

public class Usuario : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public string Telefone { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public DateTime? DataAlteracao { get; private set; }
    public bool Ativo { get; private set; }

    [JsonConstructor]
    public Usuario(string nome, string cpf, string telefone, DateTime dataCadastro, bool ativo)
    {
        Nome = nome;
        Cpf = cpf;
        Telefone = telefone;
        DataCadastro = dataCadastro;
        Ativo = ativo;
    }
    protected Usuario() { }
    public void AlterarUsuario(string nome, string cpf, string telefone, DateTime dataAlteracao, bool ativo)
    {
        Nome = nome;
        Cpf = cpf;
        Telefone = telefone;
        DataAlteracao = dataAlteracao;
        Ativo = ativo;
    }

    public override bool EhValido()
    {
        return !string.IsNullOrWhiteSpace(Nome) &&
               !string.IsNullOrWhiteSpace(Cpf);
    }
}