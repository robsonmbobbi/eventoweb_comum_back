using EventoWeb.Comum.Negocio;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class PessoaTeste
    {
        [Fact]
        public void CriarSucesso()
        {
            var p = new Pessoa(
                new CPF("05506427654"),
                EnumSexo.Masculino,
                new DataAniversario(new DateTime(1990, 5, 20)),
                "Nome da Pessoa",
                new EMail("joao@uol.com.br"),
                new Celular("37991925134")
            );

            Assert.Equal("05506427654", p.CPF.Numero);
            Assert.Equal(EnumSexo.Masculino, p.Masculino);
            Assert.Equal(new DateTime(1990, 5, 20), p.DataNascimento.Data);
            Assert.Equal("Nome da Pessoa", p.Nome);
            Assert.Equal("joao@uol.com.br", p.Email.Endereco);
            Assert.Equal("37991925134", p.CelularWP.Numero);
        }
        
        [Fact]
        public void CriarPessoaComCPFNulo()
        {
            Assert.Throws<ArgumentNullException>(() => new Pessoa(
                null, // CPF inválido
                EnumSexo.Masculino,
                new DataAniversario(new DateTime(1990, 5, 20)),
                "Nome da Pessoa",
                new EMail("joao@uol.com.br"),
                new Celular("37991925134")
            ));
        }

        [Fact]
        public void CriarPessoaComNomeVazioOuNulo()
        {
            Assert.Throws<ArgumentException>(() => new Pessoa(
                new CPF("05506427654"),
                EnumSexo.Masculino,
                new DataAniversario(new DateTime(1990, 5, 20)),
                "",
                new EMail("joao@uol.com.br"),
                new Celular("37991925134")
            ));

            Assert.Throws<ArgumentException>(() => new Pessoa(
                new CPF("05506427654"),
                EnumSexo.Masculino,
                new DataAniversario(new DateTime(1990, 5, 20)),
                null,
                new EMail("joao@uol.com.br"),
                new Celular("37991925134")
            ));            
        }

        [Fact]
        public void CriarPessoaComEmailNulo()
        {
            Assert.Throws<ArgumentNullException>(() => new Pessoa(
                new CPF("05506427654"),
                EnumSexo.Masculino,
                new DataAniversario(new DateTime(1990, 5, 20)),
                "Nome da Pessoa",
                null,
                new Celular("37991925134")
            ));
        }

        [Fact]
        public void CriarPessoaComCelularNulo()
        {
            Assert.Throws<ArgumentNullException>(() => new Pessoa(
                new CPF("05506427654"),
                EnumSexo.Masculino,
                new DataAniversario(new DateTime(1990, 5, 20)),
                "Nome da Pessoa",
                new EMail("joao@uol.com.br"),
                null // número inválido
            ));
        }

        [Fact]
        public void CriarPessoaComDataNascimentoNula()
        {
            Assert.Throws<ArgumentNullException>(() => new Pessoa(
                new CPF("05506427654"),
                EnumSexo.Masculino,
                null,
                "Nome da Pessoa",
                new EMail("joao@uol.com.br"),
                new Celular("37991925134")
            ));
        }

        [Fact]
        public void VerificarIgualdadeEntrePessoas()
        {
            var pessoa1 = new Pessoa(
                new CPF("05506427654"),
                EnumSexo.Masculino,
                new DataAniversario(new DateTime(1990, 5, 20)),
                "Nome da Pessoa",
                new EMail("joao@uol.com.br"),
                new Celular("37991925134")
            );

            var pessoa2 = new Pessoa(
                new CPF("05506427654"),
                EnumSexo.Masculino,
                new DataAniversario(new DateTime(1990, 5, 20)),
                "Nome da Pessoa",
                new EMail("joao@uol.com.br"),
                new Celular("37991925134")
            );

            Assert.NotEqual(pessoa1, pessoa2);
        }
    }
}