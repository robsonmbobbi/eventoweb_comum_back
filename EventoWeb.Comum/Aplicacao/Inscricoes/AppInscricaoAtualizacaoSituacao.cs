using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Negocio.Repositorios;
using EventoWeb.Comum.Negocio.Servicos.Notificacoes.Inscricoes;

namespace EventoWeb.Comum.Aplicacao.Inscricoes;

public class AppInscricaoAtualizacaoSituacao(IContexto contexto, IInscricoes inscricoes, SrvNotificacaoInscricao notificacao)
    : AppInscricaoBase(contexto, inscricoes)
{
    public void Aceitar(int idInscricao)
    {       
        ExecutarSeguramente(() =>
        {
            var inscricao = ObterOuExcecao(idInscricao);

            inscricao.Aceitar();
            
            Inscricoes.Atualizar(inscricao);

            notificacao.Notificar([inscricao], EnumTipoNotificacaoInscricao.InscricaoAceita);
        });
    }

    public void Rejeitar(int idInscricao)
    {
        ExecutarSeguramente(() =>
        {
            var inscricao = ObterOuExcecao(idInscricao);

            inscricao.Rejeitar();

            Inscricoes.Atualizar(inscricao);

            notificacao.Notificar([inscricao], EnumTipoNotificacaoInscricao.InscricaoRejeitada);
        });
    }
}