using MediatR;

namespace MMorais.Application.Usuarios.Events;

public class UsuarioEventHandler :
    INotificationHandler<UsuarioCadastradoEvent>,
    INotificationHandler<UsuarioAlteradoEvent>,
    INotificationHandler<UsuarioExcluidoEvent>
{
    public Task Handle(UsuarioCadastradoEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    public Task Handle(UsuarioAlteradoEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    public Task Handle(UsuarioExcluidoEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}