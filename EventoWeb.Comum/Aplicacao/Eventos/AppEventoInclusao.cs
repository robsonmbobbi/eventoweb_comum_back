using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Eventos;

public class AppEventoInclusao: AppEventoBase
{
    public AppEventoInclusao(IContexto contexto, IEventos eventos) :
        base(contexto, eventos){}

    public DTOEvento Incluir(DTOEvento dto)
    {
        ExecutarSeguramente(() =>
        {
            var evento = new Evento(
                new String200(dto.Nome), 
                new Periodo(dto.DataInicialInscricao, dto.DataFinalInscricao), 
                new Periodo(dto.DataInicialRealizacao, dto.DataFinalRealizacao))
            {
                Logotipo = (string.IsNullOrWhiteSpace(dto.Logotipo ?? "")? null: new ArquivoBinario(Convert.FromBase64String(dto.Logotipo!), EnumTipoArquivoBinario.ImagemJPEG)),
                IdadeMinimaInscricaoAdulto = new InteiroPositivo(dto.IdadeMinimaAdulto)
            }; 
            
            Eventos.Incluir(evento);

            dto.Id = evento.Id;
        });

        return dto;
    }
}