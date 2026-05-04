using EventoWeb.Comum.Negocio.ObjetosValor;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class CidadeTeste
    {
        [Theory]
        [InlineData("São Paulo")]
        [InlineData("Rio de Janeiro")]
        [InlineData("Belo Horizonte")]
        [InlineData("Divinópolis")]
        [InlineData("Itabirito")]
        public void CriarCidadeValida_DeveFuncionar(string cidadeValida)
        {
            var cidade = new Cidade(cidadeValida);
            Assert.Equal(cidadeValida, cidade.Nome);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CriarCidadeVazia_DeveLancarArgumentException(string cidadeVazia)
        {
            Assert.Throws<ArgumentException>(() => new Cidade(cidadeVazia));
        }

        [Fact]
        public void CriarCidadeNull_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Cidade(null));
        }

        [Fact]
        public void CriarCidadeComComprimentoMaximo_DeveFuncionar()
        {
            var cidadeComMaximo = new string('A', 300);
            var cidade = new Cidade(cidadeComMaximo);
            Assert.Equal(cidadeComMaximo, cidade.Nome);
        }

        [Fact]
        public void CriarCidadeExcedendoComprimentoMaximo_DeveLancarArgumentException()
        {
            var cidadeExcedida = new string('A', 301);
            Assert.Throws<ArgumentException>(() => new Cidade(cidadeExcedida));
        }
    }
}
