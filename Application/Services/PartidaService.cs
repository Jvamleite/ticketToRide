using TicketToRide.Application.DTOs;
using TicketToRide.Application.Mappers.Interfaces;
using TicketToRide.Application.Observers;
using TicketToRide.Application.Services.Interfaces;
using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;

namespace TicketToRide.Application.Services
{
    public class PartidaService : IPartidaService
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

        public PartidaDTO ObterPartida(string id)
        {
            Partida? partida = _partidaRepository.ObterPartida(id) ?? throw new ArgumentException("Partida não encontrada");
            return _mapper.Map<Partida, PartidaDTO>(partida);
        }

        public PartidaDTO IniciarPartida(string id)
        {
            Partida? partida = _partidaRepository.ObterPartida(id) ?? throw new ArgumentException("Partida não encontrada");

            if (partida.Jogadores.Count < MinimoJogadores)
            {
                throw new ArgumentException($"Partida deve ter no mínimo {MinimoJogadores} jogadores");
            }

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

        public PontuacaoDTO ObterPontuacao(string partidaId)
        {
            PartidaDTO partida = ObterPartida(partidaId);

            List<JogadorDTO> ranking = [.. partida.Jogadores.OrderByDescending(j => j.Pontuacao)];
            JogadorDTO? vencedor = ranking.FirstOrDefault();

            return new PontuacaoDTO
            {
                Ranking = ranking.Select((j, index) => new RankingItemDTO
                {
                    Posicao = index + 1,
                    Jogador = j.Nome,
                    Pontos = j.Pontuacao,
                    Rotas = j.NumeroRotas,
                    Bilhetes = j.NumeroBilhetes
                }),
                Vencedor = vencedor != null ? new VencedorDTO
                {
                    Nome = vencedor.Nome,
                    Pontos = vencedor.Pontuacao
                } : null
            };
        }
    }
}