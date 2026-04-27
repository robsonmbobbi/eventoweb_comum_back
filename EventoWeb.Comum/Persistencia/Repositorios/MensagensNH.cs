using System.Collections.Generic;
using EventoWeb.Comum.Negocio.Entidades.Notificacoes;
using EventoWeb.Comum.Negocio.Repositorios;
using NHibernate;

namespace EventoWeb.Comum.Persistencia.Repositorios
{
    public class MensagensNH(ISession sessao) : PersistenciaNH<MensagemNotificacao>(sessao), IMensagens
    {
        public IList<MensagemNotificacao> ListarPendentesPorMeio(EnumMeioNotificacao meio)
        {
            return Sessao
                .QueryOver<MensagemNotificacao>()
                .Where(m => m.Situacao == EnumSituacaoEnvioNotificacao.Pendente)
                .JoinQueryOver(m => m.Modelo)
                .Where(m=> m.Meio == meio)
                .List();
        }
    }
}
