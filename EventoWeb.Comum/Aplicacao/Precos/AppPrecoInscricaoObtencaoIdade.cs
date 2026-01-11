using EventoWeb.Comum.Negocio.Repositorios;
using EventoWeb.Comum.Negocio.Servicos;

namespace EventoWeb.Comum.Aplicacao.Precos;

public class AppPrecoInscricaoObtencaoIdade: AppBase
{
    private readonly IPrecosInscricao m_Precos;
    private readonly IEventos m_Eventos;

    public AppPrecoInscricaoObtencaoIdade(IContexto contexto, IEventos eventos, IPrecosInscricao precos) : base(contexto)
    {
        m_Precos = precos;
        m_Eventos = eventos;
    }

    public DTOPrecoInscricao? Obter(int idEvento, DateTime dataNascimento)
    {
        DTOPrecoInscricao? dto = null;
        
        ExecutarSeguramente(() =>
        {
            var evento = m_Eventos.Obter(idEvento) ?? 
                         throw new Exception($"Evento com id {idEvento} não encontrado!");
            
            var srv = new SrvBuscaPrecoInscricao(m_Precos);
            dto = srv.Buscar(evento, dataNascimento)?.Converter();
        });

        return dto;
    }
}