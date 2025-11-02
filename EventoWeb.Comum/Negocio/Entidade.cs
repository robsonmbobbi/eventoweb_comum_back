namespace EventoWeb.Comum.Negocio
{
    public class Entidade
    {
        private int m_Id = 0;

        public virtual int Id
        {
            get { return m_Id; }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Entidade other)
                return false;

            if (Id == 0 && other.Id == 0)
                return (object)this == other;
            else
                return Id == other.Id;
        }

        public override int GetHashCode()
        {
            if (Id == 0)
                return base.GetHashCode();

            string stringRepresentation =
                System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType?.FullName
                + "#" + Id.ToString();

            return stringRepresentation.GetHashCode();
        }

        public virtual Type GetTypeUnproxied()
        {
            return this.GetType();
        }
    }
}