# Ticket to Ride - Jogo Digital Completo

Este projeto implementa uma versão digital e simplificada do jogo de tabuleiro Ticket to Ride como Trabalho Final da disciplina de Projeto de Software.
A aplicação utiliza ASP.NET Core 9.0 Web API como backend e HTML, CSS e JavaScript (sem frameworks) com Bootstrap no frontend, seguindo os princípios de Clean Architecture (camadas Domain, Application e API).

## 🚀 Tecnologias Utilizadas

- **Backend**: ASP.NET Core 9.0 Web API com Clean Architecture
- **Frontend**: HTML5, CSS3, JavaScript (Vanilla)
- **UI Framework**: Bootstrap 5.3.0
- **Ícones**: Font Awesome 6.0.0
- **Arquitetura**: Clean Architecture (Domain, Application, API)
- **Persistência**: Persistência: armazenamento em memória utilizando um Dictionary estático (sem banco de dados externo)
- **Padrões**: DTOs, Services, Repository, Observer, Mappers (Mapeamento entre Domain e DTOs)

## 📁 Estrutura do Projeto

```
TicketToRideAPI/
├── Connected Services/
├── Dependências/
├── Properties/
├── wwwroot/
│   ├── css/
│   │   └── style.css
│   ├── js/
│   │   ├── app.js
│   │   ├── jogo.js
│   │   └── partida.js
│   ├── default.html
│   └── index.html
│
├── Application/
│   ├── DTOs/
│   │   ├── Request/
│   │   │   ├── AdicionarJogadorRequest.cs
│   │   │   ├── ComprarBilhetesRequest.cs
│   │   │   ├── ComprarCartasRequest.cs
│   │   │   ├── IniciarPartidaRequest.cs
│   │   │   ├── PassarTurnoRequest.cs
│   │   │   └── ReivindicarRotaRequest.cs
│   │   ├── BilheteDestinoDTO.cs
│   │   ├── CartaDTO.cs
│   │   ├── CartaVeiculoDto.cs
│   │   ├── JogadorDTO.cs
│   │   ├── PartidaDTO.cs
│   │   ├── PontuacaoDTO.cs
│   │   ├── RotaDTO.cs
│   │   └── TurnoDTO.cs
│   │
│   ├── Mappers/
│   │   ├── Interfaces/
│   │   │   └── IMapper.cs
│   │   ├── BilheteDestinoMapper.cs
│   │   ├── CartaVeiculoMapper.cs
│   │   ├── CompositeMapper.cs
│   │   ├── JogadorMapper.cs
│   │   ├── PartidaMapper.cs
│   │   ├── RotaMapper.cs
│   │   └── TurnoMapper.cs
│   │
│   ├── Observers/
│   │   ├── CalculadorPontuacaoObserver.cs
│   │   ├── DistribuidorCartasObserver.cs
│   │   └── VerificarBilhetesObserver.cs
│   │
│   ├── Services/ orquestram regras de negócio e chamadas ao domínio.
│   │   ├── Interfaces/
│   │   │   ├── IJogadorService.cs
│   │   │   ├── IPartidaService.cs
│   │   │   └── ITurnoService.cs
│   │   ├── JogadorService.cs
│   │   ├── PartidaService.cs
│   │   └── TurnoService.cs
│   │
│   ├── DadosJogo.cs
│   ├── Configuration/
│   │   ├── MapperConfiguration.cs
│   │   └── ObserverConfiguration.cs
│   │
│   └── Controllers/ expõem os endpoints REST
│       ├── JogadorController.cs
│       ├── PartidaController.cs
│       └── TurnoController.cs
│
├── Domain/
│   ├── Entities/ representam os conceitos do jogo (Jogador, Rota, Partida, etc.).
│   │   ├── Baralho.cs
│   │   ├── BaralhoCartasDestino.cs
│   │   ├── BaralhoCartasVeiculo.cs
│   │   ├── BilheteDestino.cs
│   │   ├── Carta.cs
│   │   ├── CartaVeiculo.cs
│   │   ├── Cidade.cs
│   │   ├── Jogador.cs
│   │   ├── Partida.cs
│   │   ├── Rota.cs
│   │   ├── Tabuleiro.cs
│   │   └── Turno.cs
│   │
│   ├── Enums/
│   │   ├── Acao.cs
│   │   └── Cor.cs
│   │
│   ├── Interfaces/
│   │   ├── IJogadorSubject.cs
│   │   ├── IPartidaRepository.cs
│   │   ├── IPartidaSubject.cs
│   │   └── ISubject.cs
│   │
│   └── EnumExtensions.cs
│
├── Filters/
│   └── DomainExceptionFilter.cs
│
├── Infrastructure/
│   └── Repositories/ implementação do repositório de partidas em memória.
│       └── PartidaRepositoryMemory.cs
│
├── .gitignore
├── appsettings.json
├── LICENSE
└── Program.cs
```

