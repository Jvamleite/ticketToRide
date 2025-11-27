using TicketToRide.Application.DTOs;

namespace TicketToRide.Domain.Entities
{
    public class BilheteDestino : Carta
    {
        public Cidade Origem { get; }
        public Cidade Destino { get; }
        public int Pontos { get; }
        private bool IsCompleto { get; set; }

        public BilheteDestino(Cidade origem, Cidade destino, int pontos) : base($"{origem.ObterNomeFormatado(destino)}")
        {
            Origem = origem;
            Destino = destino;
            Pontos = pontos;
        }

        public bool EstaCompleto()
        {
            return IsCompleto;
        }

        public void AtualizarBilhete(IEnumerable<Rota> rotasJogador)
        {
            if (ExisteCaminho(rotasJogador))
            {
                IsCompleto = true;
            }
        }

        private bool ExisteCaminho(IEnumerable<Rota> rotas)
        {
            var visitadas = new HashSet<Cidade> { Origem };
            var fila = new Queue<Cidade>();
            fila.Enqueue(Origem);

            while (fila.Count > 0)
            {
                var cidadeAtual = fila.Dequeue();

                if (ProcessarVizinhos(rotas, cidadeAtual, visitadas, fila))
                    return true;
            }

            return false;
        }

        private bool ProcessarVizinhos(IEnumerable<Rota> rotas, Cidade cidadeAtual,
            HashSet<Cidade> visitadas, Queue<Cidade> fila)
        {
            foreach (var rota in rotas)
            {
                var vizinho = rota.ObterCidadeVizinha(cidadeAtual);

                if (vizinho != null && visitadas.Add(vizinho))
                {
                    if (vizinho.Equals(Destino))
                        return true;

                    fila.Enqueue(vizinho);
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