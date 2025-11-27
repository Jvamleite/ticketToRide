namespace TicketToRideAPI.Application.DTOs.Request
{
    public class ComprarBilhetesRequest
    {
        public string JogadorId { get; set; } = string.Empty;
        public List<int> BilhetesSelecionados { get; set; } = [];
        public bool PrimeiroTurno { get; set; }
    }
}