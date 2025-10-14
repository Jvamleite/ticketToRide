using TicketToRide.Domain.Entities;
using TicketToRide.Domain.Enums;

namespace TicketToRide.Application
{
    public static class DadosJogo
    {
        public static List<Cidade> ObterCidades()
        {
            return new List<Cidade>
            {
                // Cidades do Oeste
                new Cidade("Vancouver"),
                new Cidade("Seattle"),
                new Cidade("Portland"),
                new Cidade("San Francisco"),
                new Cidade("Los Angeles"),
                new Cidade("Las Vegas"),
                new Cidade("Salt Lake City"),
                new Cidade("Denver"),
                new Cidade("Phoenix"),
                new Cidade("Santa Fe"),
                new Cidade("El Paso"),
                new Cidade("Dallas"),
                new Cidade("Houston"),
                new Cidade("New Orleans"),
                new Cidade("Miami"),
                new Cidade("Atlanta"),
                new Cidade("Charleston"),
                new Cidade("Raleigh"),
                new Cidade("Washington"),
                new Cidade("New York"),
                new Cidade("Boston"),
                new Cidade("Montreal"),
                new Cidade("Toronto"),
                new Cidade("Chicago"),
                new Cidade("Saint Louis"),
                new Cidade("Kansas City"),
                new Cidade("Oklahoma City"),
                new Cidade("Little Rock"),
                new Cidade("Nashville"),
                new Cidade("Pittsburgh"),
                new Cidade("Sault St. Marie"),
                new Cidade("Duluth"),
                new Cidade("Helena"),
                new Cidade("Winnipeg"),
                new Cidade("Calgary"),
                new Cidade("Vancouver")
            };
        }

