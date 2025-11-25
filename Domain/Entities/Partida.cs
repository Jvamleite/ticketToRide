using TicketToRide.Application;

namespace TicketToRide.Domain.Entities
{
    public class Partida
    {
        private const int MinimoJogadores = 2;
        private const int MaximoJogadores = 5;
        private const int CartasVeiculoIniciaisPorJogador = 4;
        private const int CartasDestinoIniciaisPorJogador = 3;
        private const int BonusRotaMaisLonga = 10;

        public string Id { get; }
        public List<Jogador> Jogadores { get; } = [];
        public Tabuleiro Tabuleiro { get; }
        public BaralhoCartasDestino BaralhoCartasDestino { get; }
        public BaralhoCartasVeiculo BaralhoCartasVeiculo { get; }
        public Turno? TurnoAtual { get; private set; }
        public bool Iniciada { get; set; }
        public bool Finalizada { get; set; }

        public Partida()
        {
            Tabuleiro = new Tabuleiro(DadosJogo.ObterRotas());
            BaralhoCartasDestino = new BaralhoCartasDestino();
            BaralhoCartasVeiculo = new BaralhoCartasVeiculo();
        }

        public void IniciarPartida(int numeroDeJogadores)
        {
            ValidarNumeroJogadoresEsperado(numeroDeJogadores);

            if (Iniciada)
            {
                throw new InvalidOperationException("Partida já iniciada");
            }

            //Subir depois validacoes para fora do dominio

            DistribuirCartasIniciais();

            TurnoAtual = new Turno(1, Joga[0]);

            Iniciada = true;
        }

        private void ValidarNumeroJogadoresEsperado(int numeroDeJogadores)
        {
            if (numeroDeJogadores < MinimoJogadores || numeroDeJogadores > MaximoJogadores)
            {
                throw new ArgumentException($"Número de jogadores deve ser entre {MinimoJogadores} e {MaximoJogadores}");
            }

            if (Jogadores.Count != numeroDeJogadores)
            {
                throw new InvalidOperationException($"Número de jogadores não corresponde ao esperado. Esperado: {numeroDeJogadores}, Atual: {_jogadores.Count}");
            }
        }

        private void DistribuirCartasIniciais()
        {
            foreach (Jogador jogador in Jogadores)
            {
                jogador.ComprarCartasVeiculo(BaralhoCartasVeiculo.Comprar(CartasVeiculoIniciaisPorJogador));
                jogador.ComprarBilhetesDestino(BaralhoCartasDestino.Comprar(CartasDestinoIniciaisPorJogador));
            }
        }

        public void FinalizarPartida()
        {
            if (!Iniciada)
            {
                throw new InvalidOperationException("Partida não foi iniciada");
            }

            if (Finalizada)
            {
                throw new InvalidOperationException("Partida já finalizada");
            }

            CalcularPontuacaoFinal();
            Finalizada = true;
        }

        public void CalcularPontuacaoFinal()
        {
            foreach (Jogador jogador in Jogadores)
            {
                jogador.AtualizarPontuacao();
            }

            Jogador? vencedorRotaLonga = CalcularRotaMaisLonga();

            vencedorRotaLonga?.AdicionarPontuacao(10);
        }

        public Jogador? CalcularRotaMaisLonga()
        {
            Jogador? vencedor = null;
            int maiorComprimento = 0;

            foreach (Jogador jogador in Jogadores)
            {
                int comprimento = jogador.CalcularComprimentoRotaContinua();
                if (comprimento > maiorComprimento)
                {
                    maiorComprimento = comprimento;
                    vencedor = jogador;
                }
            }

            return vencedor;
        }

        public Turno AvancarTurno()
        {
            if (TurnoAtual == null)
            {
                throw new InvalidOperationException("Não há turno atual");
            }

            Jogador proximoJogador = ObterProximoJogador(TurnoAtual.JogadorAtual);
            Turno proximoTurno = new(TurnoAtual.Numero + 1, proximoJogador);
            TurnoAtual = proximoTurno;
            return proximoTurno;
        }

        private Jogador ObterProximoJogador(Jogador jogadorAtual)
        {
            int indiceAtual = Jogadores.IndexOf(jogadorAtual);
            int proximoIndice = (indiceAtual + 1) % Jogadores.Count;
            return Jogadores[proximoIndice];
        }

        public void AdicionarJogador(Jogador jogador)
        {
            if (Iniciada)
            {
                throw new InvalidOperationException("Não é possível adicionar jogadores após o início da partida");
            }

            if (Jogadores.Count >= MaximoJogadores)
            {
                throw new InvalidOperationException($"Número máximo de jogadores atingido ({MaximoJogadores})");
            }

            Jogadores.Add(jogador);
        }

        public Jogador? ObterJogador(string jogadorId)
        {
            return Jogadores.FirstOrDefault(j => j.Id == jogadorId);
        }

        public bool PodeIniciar()
        {
            return Jogadores.Count >= MinimoJogadores && Jogadores.Count <= MaximoJogadores && !Iniciada;
        }

        public List<Jogador> ObterRanking()
        {
            return [.. Jogadores.OrderByDescending(j => j.Pontuacao)];
        }

        public override string ToString()
        {
            return $"Partida {Id} - {Jogadores.Count} jogadores - {ObterStatusPartida()}";
        }

        private string ObterStatusPartida()
        {
            if (Finalizada)
            {
                return "Finalizada";
            }

            if (Iniciada)
            {
                return "Em Andamento";
            }

            return "Aguardando";
        }
    }
}