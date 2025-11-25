using TicketToRide.Application.DTOs;
using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;

namespace TicketToRide.Application.Services
{
    public class PartidaService
    {
        private readonly IPartidaRepository _partidaRepository;

        public PartidaService(IPartidaRepository partidaRepository)
        {
            _partidaRepository = partidaRepository;
        }

        public PartidaDTO CriarPartida()
        {
            Partida partida = _partidaRepository.CriarPartida();
            _partidaRepository.SalvarPartida(partida);
            return partida.MapearParaDTO();
        }

        public PartidaDTO? ObterPartida(string id)
        {
            Partida? partida = _partidaRepository.ObterPartida(id);
            return partida?.MapearParaDTO();
        }

        public PartidaDTO IniciarPartida(string id, int numJogadores)
        {
            Partida? partida = _partidaRepository.ObterPartida(id);
            if (partida == null)
            {
                throw new ArgumentException("Partida não encontrada");
            }

            partida.IniciarPartida(numJogadores);
            _partidaRepository.SalvarPartida(partida);
            return partida.MapearParaDTO();
        }

        public PartidaDTO FinalizarPartida(string id)
        {
            Partida? partida = _partidaRepository.ObterPartida(id);
            if (partida == null)
            {
                throw new ArgumentException("Partida não encontrada");
            }

            partida.FinalizarPartida();
            _partidaRepository.SalvarPartida(partida);
            return partida.MapearParaDTO();
        }

        public List<PartidaDTO> ObterTodasPartidas()
        {
            List<Partida> partidas = _partidaRepository.ObterTodasPartidas();
            return partidas.Select(p => p.MapearParaDTO()).ToList();
        }

        public bool ExistePartida(string id)
        {
            return _partidaRepository.ExistePartida(id);
        }
    }
}