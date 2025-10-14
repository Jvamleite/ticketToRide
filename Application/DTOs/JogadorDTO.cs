using TicketToRide.Domain.Entities;

namespace TicketToRide.Application.DTOs
{
    public class JogadorDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public int Pontuacao { get; set; }
        public int PecasTremRestante { get; set; }
        public List<CartaDTO> MaoCartas { get; set; } = new();
        public List<BilheteDestinoDTO> BilhetesDestino { get; set; } = new();
        public List<RotaDTO> RotasConquistadas { get; set; } = new();
        public int NumeroCartas { get; set; }
        public int NumeroBilhetes { get; set; }
        public int NumeroRotas { get; set; }
    }
}
