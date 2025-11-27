namespace TicketToRide.Domain.Entities
{
    public class BaralhoCartasDestino : Baralho<BilheteDestino>
    {
        private const int QuantidadeOpcoesSelecionaveis = 3;

        public BaralhoCartasDestino(IEnumerable<BilheteDestino> bilhetes)
        {
            InicializarMonteCompra(bilhetes);
        }

        public IEnumerable<BilheteDestino> ListarOpcoesSelecionaveis()
        {
            List<BilheteDestino> opcoes = [];
            for (int i = 0; i < QuantidadeOpcoesSelecionaveis; i++)
            {
                opcoes.Add(ObterCartaPorIndice(i));
            }

            return opcoes;
        }

        public BilheteDestino? ComprarCartaDestino(int indice)
        {
            return ComprarCartaPorIndice(indice);
        }

        public void DescartarNaoEscolhidas(IEnumerable<int> indicesEscolhidos)
        {
            foreach (int indice in ObterIndicesNaoEscolhidos(indicesEscolhidos))
            {
                BilheteDestino? carta = ObterCartaPorIndice(indice);

                if (carta is not null)
                {
                    Descartar(carta);
                }
            }
        }

        private static IEnumerable<int> ObterIndicesNaoEscolhidos(IEnumerable<int> indicesEscolhidos)
        {
            HashSet<int> escolhidos = [.. indicesEscolhidos];
            return Enumerable.Range(0, QuantidadeOpcoesSelecionaveis)
                             .Where(i => !escolhidos.Contains(i));
        }
    }
}