        public static List<Rota> ObterRotas()
        {
            var cidades = ObterCidades();
            var rotas = new List<Rota>();

            // Função auxiliar para encontrar cidade por nome
            Cidade EncontrarCidade(string nome) => cidades.First(c => c.Nome == nome);

            // Rotas do Oeste
            rotas.Add(new Rota("VAN-SEA-1", EncontrarCidade("Vancouver"), EncontrarCidade("Seattle"), Cor.CINZA, 1));
            rotas.Add(new Rota("VAN-SEA-2", EncontrarCidade("Vancouver"), EncontrarCidade("Seattle"), Cor.CINZA, 1, true));
            rotas.Add(new Rota("SEA-POR-1", EncontrarCidade("Seattle"), EncontrarCidade("Portland"), Cor.CINZA, 1));
            rotas.Add(new Rota("SEA-POR-2", EncontrarCidade("Seattle"), EncontrarCidade("Portland"), Cor.CINZA, 1, true));
            rotas.Add(new Rota("POR-SF-1", EncontrarCidade("Portland"), EncontrarCidade("San Francisco"), Cor.VERDE, 5));
            rotas.Add(new Rota("POR-SF-2", EncontrarCidade("Portland"), EncontrarCidade("San Francisco"), Cor.ROSA, 5, true));
            rotas.Add(new Rota("SF-LA-1", EncontrarCidade("San Francisco"), EncontrarCidade("Los Angeles"), Cor.AMARELO, 3));
            rotas.Add(new Rota("SF-LA-2", EncontrarCidade("San Francisco"), EncontrarCidade("Los Angeles"), Cor.ROSA, 3, true));
            rotas.Add(new Rota("LA-LV-1", EncontrarCidade("Los Angeles"), EncontrarCidade("Las Vegas"), Cor.CINZA, 2));
            rotas.Add(new Rota("LA-PHO-1", EncontrarCidade("Los Angeles"), EncontrarCidade("Phoenix"), Cor.CINZA, 3));
            rotas.Add(new Rota("LV-SLC-1", EncontrarCidade("Las Vegas"), EncontrarCidade("Salt Lake City"), Cor.LARANJA, 3));
            rotas.Add(new Rota("SLC-DEN-1", EncontrarCidade("Salt Lake City"), EncontrarCidade("Denver"), Cor.VERMELHO, 3));
            rotas.Add(new Rota("SLC-DEN-2", EncontrarCidade("Salt Lake City"), EncontrarCidade("Denver"), Cor.AMARELO, 3, true));
            rotas.Add(new Rota("DEN-PHO-1", EncontrarCidade("Denver"), EncontrarCidade("Phoenix"), Cor.BRANCO, 4));
            rotas.Add(new Rota("DEN-SF-1", EncontrarCidade("Denver"), EncontrarCidade("Santa Fe"), Cor.CINZA, 2));
            rotas.Add(new Rota("SF-EP-1", EncontrarCidade("Santa Fe"), EncontrarCidade("El Paso"), Cor.CINZA, 2));
            rotas.Add(new Rota("EP-DAL-1", EncontrarCidade("El Paso"), EncontrarCidade("Dallas"), Cor.VERMELHO, 4));
            rotas.Add(new Rota("DAL-HOU-1", EncontrarCidade("Dallas"), EncontrarCidade("Houston"), Cor.CINZA, 1));
            rotas.Add(new Rota("DAL-HOU-2", EncontrarCidade("Dallas"), EncontrarCidade("Houston"), Cor.CINZA, 1, true));
            rotas.Add(new Rota("HOU-NO-1", EncontrarCidade("Houston"), EncontrarCidade("New Orleans"), Cor.CINZA, 2));
            rotas.Add(new Rota("NO-MIA-1", EncontrarCidade("New Orleans"), EncontrarCidade("Miami"), Cor.VERMELHO, 6));
            rotas.Add(new Rota("MIA-ATL-1", EncontrarCidade("Miami"), EncontrarCidade("Atlanta"), Cor.AZUL, 4));
            rotas.Add(new Rota("ATL-CHA-1", EncontrarCidade("Atlanta"), EncontrarCidade("Charleston"), Cor.CINZA, 2));
            rotas.Add(new Rota("CHA-RAL-1", EncontrarCidade("Charleston"), EncontrarCidade("Raleigh"), Cor.CINZA, 2));
            rotas.Add(new Rota("RAL-WAS-1", EncontrarCidade("Raleigh"), EncontrarCidade("Washington"), Cor.CINZA, 2));
            rotas.Add(new Rota("WAS-NY-1", EncontrarCidade("Washington"), EncontrarCidade("New York"), Cor.LARANJA, 2));
            rotas.Add(new Rota("WAS-NY-2", EncontrarCidade("Washington"), EncontrarCidade("New York"), Cor.PRETO, 2, true));
            rotas.Add(new Rota("NY-BOS-1", EncontrarCidade("New York"), EncontrarCidade("Boston"), Cor.AMARELO, 2));
            rotas.Add(new Rota("NY-BOS-2", EncontrarCidade("New York"), EncontrarCidade("Boston"), Cor.VERMELHO, 2, true));
            rotas.Add(new Rota("BOS-MON-1", EncontrarCidade("Boston"), EncontrarCidade("Montreal"), Cor.CINZA, 2));
            rotas.Add(new Rota("MON-TOR-1", EncontrarCidade("Montreal"), EncontrarCidade("Toronto"), Cor.CINZA, 3));
            rotas.Add(new Rota("TOR-CHI-1", EncontrarCidade("Toronto"), EncontrarCidade("Chicago"), Cor.BRANCO, 4));
            rotas.Add(new Rota("CHI-STL-1", EncontrarCidade("Chicago"), EncontrarCidade("Saint Louis"), Cor.VERDE, 2));
            rotas.Add(new Rota("CHI-STL-2", EncontrarCidade("Chicago"), EncontrarCidade("Saint Louis"), Cor.BRANCO, 2, true));
            rotas.Add(new Rota("STL-KC-1", EncontrarCidade("Saint Louis"), EncontrarCidade("Kansas City"), Cor.AZUL, 2));
            rotas.Add(new Rota("STL-KC-2", EncontrarCidade("Saint Louis"), EncontrarCidade("Kansas City"), Cor.ROSA, 2, true));
            rotas.Add(new Rota("KC-OKC-1", EncontrarCidade("Kansas City"), EncontrarCidade("Oklahoma City"), Cor.CINZA, 2));
            rotas.Add(new Rota("OKC-LR-1", EncontrarCidade("Oklahoma City"), EncontrarCidade("Little Rock"), Cor.CINZA, 2));
            rotas.Add(new Rota("LR-NAS-1", EncontrarCidade("Little Rock"), EncontrarCidade("Nashville"), Cor.BRANCO, 3));
            rotas.Add(new Rota("NAS-ATL-1", EncontrarCidade("Nashville"), EncontrarCidade("Atlanta"), Cor.CINZA, 1));
            rotas.Add(new Rota("NAS-PIT-1", EncontrarCidade("Nashville"), EncontrarCidade("Pittsburgh"), Cor.CINZA, 3));
            rotas.Add(new Rota("PIT-NY-1", EncontrarCidade("Pittsburgh"), EncontrarCidade("New York"), Cor.VERDE, 2));
            rotas.Add(new Rota("PIT-CHI-1", EncontrarCidade("Pittsburgh"), EncontrarCidade("Chicago"), Cor.PRETO, 3));
            rotas.Add(new Rota("CHI-DUL-1", EncontrarCidade("Chicago"), EncontrarCidade("Duluth"), Cor.VERMELHO, 3));
            rotas.Add(new Rota("DUL-SSM-1", EncontrarCidade("Duluth"), EncontrarCidade("Sault St. Marie"), Cor.CINZA, 3));
            rotas.Add(new Rota("SSM-MON-1", EncontrarCidade("Sault St. Marie"), EncontrarCidade("Montreal"), Cor.PRETO, 5));
            rotas.Add(new Rota("DUL-WIN-1", EncontrarCidade("Duluth"), EncontrarCidade("Winnipeg"), Cor.PRETO, 4));
            rotas.Add(new Rota("WIN-CAL-1", EncontrarCidade("Winnipeg"), EncontrarCidade("Calgary"), Cor.BRANCO, 6));
            rotas.Add(new Rota("CAL-VAN-1", EncontrarCidade("Calgary"), EncontrarCidade("Vancouver"), Cor.VERMELHO, 3));
            rotas.Add(new Rota("HEL-CAL-1", EncontrarCidade("Helena"), EncontrarCidade("Calgary"), Cor.CINZA, 4));
            rotas.Add(new Rota("HEL-WIN-1", EncontrarCidade("Helena"), EncontrarCidade("Winnipeg"), Cor.AZUL, 4));
            rotas.Add(new Rota("DEN-HEL-1", EncontrarCidade("Denver"), EncontrarCidade("Helena"), Cor.VERDE, 4));
            rotas.Add(new Rota("DEN-KC-1", EncontrarCidade("Denver"), EncontrarCidade("Kansas City"), Cor.PRETO, 4));
            rotas.Add(new Rota("DEN-KC-2", EncontrarCidade("Denver"), EncontrarCidade("Kansas City"), Cor.LARANJA, 4, true));
            rotas.Add(new Rota("DEN-OKC-1", EncontrarCidade("Denver"), EncontrarCidade("Oklahoma City"), Cor.ROSA, 4));
            rotas.Add(new Rota("DEN-SLC-1", EncontrarCidade("Denver"), EncontrarCidade("Salt Lake City"), Cor.ROSA, 3));
            rotas.Add(new Rota("SLC-HEL-1", EncontrarCidade("Salt Lake City"), EncontrarCidade("Helena"), Cor.ROSA, 3));
            rotas.Add(new Rota("SLC-HEL-2", EncontrarCidade("Salt Lake City"), EncontrarCidade("Helena"), Cor.AMARELO, 3, true));
            rotas.Add(new Rota("HEL-DUL-1", EncontrarCidade("Helena"), EncontrarCidade("Duluth"), Cor.LARANJA, 6));
            rotas.Add(new Rota("DUL-KC-1", EncontrarCidade("Duluth"), EncontrarCidade("Kansas City"), Cor.AZUL, 3));
            rotas.Add(new Rota("DUL-KC-2", EncontrarCidade("Duluth"), EncontrarCidade("Kansas City"), Cor.ROSA, 3, true));
            rotas.Add(new Rota("KC-STL-1", EncontrarCidade("Kansas City"), EncontrarCidade("Saint Louis"), Cor.AZUL, 2));
            rotas.Add(new Rota("KC-STL-2", EncontrarCidade("Kansas City"), EncontrarCidade("Saint Louis"), Cor.ROSA, 2, true));
            rotas.Add(new Rota("STL-CHI-1", EncontrarCidade("Saint Louis"), EncontrarCidade("Chicago"), Cor.VERDE, 2));
            rotas.Add(new Rota("STL-CHI-2", EncontrarCidade("Saint Louis"), EncontrarCidade("Chicago"), Cor.BRANCO, 2, true));
            rotas.Add(new Rota("CHI-TOR-1", EncontrarCidade("Chicago"), EncontrarCidade("Toronto"), Cor.BRANCO, 4));
            rotas.Add(new Rota("CHI-TOR-2", EncontrarCidade("Chicago"), EncontrarCidade("Toronto"), Cor.PRETO, 4, true));
            rotas.Add(new Rota("TOR-MON-1", EncontrarCidade("Toronto"), EncontrarCidade("Montreal"), Cor.CINZA, 3));
            rotas.Add(new Rota("MON-SSM-1", EncontrarCidade("Montreal"), EncontrarCidade("Sault St. Marie"), Cor.CINZA, 3));
            rotas.Add(new Rota("SSM-DUL-1", EncontrarCidade("Sault St. Marie"), EncontrarCidade("Duluth"), Cor.CINZA, 3));
            rotas.Add(new Rota("DUL-HEL-1", EncontrarCidade("Duluth"), EncontrarCidade("Helena"), Cor.LARANJA, 6));
            rotas.Add(new Rota("HEL-SLC-1", EncontrarCidade("Helena"), EncontrarCidade("Salt Lake City"), Cor.ROSA, 3));
            rotas.Add(new Rota("HEL-SLC-2", EncontrarCidade("Helena"), EncontrarCidade("Salt Lake City"), Cor.AMARELO, 3, true));
            rotas.Add(new Rota("SLC-DEN-1", EncontrarCidade("Salt Lake City"), EncontrarCidade("Denver"), Cor.VERMELHO, 3));
            rotas.Add(new Rota("SLC-DEN-2", EncontrarCidade("Salt Lake City"), EncontrarCidade("Denver"), Cor.AMARELO, 3, true));
            rotas.Add(new Rota("DEN-KC-1", EncontrarCidade("Denver"), EncontrarCidade("Kansas City"), Cor.PRETO, 4));
            rotas.Add(new Rota("DEN-KC-2", EncontrarCidade("Denver"), EncontrarCidade("Kansas City"), Cor.LARANJA, 4, true));
            rotas.Add(new Rota("DEN-OKC-1", EncontrarCidade("Denver"), EncontrarCidade("Oklahoma City"), Cor.ROSA, 4));
            rotas.Add(new Rota("OKC-DAL-1", EncontrarCidade("Oklahoma City"), EncontrarCidade("Dallas"), Cor.CINZA, 2));
            rotas.Add(new Rota("DAL-HOU-1", EncontrarCidade("Dallas"), EncontrarCidade("Houston"), Cor.CINZA, 1));
            rotas.Add(new Rota("DAL-HOU-2", EncontrarCidade("Dallas"), EncontrarCidade("Houston"), Cor.CINZA, 1, true));
            rotas.Add(new Rota("HOU-NO-1", EncontrarCidade("Houston"), EncontrarCidade("New Orleans"), Cor.CINZA, 2));
            rotas.Add(new Rota("NO-ATL-1", EncontrarCidade("New Orleans"), EncontrarCidade("Atlanta"), Cor.AMARELO, 4));
            rotas.Add(new Rota("NO-ATL-2", EncontrarCidade("New Orleans"), EncontrarCidade("Atlanta"), Cor.LARANJA, 4, true));
            rotas.Add(new Rota("ATL-MIA-1", EncontrarCidade("Atlanta"), EncontrarCidade("Miami"), Cor.AZUL, 4));
            rotas.Add(new Rota("MIA-NO-1", EncontrarCidade("Miami"), EncontrarCidade("New Orleans"), Cor.VERMELHO, 6));
            rotas.Add(new Rota("NO-HOU-1", EncontrarCidade("New Orleans"), EncontrarCidade("Houston"), Cor.CINZA, 2));
            rotas.Add(new Rota("HOU-DAL-1", EncontrarCidade("Houston"), EncontrarCidade("Dallas"), Cor.CINZA, 1));
            rotas.Add(new Rota("HOU-DAL-2", EncontrarCidade("Houston"), EncontrarCidade("Dallas"), Cor.CINZA, 1, true));
            rotas.Add(new Rota("DAL-OKC-1", EncontrarCidade("Dallas"), EncontrarCidade("Oklahoma City"), Cor.CINZA, 2));
            rotas.Add(new Rota("OKC-KC-1", EncontrarCidade("Oklahoma City"), EncontrarCidade("Kansas City"), Cor.CINZA, 2));
            rotas.Add(new Rota("KC-DEN-1", EncontrarCidade("Kansas City"), EncontrarCidade("Denver"), Cor.PRETO, 4));
            rotas.Add(new Rota("KC-DEN-2", EncontrarCidade("Kansas City"), EncontrarCidade("Denver"), Cor.LARANJA, 4, true));
            rotas.Add(new Rota("DEN-SLC-1", EncontrarCidade("Denver"), EncontrarCidade("Salt Lake City"), Cor.ROSA, 3));
            rotas.Add(new Rota("SLC-LV-1", EncontrarCidade("Salt Lake City"), EncontrarCidade("Las Vegas"), Cor.LARANJA, 3));
            rotas.Add(new Rota("LV-LA-1", EncontrarCidade("Las Vegas"), EncontrarCidade("Los Angeles"), Cor.CINZA, 2));
            rotas.Add(new Rota("LA-SF-1", EncontrarCidade("Los Angeles"), EncontrarCidade("San Francisco"), Cor.AMARELO, 3));
            rotas.Add(new Rota("LA-SF-2", EncontrarCidade("Los Angeles"), EncontrarCidade("San Francisco"), Cor.ROSA, 3, true));
            rotas.Add(new Rota("SF-POR-1", EncontrarCidade("San Francisco"), EncontrarCidade("Portland"), Cor.VERDE, 5));
            rotas.Add(new Rota("SF-POR-2", EncontrarCidade("San Francisco"), EncontrarCidade("Portland"), Cor.ROSA, 5, true));
            rotas.Add(new Rota("POR-SEA-1", EncontrarCidade("Portland"), EncontrarCidade("Seattle"), Cor.CINZA, 1));
            rotas.Add(new Rota("POR-SEA-2", EncontrarCidade("Portland"), EncontrarCidade("Seattle"), Cor.CINZA, 1, true));
            rotas.Add(new Rota("SEA-VAN-1", EncontrarCidade("Seattle"), EncontrarCidade("Vancouver"), Cor.CINZA, 1));
            rotas.Add(new Rota("SEA-VAN-2", EncontrarCidade("Seattle"), EncontrarCidade("Vancouver"), Cor.CINZA, 1, true));
            rotas.Add(new Rota("VAN-CAL-1", EncontrarCidade("Vancouver"), EncontrarCidade("Calgary"), Cor.VERMELHO, 3));
            rotas.Add(new Rota("CAL-WIN-1", EncontrarCidade("Calgary"), EncontrarCidade("Winnipeg"), Cor.BRANCO, 6));
            rotas.Add(new Rota("WIN-DUL-1", EncontrarCidade("Winnipeg"), EncontrarCidade("Duluth"), Cor.PRETO, 4));
            rotas.Add(new Rota("DUL-HEL-1", EncontrarCidade("Duluth"), EncontrarCidade("Helena"), Cor.LARANJA, 6));
            rotas.Add(new Rota("HEL-DEN-1", EncontrarCidade("Helena"), EncontrarCidade("Denver"), Cor.VERDE, 4));
            rotas.Add(new Rota("DEN-PHO-1", EncontrarCidade("Denver"), EncontrarCidade("Phoenix"), Cor.BRANCO, 4));
            rotas.Add(new Rota("PHO-LA-1", EncontrarCidade("Phoenix"), EncontrarCidade("Los Angeles"), Cor.CINZA, 3));
            rotas.Add(new Rota("PHO-SF-1", EncontrarCidade("Phoenix"), EncontrarCidade("Santa Fe"), Cor.CINZA, 3));
            rotas.Add(new Rota("SF-EP-1", EncontrarCidade("Santa Fe"), EncontrarCidade("El Paso"), Cor.CINZA, 2));
            rotas.Add(new Rota("EP-DAL-1", EncontrarCidade("El Paso"), EncontrarCidade("Dallas"), Cor.VERMELHO, 4));
            rotas.Add(new Rota("EP-HOU-1", EncontrarCidade("El Paso"), EncontrarCidade("Houston"), Cor.VERDE, 6));
            rotas.Add(new Rota("EP-NO-1", EncontrarCidade("El Paso"), EncontrarCidade("New Orleans"), Cor.ROSA, 5));
            rotas.Add(new Rota("EP-ATL-1", EncontrarCidade("El Paso"), EncontrarCidade("Atlanta"), Cor.AMARELO, 6));
            rotas.Add(new Rota("EP-MIA-1", EncontrarCidade("El Paso"), EncontrarCidade("Miami"), Cor.AZUL, 6));
            rotas.Add(new Rota("EP-CHA-1", EncontrarCidade("El Paso"), EncontrarCidade("Charleston"), Cor.LARANJA, 5));
            rotas.Add(new Rota("EP-RAL-1", EncontrarCidade("El Paso"), EncontrarCidade("Raleigh"), Cor.PRETO, 5));
            rotas.Add(new Rota("EP-WAS-1", EncontrarCidade("El Paso"), EncontrarCidade("Washington"), Cor.BRANCO, 5));
            rotas.Add(new Rota("EP-NY-1", EncontrarCidade("El Paso"), EncontrarCidade("New York"), Cor.VERMELHO, 5));
            rotas.Add(new Rota("EP-BOS-1", EncontrarCidade("El Paso"), EncontrarCidade("Boston"), Cor.AZUL, 5));
            rotas.Add(new Rota("EP-MON-1", EncontrarCidade("El Paso"), EncontrarCidade("Montreal"), Cor.VERDE, 5));
            rotas.Add(new Rota("EP-TOR-1", EncontrarCidade("El Paso"), EncontrarCidade("Toronto"), Cor.ROSA, 5));
            rotas.Add(new Rota("EP-CHI-1", EncontrarCidade("El Paso"), EncontrarCidade("Chicago"), Cor.AMARELO, 5));
            rotas.Add(new Rota("EP-STL-1", EncontrarCidade("El Paso"), EncontrarCidade("Saint Louis"), Cor.LARANJA, 5));
            rotas.Add(new Rota("EP-KC-1", EncontrarCidade("El Paso"), EncontrarCidade("Kansas City"), Cor.PRETO, 5));
            rotas.Add(new Rota("EP-OKC-1", EncontrarCidade("El Paso"), EncontrarCidade("Oklahoma City"), Cor.BRANCO, 5));
            rotas.Add(new Rota("EP-LR-1", EncontrarCidade("El Paso"), EncontrarCidade("Little Rock"), Cor.VERMELHO, 5));
            rotas.Add(new Rota("EP-NAS-1", EncontrarCidade("El Paso"), EncontrarCidade("Nashville"), Cor.AZUL, 5));
            rotas.Add(new Rota("EP-PIT-1", EncontrarCidade("El Paso"), EncontrarCidade("Pittsburgh"), Cor.VERDE, 5));
            rotas.Add(new Rota("EP-DUL-1", EncontrarCidade("El Paso"), EncontrarCidade("Duluth"), Cor.ROSA, 5));
            rotas.Add(new Rota("EP-SSM-1", EncontrarCidade("El Paso"), EncontrarCidade("Sault St. Marie"), Cor.AMARELO, 5));
            rotas.Add(new Rota("EP-WIN-1", EncontrarCidade("El Paso"), EncontrarCidade("Winnipeg"), Cor.LARANJA, 5));
            rotas.Add(new Rota("EP-CAL-1", EncontrarCidade("El Paso"), EncontrarCidade("Calgary"), Cor.PRETO, 5));
            rotas.Add(new Rota("EP-VAN-1", EncontrarCidade("El Paso"), EncontrarCidade("Vancouver"), Cor.BRANCO, 5));
            rotas.Add(new Rota("EP-SEA-1", EncontrarCidade("El Paso"), EncontrarCidade("Seattle"), Cor.VERMELHO, 5));
            rotas.Add(new Rota("EP-POR-1", EncontrarCidade("El Paso"), EncontrarCidade("Portland"), Cor.AZUL, 5));
            rotas.Add(new Rota("EP-SF-1", EncontrarCidade("El Paso"), EncontrarCidade("San Francisco"), Cor.VERDE, 5));
            rotas.Add(new Rota("EP-LA-1", EncontrarCidade("El Paso"), EncontrarCidade("Los Angeles"), Cor.ROSA, 5));
            rotas.Add(new Rota("EP-LV-1", EncontrarCidade("El Paso"), EncontrarCidade("Las Vegas"), Cor.AMARELO, 5));
            rotas.Add(new Rota("EP-SLC-1", EncontrarCidade("El Paso"), EncontrarCidade("Salt Lake City"), Cor.LARANJA, 5));
            rotas.Add(new Rota("EP-DEN-1", EncontrarCidade("El Paso"), EncontrarCidade("Denver"), Cor.PRETO, 5));
            rotas.Add(new Rota("EP-HEL-1", EncontrarCidade("El Paso"), EncontrarCidade("Helena"), Cor.BRANCO, 5));
            rotas.Add(new Rota("EP-PHO-1", EncontrarCidade("El Paso"), EncontrarCidade("Phoenix"), Cor.VERMELHO, 5));
            rotas.Add(new Rota("EP-SF-1", EncontrarCidade("El Paso"), EncontrarCidade("Santa Fe"), Cor.AZUL, 5));
            
            return rotas;
        }

