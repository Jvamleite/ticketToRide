namespace TicketToRide.Domain.Entities
{
    public abstract class Carta
    {
        protected string Nome { get; }

        protected Carta(string nome)
        {
            Nome = nome;
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}