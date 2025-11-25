using TicketToRide.Domain.Enums;
using TicketToRideAPI.Application.DTOs;
using TicketToRideAPI.Domain;

namespace TicketToRide.Domain.Entities
{
    public class CartaVeiculo : Carta
    {
        public Cor Cor { get; }

        public CartaVeiculo(Cor cor) : base(cor.GetEnumDescription())
        {
            Cor = cor;
        }

        public bool PodeSerUsadaPara(Cor corRota)
        {
            return Cor == Cor.LOCOMOTIVA || Cor == corRota;
        }

        public override string ToString()
        {
            return Cor.GetEnumDescription();
        }

        public CartaVeiculoDTO MapearParaDTO()
        {
            return new CartaVeiculoDTO
            {
                Nome = Nome,
                Cor = Cor.GetEnumDescription()
            };
        }
    }
}