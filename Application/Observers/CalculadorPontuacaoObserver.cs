using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;

namespace TicketToRide.Application.Observers
{
    public class CalculadorPontuacaoObserver : IObserver
    {
        private const int BonusRotaMaisLonga = 10;
        private readonly IPartidaRepository _partidaRepository;

        public CalculadorPontuacaoObserver(
            IPartidaRepository partidaRepository)
        {
            _partidaRepository = partidaRepository;
        }

        public void Update(ISubject partidaSubject)
        {
            Partida partida = partidaSubject as Partida
                ?? throw new InvalidOperationException("Subject deve ser do tipo Partida");

            Console.WriteLine("  [CalculadorPontuacaoObserver] Calculando pontuação final...");

            try
            {
                CalcularPontuacaoFinal(partida);
                _partidaRepository.SalvarPartida(partida);
                Console.WriteLine("  [CalculadorPontuacaoObserver] Pontuação calculada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [CalculadorPontuacaoObserver] Erro ao calcular pontuação: {ex.Message}");
            }
        }

        private static void CalcularPontuacaoFinal(Partida partida)
        {
            foreach (Jogador jogador in partida.Jogadores)
            {
                int pontosBilhetes = jogador.CalcularPontuacaoTotal();

                jogador.AdicionarPontuacao(pontosBilhetes);

                Console.WriteLine($"    - Jogador '{jogador.Nome}' ganhou {pontosBilhetes} pontos em bilhetes");
            }

            Jogador? vencedorRotaLonga = partida.CalcularRotaMaisLonga();
            if (vencedorRotaLonga is not null)
            {
                vencedorRotaLonga.AdicionarPontuacao(BonusRotaMaisLonga);
                Console.WriteLine($"    - Jogador '{vencedorRotaLonga.Nome}' recebeu {BonusRotaMaisLonga} pontos pela maior rota contínua");
            }
        }
    }
}