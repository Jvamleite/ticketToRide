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
            HashSet<Cidade> visitadas = [];
            Queue<Cidade> fila = new();
            fila.Enqueue(Origem);
            visitadas.Add(Origem);

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
                        if (proximaCidade.Equals(Destino))
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

        public new BilheteDestinoDTO MapearParaDTO()
        {
            return new BilheteDestinoDTO
            {
                Origem = Origem.Nome,
                Destino = Destino.Nome,
                Pontos = Pontos,
                IsCompleto = false,
                Descricao = ToString()
            };
        }
    }
}