using TicketToRide.Application.DTOs;
using TicketToRide.Application.Mappers.Interfaces;
using TicketToRide.Domain.Entities;

namespace TicketToRide.Application.Mappers
{
    public class BilheteDestinoMapper : EntityMapper
    {
        protected override TDestination MapInternal<TSource, TDestination>(TSource source)
        {
            if (source is BilheteDestino bilhete)
            {
                BilheteDestinoDTO dto = new()
                {
                    Origem = bilhete.Origem.Nome,
                    Destino = bilhete.Destino.Nome,
                    Pontos = bilhete.Pontos,
                    IsCompleto = false,
                    Descricao = bilhete.ToString(),
                };

                return (TDestination)(object)dto;
            }

            throw new InvalidOperationException(
                $"Mapeamento não suportado: {typeof(TSource).Name} -> {typeof(TDestination).Name}");
        }
    }
}