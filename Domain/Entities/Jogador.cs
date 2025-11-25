using TicketToRide.Application.DTOs;
using TicketToRide.Domain.Enums;
using TicketToRide.Domain.Interfaces;
using TicketToRideAPI.Domain.Interfaces;

namespace TicketToRide.Domain.Entities
{
    public class Jogador : IJogadorSubject
    {
        public string Id { get; }
        public string Nome { get; }
        public int Pontuacao { get; private set; } = 0;
        private int PecasTremRestante { get; set; } = 45;
        private List<CartaVeiculo> MaoCartas { get; } = [];
        public List<BilheteDestino> BilhetesDestino { get; } = [];
        public List<Rota> RotasConquistadas { get; } = [];

        private List<IObserver> _observers = [];

        public Jogador(string id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public void ConquistarRota(Rota rota)
        {
            RemoverCartasDaMao(SelecionarCartasParaRota(rota));

            RotasConquistadas.Add(rota);

            RemoverPecasTrem(rota.Tamanho);

            rota.ConquistarRota();

            Pontuacao += rota.CalcularPontos();

            this.Notify();
        }

        public bool TemPecasSuficientesParaConquistarRota(Rota rota)
        {
            return PecasTremRestante >= rota.Tamanho;
        }

        private void RemoverCartasDaMao(List<CartaVeiculo> cartasUsadas)
        {
            foreach (CartaVeiculo carta in cartasUsadas)
            {
                MaoCartas.Remove(carta);
            }
        }

        public bool TemCartasSuficientesParaConquistarRota(Rota rota)
        {
            List<CartaVeiculo> cartas = SelecionarCartasParaRota(rota);
            if (cartas.Count < rota.Tamanho)
            {
                return false;
            }

            return cartas.All(c => MaoCartas.Contains(c)
                                         && rota.PodeSerConquistadaCom(c));
        }

        public int CalcularComprimentoRotaContinua()
        {
            // Implementar algoritmo para encontrar a rota contínua mais longa
            // Por simplicidade, retornar a soma de todas as rotas por enquanto
            return RotasConquistadas.Sum(r => r.Tamanho);
        }

        private void RemoverPecasTrem(int quantidade)
        {
            PecasTremRestante = Math.Max(0, PecasTremRestante - quantidade);
        }

        public void AdiconarCartasVeiculo(IEnumerable<CartaVeiculo> cartas)
        {
            MaoCartas.AddRange(cartas);
        }

        public void AdicionarBilhetesDestino(IEnumerable<BilheteDestino> bilhetes)
        {
            BilhetesDestino.AddRange(bilhetes);
        }

        private List<CartaVeiculo> ObterCartasParaCor(Cor cor)
        {
            return [.. MaoCartas.Where(c => c.PodeSerUsadaPara(cor))];
        }

        private List<CartaVeiculo> SelecionarCartasParaRota(Rota rota)
        {
            List<CartaVeiculo> cartasSelecionadas = [];
            List<CartaVeiculo> cartasDisponiveis = ObterCartasParaCor(rota.Cor);

            List<CartaVeiculo> locomotivas = [.. cartasDisponiveis.Where(c => c.Cor == Cor.LOCOMOTIVA)];
            List<CartaVeiculo> cartasNormais = [.. cartasDisponiveis.Except(locomotivas)];

            for (int i = 0; i < Math.Min(rota.Tamanho, cartasNormais.Count); i++)
            {
                cartasSelecionadas.Add(cartasNormais[i]);
            }

            int cartasFaltando = rota.Tamanho - cartasSelecionadas.Count;
            for (int i = 0; i < Math.Min(cartasFaltando, locomotivas.Count); i++)
            {
                cartasSelecionadas.Add(locomotivas[i]);
            }

            return cartasSelecionadas;
        }

        public void AdicionarPontuacao(int pontos)
        {
            Pontuacao += pontos;
        }

        public JogadorDTO MapearJogadorParaDTO()
        {
            return new JogadorDTO
            {
                Id = Id,
                Nome = Nome,
                Pontuacao = Pontuacao,
                PecasTremRestante = PecasTremRestante,
                MaoCartas = MaoCartas.ConvertAll(c => c.MapearParaDTO()),
                BilhetesDestino = BilhetesDestino.ConvertAll(b => b.MapearParaDTO()),
                RotasConquistadas = RotasConquistadas.ConvertAll(r => r.MapearParaDTO()),
                NumeroCartas = MaoCartas.Count,
                NumeroBilhetes = BilhetesDestino.Count,
                NumeroRotas = RotasConquistadas.Count
            };
        }

        public void Attach(IObserver observer)
        {
            Console.WriteLine($"Subject (Jogador {Nome}): Attached an observer.");
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            Console.WriteLine($"Subject (Jogador {Nome}): Detached an observer.");
        }

        public void Notify()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update(this);
            }
        }
    }
}