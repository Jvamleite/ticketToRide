using TicketToRide.Application.DTOs;
using TicketToRide.Domain.Enums;
using TicketToRide.Domain.Interfaces;
using TicketToRideAPI.Domain.Interfaces;

namespace TicketToRide.Domain.Entities
{
    public class Partida : IPartidaSubject
    {
        private const int MinimoJogadores = 2;
        private const int MaximoJogadores = 5;

        public string Id { get; }
        public List<Jogador> Jogadores { get; } = [];
        public Tabuleiro Tabuleiro { get; }
        public BaralhoCartasDestino BaralhoCartasDestino { get; }
        public BaralhoCartasVeiculo BaralhoCartasVeiculo { get; }
        public Turno? TurnoAtual { get; private set; }
        public bool Iniciada { get; private set; }
        public bool Finalizada { get; private set; }

        private List<IObserver> _observadores = [];

        public Partida(string id)
        {
            Id = id;
        }

        public Partida(
            string id,
            Tabuleiro tabuleiro,
            BaralhoCartasDestino baralhoDestino,
            BaralhoCartasVeiculo baralhoVeiculo)
        {
            Id = id;
            Tabuleiro = tabuleiro;
            BaralhoCartasDestino = baralhoDestino;
            BaralhoCartasVeiculo = baralhoVeiculo;
        }

        public void IniciarPartida(int numeroDeJogadores)
        {
            if (Iniciada)
            {
                throw new InvalidOperationException("Partida já iniciada");
            }

            ValidarNumeroJogadoresEsperado(numeroDeJogadores);

            if (Jogadores.Count != numeroDeJogadores)
            {
                throw new InvalidOperationException($"Número de jogadores não corresponde ao esperado. Esperado: {numeroDeJogadores}, Atual: {Jogadores.Count}");
            }

            TurnoAtual = new Turno(1, Jogadores[0]);
            Iniciada = true;

            this.Notify();
        }

        private void ValidarNumeroJogadoresEsperado(int numeroDeJogadores)
        {
            if (numeroDeJogadores < MinimoJogadores || numeroDeJogadores > MaximoJogadores)
            {
                throw new ArgumentException($"Número de jogadores deve ser entre {MinimoJogadores} e {MaximoJogadores}");
            }

            if (Jogadores.Count != numeroDeJogadores)
            {
                throw new InvalidOperationException($"Número de jogadores não corresponde ao esperado. Esperado: {numeroDeJogadores}, Atual: {Jogadores.Count}");
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

            Finalizada = true;

            this.Notify();
        }

        public Jogador? CalcularRotaMaisLonga()
        {
            return Jogadores
                .Select(j => new { Jogador = j, Comprimento = j.CalcularComprimentoRotaContinua() })
                .OrderByDescending(x => x.Comprimento)
                .FirstOrDefault()
                ?.Jogador;
        }

        public Turno AvancarTurno()
        {
            if (TurnoAtual == null)
            {
                throw new InvalidOperationException("Não há turno atual");
            }

            Jogador proximoJogador = ObterProximoJogador(TurnoAtual.ObterJogadorAtual());
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

        private bool PodeIniciar()
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

        public bool EstaNaVezDoJogador(string jogadorId)
        {
            return TurnoAtual?.ObterJogadorAtual().Id == jogadorId;
        }

        public PartidaDTO MapearParaDTO()
        {
            return new PartidaDTO
            {
                Id = Id,
                Jogadores = Jogadores.ConvertAll(x => x.MapearJogadorParaDTO()),
                Rotas = Tabuleiro.Rotas.ConvertAll(x => x.MapearParaDTO()),
                TurnoAtual = TurnoAtual?.MapearParaDTO(),
                PartidaIniciada = Iniciada,
                PartidaFinalizada = Finalizada,
                NumeroJogadores = Jogadores.Count,
                PodeIniciar = PodeIniciar(),
                CartasVisiveis = BaralhoCartasVeiculo.ListarCartasReveladas().Select(x => x.MapearParaDTO()),
                OpcoesBilheteDestino = BaralhoCartasDestino.ListarOpcoesSelecionaveis().Select(x => x.MapearParaDTO()),
            };
        }

        public void ExecutarAcaoTurno(Acao acaoRealizada)
        {
            TurnoAtual?.SalvarAcaoRealizada(acaoRealizada);
            AvancarTurno();
        }

        public void Attach(IObserver observer)
        {
            if (!_observadores.Contains(observer))
            {
                _observadores.Add(observer);
                Console.WriteLine($"[Partida {Id}] Observer anexado: {observer.GetType().Name}");
            }
        }

        public void Detach(IObserver observer)
        {
            if (_observadores.Contains(observer))
            {
                _observadores.Remove(observer);
                Console.WriteLine($"[Partida {Id}] Observer desanexado: {observer.GetType().Name}");
            }
        }

        public void Notify()
        {
            Console.WriteLine($"[Partida {Id}] Notificando observadores sobre mudança de estado...");

            foreach (IObserver observer in _observadores)
            {
                observer.Update(this);
            }
        }
    }
}