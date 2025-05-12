using CartolaFCRPG.Models;

namespace CartolaFCRPG.utils
{
    public class SimuladorPartida
    {
        private readonly Campo _campo;
        private readonly List<Jogador> _jogadores;
        private int _tickAtual = 0;
        private const int MaxTicks = 10; // 90 minutos * 60 segundos = 5400 (1 tick por segundo)

        private static Random _random = new Random();

        public SimuladorPartida(Campo campo, List<Jogador> jogadores)
        {
            _campo = campo;
            _jogadores = jogadores;
        }

        public void IniciarSimulacao()
        {
            while (_tickAtual < MaxTicks)
            {
                Tick();
                _tickAtual++;
            }

            Console.WriteLine("Fim da partida!");
        }

        private void Tick()
        {
            // passos
            // 1. Executa a ação do jogador com a bola
            // 1.2 atualizar a posse
            // 2. Movimenta todos os jogadores (ou deixa-os parados)
            // 3. Loga eventos e salva o estado atual

            // 1. O jogador com a bola decide o que fazer
            ExecutarAcaoDoJogadorComBola();

            // 2. Jogadores adversários podem interceptar ou desarmar
            // ResolverIntercepcoes();

            // 3. A posse pode mudar se a ação falhar ou for interceptada
            // AtualizarPosseDeBola();

            // 4. Todos os jogadores se movem com base no perfil tático e posse
            ExecutarMovimentacoes();

            // 5. (Opcional) Log de ações para replay ou depuração
            LogEventos();
        }

        private void ExecutarAcaoDoJogadorComBola()
        {
            var jogador = _campo.posse.JogadorComBola;
            if (jogador == null)
                return;

            var companheiros = _jogadores
                .Where(j => j.Time == jogador.Time && j != jogador)
                .ToList();
            var oponentes = _jogadores.Where(j => j.Time != jogador.Time).ToList();

            var acao = DecisorComBola.EscolherAcaoComBola(
                jogador,
                companheiros,
                oponentes,
                _random
            );

            switch (acao)
            {
                case AcaoComBola.Chutar:
                    // _campo.TentarChuteAGol(jogador); não implementado
                    Console.WriteLine($"{jogador.Nome} chutou a gol!");
                    break;

                case AcaoComBola.Passar:
                    var alvoPasse = companheiros.FirstOrDefault(); // Exemplo: escolher o primeiro companheiro
                    if (alvoPasse != null)
                        Passes.RealizarPasse(jogador, alvoPasse, _jogadores, _campo.posse, _random); // _jogadores está errado, preciso fazer calcular isso
                    break;

                case AcaoComBola.Conduzir:
                    var direcao = DecisorDeMovimentacao.DecidirNovaPosicao(jogador, true, _random); // Exemplo: decidir direção de condução
                    Movimentacao.MoverJogadorHorizontal(jogador, (int)direcao.Faixa);
                    Movimentacao.MoverJogadorVertical(jogador, (int)direcao.Zona);
                    break;

                case AcaoComBola.FicarParado:
                    // Jogador não faz nada
                    break;
            }
        }

        // private void ResolverIntercepcoes()
        // {
        //     var adversarios = _jogadores.Where(j => j.Time != _posse.TimeComPosse);
        //     foreach (var defensor in adversarios)
        //     {
        //         if (_campo.PodeInterceptar(defensor, _posse.JogadorComBola))
        //         {
        //             bool interceptou = defensor.TentarInterceptar(_posse.JogadorComBola);
        //             if (interceptou)
        //             {
        //                 _posse.PerderPossePara(defensor);
        //                 break;
        //             }
        //         }
        //     }
        // }

        // private void AtualizarPosseDeBola()
        // {
        //     // Já é tratado na lógica de interceptação ou passe mal-sucedido
        // }

        private void ExecutarMovimentacoes()
        {
            foreach (var jogador in _jogadores)
            {
                if (jogador != _campo.posse.JogadorComBola)
                {
                    var direcao = DecisorDeMovimentacao.DecidirNovaPosicao(
                        jogador,
                        _campo.posse.TimeComPosse == jogador.Time,
                        _random
                    );
                    Movimentacao.MoverJogadorHorizontal(jogador, (int)direcao.Faixa);
                    Movimentacao.MoverJogadorVertical(jogador, (int)direcao.Zona);
                }
            }
        }

        private void LogEventos()
        {
            // Exemplo: registrar ações, trocas de posse, gols, etc.
        }
    }
}
