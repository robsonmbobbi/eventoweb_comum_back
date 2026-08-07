using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.FormasPagamento;

public class AppFormasPagamentoListagem: AppBase
{
    private readonly IFormasPagamento m_Formas;

    public AppFormasPagamentoListagem(IContexto contexto, IFormasPagamento formasPagamento) : base(contexto)
    {
        m_Formas = formasPagamento;
    }

    public IList<DTOFormaPagamento> ListarTodas(int idEvento)
    {
        List<DTOFormaPagamento> lista = [];
        
        ExecutarSeguramente(() =>
        {
            lista.AddRange(
                m_Formas
                    .ListarTodas(idEvento)
                    .Select( x=> x.Converter())
                    .ToList()
            );
        });

        return lista;
    }
}