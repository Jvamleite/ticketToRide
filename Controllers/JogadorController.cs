using Microsoft.AspNetCore.Mvc;
using TicketToRide.Application.DTOs;
using TicketToRide.Application.DTOs.Request;
using TicketToRide.Application.Services.Interfaces;

namespace TicketToRide.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogadorController : ControllerBase
    {
        private readonly IJogadorService _jogadorService;

        public JogadorController(IJogadorService jogadorService)
        {
            _jogadorService = jogadorService;
        }

        [HttpPost("partida/{partidaId}/jogador")]
        public ActionResult<JogadorDTO> AdicionarJogador(string partidaId, [FromBody] AdicionarJogadorRequest request)
        {
            JogadorDTO jogador = _jogadorService.AdicionarJogador(partidaId, request.Nome);

            return Ok(jogador);
        }

        [HttpGet("partida/{partidaId}/jogador/{jogadorId}")]
        public ActionResult<JogadorDTO> ObterJogador(string partidaId, string jogadorId)
        {
            JogadorDTO? jogador = _jogadorService.ObterJogador(partidaId, jogadorId);

            return Ok(jogador);
        }

        [HttpGet("partida/{partidaId}/jogadores")]
        public ActionResult<List<JogadorDTO>> ObterJogadores(string partidaId)
        {
            List<JogadorDTO> jogadores = _jogadorService.ObterJogadores(partidaId);
            return Ok(jogadores);
        }

        [HttpDelete("partida/{partidaId}/jogador/{jogadorId}")]
        public ActionResult RemoverJogador(string partidaId, string jogadorId)
        {
            _jogadorService.RemoverJogador(partidaId, jogadorId);

            return Ok(new { message = "Jogador removido com sucesso" });
        }

        [HttpGet("partida/{partidaId}/ranking")]
        public ActionResult<List<JogadorDTO>> ObterRanking(string partidaId)
        {
            List<JogadorDTO> ranking = _jogadorService.ObterRanking(partidaId);
            return Ok(ranking);
        }
    }
}