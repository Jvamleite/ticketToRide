using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;

namespace TicketToRideAPI.Domain.Interfaces
{
    public interface IPartidaSubject : ISubject
    {
        string Id { get; }
        IReadOnlyList<Jogador> Jogadores { get; }
        BaralhoCartasVeiculo BaralhoCartasVeiculo { get; }
    }
}