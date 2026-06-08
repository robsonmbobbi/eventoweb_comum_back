using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Testes.Negocio
{
    public class InscricaoInfantilTeste
    {
        [Fact]
        public void CriarInscricaoInfantilComDadosValidos_DeveDefinirPropriedadesCorretas()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoComIdadeMinima(13);
            var crianca = TestFixtures.Pessoas.CriarCrianca(idade: 10);
            var pessoaResponsavel1 = TestFixtures.Pessoas.CriarAdulto();
            var pessoaResponsavel2 = TestFixtures.Pessoas.CriarAdulto();
            var responsavel1 = new InscricaoParticipante(evento, pessoaResponsavel1, DateTime.Now);
            var responsavel2 = new InscricaoParticipante(evento, pessoaResponsavel2, DateTime.Now);

            // Act
            var inscricao = new InscricaoInfantil(
                crianca,
                evento,
                responsavel1,
                responsavel2,
                DateTime.Now,
                true
            );

            // Assert
            Assert.NotNull(inscricao);
            Assert.Equal(crianca, inscricao.Pessoa);
            Assert.Equal(evento, inscricao.Evento);
            Assert.Equal(EnumSituacaoInscricao.Limbo, inscricao.Situacao);
            Assert.True(inscricao.DormeEvento);
            Assert.Equal(responsavel1, inscricao.InscricaoResponsavel1);
            Assert.Equal(responsavel2, inscricao.InscricaoResponsavel2);
        }

        [Fact]
        public void CriarInscricaoInfantilComIdadeSuperiorAoMinimo_DeveLancarExcecao()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoComIdadeMinima(13);
            var pessoa = TestFixtures.Pessoas.CriarPessoaComIdade(13); // Não é criança
            var responsavel1 = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida(evento);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                new InscricaoInfantil(pessoa, evento, responsavel1, null, DateTime.Now, false)
            );
        }

        [Fact]
        public void CriarInscricaoInfantilComResponsaveisDeEventosDiferentes_DeveLancarExcecao()
        {
            // Arrange
            var evento1 = TestFixtures.Eventos.CriarEventoValido();
            var evento2 = TestFixtures.Eventos.CriarEventoValido();
            var crianca = TestFixtures.Pessoas.CriarCrianca();
            var responsavel1 = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida(evento1);
            var responsavel2 = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida(evento2);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                new InscricaoInfantil(crianca, evento1, responsavel1, responsavel2, DateTime.Now, false)
            );
        }

        [Fact]
        public void CriarInscricaoInfantilComResponsaveisDuplicados_DeveLancarExcecao()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var crianca = TestFixtures.Pessoas.CriarCrianca();
            var responsavel = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida(evento);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                new InscricaoInfantil(crianca, evento, responsavel, responsavel, DateTime.Now, false)
            );
        }

        [Fact]
        public void CriarInscricaoInfantilComUmResponsavelApenasQuandoNaoDorme_DeveFuncionar()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var crianca = TestFixtures.Pessoas.CriarCrianca();
            var responsavel = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida(evento);
            responsavel.DormeEvento = false;

            // Act
            var inscricao = new InscricaoInfantil(
                crianca,
                evento,
                responsavel,
                null,
                DateTime.Now,
                false
            );

            // Assert
            Assert.NotNull(inscricao);
            Assert.Equal(responsavel, inscricao.InscricaoResponsavel1);
            Assert.Null(inscricao.InscricaoResponsavel2);
        }

        [Fact]
        public void CriarInscricaoInfantilQueDormeComResponsaveisSemDormirDeveLancarExcecao()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var crianca = TestFixtures.Pessoas.CriarCrianca();
            var pessoaResponsavel1 = TestFixtures.Pessoas.CriarAdulto();
            var pessoaResponsavel2 = TestFixtures.Pessoas.CriarAdulto();
            var responsavel1 = new InscricaoParticipante(evento, pessoaResponsavel1, DateTime.Now);
            var responsavel2 = new InscricaoParticipante(evento, pessoaResponsavel2, DateTime.Now);

            responsavel1.DormeEvento = false;
            responsavel2.DormeEvento = false;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                new InscricaoInfantil(crianca, evento, responsavel1, responsavel2, DateTime.Now, true)
            );
        }

        [Fact]
        public void CriarInscricaoInfantilQueDormeComPeloMenosUmResponsavelDormindo_DeveFuncionar()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var crianca = TestFixtures.Pessoas.CriarCrianca();
            var pessoaResponsavel1 = TestFixtures.Pessoas.CriarAdulto();
            var pessoaResponsavel2 = TestFixtures.Pessoas.CriarAdulto();
            var responsavel1 = new InscricaoParticipante(evento, pessoaResponsavel1, DateTime.Now);
            var responsavel2 = new InscricaoParticipante(evento, pessoaResponsavel2, DateTime.Now);

            responsavel1.DormeEvento = true;
            responsavel2.DormeEvento = false;

            // Act
            var inscricao = new InscricaoInfantil(
                crianca,
                evento,
                responsavel1,
                responsavel2,
                DateTime.Now,
                true
            );

            // Assert
            Assert.NotNull(inscricao);
            Assert.True(inscricao.DormeEvento);
        }

        [Fact]
        public void AceitarInscricaoInfantilComResponsaveisPendentes_DeveLancarExcecao()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var crianca = TestFixtures.Pessoas.CriarCrianca();
            var pessoaResponsavel1 = TestFixtures.Pessoas.CriarAdulto();
            var pessoaResponsavel2 = TestFixtures.Pessoas.CriarAdulto();
            var responsavel1 = new InscricaoParticipante(evento, pessoaResponsavel1, DateTime.Now);
            var responsavel2 = new InscricaoParticipante(evento, pessoaResponsavel2, DateTime.Now);

            var inscricao = new InscricaoInfantil(
                crianca,
                evento,
                responsavel1,
                responsavel2,
                DateTime.Now,
                false
            );

            inscricao.TornarPendente();

            // Act & Assert
            Assert.Throws<Exception>(() => inscricao.Aceitar());
        }

        [Fact]
        public void AceitarInscricaoInfantilComResponsavelPrincipalAceito_DeveFuncionar()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var crianca = TestFixtures.Pessoas.CriarCrianca();
            var responsavel1 = TestFixtures.Inscricoes.CriarInscricaoParticipanteAceita(evento);
            var responsavel2 = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida(evento);

            var inscricao = new InscricaoInfantil(
                crianca,
                evento,
                responsavel1,
                responsavel2,
                DateTime.Now,
                false
            );

            inscricao.TornarPendente();

            // Act
            inscricao.Aceitar();

            // Assert
            Assert.Equal(EnumSituacaoInscricao.Aceita, inscricao.Situacao);
        }

        [Fact]
        public void AceitarInscricaoInfantilComResponsavelSecundarioAceito_DeveFuncionar()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var crianca = TestFixtures.Pessoas.CriarCrianca();
            var responsavel1 = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida(evento);
            var responsavel2 = TestFixtures.Inscricoes.CriarInscricaoParticipanteAceita(evento);

            var inscricao = new InscricaoInfantil(
                crianca,
                evento,
                responsavel1,
                responsavel2,
                DateTime.Now,
                false
            );

            inscricao.TornarPendente();

            // Act
            inscricao.Aceitar();

            // Assert
            Assert.Equal(EnumSituacaoInscricao.Aceita, inscricao.Situacao);
        }

        [Fact]
        public void ReatribuirResponsaveisValidos_DeveAlterarResponsaveis()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var crianca = TestFixtures.Pessoas.CriarCrianca();
            var pessoaResponsavel1 = TestFixtures.Pessoas.CriarAdulto();
            var pessoaResponsavel2 = TestFixtures.Pessoas.CriarAdulto();
            var responsavel1 = new InscricaoParticipante(evento, pessoaResponsavel1, DateTime.Now);
            var responsavel2 = new InscricaoParticipante(evento, pessoaResponsavel2, DateTime.Now);

            var inscricao = new InscricaoInfantil(
                crianca,
                evento,
                responsavel1,
                responsavel2,
                DateTime.Now,
                false
            );

            var pessoaNova1 = TestFixtures.Pessoas.CriarAdulto();
            var pessoaNova2 = TestFixtures.Pessoas.CriarAdulto();
            var novoResponsavel1 = new InscricaoParticipante(evento, pessoaNova1, DateTime.Now);
            var novoResponsavel2 = new InscricaoParticipante(evento, pessoaNova2, DateTime.Now);

            // Act
            inscricao.AtribuirResponsaveis(novoResponsavel1, novoResponsavel2);

            // Assert
            Assert.Equal(novoResponsavel1, inscricao.InscricaoResponsavel1);
            Assert.Equal(novoResponsavel2, inscricao.InscricaoResponsavel2);
        }

        [Fact]
        public void ReatribuirComResponsaveisDuplicados_DeveLancarExcecao()
        {
            // Arrange
            var evento = TestFixtures.Eventos.CriarEventoValido();
            var crianca = TestFixtures.Pessoas.CriarCrianca();
            var responsavel = TestFixtures.Inscricoes.CriarInscricaoParticipanteValida(evento);

            var inscricao = new InscricaoInfantil(
                crianca,
                evento,
                responsavel,
                null,
                DateTime.Now,
                false
            );

            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                inscricao.AtribuirResponsaveis(responsavel, responsavel)
            );
        }
    }
}