## 🎮 Funcionalidades Implementadas

### ✅ Requisitos Funcionais Atendidos

- **RF01**: Gerenciamento de Partida Multijogador (2-5 jogadores)
- **RF02**: Mecânica de Turnos com 3 ações:
  - RF02.1: Comprar Cartas de Vagão
  - RF02.2: Reivindicar uma Rota
  - RF02.3: Comprar Bilhetes de Destino
- **RF03**: Gestão de Recursos (decks, embaralhamentos, mãos)
- **RF04**: Controle de Estado do Jogo (criar, iniciar, finalizar)
- **RF05**: Cálculo de Pontuação (imediato e final)

### ✅ Requisitos Não-Funcionais Atendidos

- **RNF01**: Desempenho < 3 segundos
- **RNF02**: Interface intuitiva com representação visual
- **RNF03**: Regras oficiais aplicadas consistentemente
- **RNF04**: Suporte a até 5 jogadores simultâneos sem degradação perceptível de performance.

## 🛠️ Como Executar

### Pré-requisitos
- [.NET 9.0 SDK](https://dotnet.microsoft.com/)
- Navegador web moderno (Chrome, Edge, Firefox, etc.)

### Passos para Executar

1. **Clonar o repositório:**
   ```bash
   git clone <url-do-repositorio>
   cd TicketToRideAPI

2. **Restaurar dependências e compilar:**
  Primeiro tem que buildar
   ```bash
   dotnet restore
   ```
   ```bash
   dotnet build
   ```
3. **Executar a aplicação:**
   ```bash
   dotnet run
   ```

4. **Acessar a aplicação:**
   - **Interface do jogo (frontend)**: https://localhost:5257
   - **API REST** (exemplos):
     - https://localhost:7000/api/partida
     - http://localhost:5000/api/partida

## 🎯 Como Jogar

### 1. Configuração da Partida
- Adicione entre 2 a 5 jogadores
- Cada jogador deve ter um nome único
- Clique em "Iniciar Partida" quando estiver pronto

### 2. Durante o Jogo
- **Sua vez**: Execute uma das 3 ações disponíveis
- **Comprar Cartas**: Adquira cartas de vagão do monte
- **Reivindicar Rota**: Use cartas para conquistar rotas entre cidades
- **Comprar Bilhetes**: Adquira bilhetes de destino para pontos extras

### 3. Objetivo
- Conquistar rotas para conectar cidades
- Completar bilhetes de destino
- Acumular a maior pontuação possível

### 4. Pontuação
- **Rotas**: 1-15 pontos baseado no tamanho
- **Bilhetes**: Pontos extras se completos, penalidade se incompletos
- **Bônus**: 10 pontos para a rota contínua mais longa

## 🔧 API Endpoints

### Partida
- `POST /api/partida/criar` - Criar nova partida
- `GET /api/partida/{id}` - Obter estado da partida
- `POST /api/partida/{id}/iniciar` - Iniciar partida
- `POST /api/partida/{id}/finalizar` - Finalizar partida
- `GET /api/partida/{id}/pontuacao` - Obter pontuação

### Jogador
- `POST /api/jogador/partida/{id}/jogador` - Adicionar jogador
- `GET /api/jogador/partida/{id}/jogador/{jogadorId}` - Obter jogador
- `DELETE /api/jogador/partida/{id}/jogador/{jogadorId}` - Remover jogador

### Turno
- `GET /api/turno/partida/{id}/turno/atual` - Obter turno atual
- `POST /api/turno/partida/{id}/turno/comprar-cartas` - Comprar cartas
- `POST /api/turno/partida/{id}/turno/reivindicar-rota` - Reivindicar rota
- `POST /api/turno/partida/{id}/turno/comprar-bilhetes` - Comprar bilhetes

## 🗺️ Dados do Jogo

O jogo inclui:
- **36 cidades** da América do Norte
- **100+ rotas** entre cidades com cores e tamanhos variados
- **30 bilhetes de destino** com diferentes valores de pontos
- **110 cartas de vagão** (12 de cada cor + 14 locomotivas)

## 🐛 Solução de Problemas

### Erro de Certificado SSL
Se houver problemas com HTTPS:
1. Aceitar o certificado no navegador
2. Ou usar HTTP: http://localhost:5000

### Porta em Uso
Se as portas estiverem ocupadas, edite `Properties/launchSettings.json`

### CORS Issues
O CORS está configurado para localhost:3000 e 127.0.0.1:3000
