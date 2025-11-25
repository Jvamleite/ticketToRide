using TicketToRideApi.Domain.Events;

namespace TicketToRideApi.Domain.Interfaces
{
    public interface IObserver<TEvent>
    {
        void Update(TEvent @event);
    }

    public interface IObservable<TEvent>
    {
        void Subscribe(IObserver<TEvent> observer);

        void Unsubscribe(IObserver<TEvent> observer);

        void Notify(TEvent @event);
    }

    public interface IDomainEventDispatcher
    {
        void Publish<TEvent>(TEvent evt) where TEvent : IDomainEvent;

        void Subscribe<TEvent>(IObserver<TEvent> observer) where TEvent : IDomainEvent;

        void Unsubscribe<TEvent>(IObserver<TEvent> observer) where TEvent : IDomainEvent;
    }
}