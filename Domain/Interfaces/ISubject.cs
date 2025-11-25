namespace TicketToRide.Domain.Interfaces
{
    public interface IObserver
    {
        void Update(ISubject partida);
    }

    public interface ISubject
    {
        void Attach(IObserver observer);

        void Detach(IObserver observer);

        void Notify();
    }
}