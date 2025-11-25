using TicketToRide.Application.DTOs;
using TicketToRide.Domain.Enums;

namespace TicketToRide.Domain.Entities
{
    public class Jogador
    {
        private const int LIMITE_MAXIMO_CARTAS_MAO = 10;
        public string Id { get; }
        public string Nome { get; }
        public int Pontuacao { get; private set; } = 0;
        private int PecasTremRestante { get; set; } = 45;
        private List<CartaVeiculo> MaoCartas { get; } = [];
        private List<BilheteDestino> BilhetesDestino { get; } = [];
        private List<Rota> RotasConquistadas { get; } = [];

        public Jogador(string id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public int ConquistarRota(Rota rota)
        {
            RemoverCartasDaMao(SelecionarCartasParaRota(rota));

            RotasConquistadas.Add(rota);

            RemoverPecasTrem(rota.Tamanho);

            int pontos = rota.CalcularPontos();
            Pontuacao += pontos;

            return pontos;
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

        public void ComprarCartasVeiculo(IEnumerable<CartaVeiculo> cartas)
        {
            MaoCartas.AddRange(cartas);
        }

        public void ComprarBilhetesDestino(IEnumerable<BilheteDestino> bilhetes)
        {
            BilhetesDestino.AddRange(bilhetes);
        }

        private int CalcularPontosBilhetes()
        {
            int pontos = 0;
            foreach (BilheteDestino bilhete in BilhetesDestino)
            {
                if (bilhete.EstaCompleto(RotasConquistadas))
                {
                    pontos += bilhete.Pontos;
                }
                else
                {
                    pontos -= bilhete.Pontos;
                }
            }
            return pontos;
        }

        public void AtualizarPontuacao()
        {
            int pontosBilhetes = CalcularPontosBilhetes();

            Pontuacao += pontosBilhetes;
        }

        public List<CartaVeiculo> ObterCartasParaCor(Cor cor)
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

        public bool PodeComprarCartas()
        {
            return MaoCartas.Count < LIMITE_MAXIMO_CARTAS_MAO;
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
    }
}