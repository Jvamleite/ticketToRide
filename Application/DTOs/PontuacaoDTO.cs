namespace TicketToRide.Application.DTOs
{
    public class PontuacaoDTO
    {
        public IEnumerable<RankingItemDTO> Ranking { get; set; } = [];
        public VencedorDTO? Vencedor { get; set; }
    }

    public class RankingItemDTO
    {
        public int Posicao { get; set; }
        public string Jogador { get; set; } = string.Empty;
        public int Pontos { get; set; }
        public int Rotas { get; set; }
        public int Bilhetes { get; set; }
    }

    public class VencedorDTO
    {
        public string Nome { get; set; } = string.Empty;
        public int Pontos { get; set; }
    }
}