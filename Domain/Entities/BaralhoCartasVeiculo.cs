namespace TicketToRide.Domain.Entities
{
    public class BaralhoCartasVeiculo : Baralho<CartaVeiculo>
    {
        public BaralhoCartasVeiculo(IEnumerable<CartaVeiculo> cartas)
        {
            InicializarMonteCompra(cartas);
        }

        public CartaVeiculo? ComprarCartaRevelada(int indice)
        {
            if (indice < 0 || indice >= ObterTamanhoMonte())
            {
                return null;
            }

            return ObterCartaPorIndice(indice);
        }
    }
}