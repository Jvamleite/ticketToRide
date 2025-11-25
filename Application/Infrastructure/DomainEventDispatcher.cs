using System.Collections.Concurrent;
using TicketToRideApi.Domain.Events;
using TicketToRideApi.Domain.Interfaces;

namespace TicketToRideApi.Application.Infrastructure
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly ConcurrentDictionary<Type, List<object>> _subscribers = new();

        public void Publish<TEvent>(TEvent evt) where TEvent : IDomainEvent
        {
            if (_subscribers.TryGetValue(typeof(TEvent), out List<object>? observersList))
            {
                foreach (Domain.Interfaces.IObserver<TEvent>? observer in observersList.Cast<Domain.Interfaces.IObserver<TEvent>>().ToList())
                {
                    try
                    {
                        observer.Update(evt);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] Erro ao notificar observer: {ex.Message}");
                    }
                }
            }
        }

        public void Subscribe<TEvent>(Domain.Interfaces.IObserver<TEvent> observer) where TEvent : IDomainEvent
        {
            _subscribers.AddOrUpdate(
                typeof(TEvent),
                [observer],
                (_, list) =>
                {
                    if (!list.Contains(observer))
                    {
                        list.Add(observer);
                    }

                    return list;
                }
            );
        }

        public void Unsubscribe<TEvent>(Domain.Interfaces.IObserver<TEvent> observer) where TEvent : IDomainEvent
        {
            if (_subscribers.TryGetValue(typeof(TEvent), out List<object>? list))
            {
                list.Remove(observer);
            }
        }
    }
}