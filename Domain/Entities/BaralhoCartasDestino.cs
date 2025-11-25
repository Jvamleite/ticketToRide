namespace TicketToRide.Domain.Entities
{
    public class BaralhoCartasDestino : Baralho<BilheteDestino>
    {
        public BaralhoCartasDestino(IEnumerable<BilheteDestino> bilhetes)
        {
            InicializarMonteCompra(bilhetes);
        }
    }
}