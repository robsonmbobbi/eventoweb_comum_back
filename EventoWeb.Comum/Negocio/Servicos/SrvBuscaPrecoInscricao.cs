using EventoWeb.Comum.Negocio.Entidades;
using EventoWeb.Comum.Negocio.ObjetosValor;
using EventoWeb.Comum.Negocio.Repositorios;

namespace EventoWeb.Comum.Negocio.Servicos;

public class SrvBuscaPrecoInscricao (IPrecosInscricao precos)
{
    private IPrecosInscricao m_PrecosInscricao = precos;
    
    public PrecoInscricao? Buscar(Evento evento, DateTime dataNascimentoPessoa)
    {
        var idade = DataAniversario.ObterIdadeAnos(
            dataNascimentoPessoa, 
            evento.PeriodoRealizacaoEvento.DataInicial);
        
        return m_PrecosInscricao.ObterPelaIdade(evento.Id, idade);
    }
}