using EventoWeb.Comum.Negocio.Entidades.Financeiro;
using EventoWeb.Comum.Negocio.ObjetosValor;

namespace EventoWeb.Comum.Negocio.Entidades;

public enum EnumTipoPedido { Debito, Desconto, Isencao }

public class Pedido : Entidade
{
    private IList<Inscricao> m_Inscricoes;

    public Pedido(Pessoa pagador, IEnumerable<Inscricao> inscricoes, ValorMonetario valor, EnumTipoPedido tipo, FormaPagamento? forma)
    {
        if (!inscricoes.Any())
            throw new Exception($"{nameof(inscricoes)} não pode ser vazio.");

        if (inscricoes.Any(i => i.Situacao != EnumSituacaoInscricao.Limbo))
            throw new Exception($"Somente são aceitas inscrições que estejam no limbo");

        if (tipo == EnumTipoPedido.Debito && forma == null)
            throw new Exception($"Forma de pagamento deve ser informada para pedidos do tipo {EnumTipoPedido.Debito}.");

        Pagador = pagador ?? throw new Exception($"{nameof(pagador)} não pode ser nulo.");
        m_Inscricoes = [.. inscricoes];
        Valor = valor ?? throw new Exception($"{nameof(valor)} não pode ser nulo."); ;
        Tipo = tipo;
        FormaPagamento = forma;

        Conta = new Conta(pagador, EnumTipoTransacao.Receita, valor, DateTime.Now);
    }

    protected Pedido()
    {
    }

    public virtual IEnumerable<Inscricao> Inscricoes => m_Inscricoes;
    public virtual ValorMonetario Valor { get; protected set; }
    public virtual EnumTipoPedido Tipo { get; protected set; }
    public virtual FormaPagamento? FormaPagamento { get; protected set; }
    public virtual Conta Conta { get; protected set; }
    public virtual Pessoa Pagador { get; protected set; }
}