using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Testes.Negocio.ObjetosValor
{
    public class TelefoneTeste
    {
        [Theory]
        [InlineData("12345678901")]
        [InlineData("3132299090")]
        [InlineData("11989093445")]
        public void CriarTelefone_NumeroValido_DeveCriarInstancia(string numero)
        {
            // Act
            var telefone = new Telefone(numero);

            // Assert
            Assert.NotNull(telefone);
            Assert.Equal(numero, telefone.Numero);
        }  

        [Theory]
        [InlineData("(11) 98765-4321")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("12345ABC678")]
        [InlineData("1234567890123")]
        [InlineData("1234")]
        [InlineData("313229909")]
        public void CriarTelefone_NumeroInvalido_DeveLancarArgumentException(string numero)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Telefone(numero));
        }                
    }
}