using TicketToRide.Application.DTOs;
using TicketToRide.Application.EventHandlers;
using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;
using TicketToRide.Application.Mappers.Interfaces;

namespace TicketToRide.Application.Services
{
    public class PartidaService
    {
        private static int _contadorId = 1;
        private const int MinimoJogadores = 2;

        private readonly IPartidaRepository _partidaRepository;
        private readonly IMapper _mapper;
        private readonly DistribuidorCartasObserver _distribuidorCartasObserver;
        private readonly CalculadorPontuacaoObserver _calculadorPontuacaoObserver;

        public PartidaService(
            IPartidaRepository partidaRepository,
            IMapper mapper,
            DistribuidorCartasObserver distribuidorCartasObserver,
            CalculadorPontuacaoObserver calculadorPontuacaoObserver)
        {
            _partidaRepository = partidaRepository;
            _mapper = mapper;
            _distribuidorCartasObserver = distribuidorCartasObserver;
            _calculadorPontuacaoObserver = calculadorPontuacaoObserver;
        }

        public PartidaDTO CriarPartida()
        {
            Partida partida = Partida.CriarPartida(
                $"PARTIDA-{_contadorId++}",
                new Tabuleiro(DadosJogo.ObterRotas()),
                new BaralhoCartasDestino(DadosJogo.ObterBilhetesDestino()),
                new BaralhoCartasVeiculo(DadosJogo.GerarCartasIniciais())
                );

            partida.Attach(_distribuidorCartasObserver);
            partida.Attach(_calculadorPontuacaoObserver);

            _partidaRepository.SalvarPartida(partida);

            return _mapper.Map<Partida, PartidaDTO>(partida);
        }

        public PartidaDTO? ObterPartida(string id)
        {
            Partida? partida = _partidaRepository.ObterPartida(id);
            return _mapper.Map<Partida, PartidaDTO>(partida);
        }

        public PartidaDTO IniciarPartida(string id, int numJogadores)
        {
            Partida? partida = _partidaRepository.ObterPartida(id) ?? throw new ArgumentException("Partida não encontrada");

            if (partida.Jogadores.Count < MinimoJogadores)
                throw new ArgumentException($"Partida deve ter no mínimo {MinimoJogadores} jogadores");

            partida.IniciarPartida();

            return _mapper.Map<Partida, PartidaDTO>(partida);
        }

        public PartidaDTO FinalizarPartida(string id)
        {
            Partida? partida = _partidaRepository.ObterPartida(id) ?? throw new ArgumentException("Partida não encontrada");

            partida.FinalizarPartida();

            return _mapper.Map<Partida, PartidaDTO>(partida);
        }

        public List<PartidaDTO> ObterTodasPartidas()
        {
            List<Partida> partidas = _partidaRepository.ObterTodasPartidas();
            return _mapper.MapList<Partida, PartidaDTO>(partidas);
        }
    }
}