using AutoMapper;
using MMorais.Application.Usuarios.Queries.ViewModel;
using MMorais.Application.Usuarios.Queries;
using MMorais.Domain;
using Moq;
using static MMorais.Application.Usuarios.Queries.ObterTodosUsuariosQuery;

namespace MMorais.Application.Tests.Queries;

public class UsuariosQueryHandlerTests
{
    [Fact(DisplayName = "Consultar Usuários Query")]
    [Trait("Usuário", "Usuário Query Handler")]
    public async Task Handle_DeveRetornarUsuariosCorretos()
    {
        // Arrange
        var usuarioRepositoryMock = new Mock<IUsuarioDapperRepository>();
        var mapperMock = new Mock<IMapper>();

        var handler = new ObterTodosUsuariosQueryHandler(usuarioRepositoryMock.Object, mapperMock.Object);

        var query = new ObterTodosUsuariosQuery();

        var usuariosDoRepositorio = new List<Usuario>
        {
            new Usuario("Usuário 1", "123.456.789-00", "(11)98765-4321", DateTime.Now, true),
            new Usuario("Usuário 2", "987.654.321-00", "(11)12345-6789", DateTime.Now, true)
            // Adicione mais usuários conforme necessário
        };

        var usuariosViewModelEsperados = new List<UsuarioViewModel>
        {
            new UsuarioViewModel { Nome = "Usuário 1", Cpf = "123.456.789-00", Telefone = "(11)98765-4321" },
            new UsuarioViewModel { Nome = "Usuário 2", Cpf = "987.654.321-00", Telefone = "(11)12345-6789" }
            // Adicione mais view models conforme necessário
        };

        // Configuração do mock do repositório para retornar a lista de usuários ao chamar ObterTodos()
        usuarioRepositoryMock.Setup(r => r.ObterTodos()).ReturnsAsync(usuariosDoRepositorio);

        // Configuração do mock do mapper para mapear corretamente a lista de usuários para a lista de view models
        mapperMock.Setup(m => m.Map<IEnumerable<UsuarioViewModel>>(usuariosDoRepositorio)).Returns(usuariosViewModelEsperados);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<UsuarioViewModel>>(result);

        var resultadoComoLista = result.ToList();

        Assert.Equal(usuariosViewModelEsperados.Count, resultadoComoLista.Count);

        for (int i = 0; i < usuariosViewModelEsperados.Count; i++)
        {
            Assert.Equal(usuariosViewModelEsperados[i].Nome, resultadoComoLista[i].Nome);
            Assert.Equal(usuariosViewModelEsperados[i].Cpf, resultadoComoLista[i].Cpf);
            Assert.Equal(usuariosViewModelEsperados[i].Telefone, resultadoComoLista[i].Telefone);
            // Adicione mais verificações conforme necessário
        }
    }
}