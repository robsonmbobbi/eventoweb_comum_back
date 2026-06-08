using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class ArquivoBinarioTeste
    {
        [Fact]
        public void CriarArquivoBinarioComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var arquivo = new byte[100];
            for (int i = 0; i < 100; i++)
            {
                arquivo[i] = (byte)(i % 256);
            }
            var tipo = EnumTipoArquivoBinario.PDF;

            // Act
            var arquivoBinario = new ArquivoBinario(arquivo, tipo);

            // Assert
            Assert.NotNull(arquivoBinario);
            Assert.Equal(100, arquivoBinario.Arquivo.Length);
            Assert.Equal(EnumTipoArquivoBinario.PDF, arquivoBinario.Tipo);
        }

        [Fact]
        public void CriarArquivoBinarioComTipoImagemPNG_DeveDefinirCorretamente()
        {
            // Arrange
            var arquivo = TestFixtures.Arquivos.CriarImagemPNG();

            // Act & Assert
            Assert.Equal(EnumTipoArquivoBinario.ImagemPNG, arquivo.Tipo);
        }

        [Fact]
        public void CriarArquivoBinarioComTipoImagemJPEG_DeveDefinirCorretamente()
        {
            // Arrange
            var arquivo = TestFixtures.Arquivos.CriarImagemJPEG();

            // Act & Assert
            Assert.Equal(EnumTipoArquivoBinario.ImagemJPEG, arquivo.Tipo);
        }

        [Fact]
        public void CriarArquivoBinarioComArquivoNulo_DeveLancarExcecao()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => 
                new ArquivoBinario(null, EnumTipoArquivoBinario.PDF)
            );
        }

        [Fact]
        public void CriarArquivoBinarioComArquivoVazio_DeveLancarExcecao()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => 
                new ArquivoBinario(new byte[0], EnumTipoArquivoBinario.PDF)
            );
        }

        [Fact]
        public void AlterarArquivoValido_DeveAlterarPropriedade()
        {
            // Arrange
            var arquivoBinario = TestFixtures.Arquivos.CriarArquivoBinarioValido(100);
            var novoArquivo = new byte[50];

            // Act
            arquivoBinario.Arquivo = novoArquivo;

            // Assert
            Assert.Equal(50, arquivoBinario.Arquivo.Length);
        }

        [Fact]
        public void AtribuirArquivoNulo_DeveLancarExcecao()
        {
            // Arrange
            var arquivoBinario = TestFixtures.Arquivos.CriarArquivoBinarioValido();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                arquivoBinario.Arquivo = null
            );
        }

        [Fact]
        public void AtribuirArquivoVazio_DeveLancarExcecao()
        {
            // Arrange
            var arquivoBinario = TestFixtures.Arquivos.CriarArquivoBinarioValido();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                arquivoBinario.Arquivo = new byte[0]
            );
        }

        [Fact]
        public void AlterarTipoValido_DeveAlterarPropriedade()
        {
            // Arrange
            var arquivoBinario = TestFixtures.Arquivos.CriarArquivoBinarioValido();

            // Act
            arquivoBinario.Tipo = EnumTipoArquivoBinario.ImagemJPEG;

            // Assert
            Assert.Equal(EnumTipoArquivoBinario.ImagemJPEG, arquivoBinario.Tipo);
        }

        [Fact]
        public void CriarArquivoBinarioPDF_DeveDefinirTipoPDF()
        {
            // Arrange
            var arquivo = TestFixtures.Arquivos.CriarPDF();

            // Assert
            Assert.Equal(EnumTipoArquivoBinario.PDF, arquivo.Tipo);
        }
    }
}
