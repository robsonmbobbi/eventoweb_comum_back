using EventoWeb.Comum.Negocio.Entidades;

namespace EventoWeb.Comum.Testes.Negocio.Fixtures
{
    /// <summary>
    /// Fixtures para criação de dados de teste reutilizáveis relacionados a Arquivo
    /// </summary>
    public static class ArquivosFixtures
    {
        public static ArquivoBinario CriarArquivoBinarioValido(
            int tamanho = 100,
            EnumTipoArquivoBinario tipo = EnumTipoArquivoBinario.PDF)
        {
            var dados = new byte[tamanho];
            for (int i = 0; i < tamanho; i++)
            {
                dados[i] = (byte)(i % 256);
            }
            return new ArquivoBinario(dados, tipo);
        }

        public static ArquivoBinario CriarImagemPNG(int tamanho = 100)
        {
            return CriarArquivoBinarioValido(tamanho, EnumTipoArquivoBinario.ImagemPNG);
        }

        public static ArquivoBinario CriarImagemJPEG(int tamanho = 100)
        {
            return CriarArquivoBinarioValido(tamanho, EnumTipoArquivoBinario.ImagemJPEG);
        }

        public static ArquivoBinario CriarPDF(int tamanho = 100)
        {
            return CriarArquivoBinarioValido(tamanho, EnumTipoArquivoBinario.PDF);
        }
    }
}
