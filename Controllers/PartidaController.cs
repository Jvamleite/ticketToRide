using Microsoft.AspNetCore.Mvc;
using TicketToRide.Application.DTOs;
using TicketToRide.Application.Services.Interfaces;

namespace TicketToRide.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartidaController : ControllerBase
    {
        private readonly IPartidaService _partidaService;

        public PartidaController(IPartidaService partidaService)
        {
            _partidaService = partidaService;
        }

        [HttpPost("criar")]
        public ActionResult<PartidaDTO> CriarPartida()
        {
            PartidaDTO partida = _partidaService.CriarPartida();
            return Ok(partida);
        }

        [HttpGet("{id}")]
        public ActionResult<PartidaDTO> ObterPartida(string id)
        {
            PartidaDTO partida = _partidaService.ObterPartida(id);

            return Ok(partida);
        }

        [HttpPost("{id}/iniciar")]
        public ActionResult<PartidaDTO> IniciarPartida(string id, [FromBody] IniciarPartidaRequest request)
        {
            PartidaDTO partida = _partidaService.IniciarPartida(id);
            return Ok(partida);
        }

        [HttpPost("{id}/finalizar")]
        public ActionResult<PartidaDTO> FinalizarPartida(string id)
        {
            PartidaDTO partida = _partidaService.FinalizarPartida(id);
            return Ok(partida);
        }

        [HttpGet("{id}/pontuacao")]
        public ActionResult<object> ObterPontuacao(string id)
        {
            PontuacaoDTO pontuacao = _partidaService.ObterPontuacao(id);

            return Ok(pontuacao);
        }

        [HttpGet]
        public ActionResult<List<PartidaDTO>> ObterTodasPartidas()
        {
            List<PartidaDTO> partidas = _partidaService.ObterTodasPartidas();
            return Ok(partidas);
        }
    }
}