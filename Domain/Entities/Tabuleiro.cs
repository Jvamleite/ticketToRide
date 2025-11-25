namespace TicketToRide.Domain.Entities
{
    public class Tabuleiro
    {
        public List<Rota> Rotas { get; } = [];

        public Tabuleiro(IEnumerable<Rota> rotas)
        {
            Rotas.AddRange(rotas);
        }

        public Rota? ObterRotaPorId(string idRota)
        {
            return Rotas.FirstOrDefault(r => r.Id == idRota);
        }
    }
}