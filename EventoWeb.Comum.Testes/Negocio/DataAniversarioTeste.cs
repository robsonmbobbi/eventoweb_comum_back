using EventoWeb.Comum.Negocio;
using EventoWeb.Comum.Negocio.Entidades;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class DataAniversarioTeste
    {
        [Fact]
        public void CriarDataValida_DeveFuncionar()
        {
            var data = new DateTime(1990, 5, 20);
            var aniversario = new DataAniversario(data);
            Assert.Equal(data, aniversario.Data);
        }

        [Fact]
        public void CriarDataNoFuturo_DeveLancarArgumentException()
        {
            var dataFutura = DateTime.Now.AddDays(1);
            Assert.Throws<ArgumentException>(() => new DataAniversario(dataFutura));
        }

        [Theory]
        [InlineData("2000-01-01", "2020-01-01", 20)]
        [InlineData("2000-05-20", "2020-05-19", 19)]
        [InlineData("2000-05-20", "2020-05-20", 20)]
        [InlineData("2000-12-31", "2021-01-01", 20)]
        public void CalcularIdadeEmAnos_DeveRetornarCorreto(string nascimento, string atual, int idadeEsperada)
        {
            var dataNasc = DateTime.Parse(nascimento);
            var dataAtual = DateTime.Parse(atual);
            var aniversario = new DataAniversario(dataNasc);
            var idade = aniversario.CalcularIdadeEmAnos(dataAtual);
            Assert.Equal(idadeEsperada, idade);
        }

        [Fact]
        public void CalcularIdadeEmAnos_DataAtualMenorQueNascimento_DeveLancarArgumentException()
        {
            var aniversario = new DataAniversario(new DateTime(2000, 1, 1));
            var dataAtual = new DateTime(1999, 12, 31);
            Assert.Throws<ArgumentException>(() => aniversario.CalcularIdadeEmAnos(dataAtual));
        }
    }
}
