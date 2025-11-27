namespace TicketToRide.Controllers
{
    public class ComprarCartasRequest
    {
        public string JogadorId { get; set; } = string.Empty;
        public List<int>? Indices { get; set; }
    }
}