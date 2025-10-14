using TicketToRide.Domain.Enums;

namespace TicketToRide.Domain.Entities
{
    public class Tabuleiro
    {
        public List<Rota> Rotas { get; set; } = new();

        public Tabuleiro()
        {
            // As rotas serão inicializadas pela classe DadosJogo
        }

        public void AdicionarRotas(List<Rota> rotas)
        {
            Rotas.AddRange(rotas);
        }

        public Rota? GetRota(string idRota)
        {
            return Rotas.FirstOrDefault(r => r.Id == idRota);
        }

        public bool IsRotaDisponivel(string idRota)
        {
            var rota = GetRota(idRota);
            return rota?.EstaDisponivel() ?? false;
        }

        public List<Rota> GetRotasDisponiveis()
        {
            return Rotas.Where(r => r.EstaDisponivel()).ToList();
        }

        public List<Rota> GetRotasConquistadas()
        {
            return Rotas.Where(r => !r.EstaDisponivel()).ToList();
        }

        public List<Rota> GetRotasPorJogador(Jogador jogador)
        {
            return Rotas.Where(r => r.Jogador == jogador).ToList();
        }

        public List<Rota> GetRotasEntreCidades(Cidade origem, Cidade destino)
        {
            return Rotas.Where(r => 
                (r.Origem.Equals(origem) && r.Destino.Equals(destino)) ||
                (r.Origem.Equals(destino) && r.Destino.Equals(origem))
            ).ToList();
        }

        public List<Rota> GetRotasPorCor(Cor cor)
        {
            return Rotas.Where(r => r.Cor == cor).ToList();
        }

        public int GetTotalRotas()
        {
            return Rotas.Count;
        }

        public int GetRotasDisponiveisCount()
        {
            return GetRotasDisponiveis().Count;
        }

        public int GetRotasConquistadasCount()
        {
            return GetRotasConquistadas().Count;
        }
    }
}
