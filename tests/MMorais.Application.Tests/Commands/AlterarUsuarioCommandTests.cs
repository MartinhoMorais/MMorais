using MMorais.Application.Usuarios.Commands;

namespace MMorais.Application.Tests.Commands
{
    public class AlterarUsuarioCommandTests
    {
        [Fact(DisplayName = "Alterar Usuário Command Válido")]
        [Trait("Usuário", "Alteracao - Usuário Command")]
        public void AlterarUsuarioCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var usuarioCommand = new AlterarUsuarioCommand(Guid.NewGuid(), "Usuário 01", "631.285.396-93", "(11)91111-1111", DateTime.Now, true);

            // Act
            var result = usuarioCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Alterar Usuário Command Inválido")]
        [Trait("Usuário", "Alteracao - Usuário Command")]
        public void AlterarUsuarioCommand_CommandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var usuarioCommand = new AlterarUsuarioCommand(Guid.Empty, "", "", "", DateTime.Now, true);

            // Act
            var result = usuarioCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AlterarUsuarioValidator.IdUsuarioErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AlterarUsuarioValidator.NomeErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AlterarUsuarioValidator.CpfErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AlterarUsuarioValidator.TelefoneErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));

        }
    }
}