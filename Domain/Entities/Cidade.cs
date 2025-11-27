namespace TicketToRide.Domain.Entities
{
    public class Cidade
    {
        public string Nome { get; }

        public Cidade(string nome)
        {
            Nome = nome;
        }

        public override string ToString()
        {
            return Nome;
        }

        public string ObterNomeFormatado(Cidade destino)
        {
            return $"{Nome} → {destino.Nome}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Cidade outraCidade)
            {
                return Nome.Equals(outraCidade.Nome, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Nome.ToUpperInvariant().GetHashCode();
        }
    }
}