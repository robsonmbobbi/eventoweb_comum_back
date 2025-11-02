using System;
using EventoWeb.Comum.Negocio;
using Xunit;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class EMailTeste
    {
        [Theory]
        [InlineData("joao@uol.com.br")]
        [InlineData("luix@viena.com")]
        [InlineData("joana.maria@123.cx.op")]
        public void CriarEMail_EnderecoValido_DeveCriarInstancia(string endereco)
        {
            // Act
            var email = new EMail(endereco);

            // Assert
            Assert.NotNull(email);
            Assert.Equal(endereco, email.Endereco);
        }

        [Fact]
        public void CriarEMail_EnderecoNulo_DeveLancarArgumentException()
        {
            // Arrange
            string endereco = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new EMail(endereco));
            Assert.Equal("O endereço de e-mail não pode estar em branco", exception.Message);
        }

        [Fact]
        public void CriarEMail_EnderecoVazio_DeveLancarArgumentException()
        {
            // Arrange
            var endereco = "";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new EMail(endereco));
            Assert.Equal("O endereço de e-mail não pode estar em branco", exception.Message);
        }

        [Fact]
        public void CriarEMail_EnderecoMuitoLongo_DeveLancarArgumentException()
        {
            // Arrange
            var endereco = new string('a', 301) + "@exemplo.com";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new EMail(endereco));
            Assert.Equal("O endereço de e-mail não pode ter mais de 300 caracteres", exception.Message);
        }

        [Theory]
        [InlineData("luix@viena")]
        [InlineData("joana.maria")]
        [InlineData("joana.maria@")]
        [InlineData("@joana.maria")]
        public void CriarEMail_EnderecoInvalido_DeveLancarArgumentException(string endereco)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new EMail(endereco));
            Assert.Equal("O endereço de e-mail informado não é válido", exception.Message);
        }
    }
}