using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;
using TicketToRideApi.Domain.Events;
using TicketToRideAPI.Application.Services;

namespace TicketToRideAPI.Application.EventHandlers
{
    public class PartidaObserver :
        TicketToRideApi.Domain.Interfaces.IObserver<PartidaIniciadaEvent>,
        TicketToRideApi.Domain.Interfaces.IObserver<PartidaFinalizadaEvent>
    {
        private const int CartasVeiculoIniciaisPorJogador = 4;
        private const int CartasDestinoIniciaisPorJogador = 3;

        private readonly IPartidaRepository _partidaRepository;
        private readonly IPontuacaoService _pontuacaoService;

        public PartidaObserver(
            IPartidaRepository partidaRepository,
            IPontuacaoService pontuacaoService)
        {
            _partidaRepository = partidaRepository;
            _pontuacaoService = pontuacaoService;
        }

        public void Update(PartidaIniciadaEvent e)
        {
            TicketToRide.Domain.Entities.Partida? partida = _partidaRepository.ObterPartida(e.IdPartida) ?? throw new InvalidOperationException("Partida não encontrada ao iniciar.");

            partida.IniciarPartida(e.NumeroJogadores);

            DistribuirCartasIniciais(partida);

            _partidaRepository.SalvarPartida(partida);
        }

        private static void DistribuirCartasIniciais(Partida partida)
        {
            foreach (Jogador jogador in partida.Jogadores)
            {
                List<CartaVeiculo> cartas = partida.BaralhoCartasVeiculo.Comprar(CartasVeiculoIniciaisPorJogador);
                jogador.AdiconarCartasVeiculo(cartas);

                List<BilheteDestino> bilhetes = partida.BaralhoCartasDestino.Comprar(CartasDestinoIniciaisPorJogador);
                jogador.AdicionarBilhetesDestino(bilhetes);
            }
        }

        public void Update(PartidaFinalizadaEvent e)
        {
            Partida? partida = _partidaRepository.ObterPartida(e.IdPartida) ?? throw new InvalidOperationException("Partida não encontrada ao iniciar.");

            partida.FinalizarPartida();

            foreach (Jogador jogador in partida.Jogadores)
            {
                int pontosBilhetes = _pontuacaoService.CalcularPontuacaoBilhetes(
                    jogador.BilhetesDestino,
                    jogador.RotasConquistadas);

                jogador.AdicionarPontuacao(pontosBilhetes);
            }

            Jogador? vencedorRotaLonga = partida.CalcularRotaMaisLonga();

            if (vencedorRotaLonga is not null)
            {
                int bonus = _pontuacaoService.CalcularBonusRotaMaisLonga();
                vencedorRotaLonga.AdicionarPontuacao(bonus);

                Console.WriteLine($"[Pontuação] Jogador {vencedorRotaLonga.Nome} recebeu {bonus} pontos pela maior rota contínua.");
            }

            _partidaRepository.SalvarPartida(partida);
        }
    }
}