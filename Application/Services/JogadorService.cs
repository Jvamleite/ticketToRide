using TicketToRide.Application.DTOs;
using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;

namespace TicketToRide.Application.Services
{
    public class JogadorService
    {
        private readonly IPartidaRepository _partidaRepository;

        public JogadorService(IPartidaRepository partidaRepository)
        {
            _partidaRepository = partidaRepository;
        }

        public JogadorDTO AdicionarJogador(string partidaId, string nome)
        {
            Partida? partida = _partidaRepository.ObterPartida(partidaId);
            if (partida == null)
            {
                throw new ArgumentException("Partida não encontrada");
            }

            if (partida.Iniciada)
            {
                throw new InvalidOperationException("Não é possível adicionar jogadores após o início da partida");
            }

            if (partida.Jogadores.Count >= 5)
            {
                throw new InvalidOperationException("Número máximo de jogadores atingido (5)");
            }

            string jogadorId = $"JOGADOR_{partida.Jogadores.Count + 1}";
            Jogador jogador = new(jogadorId, nome);

            partida.AdicionarJogador(jogador);
            _partidaRepository.SalvarPartida(partida);

            return jogador.MapearJogadorParaDTO();
        }

        public JogadorDTO? ObterJogador(string partidaId, string jogadorId)
        {
            Partida? partida = _partidaRepository.ObterPartida(partidaId);
            if (partida == null)
            {
                throw new ArgumentException("Partida não encontrada");
            }

            Jogador? jogador = partida.ObterJogador(jogadorId);
            return jogador != null ? jogador.MapearJogadorParaDTO() : null;
        }

        public List<JogadorDTO> ObterJogadores(string partidaId)
        {
            Partida? partida = _partidaRepository.ObterPartida(partidaId);
            return partida == null
                ? throw new ArgumentException("Partida não encontrada")
                : partida.Jogadores.ConvertAll(x => x.MapearJogadorParaDTO());
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

            partida.Jogadores.Remove(jogador);

            _partidaRepository.SalvarPartida(partida);

            return true;
        }

        public List<JogadorDTO> ObterRanking(string partidaId)
        {
            Partida? partida = _partidaRepository.ObterPartida(partidaId) ?? throw new ArgumentException("Partida não encontrada");
            List<Jogador> ranking = partida.ObterRanking();
            return ranking.ConvertAll(x => x.MapearJogadorParaDTO());
        }
    }
}