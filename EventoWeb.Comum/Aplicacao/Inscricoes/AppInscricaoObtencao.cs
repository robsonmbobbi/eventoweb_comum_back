using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Inscricoes;

public class AppInscricaoObtencao(IContexto contexto, IInscricoes inscricoes) : 
    AppInscricaoBase(contexto, inscricoes)
{
    public DTOInscricao? Obter(int id)
    {
        DTOInscricao? dto = null;
        
        ExecutarSeguramente(() =>
        {
            dto = Inscricoes.Obter(id)?.Converter();
        });

        return dto;
    }
}