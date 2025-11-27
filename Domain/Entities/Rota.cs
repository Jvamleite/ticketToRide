using TicketToRide.Domain.Enums;

namespace TicketToRide.Domain.Entities
{
    public class Rota
    {
        private static readonly Dictionary<int, int> PontosPorTamanho = new()
        {
            { 1, 1 }, { 2, 2 }, { 3, 4 }, { 4, 7 }, { 5, 10 }, { 6, 15 }
        };

        public string Id { get; }
        public Cidade Origem { get; }
        public Cidade Destino { get; }
        public Cor Cor { get; }
        public int Tamanho { get; }
        public bool Dupla { get; }
        public bool Disponivel { get; private set; } = true;

        public record RotaConfig(Cidade Origem, Cidade Destino, Cor Cor, int Tamanho, bool EhDupla = false);

        public Rota(string id, RotaConfig config)
        {
            Id = id;
            Origem = config.Origem;
            Destino = config.Destino;
            Cor = config.Cor;
            Tamanho = config.Tamanho;
            Dupla = config.EhDupla;
        }

        public int CalcularPontos() => PontosPorTamanho.GetValueOrDefault(Tamanho, 0);

        public bool PodeSerConquistadaCom(CartaVeiculo carta)
        {
            return carta.PodeSerUsadaPara(Cor);
        }

        public Cidade? ObterCidadeVizinha(Cidade cidade)
        {
            if (Origem.Equals(cidade))
            {
                return Destino;
            }

            if (Destino.Equals(cidade))
            {
                return Origem;
            }

            return null;
        }

        public void ConquistarRota()
        {
            Disponivel = false;
        }
    }
}