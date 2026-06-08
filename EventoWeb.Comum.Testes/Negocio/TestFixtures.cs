using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Testes.Negocio
{
    /// <summary>
    /// Fixtures e Builders para criação de dados de teste reutilizáveis
    /// </summary>
    public static class TestFixtures
    {
        // ============ PESSOAS ============
        public static class Pessoas
        {
            public static Pessoa CriarPessoaValida()
            {
                return new Pessoa(
                    new CPF("05506427654"),
                    new String200("João Silva"),
                    new EMail("joao@example.com"),
                    new Telefone("37991925134")
                );
            }

            public static Pessoa CriarPessoaComNome(string nome)
            {
                return new Pessoa(
                    new CPF("05506427654"),
                    new String200(nome),
                    new EMail("joao@example.com"),
                    new Telefone("37991925134")
                );
            }

            public static Pessoa CriarPessoaComDataNascimento(int dia, int mes, int ano)
            {
                var pessoa = CriarPessoaValida();
                pessoa.DataNascimento = new DataAniversario(new DateTime(ano, mes, dia));
                return pessoa;
            }

            public static Pessoa CriarPessoaComIdade(int idade)
            {
                var dataNascimento = DateTime.Now.AddYears(-idade);
                var pessoa = CriarPessoaValida();
                pessoa.DataNascimento = new DataAniversario(dataNascimento);
                return pessoa;
            }

            public static Pessoa CriarCrianca(int idade = 10)
            {
                return CriarPessoaComIdade(idade);
            }

            public static Pessoa CriarAdulto(int idade = 25)
            {
                return CriarPessoaComIdade(idade);
            }
        }

        // ============ EVENTOS ============
        public static class Eventos
        {
            public static Evento CriarEventoValido()
            {
                var periodoInscricao = new Periodo(
                    DateTime.Now,
                    DateTime.Now.AddDays(30)
                );
                var periodoRealizacao = new Periodo(
                    DateTime.Now.AddDays(31),
                    DateTime.Now.AddDays(35)
                );

                return new Evento(
                    new String200("Congresso de Espiritismo 2024"),
                    periodoInscricao,
                    periodoRealizacao
                );
            }

            public static Evento CriarEventoComNome(string nome)
            {
                var periodoInscricao = new Periodo(
                    DateTime.Now,
                    DateTime.Now.AddDays(30)
                );
                var periodoRealizacao = new Periodo(
                    DateTime.Now.AddDays(31),
                    DateTime.Now.AddDays(35)
                );

                return new Evento(
                    new String200(nome),
                    periodoInscricao,
                    periodoRealizacao
                );
            }

            public static Evento CriarEventoComIdadeMinima(int idadeMinima)
            {
                var evento = CriarEventoValido();
                evento.IdadeMinimaInscricaoAdulto = new InteiroPositivo(idadeMinima);
                return evento;
            }
        }

        // ============ INSCRIÇÕES ============
        public static class Inscricoes
        {
            public static InscricaoParticipante CriarInscricaoParticipanteValida(
                Evento? evento = null,
                Pessoa? pessoa = null)
            {
                evento ??= Eventos.CriarEventoValido();
                pessoa ??= Pessoas.CriarAdulto();

                return new InscricaoParticipante(
                    evento,
                    pessoa,
                    DateTime.Now
                );
            }

            public static InscricaoParticipante CriarInscricaoParticipanteEmPendente(
                Evento? evento = null,
                Pessoa? pessoa = null)
            {
                var inscricao = CriarInscricaoParticipanteValida(evento, pessoa);
                inscricao.TornarPendente();
                return inscricao;
            }

            public static InscricaoParticipante CriarInscricaoParticipanteAceita(
                Evento? evento = null,
                Pessoa? pessoa = null)
            {
                var inscricao = CriarInscricaoParticipanteEmPendente(evento, pessoa);
                inscricao.Tipo = EnumTipoParticipante.Participante;
                inscricao.Aceitar();
                return inscricao;
            }
        }

        // ============ ARQUIVOS ============
        public static class Arquivos
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

        // ============ PREÇOS ============
        public static class Precos
        {
            public static PrecoInscricao CriarPrecoInscricaoValido(Evento? evento = null)
            {
                evento ??= Eventos.CriarEventoValido();
                return new PrecoInscricao(evento, new InteiroPositivo(17));
            }

            public static FormaPagamento CriarFormaPagamentoValida(
                string nome = "Cartão de Crédito",
                EnumTipoPagamento tipo = EnumTipoPagamento.CartaoCredito)
            {
                return new FormaPagamento(
                    new String200(nome),
                    tipo
                );
            }

            public static FormaPagamento CriarFormaPagamentoComParcelamento(
                int minParcelas = 1,
                int maxParcelas = 12)
            {
                var forma = CriarFormaPagamentoValida();
                forma.DefinirParcelas(new IntervaloInteiroPositivo(minParcelas, maxParcelas));
                return forma;
            }
        }

        // ============ FINANCEIRO ============
        public static class Financeiro
        {
            public static ContaBancaria CriarContaBancariaValida(string nome = "Banco Brasil - Corrente")
            {
                return new ContaBancaria(new String200(nome));
            }

            public static Conta CriarContaValida(
                Pessoa? pessoa = null,
                EnumTipoTransacao tipo = EnumTipoTransacao.Receita,
                decimal valor = 500.00m,
                DateTime? dataVencimento = null)
            {
                pessoa ??= Pessoas.CriarPessoaValida();
                dataVencimento ??= DateTime.Now.AddDays(30);

                return new Conta(
                    pessoa,
                    tipo,
                    new ValorMonetario(valor),
                    dataVencimento.Value
                );
            }

            public static Transacao CriarTransacaoValida(
                ContaBancaria? contaBancaria = null,
                EnumTipoTransacao tipo = EnumTipoTransacao.Receita,
                decimal valor = 300.00m,
                string? descricao = null)
            {
                contaBancaria ??= CriarContaBancariaValida();
                descricao ??= "Pagamento recebido";

                return new Transacao(
                    tipo,
                    contaBancaria,
                    DateTime.Now,
                    new ValorMonetario(valor),
                    new String200(descricao)
                );
            }

            public static TransacaoConta CriarTransacaoContaValida(
                Conta? conta = null,
                decimal valorTransacao = 400.00m,
                decimal? multa = null,
                decimal? juros = null,
                decimal? desconto = null)
            {
                conta ??= CriarContaValida();

                return new TransacaoConta(
                    CriarContaBancariaValida(),
                    conta,
                    DateTime.Now,
                    new ValorMonetario(valorTransacao),
                    multa.HasValue ? new ValorMonetario(multa.Value) : null,
                    juros.HasValue ? new ValorMonetario(juros.Value) : null,
                    desconto.HasValue ? new ValorMonetario(desconto.Value) : null
                );
            }
        }

        // ============ PEDIDOS ============
        public static class Pedidos
        {
            public static Pedido CriarPedidoDeDebitoValido(
                Pessoa? pagador = null,
                Evento? evento = null,
                Pessoa? pessoaInscricao = null)
            {
                pagador ??= Pessoas.CriarPessoaValida();
                evento ??= Eventos.CriarEventoValido();
                pessoaInscricao ??= Pessoas.CriarAdulto();

                var inscricao = new InscricaoParticipante(evento, pessoaInscricao, DateTime.Now);

                var forma = Precos.CriarFormaPagamentoValida();

                return new Pedido(
                    pagador,
                    new[] { inscricao },
                    new ValorMonetario(100.00m),
                    EnumTipoPedido.Debito,
                    forma,
                    null
                );
            }

            public static Pedido CriarPedidoDeDescontoValido(
                Pessoa? pagador = null,
                Evento? evento = null,
                Pessoa? pessoaInscricao = null)
            {
                pagador ??= Pessoas.CriarPessoaValida();
                evento ??= Eventos.CriarEventoValido();
                pessoaInscricao ??= Pessoas.CriarAdulto();

                var inscricao = new InscricaoParticipante(evento, pessoaInscricao, DateTime.Now);

                return new Pedido(
                    pagador,
                    new[] { inscricao },
                    new ValorMonetario(100.00m),
                    EnumTipoPedido.Desconto,
                    null,
                    null
                );
            }

            public static Pedido CriarPedidoDeIsencaoValido(
                Pessoa? pagador = null,
                Evento? evento = null,
                Pessoa? pessoaInscricao = null)
            {
                pagador ??= Pessoas.CriarPessoaValida();
                evento ??= Eventos.CriarEventoValido();
                pessoaInscricao ??= Pessoas.CriarAdulto();

                var inscricao = new InscricaoParticipante(evento, pessoaInscricao, DateTime.Now);

                return new Pedido(
                    pagador,
                    new[] { inscricao },
                    new ValorMonetario(0.00m),
                    EnumTipoPedido.Isencao,
                    null,
                    null
                );
            }
        }
    }
}
