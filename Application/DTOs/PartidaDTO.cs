using TicketToRide.Domain.Entities;

namespace TicketToRide.Application.DTOs
{
    public class PartidaDTO
    {
        public string Id { get; set; } = string.Empty;
        public List<JogadorDTO> Jogadores { get; set; } = new();
        public List<RotaDTO> Rotas { get; set; } = new();
        public TurnoDTO? TurnoAtual { get; set; }
        public bool PartidaIniciada { get; set; }
        public bool PartidaFinalizada { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int NumeroJogadores { get; set; }
        public bool PodeIniciar { get; set; }
    }
}
