using System;
using EventoWeb.Comum.Negocio;
using EventoWeb.Comum.Negocio.Entidades;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class NomeCompletoTeste
    {
        [Fact]
        public void CriarNome_NomeValido_DeveCriarInstancia()
        {
            // Arrange
            var nome = "João da Silva";

            // Act
            var completo = new NomeCompleto(nome);

            // Assert
            Assert.NotNull(completo);
            Assert.Equal(nome, completo.Nome);
        }

        [Fact]
        public void CriarNome_NomeNulo_DeveLancarArgumentException()
        {
            // Arrange
            string nome = null;

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new NomeCompleto(nome));

            // Assert
            Assert.Equal("nome", ex.ParamName);
            Assert.Contains("Nome não pode ser vazio ou nulo", ex.Message);
        }

        [Fact]
        public void CriarNome_NomeVazio_DeveLancarArgumentException()
        {
            // Arrange
            var nome = "";

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new NomeCompleto(nome));

            // Assert
            Assert.Equal("nome", ex.ParamName);
            Assert.Contains("Nome não pode ser vazio ou nulo", ex.Message);
        }

        [Fact]
        public void CriarNome_NomeApenasEspacos_DeveLancarArgumentException()
        {
            // Arrange
            var nome = "   ";

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new NomeCompleto(nome));

            // Assert
            Assert.Equal("nome", ex.ParamName);
            Assert.Contains("Nome não pode ser vazio ou nulo", ex.Message);
        }

        [Fact]
        public void CriarNome_NomeMuitoLongo_DeveLancarArgumentException()
        {
            // Arrange
            var nome = new string('a', 201);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new NomeCompleto(nome));

            // Assert
            Assert.Equal("nome", ex.ParamName);
            Assert.Contains("no máximo 200 caracteres", ex.Message);
        }
    }
}
