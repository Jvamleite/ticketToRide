using TicketToRide.Application.DTOs;
using TicketToRide.Domain.Enums;

namespace TicketToRide.Domain.Entities
{
    public class Turno
    {
        public int Numero { get; }
        private Jogador JogadorAtual { get; }
        public Acao? AcaoRealizada { get; private set; }
        public bool AcaoCompletada => AcaoRealizada is not null;

        public Turno(int numero, Jogador jogadorAtual)
        {
            Numero = numero;
            JogadorAtual = jogadorAtual;
        }

        public void SalvarAcaoRealizada(Acao acao)
        {
            AcaoRealizada = acao;
        }

        public bool PodeExecutarAcao()
        {
            return !AcaoCompletada;
        }

        public Jogador ObterJogadorAtual()
        {
            return JogadorAtual;
        }

        public TurnoDTO MapearParaDTO()
        {
            return new TurnoDTO
            {
                Numero = Numero,
                JogadorId = JogadorAtual.Id,
                JogadorNome = JogadorAtual.Nome,
                AcaoRealizada = AcaoRealizada,
                AcaoCompletada = AcaoCompletada,
                PodeExecutarAcao = PodeExecutarAcao()
            };
        }
    }
}