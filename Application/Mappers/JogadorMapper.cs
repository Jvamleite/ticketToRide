using TicketToRide.Application.DTOs;
using TicketToRide.Application.Mappers.Interfaces;
using TicketToRide.Domain.Entities;

namespace TicketToRide.Application.Mappers
{
    public class JogadorMapper : EntityMapper
    {
        private readonly IServiceProvider _serviceProvider;

        public JogadorMapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override TDestination MapInternal<TSource, TDestination>(TSource source)
        {
            IMapper mapper = _serviceProvider.GetRequiredService<IMapper>();
            if (source is Jogador jogador)
            {
                JogadorDTO dto = new()
                {
                    Id = jogador.Id,
                    Nome = jogador.Nome,
                    Pontuacao = jogador.Pontuacao,
                    PecasTremRestante = jogador.PecasTremRestante,
                    MaoCartas = [.. jogador.MaoCartas.Select(c => mapper.Map<CartaVeiculo, CartaVeiculoDTO>(c))],
                    BilhetesDestino = [.. jogador.BilhetesDestino.Select(b => mapper.Map<BilheteDestino, BilheteDestinoDTO>(b))],
                    RotasConquistadas = jogador.RotasConquistadas.ConvertAll(r => mapper.Map<Rota, RotaDTO>(r)),
                    NumeroCartas = jogador.MaoCartas.Count,
                    NumeroBilhetes = jogador.BilhetesDestino.Count,
                    NumeroRotas = jogador.RotasConquistadas.Count
                };

                return (TDestination)(object)dto;
            }

            throw new InvalidOperationException(
                $"Mapeamento não suportado: {typeof(TSource).Name} -> {typeof(TDestination).Name}");
        }
    }
}