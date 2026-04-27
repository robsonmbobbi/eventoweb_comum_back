using System.Collections.Generic;
using EventoWeb.Comum.Negocio.Entidades.Notificacoes;

namespace EventoWeb.Comum.Negocio.Repositorios
{
    public interface IMensagens : IPersistencia<MensagemNotificacao>
    {
        IList<MensagemNotificacao> ListarPendentesPorMeio(EnumMeioNotificacao meio);
    }
}
