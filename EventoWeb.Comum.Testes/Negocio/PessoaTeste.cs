using EventoWeb.Comum.Negocio;
using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class PessoaTeste
    {
        [Fact]
        public void CriarSucesso()
        {
            var p = new Pessoa(
                new CPF("05506427654"),
                new ("Nome da Pessoa"),
                new EMail("joao@uol.com.br"),
                new Telefone("37991925134")
            );

            Assert.Equal("05506427654", p.CPF.Numero);
            Assert.Equal("Nome da Pessoa", p.Nome.Nome);
            Assert.Equal("joao@uol.com.br", p.Email.Endereco);
            Assert.Equal("37991925134", p.CelularWP.Numero);
        }
        
        [Fact]
        public void CriarPessoaComCPFNulo()
        {
            Assert.Throws<ArgumentNullException>(() => new Pessoa(
                null, // CPF inválido
                new ("Nome da Pessoa"),
                new EMail("joao@uol.com.br"),
                new Telefone("37991925134")
            ));
        }

        [Fact]
        public void CriarPessoaComNomeNulo()
        {
            Assert.Throws<ArgumentNullException>(() => new Pessoa(
                new CPF("05506427654"),
                null,
                new EMail("joao@uol.com.br"),
                new Telefone("37991925134")
            ));            
        }

        [Fact]
        public void CriarPessoaComEmailNulo()
        {
            Assert.Throws<ArgumentNullException>(() => new Pessoa(
                new CPF("05506427654"),
                new ("Nome da Pessoa"),
                null,
                new Telefone("37991925134")
            ));
        }

        [Fact]
        public void CriarPessoaComCelularNulo()
        {
            Assert.Throws<ArgumentNullException>(() => new Pessoa(
                new CPF("05506427654"),
                new ("Nome da Pessoa"),
                new EMail("joao@uol.com.br"),
                null // número inválido
            ));
        }

        [Fact]
        public void CriarPessoaComDataNascimentoNula_EhValida()
        {
            var pessoa = new Pessoa(
                new CPF("05506427654"),
                new ("Nome da Pessoa"),
                new EMail("joao@uol.com.br"),
                new Telefone("37991925134")
            );

            Assert.Null(pessoa.DataNascimento);
        }

        [Fact]
        public void VerificarIgualdadeEntrePessoas()
        {
            var pessoa1 = new Pessoa(
                new CPF("05506427654"),
                new ("Nome da Pessoa"),
                new EMail("joao@uol.com.br"),
                new Telefone("37991925134")
            );

            var pessoa2 = new Pessoa(
                new CPF("05506427654"),
                new ("Nome da Pessoa"),
                new EMail("joao@uol.com.br"),
                new Telefone("37991925134")
            );

            Assert.NotEqual(pessoa1, pessoa2);
        }

        [Fact]
        public void AtribuirUFValida_DeveFuncionar()
        {
            var pessoa = new Pessoa(
                new CPF("05506427654"),
                new ("Nome da Pessoa"),
                new EMail("joao@uol.com.br"),
                new Telefone("37991925134")
            );

            pessoa.UF = new UF("SP");
            Assert.NotNull(pessoa.UF);
            Assert.Equal("SP", pessoa.UF.Sigla);
        }

        [Fact]
        public void AtribuirCidadeValida_DeveFuncionar()
        {
            var pessoa = new Pessoa(
                new CPF("05506427654"),
                new ("Nome da Pessoa"),
                new EMail("joao@uol.com.br"),
                new Telefone("37991925134")
            );

            pessoa.Cidade = new Cidade("São Paulo");
            Assert.NotNull(pessoa.Cidade);
            Assert.Equal("São Paulo", pessoa.Cidade.Nome);
        }

        [Fact]
        public void AtribuirAlergiaAlimentarValida_DeveFuncionar()
        {
            var pessoa = new Pessoa(
                new CPF("05506427654"),
                new ("Nome da Pessoa"),
                new EMail("joao@uol.com.br"),
                new Telefone("37991925134")
            );

            pessoa.AlergiaAlimentos = new AlergiaAlimentar("Amendoim");
            Assert.NotNull(pessoa.AlergiaAlimentos);
            Assert.Equal("Amendoim", pessoa.AlergiaAlimentos.Descricao);
        }

        [Fact]
        public void UFNulaEhValida()
        {
            var pessoa = new Pessoa(
                new CPF("05506427654"),
                new ("Nome da Pessoa"),
                new EMail("joao@uol.com.br"),
                new Telefone("37991925134")
            );

            pessoa.UF = null;
            Assert.Null(pessoa.UF);
        }

        [Fact]
        public void CidadeNulaEhValida()
        {
            var pessoa = new Pessoa(
                new CPF("05506427654"),
                new ("Nome da Pessoa"),
                new EMail("joao@uol.com.br"),
                new Telefone("37991925134")
            );

            pessoa.Cidade = null;
            Assert.Null(pessoa.Cidade);
        }

        [Fact]
        public void AlergiaAlimentarNulaEhValida()
        {
            var pessoa = new Pessoa(
                new CPF("05506427654"),
                new ("Nome da Pessoa"),
                new EMail("joao@uol.com.br"),
                new Telefone("37991925134")
            );

            pessoa.AlergiaAlimentos = null;
            Assert.Null(pessoa.AlergiaAlimentos);
        }
    }
}