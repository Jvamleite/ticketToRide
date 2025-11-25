using TicketToRide.Application;

namespace TicketToRide.Domain.Entities
{
    public class BaralhoCartasVeiculo : Baralho<CartaVeiculo>
    {
        public BaralhoCartasVeiculo()
        {
            InicializarBaralho([]);
        }

        protected override void InicializarBaralho(IEnumerable<CartaVeiculo> cartasT)
        {
            List<CartaVeiculo> cartas = DadosJogo.GerarCartasIniciais();
            AdicionarCartasAoMonteCompra(cartas);

            Embaralhar();
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