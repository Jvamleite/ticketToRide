using TicketToRide.Application.DTOs;
using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;
using TicketToRideApi.Domain.Events;
using TicketToRideApi.Domain.Interfaces;

namespace TicketToRide.Application.Services
{
    public class PartidaService
    {
        private static int _contadorId = 1;

        private readonly IPartidaRepository _partidaRepository;
        private readonly IDomainEventDispatcher _eventDispatcher;

        public PartidaService(
            IPartidaRepository partidaRepository,
            IDomainEventDispatcher eventDispatcher)
        {
            _partidaRepository = partidaRepository;
            _eventDispatcher = eventDispatcher;
        }

        public PartidaDTO CriarPartida()
        {
            Partida partida = new(
                $"PARTIDA-{_contadorId++}",
                new Tabuleiro(DadosJogo.ObterRotas()),
                new BaralhoCartasDestino(DadosJogo.ObterBilhetesDestino()),
                new BaralhoCartasVeiculo(DadosJogo.GerarCartasIniciais())
                );
            _partidaRepository.SalvarPartida(partida);
            return partida.MapearParaDTO();
        }

        public PartidaDTO? ObterPartida(string id)
        {
            Partida? partida = _partidaRepository.ObterPartida(id);
            return partida?.MapearParaDTO();
        }

        public PartidaDTO IniciarPartida(string id, int numJogadores)
        {
            Partida? partida = _partidaRepository.ObterPartida(id) ?? throw new ArgumentException("Partida não encontrada");
            _eventDispatcher.Publish(new PartidaIniciadaEvent(
                IdPartida: partida.Id,
                NumeroJogadores: numJogadores,
                OcorridoEm: DateTime.UtcNow
                ));

            return partida.MapearParaDTO();
        }

        public PartidaDTO FinalizarPartida(string id)
        {
            Partida? partida = _partidaRepository.ObterPartida(id) ?? throw new ArgumentException("Partida não encontrada");
            _eventDispatcher.Publish(new PartidaFinalizadaEvent(
                IdPartida: partida.Id,
                OcorridoEm: DateTime.UtcNow
            ));

            return partida.MapearParaDTO();
        }

        public List<PartidaDTO> ObterTodasPartidas()
        {
            List<Partida> partidas = _partidaRepository.ObterTodasPartidas();
            return partidas.ConvertAll(p => p.MapearParaDTO());
        }
    }
}