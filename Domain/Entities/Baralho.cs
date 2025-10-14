using TicketToRide.Domain.Entities;

namespace TicketToRide.Domain.Entities
{
    public abstract class Baralho<T> where T : Carta
    {
        protected List<T> MonteCompra { get; set; } = new();
        protected List<T> MonteDescarte { get; set; } = new();

        public bool TemCarta()
        {
            return MonteCompra.Count > 0;
        }

        public T? Comprar()
        {
            if (MonteCompra.Count == 0)
            {
                if (MonteDescarte.Count > 0)
                {
                    Embaralhar();
                }
                else
                {
                    return null; // Baralho vazio
                }
            }

            var carta = MonteCompra[0];
            MonteCompra.RemoveAt(0);
            return carta;
        }

        public void Descartar(T carta)
        {
            MonteDescarte.Add(carta);
        }

        public void Embaralhar()
        {
            // Mover cartas do descarte para o monte de compra
            MonteCompra.AddRange(MonteDescarte);
            MonteDescarte.Clear();

            // Embaralhar usando Fisher-Yates
            var random = new Random();
            for (int i = MonteCompra.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (MonteCompra[i], MonteCompra[j]) = (MonteCompra[j], MonteCompra[i]);
            }
        }

        public List<T> ComprarVarias(int quantidade)
        {
            var cartas = new List<T>();
            for (int i = 0; i < quantidade; i++)
            {
                var carta = Comprar();
                if (carta != null)
                {
                    cartas.Add(carta);
                }
                else
                {
                    break; // Baralho vazio
                }
            }
            return cartas;
        }

        public int QuantidadeRestante()
        {
            return MonteCompra.Count;
        }

        public int QuantidadeDescarte()
        {
            return MonteDescarte.Count;
        }
    }
}
