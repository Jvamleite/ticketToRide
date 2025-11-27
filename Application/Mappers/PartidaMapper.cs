using TicketToRide.Application.DTOs;
using TicketToRide.Application.Mappers.Interfaces;
using TicketToRide.Domain.Entities;

namespace TicketToRide.Application.Mappers
{
    public class PartidaMapper : EntityMapper
    {
        private readonly IServiceProvider _serviceProvider;

        public PartidaMapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override TDestination MapInternal<TSource, TDestination>(TSource source)
        {
            if (source is Partida partida)
            {
                IMapper mapper = _serviceProvider.GetRequiredService<IMapper>();
                PartidaDTO dto = new()
                {
                    Id = partida.Id,
                    NumeroJogadores = partida.Jogadores.Count,
                    PartidaIniciada = partida.Iniciada,
                    PartidaFinalizada = partida.Finalizada,
                    TurnoAtual = partida.ObterTurnoAtual() == null ? null : mapper.Map<Turno, TurnoDTO>(partida.ObterTurnoAtual()),
                    Jogadores = mapper.MapList<Jogador, JogadorDTO>(partida.Jogadores),
                    Rotas = partida.Tabuleiro?.Rotas
                        .Select(r => mapper.Map<Rota, RotaDTO>(r)).ToList() ?? [],
                    CartasVisiveis = partida.BaralhoCartasVeiculo
                        .ListarCartasReveladas()
                        .Select(c => mapper.Map<CartaVeiculo, CartaVeiculoDTO>(c)),
                    OpcoesBilheteDestino = partida.BaralhoCartasDestino
                        .ListarOpcoesSelecionaveis()
                        .Select(b => mapper.Map<BilheteDestino, BilheteDestinoDTO>(b))
                };

                return (TDestination)(object)dto;
            }

            throw new InvalidOperationException(
                $"Mapeamento não suportado: {typeof(TSource).Name} -> {typeof(TDestination).Name}");
        }
    }
}