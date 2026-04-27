using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.Entidades.IntegracaoFinanceira;
using EventoWeb.Comum.Negocio.Repositorios;
using EventoWeb.Comum.Negocio.Servicos.Notificacoes.RegistrosIntegracao;

namespace EventoWeb.Comum.Negocio.Servicos.IntegracaoFinanceira
{
    public class SrvAtualizacaoCobranca
    {
        private readonly IPersistencia<Conta> m_Contas;
        private readonly IRegistrosIntegracoesFinanceiras m_RegistrosIntegracao;
        private readonly SrvNotificacaoCobrancaRecebida m_SrvNotificacaoCobrancaRecebida;
        private readonly IDictionary<EnumIntegracaoExterna, IIntegracaoExterna> m_IntegracoesExternas;

        public SrvAtualizacaoCobranca(
            IDictionary<EnumIntegracaoExterna, IIntegracaoExterna> integracoesExternas, 
            IRegistrosIntegracoesFinanceiras registrosIntegracao, 
            IPersistencia<Conta> contas,
            SrvNotificacaoCobrancaRecebida srvNotificacaoCobrancaRecebida)
        {
            m_IntegracoesExternas = integracoesExternas;
            m_Contas = contas;
            m_RegistrosIntegracao = registrosIntegracao;
            m_SrvNotificacaoCobrancaRecebida = srvNotificacaoCobrancaRecebida;
        }


        public async Task Atualizar(RegistroIntegracaoFinanceira registro)
        {
            if (registro.Situacao == EnumSituacaoIntegracao.Concluido || registro.Situacao == EnumSituacaoIntegracao.Abortado)
                throw new InvalidOperationException("Não é possível atualizar um registro que já foi concluído ou abortado.");

            var integradorExterno = m_IntegracoesExternas[registro.Integrador.IntegracaoExterna];

            var resultadoConsulta = await integradorExterno.ConsultarCobranca(registro.Integrador, registro.IdentificacaoNoIntegrador) ?? 
                throw new InvalidOperationException($"Identificação não encontrada no integrador. Id {registro.IdentificacaoNoIntegrador}");
            switch(resultadoConsulta.Status)
            {
                case EnumStatusTransacao.Recebida:
                    registro
                        .Conta.AdicionarTransacao(
                            registro.Integrador.ContaBancaria,
                            DateTime.Now,
                            registro.Valor
                        );
                    //m_Contas.Atualizar(registro.Conta);

                    var transacao = registro.Conta.Transacoes.LastOrDefault();

                    registro.Concluir(transacao!.Transacao!);
                    m_RegistrosIntegracao.Atualizar(registro);

                    m_SrvNotificacaoCobrancaRecebida.Notificar(registro, 1); // Todo: melhorar a questão do evento

                    break;
                case EnumStatusTransacao.Cancelada:
                    registro.Abortar("Transação cancelada no integrador externo.");
                    m_RegistrosIntegracao.Atualizar(registro);

                    break;
            }
        }
    }
}
