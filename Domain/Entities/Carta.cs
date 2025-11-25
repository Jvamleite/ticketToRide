using TicketToRide.Application.DTOs;

namespace TicketToRide.Domain.Entities
{
    public abstract class Carta
    {
        private string Nome { get; }

        protected Carta(string nome)
        {
            Nome = nome;
        }

        public override string ToString()
        {
            return Nome;
        }

        public CartaDTO MapearParaDTO()
        {
            return new CartaDTO
            {
                Nome = Nome,
                Descricao = ToString()
            };
        }
    }
}