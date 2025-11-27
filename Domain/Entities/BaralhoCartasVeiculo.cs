namespace TicketToRide.Domain.Entities
{
    public class BaralhoCartasVeiculo : Baralho<CartaVeiculo>
    {
        private const int QuantidadeCartasReveladas = 5;

        public BaralhoCartasVeiculo(IEnumerable<CartaVeiculo> cartas)
        {
            InicializarMonteCompra(cartas);
        }

        public CartaVeiculo? ComprarCartaRevelada(int indice)
        {
            return ComprarCartaPorIndice(indice);
        }

        public IEnumerable<CartaVeiculo> ListarCartasReveladas()
        {
            List<CartaVeiculo> cartasReveladas = [];
            for (int i = 0; i < QuantidadeCartasReveladas; i++)
            {
                cartasReveladas.Add(ObterCartaPorIndice(i));
            }

            return cartasReveladas;
        }
    }
}