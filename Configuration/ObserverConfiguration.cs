namespace TicketToRide.Configuration
{
    using Microsoft.Extensions.DependencyInjection;
    using TicketToRideApi.Application.Infrastructure;
    using TicketToRideApi.Domain.Events;
    using TicketToRideApi.Domain.Interfaces;
    using TicketToRideAPI.Application.EventHandlers;
    using TicketToRideAPI.Application.Services;

    public static class ObserverConfiguration
    {
        public static IServiceCollection AddEventHandling(this IServiceCollection services)
        {
            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();

            services.AddSingleton<IPontuacaoService, PontuacaoService>();

            services.AddSingleton<PontuacaoObserver>();
            services.AddSingleton<PartidaObserver>();

            services.AddHostedService<EventObserverRegistration>();

            return services;
        }
    }

    public class EventObserverRegistration : IHostedService
    {
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly PontuacaoObserver _pontuacao;
        private readonly PartidaObserver _partida;

        public EventObserverRegistration(
            IDomainEventDispatcher dispatcher,
            PontuacaoObserver pontuacao,
            PartidaObserver partida)
        {
            _dispatcher = dispatcher;
            _pontuacao = pontuacao;
            _partida = partida;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _dispatcher.Subscribe<RotaReivindicadaEvent>(_pontuacao);
            _dispatcher.Subscribe<PartidaIniciadaEvent>(_partida);
            _dispatcher.Subscribe<PartidaFinalizadaEvent>(_partida);

            Console.WriteLine("[STARTUP] Event observers registrados");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}