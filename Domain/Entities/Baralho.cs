namespace TicketToRide.Domain.Entities
{
    public abstract class Baralho<T> where T : Carta
    {
        private readonly Random _random;

        protected Baralho(Random? random = null)
        {
            _random = random ?? new Random();
        }

        private List<T> MonteCompra { get; } = [];
        private List<T> MonteDescarte { get; } = [];

        private bool TemCarta()
        {
            return MonteCompra.Count > 0;
        }

        protected void InicializarMonteCompra(IEnumerable<T> cartas)
        {
            AdicionarCartasAoMonteCompra(cartas);
            EmbaralharCartas();
        }

        protected void AdicionarCartasAoMonteCompra(IEnumerable<T> cartas)
        {
            MonteCompra.AddRange(cartas);
        }

        protected T? ComprarCartaPorIndice(int indice)
        {
            if (!IndiceValido(indice))
            {
                return null;
            }

            T carta = MonteCompra[indice];
            MonteCompra.RemoveAt(indice);
            return carta;
        }

        protected T? ObterCartaPorIndice(int indice)
        {
            if (!IndiceValido(indice))
            {
                return null;
            }

            return MonteCompra[indice];
        }

        private bool IndiceValido(int indice) => indice >= 0 && indice < MonteCompra.Count;

        public List<T> Comprar(int quantidade)
        {
            List<T> cartas = [];
            for (int i = 0; i < quantidade; i++)
            {
                T? carta = Comprar();
                if (carta != null)
                {
                    cartas.Add(carta);
                }
                else
                {
                    break;
                }
            }
            return cartas;
        }

        private T? Comprar()
        {
            if (!TentarComprarCartasDisponiveis())
            {
                return null;
            }

            return RemoverPrimeiraCarta();
        }

        private bool TentarComprarCartasDisponiveis()
        {
            if (TemCarta())
            {
                return true;
            }

            if (!TemCartaNoDescarte())
            {
                return false;
            }

            Embaralhar();
            return true;
        }

        private bool TemCartaNoDescarte() => MonteDescarte.Count > 0;

        private T RemoverPrimeiraCarta()
        {
            T carta = MonteCompra[0];
            MonteCompra.RemoveAt(0);
            return carta;
        }

        public void Descartar(IEnumerable<T> cartas)
        {
            MonteDescarte.AddRange(cartas);
        }

        public void Descartar(T carta)
        {
            MonteDescarte.Add(carta);
        }

        protected void Embaralhar()
        {
            RenovarMonteCompra();
            EmbaralharCartas();
        }

        private void RenovarMonteCompra()
        {
            MonteCompra.AddRange(MonteDescarte);
            MonteDescarte.Clear();
        }

        private void EmbaralharCartas()
        {
            for (int i = MonteCompra.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (MonteCompra[i], MonteCompra[j]) = (MonteCompra[j], MonteCompra[i]);
            }
        }

        protected int ObterTamanhoMonte()
        {
            return MonteCompra.Count;
        }
    }
}