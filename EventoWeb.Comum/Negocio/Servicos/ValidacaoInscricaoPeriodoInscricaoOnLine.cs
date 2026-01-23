using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Negocio.Servicos;

public class ValidacaoInscricaoPeriodoInscricaoOnLine: IValidacao<Inscricao>
{
    public void Validar(Inscricao entidade)
    {
        var dataAtual = DateTime.Now;
        if (dataAtual < entidade.Evento.PeriodoInscricaoOnLine.DataInicial ||
            dataAtual > entidade.Evento.PeriodoInscricaoOnLine.DataFinal)
        {
            throw new Exception("Fora do período de inscrição online!");
        }
    }
}