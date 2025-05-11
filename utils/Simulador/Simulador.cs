using CartolaFCRPG.Models;

namespace CartolaFCRPG.utils
{
    public class SimuladorPartida
    {
        private readonly Campo _campo;
        private readonly Posse _posse;
        private readonly List<Jogador> _jogadores;
        private int _tickAtual = 0;
        private const int MaxTicks = 10; // 90 minutos * 60 segundos = 5400 (1 tick por segundo)

        public SimuladorPartida(Campo campo, Posse posse, List<Jogador> jogadores)
        {
            _campo = campo;
            _posse = posse;
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
            ResolverIntercepcoes();

            // 3. A posse pode mudar se a ação falhar ou for interceptada
            AtualizarPosseDeBola();

            // 4. Todos os jogadores se movem com base no perfil tático e posse
            ExecutarMovimentacoes();

            // 5. (Opcional) Log de ações para replay ou depuração
            LogEventos();
        }

        private void ExecutarAcaoDoJogadorComBola()
        {
            var jogador = _posse.JogadorComBola;
            if (jogador == null)
                return;

            var acao = jogador.DecidirAcao(_campo, _posse);

            switch (acao.Tipo)
            {
                case TipoAcao.Conduzir:
                    _campo.MoverJogadorComBola(jogador, acao.Direcao);
                    break;

                case TipoAcao.Passe:
                    _campo.ExecutarPasse(jogador, acao.Alvo);
                    break;

                case TipoAcao.Chute:
                    _campo.TentarChuteAGol(jogador);
                    break;
            }
        }

        private void ResolverIntercepcoes()
        {
            var adversarios = _jogadores.Where(j => j.Time != _posse.TimeComPosse);
            foreach (var defensor in adversarios)
            {
                if (_campo.PodeInterceptar(defensor, _posse.JogadorComBola))
                {
                    bool interceptou = defensor.TentarInterceptar(_posse.JogadorComBola);
                    if (interceptou)
                    {
                        _posse.PerderPossePara(defensor);
                        break;
                    }
                }
            }
        }

        private void AtualizarPosseDeBola()
        {
            // Já é tratado na lógica de interceptação ou passe mal-sucedido
        }

        private void ExecutarMovimentacoes()
        {
            foreach (var jogador in _jogadores)
            {
                if (jogador != _posse.JogadorComBola)
                {
                    var direcao = jogador.DecidirMovimento(_campo, _posse);
                    _campo.MoverJogador(jogador, direcao);
                }
            }
        }

        private void LogEventos()
        {
            // Exemplo: registrar ações, trocas de posse, gols, etc.
        }
    }
}
