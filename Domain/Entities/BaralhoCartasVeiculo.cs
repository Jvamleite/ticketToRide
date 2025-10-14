using TicketToRide.Domain.Enums;

namespace TicketToRide.Domain.Entities
{
    public class BaralhoCartasVeiculo : Baralho<CartaVeiculo>
    {
        public BaralhoCartasVeiculo()
        {
            InicializarBaralho();
        }

        private void InicializarBaralho()
        {
            MonteCompra.Clear();
            MonteDescarte.Clear();

            // 12 cartas de cada cor (exceto locomotiva)
            var cores = new[] { Cor.VERMELHO, Cor.AZUL, Cor.VERDE, Cor.AMARELO, Cor.PRETO, Cor.BRANCO, Cor.LARANJA, Cor.ROSA };
            
            foreach (var cor in cores)
            {
                for (int i = 0; i < 12; i++)
                {
                    MonteCompra.Add(new CartaVeiculo(cor));
                }
            }

            // 14 locomotivas (coringas)
            for (int i = 0; i < 14; i++)
            {
                MonteCompra.Add(new CartaVeiculo(Cor.LOCOMOTIVA, true));
            }

            Embaralhar();
        }

        public List<CartaVeiculo> ObterCartasVisiveis(int quantidade = 5)
        {
            var cartasVisiveis = new List<CartaVeiculo>();
            
            // Se não há cartas suficientes no monte, embaralhar o descarte
            if (MonteCompra.Count < quantidade)
            {
                Embaralhar();
            }

            // Pegar as primeiras cartas do monte
            for (int i = 0; i < quantidade && i < MonteCompra.Count; i++)
            {
                cartasVisiveis.Add(MonteCompra[i]);
            }

            return cartasVisiveis;
        }

        public CartaVeiculo? ComprarCartaVisivel(int indice)
        {
            if (indice < 0 || indice >= MonteCompra.Count)
            {
                return null;
            }

            var carta = MonteCompra[indice];
            MonteCompra.RemoveAt(indice);
            return carta;
        }

        public void ReporCartaVisivel(CartaVeiculo carta)
        {
            MonteCompra.Insert(0, carta);
        }
    }
}
