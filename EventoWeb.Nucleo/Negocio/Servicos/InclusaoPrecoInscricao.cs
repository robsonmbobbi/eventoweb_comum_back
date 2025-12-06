using EventoWeb.Nucleo.Negocio.Entidades;
using EventoWeb.Nucleo.Negocio.Excecoes;
using EventoWeb.Nucleo.Negocio.Repositorios;

namespace EventoWeb.Nucleo.Negocio.Servicos;

public class InclusaoPrecoInscricao
{
    private readonly IPersistenciaPrecoInscricao m_Persistencia;

    public InclusaoPrecoInscricao(IPersistenciaPrecoInscricao persistencia)
    {
        m_Persistencia = persistencia;
    }

    public void Incluir(PrecoInscricao novoPreco)
    {
        var preco = m_Persistencia.ObterParaEssaIdade(novoPreco.Evento, novoPreco.IdadeMax);
        if (preco?.IdadeMax == novoPreco.IdadeMax)
            throw new ExcecaoNegocio(
                GetType().Name, 
                "Já existe um preço definido nessa idade");
        
        m_Persistencia.Incluir(novoPreco);
    }
}