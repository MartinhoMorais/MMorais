namespace MMorais.Core.Messages.CommonMessages.IntegrationEvents;

public class UsuarioInativadoEvent : IntegrationEvent
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string CPF { get; private set; }
    public string Telefone { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public DateTime? DataAlteracao { get; private set; }
    public bool Ativo { get; private set; }

    public UsuarioInativadoEvent(Guid id, string nome, string cpf, string telefone, DateTime dataCadastro, DateTime? dataAlteracao, bool ativo)
    {
        Id = id;
        Nome = nome;
        CPF = cpf;
        Telefone = telefone;
        DataCadastro = dataCadastro;
        DataAlteracao = dataAlteracao;
        Ativo = ativo;
    }
}