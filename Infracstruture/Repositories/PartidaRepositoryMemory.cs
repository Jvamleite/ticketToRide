using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;

namespace TicketToRide.Infracstruture.Repositories
{
    public class PartidaRepositoryMemory : IPartidaRepository
    {
        private static readonly Dictionary<string, Partida> _partidas = [];

        public Partida? ObterPartida(string id)
        {
            _partidas.TryGetValue(id, out Partida? partida);
            return partida;
        }

        public List<Partida> ObterTodasPartidas()
        {
            return [.. _partidas.Values];
        }

        public void SalvarPartida(Partida partida)
        {
            _partidas[partida.Id] = partida;
        }

        public void RemoverPartida(string id)
        {
            _partidas.Remove(id);
        }
    }
}