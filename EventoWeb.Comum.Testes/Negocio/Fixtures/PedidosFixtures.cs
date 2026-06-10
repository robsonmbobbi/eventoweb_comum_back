using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.EventosFixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.PessoasFixtures;
using static EventoWeb.Comum.Testes.Negocio.Fixtures.PrecosFixtures;

namespace EventoWeb.Comum.Testes.Negocio.Fixtures
{
    /// <summary>
    /// Fixtures para criação de dados de teste reutilizáveis relacionados a Pedido
    /// </summary>
    public static class PedidosFixtures
    {
        public static Pedido CriarPedidoDeDebitoValido(
            Pessoa? pagador = null,
            Evento? evento = null,
            Pessoa? pessoaInscricao = null)
        {
            pagador ??= CriarPessoaValida();
            evento ??= CriarEventoValido();
            pessoaInscricao ??= CriarAdulto();

            var inscricao = new InscricaoParticipante(evento, pessoaInscricao, DateTime.Now);

            var forma = CriarFormaPagamentoValida();

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
            pagador ??= CriarPessoaValida();
            evento ??= CriarEventoValido();
            pessoaInscricao ??= CriarAdulto();

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
            pagador ??= CriarPessoaValida();
            evento ??= CriarEventoValido();
            pessoaInscricao ??= CriarAdulto();

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
