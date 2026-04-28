using EventoWeb.Comum.Negocio;
using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class CPFTeste
    {
        [Theory]
        [InlineData("57213951092")]
        [InlineData("12345678909")]
        [InlineData("46214790083")]
        public void CriarCPFValido_DeveFuncionar(string cpfValido)
        {
            var cpf = new CPF(cpfValido);
            Assert.Equal(cpfValido, cpf.Numero);
        }

        [Theory]
        [InlineData("")]
        [InlineData("123")]
        [InlineData("abcdefghijk")]
        [InlineData("11111111111")]
        [InlineData("1234567890011")]
        [InlineData("12345678900")]
        [InlineData("123.456.789-09")]
        public void CriarCPFInvalido_DeveLancarArgumentException(string cpfInvalido)
        {
            Assert.Throws<ArgumentException>(() => new CPF(cpfInvalido));
        }

        [Fact]
        public void CriarCPFNull_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new CPF(null));
        }
    }
}
