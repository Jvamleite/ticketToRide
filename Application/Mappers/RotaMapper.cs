using TicketToRide.Application.DTOs;
using TicketToRide.Application.Mappers.Interfaces;
using TicketToRide.Domain;
using TicketToRide.Domain.Entities;

namespace TicketToRide.Application.Mappers
{
    public class RotaMapper : EntityMapper
    {
        protected override TDestination MapInternal<TSource, TDestination>(TSource source)
        {
            if (source is Rota rota)
            {
                RotaDTO dto = new()
                {
                    Id = rota.Id,
                    Origem = rota.Origem.Nome,
                    Destino = rota.Destino.Nome,
                    Cor = rota.Cor.GetEnumDescription(),
                    Tamanho = rota.Tamanho,
                    EhDupla = rota.Dupla,
                    EstaDisponivel = rota.Disponivel,
                    Pontos = rota.CalcularPontos()
                };

                return (TDestination)(object)dto;
            }

            throw new InvalidOperationException(
                $"Mapeamento não suportado: {typeof(TSource).Name} -> {typeof(TDestination).Name}");
        }
    }
}