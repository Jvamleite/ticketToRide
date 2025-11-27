using TicketToRide.Application.DTOs;

namespace TicketToRide.Application.Services.Interfaces
{
    public interface IJogadorService
    {
        JogadorDTO AdicionarJogador(string partidaId, string nome);

        JogadorDTO? ObterJogador(string partidaId, string jogadorId);

        List<JogadorDTO> ObterJogadores(string partidaId);

        void RemoverJogador(string partidaId, string jogadorId);

        List<JogadorDTO> ObterRanking(string partidaId);
    }
}