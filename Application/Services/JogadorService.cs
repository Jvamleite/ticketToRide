using TicketToRide.Application.DTOs;
using TicketToRide.Application.Mappers.Interfaces;
using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;

namespace TicketToRide.Application.Services
{
    public class JogadorService
    {
        private readonly IPartidaRepository _partidaRepository;
        private readonly IMapper _mapper;
        private const int MaximoJogadores = 5;

        public JogadorService(
            IPartidaRepository partidaRepository,
            IMapper mapper)
        {
            _partidaRepository = partidaRepository;
            _mapper = mapper;
        }

        public JogadorDTO AdicionarJogador(string partidaId, string nome)
        {
            Partida partida = _partidaRepository.ObterPartida(partidaId) ?? throw new ArgumentException("Partida não encontrada");

            if (partida.Jogadores.Count > MaximoJogadores)
                throw new ArgumentException($"Partida pode ter no máximo {MaximoJogadores} jogadores");

            if (partida.Iniciada)
                throw new ArgumentException("Partida já iniciada");

            string jogadorId = $"JOGADOR_{partida.Jogadores.Count + 1}";
            Jogador jogador = new(jogadorId, nome);

            partida.AdicionarJogador(jogador);

            _partidaRepository.SalvarPartida(partida);

            return _mapper.Map<Jogador, JogadorDTO>(jogador);
        }

        public JogadorDTO? ObterJogador(string partidaId, string jogadorId)
        {
            Partida? partida = _partidaRepository.ObterPartida(partidaId) ?? throw new ArgumentException("Partida não encontrada");
            Jogador? jogador = partida.ObterJogador(jogadorId);
            return jogador is null ? null : _mapper.Map<Jogador, JogadorDTO>(jogador);
        }

        public List<JogadorDTO> ObterJogadores(string partidaId)
        {
            Partida? partida = _partidaRepository.ObterPartida(partidaId);
            return partida == null
                ? throw new ArgumentException("Partida não encontrada")
                : [.. partida.Jogadores.Select(x => _mapper.Map<Jogador, JogadorDTO>(x))];
        }

        public bool RemoverJogador(string partidaId, string jogadorId)
        {
            Partida? partida = _partidaRepository.ObterPartida(partidaId) ?? throw new ArgumentException("Partida não encontrada");
            if (partida.Iniciada)
            {
                throw new InvalidOperationException("Não é possível remover jogadores após o início da partida");
            }

            Jogador? jogador = partida.ObterJogador(jogadorId);
            if (jogador == null)
            {
                return false;
            }

            partida.RemoverJogador(jogador);

            _partidaRepository.SalvarPartida(partida);

            return true;
        }

        public List<JogadorDTO> ObterRanking(string partidaId)
        {
            Partida? partida = _partidaRepository.ObterPartida(partidaId) ?? throw new ArgumentException("Partida não encontrada");
            List<Jogador> ranking = partida.ObterRanking();
            return ranking.ConvertAll(x => _mapper.Map<Jogador, JogadorDTO>(x));
        }
    }
}