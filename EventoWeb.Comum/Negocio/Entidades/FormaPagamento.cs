namespace EventoWeb.Comum.Negocio.Entidades;

public class FormaPagamento : Entidade
{
    private NomeCompleto m_Nome;

    public FormaPagamento(NomeCompleto nome)
    {
        Nome = nome;
    }

    protected FormaPagamento()
    {
    }

    public virtual NomeCompleto Nome
    {
        get => m_Nome;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(Nome));
            
            m_Nome = value;
        }
    }
    
}