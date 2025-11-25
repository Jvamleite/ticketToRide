using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;
using TicketToRideAPI.Application.Services;

public class CalculadorPontuacaoObserver : IObserver
{
    private readonly IPartidaRepository _partidaRepository;
    private readonly IPontuacaoService _pontuacaoService;

    public CalculadorPontuacaoObserver(
        IPartidaRepository partidaRepository,
        IPontuacaoService pontuacaoService)
    {
        _partidaRepository = partidaRepository;
        _pontuacaoService = pontuacaoService;
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

    private void CalcularPontuacaoFinal(Partida partida)
    {
        foreach (Jogador jogador in partida.Jogadores)
        {
            int pontosBilhetes = _pontuacaoService.CalcularPontuacaoBilhetes(
                jogador.BilhetesDestino,
                jogador.RotasConquistadas);

            jogador.AdicionarPontuacao(pontosBilhetes);
            Console.WriteLine($"    - Jogador '{jogador.Nome}' ganhou {pontosBilhetes} pontos em bilhetes");
        }

        Jogador? vencedorRotaLonga = partida.CalcularRotaMaisLonga();
        if (vencedorRotaLonga is not null)
        {
            int bonus = _pontuacaoService.CalcularBonusRotaMaisLonga();
            vencedorRotaLonga.AdicionarPontuacao(bonus);
            Console.WriteLine($"    - Jogador '{vencedorRotaLonga.Nome}' recebeu {bonus} pontos pela maior rota contínua");
        }
    }
}