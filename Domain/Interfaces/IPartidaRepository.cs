using TicketToRide.Domain.Entities;

namespace TicketToRide.Domain.Interfaces
{
    public interface IPartidaRepository
    {
        Partida CriarPartida();
        Partida? ObterPartida(string id);
        List<Partida> ObterTodasPartidas();
        void SalvarPartida(Partida partida);
        void RemoverPartida(string id);
        bool ExistePartida(string id);
    }
}
