using TicketToRide.Domain.Enums;
using TicketToRide.Domain.Interfaces;
using TicketToRideAPI.Domain.Interfaces;

namespace TicketToRide.Domain.Entities
{
    public class Jogador : IJogadorSubject
    {
        private const int QuantidadeInicialPecasTrem = 45;
        private List<CartaVeiculo> maoCartas = [];
        private List<BilheteDestino> bilhetesDestino = [];

        public string Id { get; }
        public string Nome { get; }
        public int Pontuacao { get; private set; } = 0;
        public int PecasTremRestante { get; private set; } = QuantidadeInicialPecasTrem;

        public IReadOnlyList<CartaVeiculo> MaoCartas => maoCartas.AsReadOnly();
        public IReadOnlyList<BilheteDestino> BilhetesDestino => bilhetesDestino.AsReadOnly();
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

        private void RemoverCartasDaMao(List<CartaVeiculo> cartasUsadas)
        {
            foreach (CartaVeiculo carta in cartasUsadas)
            {
                maoCartas.Remove(carta);
            }
        }

        private void RemoverPecasTrem(int quantidade)
        {
            PecasTremRestante = Math.Max(0, PecasTremRestante - quantidade);
        }

        public bool TemPecasSuficientesParaConquistarRota(Rota rota)
        {
            return PecasTremRestante >= rota.Tamanho;
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
            return RotasConquistadas.Sum(r => r.Tamanho);
        }

        public void AdicionarCartasVeiculo(IEnumerable<CartaVeiculo> cartas)
        {
            maoCartas.AddRange(cartas);
        }

        public void AdicionarBilhetesDestino(IEnumerable<BilheteDestino> bilhetes)
        {
            bilhetesDestino.AddRange(bilhetes);
        }

        private List<CartaVeiculo> SelecionarCartasParaRota(Rota rota)
        {
            List<CartaVeiculo> cartasDisponiveis = ObterCartasParaCor(rota.Cor);
            (List<CartaVeiculo> locomotivas, List<CartaVeiculo> cartasNormais) = SepararPorTipo(cartasDisponiveis);
            return MontarSelecao(rota.Tamanho, cartasNormais, locomotivas);
        }

        private List<CartaVeiculo> ObterCartasParaCor(Cor cor)
        {
            return [.. MaoCartas.Where(c => c.PodeSerUsadaPara(cor))];
        }

        private static (List<CartaVeiculo> locomotivas, List<CartaVeiculo> normais) SepararPorTipo(List<CartaVeiculo> cartas)
        {
            List<CartaVeiculo> locomotivas = cartas.Where(c => c.Cor == Cor.LOCOMOTIVA).ToList();
            List<CartaVeiculo> normais = cartas.Except(locomotivas).ToList();
            return (locomotivas, normais);
        }

        private static List<CartaVeiculo> MontarSelecao(int tamanhoRota, List<CartaVeiculo> normais, List<CartaVeiculo> locomotivas)
        {
            List<CartaVeiculo> selecionadas = normais.Take(tamanhoRota).ToList();
            int faltando = tamanhoRota - selecionadas.Count;
            selecionadas.AddRange(locomotivas.Take(faltando));
            return selecionadas;
        }

        public void AdicionarPontuacao(int pontos)
        {
            Pontuacao += pontos;
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