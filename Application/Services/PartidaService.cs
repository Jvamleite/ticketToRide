using TicketToRide.Application.DTOs;
using TicketToRide.Application.EventHandlers;
using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;

namespace TicketToRide.Application.Services
{
    public class PartidaService
    {
        private static int _contadorId = 1;

        private readonly IPartidaRepository _partidaRepository;
        private readonly DistribuidorCartasObserver _distribuidorCartasObserver;
        private readonly CalculadorPontuacaoObserver _calculadorPontuacaoObserver;

        public PartidaService(
            IPartidaRepository partidaRepository,
            DistribuidorCartasObserver distribuidorCartasObserver,
            CalculadorPontuacaoObserver calculadorPontuacaoObserver)
        {
            _partidaRepository = partidaRepository;
            _distribuidorCartasObserver = distribuidorCartasObserver;
            _calculadorPontuacaoObserver = calculadorPontuacaoObserver;
        }

        public PartidaDTO CriarPartida()
        {
            Partida partida = new(
                $"PARTIDA-{_contadorId++}",
                new Tabuleiro(DadosJogo.ObterRotas()),
                new BaralhoCartasDestino(DadosJogo.ObterBilhetesDestino()),
                new BaralhoCartasVeiculo(DadosJogo.GerarCartasIniciais())
                );

            partida.Attach(_distribuidorCartasObserver);
            partida.Attach(_calculadorPontuacaoObserver);

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

            partida.IniciarPartida(numJogadores);

            return partida.MapearParaDTO();
        }

        public PartidaDTO FinalizarPartida(string id)
        {
            Partida? partida = _partidaRepository.ObterPartida(id) ?? throw new ArgumentException("Partida não encontrada");

            return partida.MapearParaDTO();
        }

        public List<PartidaDTO> ObterTodasPartidas()
        {
            List<Partida> partidas = _partidaRepository.ObterTodasPartidas();
            return partidas.ConvertAll(p => p.MapearParaDTO());
        }
    }
}