using TicketToRide.Application.DTOs;
using TicketToRide.Application.Mappers.Interfaces;
using TicketToRide.Domain;
using TicketToRide.Domain.Entities;

namespace TicketToRide.Application.Mappers
{
    public class CartaVeiculoMapper : EntityMapper
    {
        protected override TDestination MapInternal<TSource, TDestination>(TSource source)
        {
            if (source is CartaVeiculo cartaVeiculo)
            {
                CartaVeiculoDTO dto = new()
                {
                    Nome = cartaVeiculo.ToString(),
                    Cor = cartaVeiculo.Cor.GetEnumDescription()
                };

                return (TDestination)(object)dto;
            }

            throw new InvalidOperationException(
                $"Mapeamento não suportado: {typeof(TSource).Name} -> {typeof(TDestination).Name}");
        }
    }
}