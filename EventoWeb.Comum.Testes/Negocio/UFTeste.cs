using EventoWeb.Comum.Negocio.ObjetosValor;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class UFTeste
    {
        [Theory]
        [InlineData("AC")]
        [InlineData("AL")]
        [InlineData("AP")]
        [InlineData("AM")]
        [InlineData("BA")]
        [InlineData("CE")]
        [InlineData("DF")]
        [InlineData("ES")]
        [InlineData("GO")]
        [InlineData("MA")]
        [InlineData("MT")]
        [InlineData("MS")]
        [InlineData("MG")]
        [InlineData("PA")]
        [InlineData("PB")]
        [InlineData("PR")]
        [InlineData("PE")]
        [InlineData("PI")]
        [InlineData("RJ")]
        [InlineData("RN")]
        [InlineData("RS")]
        [InlineData("RO")]
        [InlineData("RR")]
        [InlineData("SC")]
        [InlineData("SP")]
        [InlineData("SE")]
        [InlineData("TO")]
        public void CriarUFValida_DeveFuncionar(string ufValida)
        {
            var uf = new UF(ufValida);
            Assert.Equal(ufValida, uf.Sigla);
        }

        [Theory]
        [InlineData("XX")]
        [InlineData("AB")]
        [InlineData("ZZ")]
        [InlineData("SP1")]
        [InlineData("S")]
        [InlineData("")]
        public void CriarUFInvalida_DeveLancarArgumentException(string ufInvalida)
        {
            Assert.Throws<ArgumentException>(() => new UF(ufInvalida));
        }

        [Fact]
        public void CriarUFNull_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new UF(null));
        }

        [Fact]
        public void CriarUFComLetraMiuscula_DeveConvertedParaMaiuscula()
        {
            var uf = new UF("sp");
            Assert.Equal("SP", uf.Sigla);
        }

        [Fact]
        public void CriarUFComEspacosBranco_DeveNormalizarEFuncionar()
        {
            var uf = new UF(" SP ");
            Assert.Equal("SP", uf.Sigla);
        }
    }
}
