using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class String100Teste
    {
        [Theory]
        [InlineData("Amendoim")]
        [InlineData("Lactose")]
        [InlineData("Glúten")]
        [InlineData("Frutos do mar")]
        [InlineData("Alérgico a leite e ovos")]
        public void CriarString100Valida_DeveFuncionar(string valorValido)
        {
            var string100 = new String100(valorValido);
            Assert.Equal(valorValido, string100.Valor);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CriarString100Vazia_DeveLancarArgumentException(string valorVazio)
        {
            Assert.Throws<ArgumentException>(() => new String100(valorVazio));
        }

        [Fact]
        public void CriarString100Null_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new String100(null));
        }

        [Fact]
        public void CriarString100ComComprimentoMaximo_DeveFuncionar()
        {
            var string100ComMaximo = new string('A', 100);
            var string100 = new String100(string100ComMaximo);
            Assert.Equal(string100ComMaximo, string100.Valor);
        }

        [Fact]
        public void CriarString100ExcedendoComprimentoMaximo_DeveLancarArgumentException()
        {
            var string100Excedida = new string('A', 101);
            Assert.Throws<ArgumentException>(() => new String100(string100Excedida));
        }
    }
}
