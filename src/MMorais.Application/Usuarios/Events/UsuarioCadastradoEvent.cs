using MMorais.Core.Messages;

namespace MMorais.Application.Usuarios.Events;

public class UsuarioCadastradoEvent : Event
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string CPF { get; private set; }
    public string Telefone { get; private set; }

    public UsuarioCadastradoEvent(Guid id, string nome, string cpf, string telefone)
    {
        Id = id;
        Nome = nome;
        CPF = cpf;
        Telefone = telefone;
    }
}