using TicketToRide.Domain.Enums;

namespace TicketToRide.Application.DTOs
{
    public class RotaDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Origem { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public Cor Cor { get; set; }
        public int Tamanho { get; set; }
        public bool EhDupla { get; set; }
        public string? JogadorId { get; set; }
        public string? JogadorNome { get; set; }
        public bool EstaDisponivel { get; set; }
        public int Pontos { get; set; }
    }
}
