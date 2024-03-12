using AutoMapper;
using MediatR;
using MMorais.Application.Usuarios.Commands;
using MMorais.Application.Usuarios.Events;
using MMorais.Application.Usuarios.Queries.ViewModel;
using MMorais.Application.Usuarios.Queries;
using MMorais.Domain;
using Moq;
using Moq.AutoMock;
using static MMorais.Application.Usuarios.Queries.ObterTodosUsuariosQuery;
namespace MMorais.Application.Tests.Handlers;

public class UsuarioCommandHandlerTests
{
    private readonly AutoMocker _mocker;
    private readonly UsuarioCommandHandler _usuarioHandler;

    public UsuarioCommandHandlerTests()
    {
        _mocker = new AutoMocker();
        _usuarioHandler = _mocker.CreateInstance<UsuarioCommandHandler>();
    }

    [Fact(DisplayName = "Cadastrar Usuário com Sucesso")]
    [Trait("Usuário", "Usuário Cadastro Command Handler")]
    public async Task CadastrarUsuario_NovoUsuario_DeveExecutarComSucesso()
    {
        // Arrange
        var usuarioCommand = new CadastrarUsuarioCommand(Guid.NewGuid(), "Usuário 01", "631.285.396-93", "(11)91111-1111", true);

        _mocker.GetMock<IUsuarioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

        // Act
        var result = await _usuarioHandler.Handle(usuarioCommand, CancellationToken.None);

        // Assert
        Assert.True(result);
        _mocker.GetMock<IUsuarioRepository>().Verify(r => r.CadastrarUsuario(It.IsAny<Usuario>()), Times.Once);
        _mocker.GetMock<IUsuarioRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }

    [Fact(DisplayName = "Cadastrar Usuário Command Inválido")]
    [Trait("Usuário", "Usuário Cadastro Command Handler")]
    public async Task CadastrarUsuario_CommandInvalido_DeveRetornarFalsoELancarEventosDeNotificacao()
    {
        // Arrange
        var usuarioCommand = new CadastrarUsuarioCommand(Guid.Empty, "", "", "", true);

        _mocker.GetMock<IUsuarioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

        // Act
        var result = await _usuarioHandler.Handle(usuarioCommand, CancellationToken.None);

        // Assert
        Assert.False(result);
        _mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Exactly(5));
    }

    [Fact(DisplayName = "Alterar Usuário com Sucesso")]
    [Trait("Usuário", "Usuário Alteração Command Handler")]
    public async Task AlterarUsuario_DeveExecutarComSucesso()
    {
        // Arrange
        var usuarioRepositoryMock = _mocker.GetMock<IUsuarioRepository>();
        var mediatorMock = _mocker.GetMock<IMediator>();

        var handler = new UsuarioCommandHandler(usuarioRepositoryMock.Object, mediatorMock.Object);
        
        var request = new AlterarUsuarioCommand(Guid.NewGuid(), "Novo Nome", "658.196.657-67", "(11)98765-4321", DateTime.Now, true);

        var usuarioExistente = new Usuario( "Nome Antigo", "556.117.529-77", "(11)12345-6789", DateTime.Now, true);
        usuarioRepositoryMock.Setup(r => r.ObterPorId(request.Id)).ReturnsAsync(usuarioExistente);
        usuarioRepositoryMock.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result);

        // Verifica se o usuário foi alterado corretamente
        Assert.Equal(request.Nome, usuarioExistente.Nome);
        Assert.Equal(request.Cpf, usuarioExistente.Cpf);
        Assert.Equal(request.Telefone, usuarioExistente.Telefone);

        // Verifica se o método Alterar Usuario do Repository foi chamado
        usuarioRepositoryMock.Verify(r => r.AlterarUsuario(usuarioExistente), Times.Once);
       
        // Verifica se o método Commit foi chamado
        usuarioRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Once);

        // Verifica se nenhum aviso de domínio foi publicado
        mediatorMock.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Alterar Usuário Command Inválido")]
    [Trait("Usuário", "Usuário Alteração Command Handler")]
    public async Task AlterarUsuario_CommandInvalido_DeveRetornarFalsoELancarEventosDeNotificacao()
    {
        // Arrange
        var usuarioRepositoryMock = _mocker.GetMock<IUsuarioRepository>();
        var mediatorMock = _mocker.GetMock<IMediator>();

        var handler = new UsuarioCommandHandler(usuarioRepositoryMock.Object, mediatorMock.Object);

        var request = new AlterarUsuarioCommand(Guid.NewGuid(), "", "", "", DateTime.Now, true);

        var usuarioExistente = new Usuario("Nome Antigo", "556.117.529-77", "(11)12345-6789", DateTime.Now, true);
        usuarioRepositoryMock.Setup(r => r.ObterPorId(request.Id)).ReturnsAsync(usuarioExistente);
        usuarioRepositoryMock.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result);
        
        // Verifica se o método Commit foi chamado
        usuarioRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Never);

        // Verifica se nenhum aviso de domínio foi publicado
        mediatorMock.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Exactly(4));
    }

    [Fact(DisplayName = "Excluir Usuário com Sucesso")]
    [Trait("Usuário", "Usuário Exclusão Command Handler")]
    public async Task ExcluirUsuario_DeveExecutarComSucesso()
    {
        // Arrange
        var usuarioRepositoryMock = _mocker.GetMock<IUsuarioRepository>();
        var mediatorMock = _mocker.GetMock<IMediator>();

        var handler = new UsuarioCommandHandler(usuarioRepositoryMock.Object, mediatorMock.Object);
        
        var request = new ExcluirUsuarioCommand(Guid.NewGuid());

        var usuarioExistente = new Usuario("Nome Antigo", "556.117.529-77", "(11)12345-6789", DateTime.Now, true);
        usuarioRepositoryMock.Setup(r => r.ObterPorId(request.Id)).ReturnsAsync(usuarioExistente);
        usuarioRepositoryMock.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result);

        // Verifica se o método remover Usuario do Repository foi chamado
        usuarioRepositoryMock.Verify(r => r.RemoverUsuario(usuarioExistente), Times.Once);

        // Verifica se o método Commit foi chamado
        usuarioRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Once);

        // Verifica se nenhum aviso de domínio foi publicado
        mediatorMock.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Excluir Usuário Command Inválido")]
    [Trait("Usuário", "Usuário Exclusão Command Handler")]
    public async Task ExcluirUsuario_CommandInvalido_DeveRetornarFalsoELancarEventosDeNotificacao()
    {
        // Arrange
        var usuarioRepositoryMock = _mocker.GetMock<IUsuarioRepository>();
        var mediatorMock = _mocker.GetMock<IMediator>();

        var handler = new UsuarioCommandHandler(usuarioRepositoryMock.Object, mediatorMock.Object);

        var request = new ExcluirUsuarioCommand(Guid.NewGuid());

        var usuarioExistente = new Usuario("Nome Antigo", "556.117.529-77", "(11)12345-6789", DateTime.Now, true);
        usuarioRepositoryMock.Setup(r => r.ObterPorId(Guid.NewGuid())).ReturnsAsync(usuarioExistente);
        usuarioRepositoryMock.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result);

        // Verifica se o método Commit foi chamado
        usuarioRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Never);

        // Verifica se nenhum aviso de domínio foi publicado
        mediatorMock.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Exactly(1));
    }
}