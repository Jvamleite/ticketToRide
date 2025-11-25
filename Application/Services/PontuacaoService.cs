using TicketToRide.Domain.Entities;

namespace TicketToRideAPI.Application.Services
{
    public interface IPontuacaoService
    {
        int CalcularPontosRota(Rota rota);

        public int CalcularPontuacaoBilhetes(
            IEnumerable<BilheteDestino> bilhetes,
            IEnumerable<Rota> rotas);

        int CalcularBonusRotaMaisLonga();
    }

    public class PontuacaoService : IPontuacaoService
    {
        private const int BonusRotaMaisLonga = 10;

        public int CalcularBonusRotaMaisLonga()
        {
            return BonusRotaMaisLonga;
        }

        public int CalcularPontuacaoBilhetes(
            IEnumerable<BilheteDestino> bilhetes,
            IEnumerable<Rota> rotas)
        {
            int total = 0;

            foreach (BilheteDestino bilhete in bilhetes)
            {
                if (bilhete.EstaCompleto(rotas))
                {
                    total += bilhete.Pontos;
                }
                else
                {
                    total -= bilhete.Pontos;
                }
            }

            return total;
        }

        public int CalcularPontosRota(Rota rota)
        {
            return rota.CalcularPontos();
        }
    }
}