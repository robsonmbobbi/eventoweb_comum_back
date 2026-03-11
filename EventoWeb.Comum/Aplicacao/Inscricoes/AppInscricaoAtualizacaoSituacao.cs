using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Inscricoes;

public class AppInscricaoAtualizacaoSituacao(IContexto contexto, IInscricoes inscricoes)
    : AppInscricaoBase(contexto, inscricoes)
{
    public void Aceitar(int idInscricao)
    {       
        ExecutarSeguramente(() =>
        {
            var inscricao = ObterOuExcecao(idInscricao);

            inscricao.Aceitar();
            
             Inscricoes.Atualizar(inscricao);
        });
    }

    public void Rejeitar(int idInscricao)
    {
        ExecutarSeguramente(() =>
        {
            var inscricao = ObterOuExcecao(idInscricao);

            inscricao.Rejeitar();

            Inscricoes.Atualizar(inscricao);
        });
    }
}