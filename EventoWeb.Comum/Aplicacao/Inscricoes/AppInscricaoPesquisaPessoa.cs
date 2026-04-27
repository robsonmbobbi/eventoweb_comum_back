using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Inscricoes;

public class AppInscricaoPesquisaPessoa: AppInscricaoBase
{
    private readonly IPessoas m_Pessoas;

    public AppInscricaoPesquisaPessoa(IContexto contexto, IInscricoes inscricoes, IPessoas pessoas) : 
        base(contexto, inscricoes)
    {
        m_Pessoas = pessoas;
    }

    public DTOInscricaoPesquisaPessoa Pesquisar(int idEvento, string CPF)
    {
        DTOInscricaoPesquisaPessoa dto = new();
        
        ExecutarSeguramente(() =>
        {
            var inscricao = Inscricoes.ObterPorCPF(idEvento, CPF);
            if (inscricao != null)
            {
                if (inscricao.Situacao == EnumSituacaoInscricao.Limbo)
                {
                    dto.Situacao = EnumSituacaoPesquisaPessoa.InscricaoNoLimbo;
                    dto.Inscricao = inscricao.Converter();
                }
                else
                {
                    dto.Situacao = EnumSituacaoPesquisaPessoa.InscricaoRealizada;
                    dto.Inscricao = inscricao.Converter();
                }
            }
            else
            {
                dto.Situacao = EnumSituacaoPesquisaPessoa.InscricaoNaoExiste;
                var pessoa = m_Pessoas.ObterPorCPF(CPF);

                if (pessoa != null)
                {
                    dto.Pessoa = pessoa.Converter();
                }
            }
        });

        return dto;
    }
}