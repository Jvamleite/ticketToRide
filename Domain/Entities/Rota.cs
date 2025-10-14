using TicketToRide.Domain.Enums;

namespace TicketToRide.Domain.Entities
{
    public class Rota
    {
        public string Id { get; set; } = string.Empty;
        public Cidade Origem { get; set; }
        public Cidade Destino { get; set; }
        public Cor Cor { get; set; }
        public int Tamanho { get; set; }
        public bool EhDupla { get; set; }
        public Jogador? Jogador { get; set; } // null = disponível

        public Rota(string id, Cidade origem, Cidade destino, Cor cor, int tamanho, bool ehDupla = false)
        {
            Id = id;
            Origem = origem;
            Destino = destino;
            Cor = cor;
            Tamanho = tamanho;
            EhDupla = ehDupla;
        }

        public int CalcularPontos()
        {
            return Tamanho switch
            {
                1 => 1,
                2 => 2,
                3 => 4,
                4 => 7,
                5 => 10,
                6 => 15,
                _ => 0
            };
        }

        public bool EstaDisponivel()
        {
            return Jogador == null;
        }

        public bool PodeSerConquistadaPor(Jogador jogador)
        {
            if (!EstaDisponivel())
            {
                return false;
            }

            // Verificar se o jogador tem cartas suficientes da cor correta
            var cartasNecessarias = jogador.ObterCartasParaCor(Cor);
            return cartasNecessarias.Count >= Tamanho;
        }

        public bool Conquistar(Jogador jogador)
        {
            if (!PodeSerConquistadaPor(jogador))
            {
                return false;
            }

            Jogador = jogador;
            return true;
        }

        public string GetId()
        {
            return Id;
        }

        public int GetTamanho()
        {
            return Tamanho;
        }

        public override string ToString()
        {
            var status = EstaDisponivel() ? "Disponível" : $"Conquistada por {Jogador?.Nome}";
            return $"{Origem.Nome} → {Destino.Nome} ({Cor}, {Tamanho} vagões) - {status}";
        }
    }
}
