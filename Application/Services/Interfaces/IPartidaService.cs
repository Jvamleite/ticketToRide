using TicketToRide.Application.DTOs;

namespace TicketToRide.Application.Services.Interfaces
{
    public interface IPartidaService
    {
        PartidaDTO CriarPartida();

        PartidaDTO ObterPartida(string id);

        PartidaDTO IniciarPartida(string id);

        PartidaDTO FinalizarPartida(string id);

        List<PartidaDTO> ObterTodasPartidas();

        PontuacaoDTO ObterPontuacao(string partidaId);
    }
}