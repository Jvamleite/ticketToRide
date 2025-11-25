using TicketToRide.Domain.Enums;

namespace TicketToRide.Application.DTOs
{
    public class CartaDTO
    {
        public string Nome { get; set; } = string.Empty;
        public Cor Cor { get; set; }
        public string Descricao { get; set; } = string.Empty;
    }
}