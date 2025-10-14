using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Interfaces;
using TicketToRide.Application.DTOs;
using TicketToRide.Application;

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
            var partida = _partidaRepository.CriarPartida();
            InicializarDadosJogo(partida);
            _partidaRepository.SalvarPartida(partida);
            return MapearParaDTO(partida);
        }

        public PartidaDTO? ObterPartida(string id)
        {
            var partida = _partidaRepository.ObterPartida(id);
            return partida != null ? MapearParaDTO(partida) : null;
        }

        public PartidaDTO IniciarPartida(string id, int numJogadores)
        {
            var partida = _partidaRepository.ObterPartida(id);
            if (partida == null)
            {
                throw new ArgumentException("Partida não encontrada");
            }

            partida.IniciarPartida(numJogadores);
            _partidaRepository.SalvarPartida(partida);
            return MapearParaDTO(partida);
        }

        public PartidaDTO FinalizarPartida(string id)
        {
            var partida = _partidaRepository.ObterPartida(id);
            if (partida == null)
            {
                throw new ArgumentException("Partida não encontrada");
            }

            partida.FinalizarPartida();
            _partidaRepository.SalvarPartida(partida);
            return MapearParaDTO(partida);
        }

        public List<PartidaDTO> ObterTodasPartidas()
        {
            var partidas = _partidaRepository.ObterTodasPartidas();
            return partidas.Select(MapearParaDTO).ToList();
        }

        public bool ExistePartida(string id)
        {
            return _partidaRepository.ExistePartida(id);
        }

        private void InicializarDadosJogo(Partida partida)
        {
            // Inicializar tabuleiro com rotas
            var rotas = DadosJogo.ObterRotas();
            partida.Tabuleiro.AdicionarRotas(rotas);

            // Inicializar baralho de bilhetes de destino
            var bilhetes = DadosJogo.ObterBilhetesDestino();
            partida.BaralhoCartasDestino.AdicionarBilhetes(bilhetes);
        }

        private PartidaDTO MapearParaDTO(Partida partida)
        {
            return new PartidaDTO
            {
                Id = partida.Id,
                Jogadores = partida.Jogadores.Select(MapearJogadorParaDTO).ToList(),
                Rotas = partida.Tabuleiro.Rotas.Select(MapearRotaParaDTO).ToList(),
                TurnoAtual = partida.TurnoAtual != null ? MapearTurnoParaDTO(partida.TurnoAtual) : null,
                PartidaIniciada = partida.PartidaIniciada,
                PartidaFinalizada = partida.PartidaFinalizada,
                DataCriacao = partida.DataCriacao,
                DataInicio = partida.DataInicio,
                DataFim = partida.DataFim,
                NumeroJogadores = partida.ObterNumeroJogadores(),
                PodeIniciar = partida.PodeIniciar()
            };
        }

        private JogadorDTO MapearJogadorParaDTO(Jogador jogador)
        {
            return new JogadorDTO
            {
                Id = jogador.Id,
                Nome = jogador.Nome,
                Pontuacao = jogador.Pontuacao,
                PecasTremRestante = jogador.PecasTremRestante,
                MaoCartas = jogador.MaoCartas.Select(MapearCartaParaDTO).ToList(),
                BilhetesDestino = jogador.BilhetesDestino.Select(MapearBilheteParaDTO).ToList(),
                RotasConquistadas = jogador.RotasConquistadas.Select(MapearRotaParaDTO).ToList(),
                NumeroCartas = jogador.MaoCartas.Count,
                NumeroBilhetes = jogador.BilhetesDestino.Count,
                NumeroRotas = jogador.RotasConquistadas.Count
            };
        }

        private RotaDTO MapearRotaParaDTO(Rota rota)
        {
            return new RotaDTO
            {
                Id = rota.Id,
                Origem = rota.Origem.Nome,
                Destino = rota.Destino.Nome,
                Cor = rota.Cor,
                Tamanho = rota.Tamanho,
                EhDupla = rota.EhDupla,
                JogadorId = rota.Jogador?.Id,
                JogadorNome = rota.Jogador?.Nome,
                EstaDisponivel = rota.EstaDisponivel(),
                Pontos = rota.CalcularPontos()
            };
        }

        private CartaDTO MapearCartaParaDTO(CartaVeiculo carta)
        {
            return new CartaDTO
            {
                Nome = carta.Nome,
                Cor = carta.Cor,
                EhLocomotiva = carta.EhLocomotiva,
                Descricao = carta.ToString()
            };
        }

        private BilheteDestinoDTO MapearBilheteParaDTO(BilheteDestino bilhete)
        {
            return new BilheteDestinoDTO
            {
                Origem = bilhete.Origem.Nome,
                Destino = bilhete.Destino.Nome,
                Pontos = bilhete.Pontos,
                IsCompleto = false, // Será calculado no contexto da partida
                Descricao = bilhete.ToString()
            };
        }

        private TurnoDTO MapearTurnoParaDTO(Turno turno)
        {
            return new TurnoDTO
            {
                Numero = turno.Numero,
                JogadorId = turno.JogadorAtual.Id,
                JogadorNome = turno.JogadorAtual.Nome,
                AcaoRealizada = turno.AcaoRealizada,
                AcaoCompletada = turno.AcaoCompletada,
                PodeExecutarAcao = turno.PodeExecutarAcao()
            };
        }
    }
}
