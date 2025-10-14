using TicketToRide.Domain.Enums;

namespace TicketToRide.Application.DTOs
{
    public class TurnoDTO
    {
        public int Numero { get; set; }
        public string JogadorId { get; set; } = string.Empty;
        public string JogadorNome { get; set; } = string.Empty;
        public Acao? AcaoRealizada { get; set; }
        public bool AcaoCompletada { get; set; }
        public bool PodeExecutarAcao { get; set; }
    }
}
