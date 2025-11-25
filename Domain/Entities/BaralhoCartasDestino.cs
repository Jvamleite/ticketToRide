using TicketToRide.Application;

namespace TicketToRide.Domain.Entities
{
    public class BaralhoCartasDestino : Baralho<BilheteDestino>
    {
        public BaralhoCartasDestino()
        {
            InicializarBaralho(DadosJogo.ObterBilhetesDestino());
        }

        protected override void InicializarBaralho(IEnumerable<BilheteDestino> bilhetes)
        {
            InicializarMonteCompra(bilhetes);
        }

        /*
         *
         * VERIFICAR NECESSIDADE DESSE FLUXO OU SE SÓ COMPRAR 3 CARTAS É SUFICIENTE

        public List<BilheteDestino> ComprarBilhetes()
        {
            ValidarQuantidadeBilhetes(quantidade);
            return Comprar(quantidade);
        }

        private static void ValidarQuantidadeBilhetes(int quantidade)
        {
            if (quantidade < BILHETES_MINIMOS_POR_COMPRA
                || quantidade > BILHETES_MAXIMOS_POR_COMPRA)
            {
                throw new InvalidOperationException(
                    $"Deve comprar entre {BILHETES_MINIMOS_POR_COMPRA} " +
                    $"e {BILHETES_MAXIMOS_POR_COMPRA} bilhetes.");
            }
        }
        */
    }
}