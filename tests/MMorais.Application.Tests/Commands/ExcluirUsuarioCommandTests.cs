using MMorais.Application.Usuarios.Commands;

namespace MMorais.Application.Tests.Commands
{
    public class ExcluirUsuarioCommandTests
    {
        [Fact(DisplayName = "Excluir Usuário Command Válido")]
        [Trait("Usuário", "Exclusão - Usuário Command")]
        public void ExcluirUsuarioCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var usuarioCommand = new ExcluirUsuarioCommand(Guid.NewGuid());

            // Act
            var result = usuarioCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Excluir Usuário Command Inválido")]
        [Trait("Usuário", "Exclusão - Usuário Command")]
        public void ExcluirUsuarioCommand_CommandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var usuarioCommand = new ExcluirUsuarioCommand(Guid.Empty);

            // Act
            var result = usuarioCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(ExcluirUsuarioValidator.IdUsuarioErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}