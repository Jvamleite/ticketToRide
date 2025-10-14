namespace TicketToRide.Application.DTOs
{
    public class BilheteDestinoDTO
    {
        public string Origem { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public int Pontos { get; set; }
        public bool IsCompleto { get; set; }
        public string Descricao { get; set; } = string.Empty;
    }
}
