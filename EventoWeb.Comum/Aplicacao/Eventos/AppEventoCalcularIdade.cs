using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Aplicacao.Eventos
{
    public class AppEventoCalcularIdade : AppBase
    {
        private readonly IEventos m_Eventos;

        public AppEventoCalcularIdade(IContexto contexto, IEventos eventos) : base(contexto)
        {
            m_Eventos = eventos;
        }

        public int CalcularIdade(int idEvento, DateTime dataNascimento)
        {
           int idade = 0;
            ExecutarSeguramente(() =>
            {
                var evento = m_Eventos.Obter(idEvento) ?? throw new Exception("Evento não encontrado.");

                idade = DataAniversario.ObterIdadeAnos(dataNascimento, evento.PeriodoRealizacaoEvento.DataInicial);

            });

        return idade;
        }
    }
}
