using TicketToRide.Application.DTOs;
using TicketToRide.Domain.Enums;

namespace TicketToRide.Domain.Entities
{
    public class Rota
    {
        public string Id { get; }
        public Cidade Origem { get; }
        public Cidade Destino { get; }
        public Cor Cor { get; }
        public int Tamanho { get; }
        private bool Dupla { get; }
        public bool Disponivel { get; } = true;

        public Rota(string id, Cidade origem, Cidade destino, Cor cor, int tamanho, bool ehDupla = false)
        {
            Id = id;
            Origem = origem;
            Destino = destino;
            Cor = cor;
            Tamanho = tamanho;
            Dupla = ehDupla;
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

        public bool PodeSerConquistadaCom(CartaVeiculo carta)
        {
            return carta.PodeSerUsadaPara(Cor);
        }

        public RotaDTO MapearParaDTO()
        {
            return new RotaDTO
            {
                Id = Id,
                Origem = Origem.Nome,
                Destino = Destino.Nome,
                Cor = Cor,
                Tamanho = Tamanho,
                EhDupla = Dupla,
                EstaDisponivel = Disponivel,
                Pontos = CalcularPontos()
            };
        }
    }
}