namespace MMorais.Domain.Tests
{
    public class UsuarioTest
    {
        [Fact(DisplayName = "Novo Usuário Válido")]
        [Trait("Usuário", "Cadastro - Usuário Domain")]
        public void NovoUsuario_DeveEstarValido()
        {
            // Arrange
            var usuario = new Usuario("Usuário 01", "631.285.396-93", "(11)91111-1111", DateTime.Now, true);
            
            // Act
            var result = usuario.EhValido();

            // Assert 
            Assert.True(result);
        }

        [Fact(DisplayName = "Novo Usuário Inválido Sem Informar Nome")]
        [Trait("Usuário", "Cadastro - Usuário Domain")]
        public void NovoUsuario_Sem_Informar_Nome_DeveEstarInvalido()
        {
            // Arrange
            var usuario = new Usuario("", "631.285.396-93", "(11)91111-1111", DateTime.Now, true);

            // Act
            var result = usuario.EhValido();

            // Assert 
            Assert.False(result);
        }

        [Fact(DisplayName = "Novo Usuário Inválido Sem Informar Cpf")]
        [Trait("Usuário", "Cadastro - Usuário Domain")]
        public void NovoUsuario_Sem_Informar_Cpf_DeveEstarInvalido()
        {
            // Arrange
            var usuario = new Usuario("Usuário 02", "", "(11)91111-1111", DateTime.Now, true);

            // Act
            var result = usuario.EhValido();

            // Assert 
            Assert.False(result);
        }

    }
}