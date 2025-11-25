using TicketToRide.Domain.Entities;

namespace TicketToRideApi.Domain.Events
{
    public interface IDomainEvent
    {
        string IdPartida { get; }
        DateTime OcorridoEm { get; }
    }

    public abstract record DomainEventBase(string IdPartida, DateTime OcorridoEm) : IDomainEvent;

    public record TurnoConcluídoEvent(
        string IdPartida,
        string IdJogador,
        DateTime OcorridoEm
    ) : DomainEventBase(IdPartida, OcorridoEm);
    public record RotaReivindicadaEvent(
        string IdPartida,
        string IdJogador,
        Rota RotaReivindicada,
        DateTime OcorridoEm
    ) : DomainEventBase(IdPartida, OcorridoEm);

    public record PartidaIniciadaEvent(
        string IdPartida,
        int NumeroJogadores,
        DateTime OcorridoEm
        ) : DomainEventBase(IdPartida, OcorridoEm);

    public record PartidaFinalizadaEvent(
        string IdPartida,
        DateTime OcorridoEm
    ) : DomainEventBase(IdPartida, OcorridoEm);
}