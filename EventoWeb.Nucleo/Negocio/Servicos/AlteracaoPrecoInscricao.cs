using EventoWeb.Nucleo.Negocio.Entidades;
using EventoWeb.Nucleo.Negocio.Excecoes;
using EventoWeb.Nucleo.Negocio.Repositorios;

namespace EventoWeb.Nucleo.Negocio.Servicos;

public class AlteracaoPrecoInscricao
{
    private readonly IPersistenciaPrecoInscricao m_Persistencia;

    public AlteracaoPrecoInscricao(IPersistenciaPrecoInscricao persistencia)
    {
        m_Persistencia = persistencia;
    }

    public void Alterar(PrecoInscricao precoAlterado)
    {
        var preco = m_Persistencia.ObterParaEssaIdade(precoAlterado.Evento, precoAlterado.IdadeMax);
        if (preco?.Id != precoAlterado.Id && preco?.IdadeMax == precoAlterado.IdadeMax)
            throw new ExcecaoNegocio(
                GetType().Name, 
                "Já existe um preço definido nessa idade");
        
        m_Persistencia.Atualizar(precoAlterado);
    }
}