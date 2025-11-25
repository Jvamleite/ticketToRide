using TicketToRide.Domain.Enums;

namespace TicketToRide.Domain.Entities
{
    public class Turno
    {
        public int Numero { get; }
        public Jogador JogadorAtual { get; }
        public Acao? AcaoRealizada { get; private set; }
        public bool AcaoCompletada => AcaoRealizada is not null;

        public Turno(int numero, Jogador jogadorAtual)
        {
            Numero = numero;
            JogadorAtual = jogadorAtual;
        }

        public void ExecutarAcao(Acao acao)
        {
            AcaoRealizada = acao;
        }

        public bool PodeExecutarAcao()
        {
            return !AcaoCompletada;
        }
    }
}