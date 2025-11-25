using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;
using TicketToRideAPI.Application.Services;

namespace TicketToRideAPI.Application.EventHandlers
{
    public class VerificarBilhetesObserver
    {
        private readonly IPontuacaoService _pontuacaoService;

        public VerificarBilhetesObserver(IPontuacaoService pontuacaoService)
        {
            _pontuacaoService = pontuacaoService;
        }

        public void Update(ISubject subject)
        {
            Jogador? jogador = subject as Jogador ?? throw new InvalidOperationException("Subject deve ser do tipo Jogador");

            try
            {
                VerificarBilhetesCompletos(jogador);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private void VerificarBilhetesCompletos(Jogador jogador)
        {
            foreach (BilheteDestino bilhete in jogador.BilhetesDestino.Where(x => !x.EstaCompleto()))
            {
                bilhete.AtualizarBilhete(jogador.RotasConquistadas);
            }
        }
    }
}