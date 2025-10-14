using Microsoft.AspNetCore.Mvc;
using TicketToRide.Application.Services;
using TicketToRide.Application.DTOs;

namespace TicketToRide.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartidaController : ControllerBase
    {
        private readonly PartidaService _partidaService;

        public PartidaController(PartidaService partidaService)
        {
            _partidaService = partidaService;
        }

        [HttpPost("criar")]
        public ActionResult<PartidaDTO> CriarPartida()
        {
            try
            {
                var partida = _partidaService.CriarPartida();
                return Ok(partida);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<PartidaDTO> ObterPartida(string id)
        {
            try
            {
                var partida = _partidaService.ObterPartida(id);
                if (partida == null)
                {
                    return NotFound(new { message = "Partida não encontrada" });
                }
                return Ok(partida);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/iniciar")]
        public ActionResult<PartidaDTO> IniciarPartida(string id, [FromBody] IniciarPartidaRequest request)
        {
            try
            {
                var partida = _partidaService.IniciarPartida(id, request.NumJogadores);
                return Ok(partida);
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

        [HttpPost("{id}/finalizar")]
        public ActionResult<PartidaDTO> FinalizarPartida(string id)
        {
            try
            {
                var partida = _partidaService.FinalizarPartida(id);
                return Ok(partida);
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

        [HttpGet("{id}/pontuacao")]
        public ActionResult<object> ObterPontuacao(string id)
        {
            try
            {
                var partida = _partidaService.ObterPartida(id);
                if (partida == null)
                {
                    return NotFound(new { message = "Partida não encontrada" });
                }

                var ranking = partida.Jogadores.OrderByDescending(j => j.Pontuacao).ToList();
                var vencedor = ranking.FirstOrDefault();

                return Ok(new
                {
                    ranking = ranking.Select((j, index) => new
                    {
                        posicao = index + 1,
                        jogador = j.Nome,
                        pontos = j.Pontuacao,
                        rotas = j.NumeroRotas,
                        bilhetes = j.NumeroBilhetes
                    }),
                    vencedor = vencedor != null ? new
                    {
                        nome = vencedor.Nome,
                        pontos = vencedor.Pontuacao
                    } : null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult<List<PartidaDTO>> ObterTodasPartidas()
        {
            try
            {
                var partidas = _partidaService.ObterTodasPartidas();
                return Ok(partidas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class IniciarPartidaRequest
    {
        public int NumJogadores { get; set; }
    }
}
