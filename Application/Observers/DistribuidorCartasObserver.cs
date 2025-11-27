using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;

namespace TicketToRide.Application.Observers
{
    public class DistribuidorCartasObserver : IObserver
    {
        private const int CartasVeiculoIniciaisPorJogador = 4;
        private readonly IPartidaRepository _partidaRepository;

        public DistribuidorCartasObserver(IPartidaRepository partidaRepository)
        {
            _partidaRepository = partidaRepository;
        }

        public void Update(ISubject partidaSubject)
        {
            Partida partida = partidaSubject as Partida
                ?? throw new InvalidOperationException("Subject deve ser do tipo Partida");

            Console.WriteLine("  [DistribuidorCartasObserver] Distribuindo cartas iniciais...");

            try
            {
                DistribuirCartasIniciais(partida);
                _partidaRepository.SalvarPartida(partida);
                Console.WriteLine("  [DistribuidorCartasObserver] Cartas distribuídas com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [DistribuidorCartasObserver] Erro ao distribuir cartas: {ex.Message}");
            }
        }

        private static void DistribuirCartasIniciais(Partida partida)
        {
            foreach (Jogador jogador in partida.Jogadores)
            {
                List<CartaVeiculo> cartas = partida.BaralhoCartasVeiculo.Comprar(CartasVeiculoIniciaisPorJogador);
                jogador.AdicionarCartasVeiculo(cartas);
                Console.WriteLine($"    - Jogador '{jogador.Nome}' recebeu {CartasVeiculoIniciaisPorJogador} cartas");
            }
        }
    }
}