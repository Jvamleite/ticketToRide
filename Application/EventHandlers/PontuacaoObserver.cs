using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;
using TicketToRideApi.Domain.Events;
using TicketToRideAPI.Application.Services;

namespace TicketToRideAPI.Application.EventHandlers
{
    public class PontuacaoObserver :
        TicketToRideApi.Domain.Interfaces.IObserver<RotaReivindicadaEvent>,
        TicketToRideApi.Domain.Interfaces.IObserver<TurnoConcluídoEvent>
    {
        private readonly IPontuacaoService _pontuacaoService;
        private readonly IPartidaRepository _partidaRepository;

        public PontuacaoObserver(
            IPontuacaoService pontuacaoService,
            IPartidaRepository partidaRepository)
        {
            _pontuacaoService = pontuacaoService;
            _partidaRepository = partidaRepository;
        }

        public void Update(RotaReivindicadaEvent e)
        {
            Partida? partida = _partidaRepository.ObterPartida(e.IdPartida);
            Jogador? jogador = partida?.ObterJogador(e.IdJogador);

            if (jogador != null)
            {
                int pontos = _pontuacaoService.CalcularPontosRota(e.RotaReivindicada);
                jogador.AdicionarPontuacao(pontos);

                _partidaRepository.SalvarPartida(partida);
            }
        }

        public void Update(TurnoConcluídoEvent e)
        {
            Console.WriteLine($"[SCORING] Turno concluído para jogador {e.IdJogador}");
        }
    }
}