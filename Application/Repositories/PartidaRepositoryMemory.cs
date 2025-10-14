using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;

namespace TicketToRide.Application.Repositories
{
    public class PartidaRepositoryMemory : IPartidaRepository
    {
        private static readonly Dictionary<string, Partida> _partidas = new();
        private static int _contadorId = 1;

        public Partida CriarPartida()
        {
            var id = $"PARTIDA_{_contadorId++}";
            var partida = new Partida
            {
                Id = id
            };

            _partidas[id] = partida;
            return partida;
        }

        public Partida? ObterPartida(string id)
        {
            _partidas.TryGetValue(id, out var partida);
            return partida;
        }

        public List<Partida> ObterTodasPartidas()
        {
            return _partidas.Values.ToList();
        }

        public void SalvarPartida(Partida partida)
        {
            if (string.IsNullOrEmpty(partida.Id))
            {
                partida.Id = $"PARTIDA_{_contadorId++}";
            }
            _partidas[partida.Id] = partida;
        }

        public void RemoverPartida(string id)
        {
            _partidas.Remove(id);
        }

        public bool ExistePartida(string id)
        {
            return _partidas.ContainsKey(id);
        }
    }
}
