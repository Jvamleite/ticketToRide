using TicketToRide.Application.DTOs;
using TicketToRide.Application.Mappers.Interfaces;
using TicketToRide.Domain.Entities;

namespace TicketToRideAPI.Application.Mappers
{
    public class TurnoMapper : EntityMapper
    {
        protected override TDestination MapInternal<TSource, TDestination>(TSource source)
        {
            if (source is Turno turno)
            {
                TurnoDTO dto = new()
                {
                    Numero = turno.Numero,
                    JogadorId = turno.JogadorAtual.Id,
                    JogadorNome = turno.JogadorAtual.Nome,
                    AcaoRealizada = turno.AcaoRealizada,
                    AcaoCompletada = turno.AcaoCompletada,
                    PodeExecutarAcao = turno.PodeExecutarAcao()
                };

                return (TDestination)(object)dto;
            }

            throw new InvalidOperationException(
                $"Mapeamento não suportado: {typeof(TSource).Name} -> {typeof(TDestination).Name}");
        }
    }
}