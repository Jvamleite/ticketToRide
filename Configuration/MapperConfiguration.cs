using TicketToRide.Application.Mappers;
using TicketToRide.Application.Mappers.Interfaces;
using TicketToRideAPI.Application.Mappers;

namespace TicketToRide.Configuration
{
    public static class MapperConfiguration
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddScoped<RotaMapper>();
            services.AddScoped<TurnoMapper>();
            services.AddScoped<PartidaMapper>();
            services.AddScoped<JogadorMapper>();
            services.AddScoped<BilheteDestinoMapper>();
            services.AddScoped<CartaVeiculoMapper>();
            services.AddScoped<IMapper, CompositeMapper>();

            return services;
        }
    }
}
