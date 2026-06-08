using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class InscricaoParticipanteTeste
    {
        [Fact]
        public void CriarInscricaoParticipanteComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var pessoa = TestFixtures.Pessoas.CriarAdulto();

            // Act
            var inscricao = new InscricaoParticipante(evento, pessoa, DateTime.Now);

            // Assert
            Assert.NotNull(inscricao);
            Assert.Equal(pessoa, inscricao.Pessoa);
            Assert.Equal(evento, inscricao.Evento);
            Assert.Equal(EnumSituacaoInscricao.Limbo, inscricao.Situacao);
            Assert.False(inscricao.ConfirmadoNoEvento);
            Assert.True(inscricao.DormeEvento);
            Assert.Null(inscricao.NomeCracha);
            Assert.Null(inscricao.Observacoes);
        }

        [Fact]
        public void CriarInscricaoParticipanteComPessoaNula_DeveLancarExcecao()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new InscricaoParticipante(evento, null, DateTime.Now)
            );
        }

        [Fact]
        public void CriarInscricaoParticipanteComEventoNulo_DeveLancarExcecao()
        {
            // Arrange
            var pessoa = TestFixtures.Pessoas.CriarAdulto();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new InscricaoParticipante(null, pessoa, DateTime.Now)
            );
        }

        [Fact]
        public void CriarInscricaoParticipanteComIdadeInsuficiente_DeveLancarExcecao()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoComIdadeMinima(13);
            var pessoa = TestFixtures.Pessoas.CriarCrianca(idade: 12);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                new InscricaoParticipante(evento, pessoa, DateTime.Now)
            );
        }

        [Fact]
        public void CriarInscricaoParticipanteComIdadeIgualAoMinimo_DeveFuncionar()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoComIdadeMinima(13);
            var pessoa = TestFixtures.Pessoas.CriarPessoaComIdade(13);

            // Act
            var inscricao = new InscricaoParticipante(evento, pessoa, DateTime.Now);

            // Assert
            Assert.NotNull(inscricao);
            Assert.Equal(pessoa, inscricao.Pessoa);
        }

        [Fact]
        public void CriarInscricaoParticipanteComIdadeSuperiorAoMinimo_DeveFuncionar()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoComIdadeMinima(13);
            var pessoa = TestFixtures.Pessoas.CriarPessoaComIdade(25);

            // Act
            var inscricao = new InscricaoParticipante(evento, pessoa, DateTime.Now);

            // Assert
            Assert.NotNull(inscricao);
        }

        [Fact]
        public void AtribuirTipoParticipanteValido_DeveAlterarPropriedade()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida();

            // Act
            inscricao.Tipo = EnumTipoParticipante.Participante;

            // Assert
            Assert.Equal(EnumTipoParticipante.Participante, inscricao.Tipo);
        }

        [Fact]
        public void AtribuirInstituicoesEspiritasFrequenta_DeveAlterarPropriedade()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida();
            var instituicoes = new String300("Instituição A");

            // Act
            inscricao.InstituicoesEspiritasFrequenta = instituicoes;

            // Assert
            Assert.Equal("Instituição A", inscricao.InstituicoesEspiritasFrequenta.Valor);
        }

        [Fact]
        public void InstituicoesEspiritasFrequentaNuloEhValido()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida();
            inscricao.InstituicoesEspiritasFrequenta = new String300("Instituição A");

            // Act
            inscricao.InstituicoesEspiritasFrequenta = null;

            // Assert
            Assert.Null(inscricao.InstituicoesEspiritasFrequenta);
        }

        [Fact]
        public void AtribuirNomeCracha_DeveAlterarPropriedade()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida();
            var nomeCracha = new String200("João da Silva");

            // Act
            inscricao.NomeCracha = nomeCracha;

            // Assert
            Assert.Equal("João da Silva", inscricao.NomeCracha.Valor);
        }

        [Fact]
        public void AtribuirObservacoes_DeveAlterarPropriedade()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida();
            var observacoes = new StringClob("Observação de teste");

            // Act
            inscricao.Observacoes = observacoes;

            // Assert
            Assert.NotNull(inscricao.Observacoes);
        }

        [Fact]
        public void AtribuirDormeEvento_DeveAlterarPropriedade()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida();

            // Act
            inscricao.DormeEvento = false;

            // Assert
            Assert.False(inscricao.DormeEvento);
        }

        [Fact]
        public void AtribuirConfirmadoNoEvento_DeveAlterarPropriedade()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida();

            // Act
            inscricao.ConfirmadoNoEvento = true;

            // Assert
            Assert.True(inscricao.ConfirmadoNoEvento);
        }

        [Fact]
        public void TornarInscricaoPendente_DeveAlterarSituacao()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida();
            Assert.Equal(EnumSituacaoInscricao.Limbo, inscricao.Situacao);

            // Act
            inscricao.TornarPendente();

            // Assert
            Assert.Equal(EnumSituacaoInscricao.Pendente, inscricao.Situacao);
        }

        [Fact]
        public void TornarPendenteAPartirDeNaoLimbo_DeveLancarExcecao()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteEmPendente();

            // Act & Assert
            Assert.Throws<Exception>(() => inscricao.TornarPendente());
        }

        [Fact]
        public void AceitarInscricao_DeveAlterarSituacao()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteEmPendente();
            inscricao.Tipo = EnumTipoParticipante.Participante;

            // Act
            inscricao.Aceitar();

            // Assert
            Assert.Equal(EnumSituacaoInscricao.Aceita, inscricao.Situacao);
        }

        [Fact]
        public void AceitarSemDefinirTipo_DeveLancarExcecao()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteEmPendente();
            // Tipo não foi definido, permanece null

            // Act & Assert
            Assert.Throws<ArgumentException>(() => inscricao.Aceitar());
        }

        [Fact]
        public void AceitarAPartirDeNaoPendente_DeveLancarExcecao()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida();
            // Permanece em Limbo

            // Act & Assert
            Assert.Throws<Exception>(() => inscricao.Aceitar());
        }

        [Fact]
        public void RejeitarInscricao_DeveAlterarSituacao()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteEmPendente();

            // Act
            inscricao.Rejeitar();

            // Assert
            Assert.Equal(EnumSituacaoInscricao.Rejeitada, inscricao.Situacao);
        }

        [Fact]
        public void RejeitarAPartirDeNaoPendente_DeveLancarExcecao()
        {
            // Arrange
            var inscricao = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida();
            // Permanece em Limbo

            // Act & Assert
            Assert.Throws<Exception>(() => inscricao.Rejeitar());
        }
    }
}
