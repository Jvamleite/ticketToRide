namespace TicketToRide.Domain.Entities
{
    public class Tabuleiro
    {
        private readonly List<Rota> rotas = [];
        public IReadOnlyList<Rota> Rotas => rotas.AsReadOnly();

        public Tabuleiro(IEnumerable<Rota> rotas)
        {
            this.rotas.AddRange(rotas);
        }

        public Rota? ObterRotaPorId(string idRota)
        {
            return Rotas.FirstOrDefault(r => r.Id == idRota);
        }
    }
}