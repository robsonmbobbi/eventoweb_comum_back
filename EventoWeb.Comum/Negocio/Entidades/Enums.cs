namespace EventoWeb.Comum.Negocio.Entidades;

public enum EnumFormaPagamento { DebitoAplicado, SolicitacaoDesconto, SolicitacaoIsencao, DescontoAplicado, IsencaoAplicada }
public enum EnumMeioPagamento { Credito, Debito, PIX, Dinheiro }
public enum EnumSituacaoPagamento { Pendente, Concluido, Erro }