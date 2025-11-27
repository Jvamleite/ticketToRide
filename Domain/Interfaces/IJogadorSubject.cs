using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;

namespace TicketToRideAPI.Domain.Interfaces
{
    public interface IJogadorSubject : ISubject
    {
        string Id { get; }
        string Nome { get; }
        int Pontuacao { get; }
        List<Rota> RotasConquistadas { get; }
        IReadOnlyList<BilheteDestino> BilhetesDestino { get; }
    }
}