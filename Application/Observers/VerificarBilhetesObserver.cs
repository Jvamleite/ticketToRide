using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;

namespace TicketToRide.Application.EventHandlers
{
    public class VerificarBilhetesObserver
    {
        public VerificarBilhetesObserver()
        {
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

        private static void VerificarBilhetesCompletos(Jogador jogador)
        {
            foreach (BilheteDestino bilhete in jogador.BilhetesDestino.Where(x => !x.EstaCompleto()))
            {
                bilhete.AtualizarBilhete(jogador.RotasConquistadas);
            }
        }
    }
}