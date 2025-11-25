namespace TicketToRide.Domain.Entities
{
    public class BilheteDestino : Carta
    {
        public Cidade Origem { get; }
        public Cidade Destino { get; }
        public int Pontos { get; }

        public BilheteDestino(Cidade origem, Cidade destino, int pontos) : base($"{origem.ObterNomeFormatado(destino)}")
        {
            Origem = origem;
            Destino = destino;
            Pontos = pontos;
        }

        public bool EstaCompleto(List<Rota> rotasJogador)
        {
            return ExisteCaminho(Origem, Destino, rotasJogador);
        }

        private static bool ExisteCaminho(Cidade origem, Cidade destino, List<Rota> rotas)
        {
            if (origem.Equals(destino))
            {
                return true;
            }

            HashSet<Cidade> visitadas = [];
            Queue<Cidade> fila = new();
            fila.Enqueue(origem);
            visitadas.Add(origem);

            while (fila.Count > 0)
            {
                Cidade cidadeAtual = fila.Dequeue();

                foreach (Rota rota in rotas)
                {
                    Cidade? proximaCidade = null;

                    if (rota.Origem.Equals(cidadeAtual))
                    {
                        proximaCidade = rota.Destino;
                    }
                    else if (rota.Destino.Equals(cidadeAtual))
                    {
                        proximaCidade = rota.Origem;
                    }

                    if (proximaCidade != null && !visitadas.Contains(proximaCidade))
                    {
                        if (proximaCidade.Equals(destino))
                        {
                            return true;
                        }

                        visitadas.Add(proximaCidade);
                        fila.Enqueue(proximaCidade);
                    }
                }
            }

            return false;
        }

        public override string ToString()
        {
            return $"{Origem.Nome} → {Destino.Nome} ({Pontos} pontos)";
        }
    }
}