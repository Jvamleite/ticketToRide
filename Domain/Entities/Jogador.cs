using TicketToRide.Domain.Enums;

namespace TicketToRide.Domain.Entities
{
    public class Jogador
    {
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public int Pontuacao { get; set; } = 0;
        public int PecasTremRestante { get; set; } = 45;
        public List<CartaVeiculo> MaoCartas { get; set; } = new();
        public List<BilheteDestino> BilhetesDestino { get; set; } = new();
        public List<Rota> RotasConquistadas { get; set; } = new();

        public Jogador(string id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public int ConquistarRota(Rota rota, List<CartaVeiculo> cartasUsadas)
        {
            if (!rota.PodeSerConquistadaPor(this))
            {
                return 0;
            }

            // Verificar se tem as cartas necessárias
            if (!TemCartasParaRota(rota, cartasUsadas))
            {
                return 0;
            }

            // Remover cartas da mão
            foreach (var carta in cartasUsadas)
            {
                MaoCartas.Remove(carta);
            }

            // Adicionar rota conquistada
            RotasConquistadas.Add(rota);
            rota.Conquistar(this);

            // Remover peças de trem
            RemoverPecasTrem(rota.Tamanho);

            // Calcular pontos da rota
            var pontos = rota.CalcularPontos();
            Pontuacao += pontos;

            return pontos;
        }

        private bool TemCartasParaRota(Rota rota, List<CartaVeiculo> cartasUsadas)
        {
            if (cartasUsadas.Count != rota.Tamanho)
            {
                return false;
            }

            // Verificar se todas as cartas estão na mão do jogador
            foreach (var carta in cartasUsadas)
            {
                if (!MaoCartas.Contains(carta))
                {
                    return false;
                }
            }

            // Verificar se as cartas podem ser usadas para a cor da rota
            var cartasValidas = cartasUsadas.Count(c => c.PodeSerUsadaPara(rota.Cor));
            return cartasValidas >= rota.Tamanho;
        }

        public int CalcularComprimentoRotaContinuo()
        {
            // Implementar algoritmo para encontrar a rota contínua mais longa
            // Por simplicidade, retornar a soma de todas as rotas por enquanto
            return RotasConquistadas.Sum(r => r.Tamanho);
        }

        public void RemoverPecasTrem(int quantidade)
        {
            PecasTremRestante = Math.Max(0, PecasTremRestante - quantidade);
        }

        public List<CartaVeiculo> ComprarCartas(List<CartaVeiculo> cartas)
        {
            MaoCartas.AddRange(cartas);
            return cartas;
        }

        public int CalcularPontosBilhetes()
        {
            int pontos = 0;
            foreach (var bilhete in BilhetesDestino)
            {
                if (bilhete.IsCompleto(RotasConquistadas))
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
            // Pontuação base das rotas já está sendo calculada em ConquistarRota
            // Adicionar pontos dos bilhetes
            var pontosBilhetes = CalcularPontosBilhetes();
            Pontuacao += pontosBilhetes;
        }

        public int GetPontuacao()
        {
            return Pontuacao;
        }

        public List<CartaVeiculo> ObterCartasParaCor(Cor cor)
        {
            return MaoCartas.Where(c => c.PodeSerUsadaPara(cor)).ToList();
        }

        public List<CartaVeiculo> ObterLocomotivas()
        {
            return MaoCartas.Where(c => c.EhLocomotiva).ToList();
        }

        public int ContarCartasPorCor(Cor cor)
        {
            return MaoCartas.Count(c => c.Cor == cor);
        }

        public int ContarLocomotivas()
        {
            return MaoCartas.Count(c => c.EhLocomotiva);
        }

        public bool TemCartasSuficientesParaRota(Rota rota)
        {
            var cartasDisponiveis = ObterCartasParaCor(rota.Cor);
            return cartasDisponiveis.Count >= rota.Tamanho;
        }

        public List<CartaVeiculo> SelecionarCartasParaRota(Rota rota)
        {
            var cartasSelecionadas = new List<CartaVeiculo>();
            var cartasDisponiveis = ObterCartasParaCor(rota.Cor);

            // Priorizar cartas normais sobre locomotivas
            var cartasNormais = cartasDisponiveis.Where(c => !c.EhLocomotiva).ToList();
            var locomotivas = cartasDisponiveis.Where(c => c.EhLocomotiva).ToList();

            // Adicionar cartas normais primeiro
            for (int i = 0; i < Math.Min(rota.Tamanho, cartasNormais.Count); i++)
            {
                cartasSelecionadas.Add(cartasNormais[i]);
            }

            // Se precisar de mais cartas, usar locomotivas
            int cartasFaltando = rota.Tamanho - cartasSelecionadas.Count;
            for (int i = 0; i < Math.Min(cartasFaltando, locomotivas.Count); i++)
            {
                cartasSelecionadas.Add(locomotivas[i]);
            }

            return cartasSelecionadas;
        }

        public void AdicionarBilhetesDestino(List<BilheteDestino> bilhetes)
        {
            BilhetesDestino.AddRange(bilhetes);
        }

        public bool PodeComprarCartas()
        {
            return MaoCartas.Count < 10; // Limite de cartas na mão
        }

        public bool PodeComprarBilhetes()
        {
            return true; // Sempre pode comprar bilhetes
        }

        public bool PodeReivindicarRota()
        {
            return PecasTremRestante > 0;
        }

        public override string ToString()
        {
            return $"{Nome} (Pontos: {Pontuacao}, Trens: {PecasTremRestante}, Cartas: {MaoCartas.Count})";
        }
    }
}
