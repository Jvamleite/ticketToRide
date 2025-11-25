namespace TicketToRide.Domain.Entities
{
    public class BaralhoCartasDestino : Baralho<BilheteDestino>
    {
        public BaralhoCartasDestino(IEnumerable<BilheteDestino> bilhetes)
        {
            InicializarMonteCompra(bilhetes);
        }

        public IEnumerable<BilheteDestino> ListarOpcoesSelecionaveis()
        {
            List<BilheteDestino> opcoes = [];
            for (int i = 0; i < 3; i++)
            {
                opcoes.Add(ObterCartaPorIndice(i));
            }

            return opcoes;
        }

        public BilheteDestino? ComprarCartaDestino(int indice)
        {
            if (indice < 0 || indice >= ObterTamanhoMonte())
            {
                return null;
            }

            return ComprarCartaPorIndice(indice);
        }

        public void DescartarNaoEscolhidas(IEnumerable<int> indiceCartasEscolhidas)
        {
            foreach (int indice in Enumerable.Range(0, 3).Where(i => !indiceCartasEscolhidas.Contains(i)))
            {
                Descartar(ObterCartaPorIndice(indice));
            }
        }
    }
}