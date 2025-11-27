using TicketToRide.Application.EventHandlers;
using TicketToRide.Application.Observers;

namespace TicketToRide.Configuration
{
    public static class ObserverConfiguration
    {
        public static IServiceCollection AddEventHandling(this IServiceCollection services)
        {
            services.AddSingleton<CalculadorPontuacaoObserver>();
            services.AddSingleton<VerificarBilhetesObserver>();
            services.AddSingleton<DistribuidorCartasObserver>();

            return services;
        }
    }
}