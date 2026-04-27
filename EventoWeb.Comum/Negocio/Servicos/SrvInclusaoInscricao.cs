using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Negocio.Servicos;

public class SrvInclusaoInscricao(IInscricoes inscricoes, IEnumerable<IValidacao<Inscricao>> validacoes)
{
    private IEnumerable<IValidacao<Inscricao>> m_Validacoes = validacoes;
    private IInscricoes m_Inscricoes = inscricoes;

    public void Incluir(Inscricao inscricao)
    {
        foreach (var validacao in m_Validacoes)
        {
            validacao.Validar(inscricao);
        }
        
        m_Inscricoes.Incluir(inscricao);
    }
}