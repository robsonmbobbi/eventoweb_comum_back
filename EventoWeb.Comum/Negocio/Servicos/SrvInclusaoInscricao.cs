using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Negocio.Servicos;

public class SrvInclusaoInscricao(IInscricoes inscricoes)
{
    private IInscricoes m_Inscricoes = inscricoes;

    public void Incluir(Inscricao inscricao)
    {
        var inscricaoExistente = m_Inscricoes.ObterPorCPF(inscricao.Evento.Id, inscricao.Pessoa.CPF.Numero);
        if (inscricaoExistente != null)
        {
            throw new Exception($"Esta pessoa, {inscricao.Pessoa.CPF.Numero} - {inscricaoExistente.Pessoa.Nome}, já esta inscrita.");
        }
        
        m_Inscricoes.Incluir(inscricao);
    }
}