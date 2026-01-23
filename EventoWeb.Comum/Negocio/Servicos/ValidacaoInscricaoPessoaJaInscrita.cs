using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Negocio.Servicos;

public class ValidacaoInscricaoPessoaJaInscrita(IInscricoes inscricoes): IValidacao<Inscricao>
{
    private IInscricoes m_Inscricoes = inscricoes;
    
    public void Validar(Inscricao entidade)
    {
        var inscricaoExistente = m_Inscricoes.ObterPorCPF(entidade.Evento.Id, entidade.Pessoa.CPF.Numero);
        if (inscricaoExistente != null)
        {
            throw new Exception($"Esta pessoa, {entidade.Pessoa.CPF.Numero} - {inscricaoExistente.Pessoa.Nome}, já esta inscrita.");
        }

    }
}