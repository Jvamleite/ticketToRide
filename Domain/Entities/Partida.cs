using TicketToRide.Domain.Enums;

namespace TicketToRide.Domain.Entities
{
    public class Partida
    {
        public string Id { get; set; } = string.Empty;
        public List<Jogador> Jogadores { get; set; } = new();
        public Tabuleiro Tabuleiro { get; set; }
        public BaralhoCartasDestino BaralhoCartasDestino { get; set; }
        public BaralhoCartasVeiculo BaralhoCartasVeiculo { get; set; }
        public Turno? TurnoAtual { get; set; }
        public bool PartidaIniciada { get; set; } = false;
        public bool PartidaFinalizada { get; set; } = false;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public Partida()
        {
            Tabuleiro = new Tabuleiro();
            BaralhoCartasDestino = new BaralhoCartasDestino();
            BaralhoCartasVeiculo = new BaralhoCartasVeiculo();
        }

        public Partida IniciarPartida(int numJogadores)
        {
            if (numJogadores < 2 || numJogadores > 5)
            {
                throw new ArgumentException("Número de jogadores deve ser entre 2 e 5");
            }

            if (Jogadores.Count != numJogadores)
            {
                throw new InvalidOperationException($"Número de jogadores não corresponde ao esperado. Esperado: {numJogadores}, Atual: {Jogadores.Count}");
            }

            PartidaIniciada = true;
            DataInicio = DateTime.Now;

            // Distribuir cartas iniciais para cada jogador
            foreach (var jogador in Jogadores)
            {
                var cartasIniciais = BaralhoCartasVeiculo.ComprarVarias(4);
                jogador.ComprarCartas(cartasIniciais);
            }

            // Criar primeiro turno
            TurnoAtual = new Turno(1, Jogadores[0]);

            return this;
        }

        public void FinalizarPartida()
        {
            if (!PartidaIniciada)
            {
                throw new InvalidOperationException("Partida não foi iniciada");
            }

            PartidaFinalizada = true;
            DataFim = DateTime.Now;

            // Calcular pontuação final de todos os jogadores
            CalcularPontuacaoFinal();
        }

        public void CalcularPontuacaoFinal()
        {
            foreach (var jogador in Jogadores)
            {
                jogador.AtualizarPontuacao();
            }

            // Adicionar bônus da rota contínua mais longa
            var vencedorRotaLonga = CalcularRotaMaisLonga();
            if (vencedorRotaLonga != null)
            {
                vencedorRotaLonga.Pontuacao += 10; // Bônus de 10 pontos
            }
        }

        public Jogador? CalcularRotaMaisLonga()
        {
            Jogador? vencedor = null;
            int maiorComprimento = 0;

            foreach (var jogador in Jogadores)
            {
                var comprimento = jogador.CalcularComprimentoRotaContinuo();
                if (comprimento > maiorComprimento)
                {
                    maiorComprimento = comprimento;
                    vencedor = jogador;
                }
            }

            return vencedor;
        }

        public Jogador? IdentificarVencedor()
        {
            if (!PartidaFinalizada)
            {
                return null;
            }

            return Jogadores.OrderByDescending(j => j.Pontuacao).FirstOrDefault();
        }

        public Turno CriarProximoTurno()
        {
            if (TurnoAtual == null)
            {
                throw new InvalidOperationException("Não há turno atual");
            }

            var proximoJogador = ObterProximoJogador(TurnoAtual.JogadorAtual);
            var proximoTurno = new Turno(TurnoAtual.Numero + 1, proximoJogador);
            TurnoAtual = proximoTurno;

            return proximoTurno;
        }

        private Jogador ObterProximoJogador(Jogador jogadorAtual)
        {
            var indiceAtual = Jogadores.IndexOf(jogadorAtual);
            var proximoIndice = (indiceAtual + 1) % Jogadores.Count;
            return Jogadores[proximoIndice];
        }

        public bool IsPartidaFinalizada()
        {
            return PartidaFinalizada;
        }

        public void AdicionarJogador(Jogador jogador)
        {
            if (PartidaIniciada)
            {
                throw new InvalidOperationException("Não é possível adicionar jogadores após o início da partida");
            }

            if (Jogadores.Count >= 5)
            {
                throw new InvalidOperationException("Número máximo de jogadores atingido (5)");
            }

            Jogadores.Add(jogador);
        }

        public Jogador? ObterJogador(string jogadorId)
        {
            return Jogadores.FirstOrDefault(j => j.Id == jogadorId);
        }

        public bool PodeIniciar()
        {
            return Jogadores.Count >= 2 && Jogadores.Count <= 5 && !PartidaIniciada;
        }

        public List<Jogador> ObterRanking()
        {
            return Jogadores.OrderByDescending(j => j.Pontuacao).ToList();
        }

        public int ObterNumeroJogadores()
        {
            return Jogadores.Count;
        }

        public bool EhVezDoJogador(string jogadorId)
        {
            return TurnoAtual?.JogadorAtual.Id == jogadorId;
        }

        public override string ToString()
        {
            var status = PartidaFinalizada ? "Finalizada" : PartidaIniciada ? "Em Andamento" : "Aguardando";
            return $"Partida {Id} - {Jogadores.Count} jogadores - {status}";
        }
    }
}
