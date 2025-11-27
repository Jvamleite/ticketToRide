using Microsoft.AspNetCore.Mvc;
using TicketToRide.Application.Services;
using TicketToRide.Application.DTOs;

namespace TicketToRide.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogadorController : ControllerBase
    {
        private readonly JogadorService _jogadorService;

        public JogadorController(JogadorService jogadorService)
        {
            _jogadorService = jogadorService;
        }

        [HttpPost("partida/{partidaId}/jogador")]
        public ActionResult<JogadorDTO> AdicionarJogador(string partidaId, [FromBody] AdicionarJogadorRequest request)
        {
            try
            {
                var jogador = _jogadorService.AdicionarJogador(partidaId, request.Nome);
                return Ok(jogador);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("partida/{partidaId}/jogador/{jogadorId}")]
        public ActionResult<JogadorDTO> ObterJogador(string partidaId, string jogadorId)
        {
            try
            {
                var jogador = _jogadorService.ObterJogador(partidaId, jogadorId);
                if (jogador == null)
                {
                    return NotFound(new { message = "Jogador não encontrado" });
                }
                return Ok(jogador);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("partida/{partidaId}/jogadores")]
        public ActionResult<List<JogadorDTO>> ObterJogadores(string partidaId)
        {
            try
            {
                var jogadores = _jogadorService.ObterJogadores(partidaId);
                return Ok(jogadores);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("partida/{partidaId}/jogador/{jogadorId}")]
        public ActionResult RemoverJogador(string partidaId, string jogadorId)
        {
            try
            {
                var removido = _jogadorService.RemoverJogador(partidaId, jogadorId);
                if (!removido)
                {
                    return NotFound(new { message = "Jogador não encontrado" });
                }
                return Ok(new { message = "Jogador removido com sucesso" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("partida/{partidaId}/ranking")]
        public ActionResult<List<JogadorDTO>> ObterRanking(string partidaId)
        {
            try
            {
                var ranking = _jogadorService.ObterRanking(partidaId);
                return Ok(ranking);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class AdicionarJogadorRequest
    {
        public string Nome { get; set; }
    }
}