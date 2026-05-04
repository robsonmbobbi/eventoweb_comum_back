using EventoWeb.Comum.Negocio.ObjetosValor;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class AlergiaAlimentarTeste
    {
        [Theory]
        [InlineData("Amendoim")]
        [InlineData("Lactose")]
        [InlineData("Glúten")]
        [InlineData("Frutos do mar")]
        [InlineData("Alérgico a leite e ovos")]
        public void CriarAlergiaAlimentarValida_DeveFuncionar(string alergiaValida)
        {
            var alergia = new AlergiaAlimentar(alergiaValida);
            Assert.Equal(alergiaValida, alergia.Descricao);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CriarAlergiaAlimentarVazia_DeveLancarArgumentException(string alergiaVazia)
        {
            Assert.Throws<ArgumentException>(() => new AlergiaAlimentar(alergiaVazia));
        }

        [Fact]
        public void CriarAlergiaAlimentarNull_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new AlergiaAlimentar(null));
        }

        [Fact]
        public void CriarAlergiaAlimentarComComprimentoMaximo_DeveFuncionar()
        {
            var alergiaComMaximo = new string('A', 100);
            var alergia = new AlergiaAlimentar(alergiaComMaximo);
            Assert.Equal(alergiaComMaximo, alergia.Descricao);
        }

        [Fact]
        public void CriarAlergiaAlimentarExcedendoComprimentoMaximo_DeveLancarArgumentException()
        {
            var alergiaExcedida = new string('A', 101);
            Assert.Throws<ArgumentException>(() => new AlergiaAlimentar(alergiaExcedida));
        }
    }
}
