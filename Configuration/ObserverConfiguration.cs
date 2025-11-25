using TicketToRide.Application.EventHandlers;
using TicketToRideAPI.Application.EventHandlers;
using TicketToRideAPI.Application.Services;

namespace TicketToRide.Configuration
{
    public static class ObserverConfiguration
    {
        public static IServiceCollection AddEventHandling(this IServiceCollection services)
        {
            services.AddSingleton<IPontuacaoService, PontuacaoService>();

            services.AddSingleton<CalculadorPontuacaoObserver>();
            services.AddSingleton<VerificarBilhetesObserver>();
            services.AddSingleton<DistribuidorCartasObserver>();

            return services;
        }
    }
}