        public static List<BilheteDestino> ObterBilhetesDestino()
        {
            var cidades = ObterCidades();
            Cidade EncontrarCidade(string nome) => cidades.First(c => c.Nome == nome);

            return new List<BilheteDestino>
            {
                // Bilhetes de curta distância (1-6 pontos)
                new BilheteDestino(EncontrarCidade("Vancouver"), EncontrarCidade("Seattle"), 1),
                new BilheteDestino(EncontrarCidade("Seattle"), EncontrarCidade("Portland"), 1),
                new BilheteDestino(EncontrarCidade("Portland"), EncontrarCidade("San Francisco"), 5),
                new BilheteDestino(EncontrarCidade("San Francisco"), EncontrarCidade("Los Angeles"), 3),
                new BilheteDestino(EncontrarCidade("Los Angeles"), EncontrarCidade("Las Vegas"), 2),
                new BilheteDestino(EncontrarCidade("Las Vegas"), EncontrarCidade("Salt Lake City"), 3),
                new BilheteDestino(EncontrarCidade("Salt Lake City"), EncontrarCidade("Denver"), 3),
                new BilheteDestino(EncontrarCidade("Denver"), EncontrarCidade("Phoenix"), 4),
                new BilheteDestino(EncontrarCidade("Phoenix"), EncontrarCidade("Los Angeles"), 3),
                new BilheteDestino(EncontrarCidade("Santa Fe"), EncontrarCidade("El Paso"), 2),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Dallas"), 4),
                new BilheteDestino(EncontrarCidade("Dallas"), EncontrarCidade("Houston"), 1),
                new BilheteDestino(EncontrarCidade("Houston"), EncontrarCidade("New Orleans"), 2),
                new BilheteDestino(EncontrarCidade("New Orleans"), EncontrarCidade("Miami"), 6),
                new BilheteDestino(EncontrarCidade("Miami"), EncontrarCidade("Atlanta"), 4),
                new BilheteDestino(EncontrarCidade("Atlanta"), EncontrarCidade("Charleston"), 2),
                new BilheteDestino(EncontrarCidade("Charleston"), EncontrarCidade("Raleigh"), 2),
                new BilheteDestino(EncontrarCidade("Raleigh"), EncontrarCidade("Washington"), 2),
                new BilheteDestino(EncontrarCidade("Washington"), EncontrarCidade("New York"), 2),
                new BilheteDestino(EncontrarCidade("New York"), EncontrarCidade("Boston"), 2),
                new BilheteDestino(EncontrarCidade("Boston"), EncontrarCidade("Montreal"), 2),
                new BilheteDestino(EncontrarCidade("Montreal"), EncontrarCidade("Toronto"), 3),
                new BilheteDestino(EncontrarCidade("Toronto"), EncontrarCidade("Chicago"), 4),
                new BilheteDestino(EncontrarCidade("Chicago"), EncontrarCidade("Saint Louis"), 2),
                new BilheteDestino(EncontrarCidade("Saint Louis"), EncontrarCidade("Kansas City"), 2),
                new BilheteDestino(EncontrarCidade("Kansas City"), EncontrarCidade("Oklahoma City"), 2),
                new BilheteDestino(EncontrarCidade("Oklahoma City"), EncontrarCidade("Little Rock"), 2),
                new BilheteDestino(EncontrarCidade("Little Rock"), EncontrarCidade("Nashville"), 3),
                new BilheteDestino(EncontrarCidade("Nashville"), EncontrarCidade("Atlanta"), 1),
                new BilheteDestino(EncontrarCidade("Nashville"), EncontrarCidade("Pittsburgh"), 3),
                new BilheteDestino(EncontrarCidade("Pittsburgh"), EncontrarCidade("New York"), 2),
                new BilheteDestino(EncontrarCidade("Pittsburgh"), EncontrarCidade("Chicago"), 3),
                new BilheteDestino(EncontrarCidade("Chicago"), EncontrarCidade("Duluth"), 3),
                new BilheteDestino(EncontrarCidade("Duluth"), EncontrarCidade("Sault St. Marie"), 3),
                new BilheteDestino(EncontrarCidade("Sault St. Marie"), EncontrarCidade("Montreal"), 5),
                new BilheteDestino(EncontrarCidade("Duluth"), EncontrarCidade("Winnipeg"), 4),
                new BilheteDestino(EncontrarCidade("Winnipeg"), EncontrarCidade("Calgary"), 6),
                new BilheteDestino(EncontrarCidade("Calgary"), EncontrarCidade("Vancouver"), 3),
                new BilheteDestino(EncontrarCidade("Helena"), EncontrarCidade("Calgary"), 4),
                new BilheteDestino(EncontrarCidade("Helena"), EncontrarCidade("Winnipeg"), 4),
                new BilheteDestino(EncontrarCidade("Denver"), EncontrarCidade("Helena"), 4),
                new BilheteDestino(EncontrarCidade("Denver"), EncontrarCidade("Kansas City"), 4),
                new BilheteDestino(EncontrarCidade("Denver"), EncontrarCidade("Oklahoma City"), 4),
                new BilheteDestino(EncontrarCidade("Denver"), EncontrarCidade("Santa Fe"), 2),
                new BilheteDestino(EncontrarCidade("Salt Lake City"), EncontrarCidade("Helena"), 3),
                new BilheteDestino(EncontrarCidade("Helena"), EncontrarCidade("Duluth"), 6),
                new BilheteDestino(EncontrarCidade("Duluth"), EncontrarCidade("Kansas City"), 3),
                new BilheteDestino(EncontrarCidade("Kansas City"), EncontrarCidade("Saint Louis"), 2),
                new BilheteDestino(EncontrarCidade("Saint Louis"), EncontrarCidade("Chicago"), 2),
                new BilheteDestino(EncontrarCidade("Chicago"), EncontrarCidade("Toronto"), 4),
                new BilheteDestino(EncontrarCidade("Toronto"), EncontrarCidade("Montreal"), 3),
                new BilheteDestino(EncontrarCidade("Montreal"), EncontrarCidade("Sault St. Marie"), 3),
                new BilheteDestino(EncontrarCidade("Sault St. Marie"), EncontrarCidade("Duluth"), 3),
                new BilheteDestino(EncontrarCidade("Duluth"), EncontrarCidade("Helena"), 6),
                new BilheteDestino(EncontrarCidade("Helena"), EncontrarCidade("Salt Lake City"), 3),
                new BilheteDestino(EncontrarCidade("Salt Lake City"), EncontrarCidade("Denver"), 3),
                new BilheteDestino(EncontrarCidade("Denver"), EncontrarCidade("Kansas City"), 4),
                new BilheteDestino(EncontrarCidade("Denver"), EncontrarCidade("Oklahoma City"), 4),
                new BilheteDestino(EncontrarCidade("Oklahoma City"), EncontrarCidade("Dallas"), 2),
                new BilheteDestino(EncontrarCidade("Dallas"), EncontrarCidade("Houston"), 1),
                new BilheteDestino(EncontrarCidade("Houston"), EncontrarCidade("New Orleans"), 2),
                new BilheteDestino(EncontrarCidade("New Orleans"), EncontrarCidade("Atlanta"), 4),
                new BilheteDestino(EncontrarCidade("Atlanta"), EncontrarCidade("Miami"), 4),
                new BilheteDestino(EncontrarCidade("Miami"), EncontrarCidade("New Orleans"), 6),
                new BilheteDestino(EncontrarCidade("New Orleans"), EncontrarCidade("Houston"), 2),
                new BilheteDestino(EncontrarCidade("Houston"), EncontrarCidade("Dallas"), 1),
                new BilheteDestino(EncontrarCidade("Dallas"), EncontrarCidade("Oklahoma City"), 2),
                new BilheteDestino(EncontrarCidade("Oklahoma City"), EncontrarCidade("Kansas City"), 2),
                new BilheteDestino(EncontrarCidade("Kansas City"), EncontrarCidade("Denver"), 4),
                new BilheteDestino(EncontrarCidade("Denver"), EncontrarCidade("Salt Lake City"), 3),
                new BilheteDestino(EncontrarCidade("Salt Lake City"), EncontrarCidade("Las Vegas"), 3),
                new BilheteDestino(EncontrarCidade("Las Vegas"), EncontrarCidade("Los Angeles"), 2),
                new BilheteDestino(EncontrarCidade("Los Angeles"), EncontrarCidade("San Francisco"), 3),
                new BilheteDestino(EncontrarCidade("San Francisco"), EncontrarCidade("Portland"), 5),
                new BilheteDestino(EncontrarCidade("Portland"), EncontrarCidade("Seattle"), 1),
                new BilheteDestino(EncontrarCidade("Seattle"), EncontrarCidade("Vancouver"), 1),
                new BilheteDestino(EncontrarCidade("Vancouver"), EncontrarCidade("Calgary"), 3),
                new BilheteDestino(EncontrarCidade("Calgary"), EncontrarCidade("Winnipeg"), 6),
                new BilheteDestino(EncontrarCidade("Winnipeg"), EncontrarCidade("Duluth"), 4),
                new BilheteDestino(EncontrarCidade("Duluth"), EncontrarCidade("Helena"), 6),
                new BilheteDestino(EncontrarCidade("Helena"), EncontrarCidade("Denver"), 4),
                new BilheteDestino(EncontrarCidade("Denver"), EncontrarCidade("Phoenix"), 4),
                new BilheteDestino(EncontrarCidade("Phoenix"), EncontrarCidade("Los Angeles"), 3),
                new BilheteDestino(EncontrarCidade("Phoenix"), EncontrarCidade("Santa Fe"), 3),
                new BilheteDestino(EncontrarCidade("Santa Fe"), EncontrarCidade("El Paso"), 2),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Dallas"), 4),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Houston"), 6),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("New Orleans"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Atlanta"), 6),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Miami"), 6),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Charleston"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Raleigh"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Washington"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("New York"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Boston"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Montreal"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Toronto"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Chicago"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Saint Louis"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Kansas City"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Oklahoma City"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Little Rock"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Nashville"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Pittsburgh"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Duluth"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Sault St. Marie"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Winnipeg"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Calgary"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Vancouver"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Seattle"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Portland"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("San Francisco"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Los Angeles"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Las Vegas"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Salt Lake City"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Denver"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Helena"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Phoenix"), 5),
                new BilheteDestino(EncontrarCidade("El Paso"), EncontrarCidade("Santa Fe"), 5)
            };
        }

        public static Dictionary<int, int> ObterTabelaPontuacao()
        {
            return new Dictionary<int, int>
            {
                { 1, 1 },
                { 2, 2 },
                { 3, 4 },
                { 4, 7 },
                { 5, 10 },
                { 6, 15 }
            };
        }
    }
}
