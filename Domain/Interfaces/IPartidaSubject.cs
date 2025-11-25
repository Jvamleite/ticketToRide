using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;

namespace TicketToRideAPI.Domain.Interfaces
{
    public interface IPartidaSubject : ISubject
    {
        string Id { get; }
        List<Jogador> Jogadores { get; }
        BaralhoCartasVeiculo BaralhoCartasVeiculo { get; }
    }
}