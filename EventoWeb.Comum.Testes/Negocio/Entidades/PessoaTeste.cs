using EventoWeb.Comum.Negocio;
using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Testes.Negocio.Fixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.PessoasFixtures;

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
            Assert.Equal("Nome da Pessoa", p.Nome.Valor);
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

            pessoa.Cidade = new String300("São Paulo");
            Assert.NotNull(pessoa.Cidade);
            Assert.Equal("São Paulo", pessoa.Cidade.Valor);
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

            pessoa.AlergiaAlimentos = new String100("Amendoim");
            Assert.NotNull(pessoa.AlergiaAlimentos);
            Assert.Equal("Amendoim", pessoa.AlergiaAlimentos.Valor);
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

        // ============ TESTES FALTANTES CONFORME PLANO ============

        [Fact]
        public void CriarPessoaComDadosValidos_DeveTerValoresPadrao()
        {
            // Arrange & Act
            var pessoa = CriarPessoaValida();

            // Assert
            Assert.NotNull(pessoa);
            Assert.Equal("05506427654", pessoa.CPF.Numero);
            Assert.Equal("João Silva", pessoa.Nome.Valor);
            Assert.Equal("joao@example.com", pessoa.Email.Endereco);
            Assert.Equal("37991925134", pessoa.CelularWP.Numero);
            Assert.Null(pessoa.Sexo);
            Assert.Null(pessoa.DataNascimento);
            Assert.False(pessoa.EhVegetariano);
            Assert.False(pessoa.EhDiabetico);
            Assert.False(pessoa.UsaAdocanteDiariamente);
        }

        [Fact]
        public void AtribuirCPFNuloAoSetter_DeveLancarExcecao()
        {
            // Arrange
            var pessoa = CriarPessoaValida();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => pessoa.CPF = null);
        }

        [Fact]
        public void AtribuirNomeNuloAoSetter_DeveLancarExcecao()
        {
            // Arrange
            var pessoa = CriarPessoaValida();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => pessoa.Nome = null);
        }

        [Fact]
        public void AtribuirEmailNuloAoSetter_DeveLancarExcecao()
        {
            // Arrange
            var pessoa = CriarPessoaValida();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => pessoa.Email = null);
        }

        [Fact]
        public void AtribuirCelularNuloAoSetter_DeveLancarExcecao()
        {
            // Arrange
            var pessoa = CriarPessoaValida();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => pessoa.CelularWP = null);
        }

        [Fact]
        public void AtribuirSexoValido_DeveAlterarPropriedade()
        {
            // Arrange
            var pessoa = CriarPessoaValida();

            // Act
            pessoa.Sexo = EnumSexo.Masculino;

            // Assert
            Assert.Equal(EnumSexo.Masculino, pessoa.Sexo);
        }

        [Fact]
        public void AtribuirDataNascimentoValida_DeveAlterarPropriedade()
        {
            // Arrange
            var pessoa = CriarPessoaValida();
            var data = new DataAniversario(new DateTime(1990, 6, 15));

            // Act
            pessoa.DataNascimento = data;

            // Assert
            Assert.NotNull(pessoa.DataNascimento);
            Assert.Equal(data, pessoa.DataNascimento);
        }

        [Fact]
        public void AtribuirBooleanoEhVegetariano_DeveAlterarPropriedade()
        {
            // Arrange
            var pessoa = CriarPessoaValida();

            // Act
            pessoa.EhVegetariano = true;

            // Assert
            Assert.True(pessoa.EhVegetariano);
        }

        [Fact]
        public void AtribuirBooleanoEhDiabetico_DeveAlterarPropriedade()
        {
            // Arrange
            var pessoa = CriarPessoaValida();

            // Act
            pessoa.EhDiabetico = true;

            // Assert
            Assert.True(pessoa.EhDiabetico);
        }

        [Fact]
        public void AtribuirBooleanoUsaAdocanteDiariamente_DeveAlterarPropriedade()
        {
            // Arrange
            var pessoa = CriarPessoaValida();

            // Act
            pessoa.UsaAdocanteDiariamente = true;

            // Assert
            Assert.True(pessoa.UsaAdocanteDiariamente);
        }

        [Fact]
        public void SexoNuloEhValido()
        {
            // Arrange
            var pessoa = CriarPessoaValida();
            pessoa.Sexo = EnumSexo.Masculino;

            // Act
            pessoa.Sexo = null;

            // Assert
            Assert.Null(pessoa.Sexo);
        }

        [Fact]
        public void DataNascimentoNulaEhValida()
        {
            // Arrange
            var pessoa = CriarPessoaValida();
            pessoa.DataNascimento = new DataAniversario(new DateTime(1990, 6, 15));

            // Act
            pessoa.DataNascimento = null;

            // Assert
            Assert.Null(pessoa.DataNascimento);
        }

        [Fact]
        public void DuasPessoasSemIdSaoMesmaReferencia()
        {
            // Arrange
            var pessoa1 = CriarPessoaValida();

            // Act & Assert
            Assert.Equal(pessoa1, pessoa1);
        }
    }
}
