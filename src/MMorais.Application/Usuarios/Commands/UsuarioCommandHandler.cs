using MediatR;
using MMorais.Domain;
using MMorais.Core.Messages;
using MMorais.Application.Usuarios.Events;
using MMorais.Core.Messages.CommonMessages.Notifications;

namespace MMorais.Application.Usuarios.Commands;

public class UsuarioCommandHandler :
    IRequestHandler<CadastrarUsuarioCommand, bool>,
    IRequestHandler<AlterarUsuarioCommand, bool>,
    IRequestHandler<ExcluirUsuarioCommand, bool>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMediator _mediator;

    public UsuarioCommandHandler(IUsuarioRepository usuarioRepository, IMediator mediator)
    {
        _usuarioRepository = usuarioRepository;
        _mediator = mediator;
    }

    public async Task<bool> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando(request)) return false;

        var existeUsuario = await _usuarioRepository.ObterPorCpf(request.Cpf);
        if (existeUsuario is not null)
        {
            await _mediator.Publish(    
                new DomainNotification(request.MessageType, "Usuário Já cadastrado."));
            return false;
        }

        var usuario = new Usuario(request.Nome, request.Cpf, request.Telefone, DateTime.Now, request.Ativo);

        await _usuarioRepository.CadastrarUsuario(usuario);

        usuario.AdicionarEvento(new UsuarioCadastradoEvent(request.Id, request.Nome, request.Cpf, request.Telefone));

        return await _usuarioRepository.UnitOfWork.Commit();

    }
    public async Task<bool> Handle(AlterarUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando(request)) return false;

        var usuario = await _usuarioRepository.ObterPorId(request.Id);
        if (usuario is null)
        {
            await _mediator.Publish(new DomainNotification("Usuário", "Usuário não encontrado!"));
            return false;
        }

        usuario.AlterarUsuario(request.Nome, request.Cpf, request.Telefone, request.DataAlteracao, request.Ativo);
        _usuarioRepository.AlterarUsuario(usuario);

        usuario.AdicionarEvento(new UsuarioAlteradoEvent(request.Id, request.Nome, request.Cpf, request.Telefone));

        return await _usuarioRepository.UnitOfWork.Commit();
    }
    public async Task<bool> Handle(ExcluirUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (!ValidarComando(request)) return false;

        var usuario = await _usuarioRepository.ObterPorId(request.Id);
        if (usuario is null)
        {
            await _mediator.Publish(new DomainNotification("Usuário", "Usuário não encontrado!"));
            return false;
        }

        usuario.AdicionarEvento(new UsuarioExcluidoEvent(request.Id, usuario.Nome, usuario.Cpf));
        
        _usuarioRepository.RemoverUsuario(usuario);
        return await _usuarioRepository.UnitOfWork.Commit();

    }
    private bool ValidarComando(Command request)
    {
        if (request.EhValido()) return true;

        foreach (var error in request.ValidationResult.Errors)
            _mediator.Publish(new DomainNotification(request.MessageType, error.ErrorMessage));

        return false;
    }
}