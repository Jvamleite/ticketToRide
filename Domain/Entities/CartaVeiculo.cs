using TicketToRide.Domain.Enums;

namespace TicketToRide.Domain.Entities
{
    public class CartaVeiculo : Carta
    {
        public Cor Cor { get; set; }
        public bool EhLocomotiva { get; set; }

        public CartaVeiculo(Cor cor, bool ehLocomotiva = false) : base(GetNomeCarta(cor, ehLocomotiva))
        {
            Cor = cor;
            EhLocomotiva = ehLocomotiva;
        }

        private static string GetNomeCarta(Cor cor, bool ehLocomotiva)
        {
            if (ehLocomotiva)
            {
                return "Locomotiva";
            }

            return cor switch
            {
                Cor.VERMELHO => "Vagão Vermelho",
                Cor.AZUL => "Vagão Azul",
                Cor.VERDE => "Vagão Verde",
                Cor.AMARELO => "Vagão Amarelo",
                Cor.PRETO => "Vagão Preto",
                Cor.BRANCO => "Vagão Branco",
                Cor.LARANJA => "Vagão Laranja",
                Cor.ROSA => "Vagão Rosa",
                _ => "Vagão"
            };
        }

        public bool PodeSerUsadaPara(Cor corRota)
        {
            return EhLocomotiva || Cor == corRota;
        }

        public override string ToString()
        {
            return EhLocomotiva ? "Locomotiva (Coringa)" : $"{Cor}";
        }
    }
}
