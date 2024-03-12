namespace MMorais.Domain.Tests
{
    public class UsuarioTest
    {
        [Fact(DisplayName = "Novo Usu�rio V�lido")]
        [Trait("Usu�rio", "Cadastro - Usu�rio Domain")]
        public void NovoUsuario_DeveEstarValido()
        {
            // Arrange
            var usuario = new Usuario("Usu�rio 01", "631.285.396-93", "(11)91111-1111", DateTime.Now, true);
            
            // Act
            var result = usuario.EhValido();

            // Assert 
            Assert.True(result);
        }

        [Fact(DisplayName = "Novo Usu�rio Inv�lido Sem Informar Nome")]
        [Trait("Usu�rio", "Cadastro - Usu�rio Domain")]
        public void NovoUsuario_Sem_Informar_Nome_DeveEstarInvalido()
        {
            // Arrange
            var usuario = new Usuario("", "631.285.396-93", "(11)91111-1111", DateTime.Now, true);

            // Act
            var result = usuario.EhValido();

            // Assert 
            Assert.False(result);
        }

        [Fact(DisplayName = "Novo Usu�rio Inv�lido Sem Informar Cpf")]
        [Trait("Usu�rio", "Cadastro - Usu�rio Domain")]
        public void NovoUsuario_Sem_Informar_Cpf_DeveEstarInvalido()
        {
            // Arrange
            var usuario = new Usuario("Usu�rio 02", "", "(11)91111-1111", DateTime.Now, true);

            // Act
            var result = usuario.EhValido();

            // Assert 
            Assert.False(result);
        }

    }
}