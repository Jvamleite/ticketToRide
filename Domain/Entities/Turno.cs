using TicketToRide.Domain.Enums;

namespace TicketToRide.Domain.Entities
{
    public class Turno
    {
        public int Numero { get; set; }
        public Jogador JogadorAtual { get; set; }
        public Acao? AcaoRealizada { get; set; }
        public bool AcaoCompletada { get; set; } = false;

        public Turno(int numero, Jogador jogadorAtual)
        {
            Numero = numero;
            JogadorAtual = jogadorAtual;
        }

        public Jogador GetJogadorAtual()
        {
            return JogadorAtual;
        }

        public void ExecutarAcao(Acao acao)
        {
            AcaoRealizada = acao;
            AcaoCompletada = true;
        }

        public bool PodeExecutarAcao()
        {
            return !AcaoCompletada;
        }

        public void ResetarAcao()
        {
            AcaoRealizada = null;
            AcaoCompletada = false;
        }

        public override string ToString()
        {
            var status = AcaoCompletada ? $"Ação: {AcaoRealizada}" : "Aguardando ação";
            return $"Turno {Numero} - {JogadorAtual.Nome} - {status}";
        }
    }
}
