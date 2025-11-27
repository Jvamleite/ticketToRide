using TicketToRide.Application.Mappers.Interfaces;
using TicketToRide.Domain.Entities;
using TicketToRideAPI.Application.Mappers;

namespace TicketToRide.Application.Mappers
{
    public class CompositeMapper : IMapper
    {
        private readonly Dictionary<Type, object> _mappers;

        public CompositeMapper(
            PartidaMapper partidaMapper,
            JogadorMapper jogadorMapper,
            BilheteDestinoMapper bilheteDestinoMapper,
            CartaVeiculoMapper cartaVeiculoMapper,
            RotaMapper rotaMapper,
            TurnoMapper turnoMapper)
        {
            _mappers = new Dictionary<Type, object>
            {
                { typeof(Partida), partidaMapper },
                { typeof(Jogador), jogadorMapper },
                { typeof(BilheteDestino), bilheteDestinoMapper },
                { typeof(CartaVeiculo), cartaVeiculoMapper },
                { typeof(Rota), rotaMapper },
                { typeof(Turno), turnoMapper }
            };
        }

        public TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            if (_mappers.TryGetValue(typeof(TSource), out object? mapper))
            {
                return ((EntityMapper)mapper).Map<TSource, TDestination>(source);
            }

            throw new InvalidOperationException($"Mapper não encontrado para {typeof(TSource).Name}");
        }

        public List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
            where TDestination : new()
        {
            return [.. source.Select(Map<TSource, TDestination>)];
        }
    }
}