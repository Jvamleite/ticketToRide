namespace TicketToRide.Domain.Entities
{
    public class BaralhoCartasDestino : Baralho<BilheteDestino>
    {
        public BaralhoCartasDestino()
        {
            InicializarBaralho();
        }

        private void InicializarBaralho()
        {
            MonteCompra.Clear();
            MonteDescarte.Clear();

            // Os bilhetes de destino serão inicializados pela classe DadosJogo
            // Esta classe apenas gerencia o baralho
        }

        public void AdicionarBilhetes(List<BilheteDestino> bilhetes)
        {
            MonteCompra.AddRange(bilhetes);
            Embaralhar();
        }

        public List<BilheteDestino> ComprarBilhetes(int quantidade = 3)
        {
            return ComprarVarias(quantidade);
        }

        public void DevolverBilhetes(List<BilheteDestino> bilhetes)
        {
            foreach (var bilhete in bilhetes)
            {
                Descartar(bilhete);
            }
        }
    }
}
