using MMorais.Application.Usuarios.Commands;

namespace MMorais.Application.Tests.Commands
{
    public class CadastrarUsuarioCommandTests
    {
        [Fact(DisplayName = "Cadastrar Usu�rio Command V�lido")]
        [Trait("Usu�rio", "Cadastro - Usu�rio Command")]
        public void CadastrarUsuarioCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var usuarioCommand = new CadastrarUsuarioCommand(Guid.NewGuid(), "Usu�rio 01", "631.285.396-93", "(11)91111-1111", true);

            // Act
            var result = usuarioCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Cadastrar Usu�rio Command Inv�lido")]
        [Trait("Usu�rio", "Cadastro - Usu�rio Command")]
        public void CadastrarUsuarioCommand_CommandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var usuarioCommand = new CadastrarUsuarioCommand(Guid.Empty, "", "", "", true);

            // Act
            var result = usuarioCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(CadastrarUsuarioValidator.IdUsuarioErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CadastrarUsuarioValidator.NomeErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CadastrarUsuarioValidator.CpfErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CadastrarUsuarioValidator.TelefoneErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));

        }
    }
}