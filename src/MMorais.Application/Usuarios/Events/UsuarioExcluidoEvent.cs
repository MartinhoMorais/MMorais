using MMorais.Core.Messages;

namespace MMorais.Application.Usuarios.Events;

public class UsuarioExcluidoEvent : Event
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string CPF { get; private set; }
    
    public UsuarioExcluidoEvent(Guid id, string nome, string cpf)
    {
        Id = id;
        Nome = nome;
        CPF = cpf;
    }
}