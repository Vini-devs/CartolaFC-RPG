public namespace CartolaFCRPG.utils {
// essas probabilidades estão errads pq dependem do perfiltatico e de ter a posse de bola
    public class DecisorComBola
    {
        private readonly Random _rand = new();

        public AcaoComBola EscolherAcaoComBola(Jogador jogador, Zona zonaAtual, List<Jogador> companheiros, List<Jogador> oponentes)
        {
            var acoes = new List<(AcaoComBola acao, int peso)>();

            // Exemplo de lógica contextual:
            if (zonaAtual == Zona.GrandeAreaAdversaria)
            {
                acoes.Add((AcaoComBola.Chutar, jogador.PrecisaoFinalizacao + jogador.Mentalidade / 2));
                acoes.Add((AcaoComBola.Passar, jogador.PrecisaoPasse));
                acoes.Add((AcaoComBola.Conduzir, jogador.Tecnica / 2));
            }
            else if (zonaAtual == Zona.MeioCampo)
            {
                acoes.Add((AcaoComBola.Passar, jogador.PrecisaoPasse + 10));
                acoes.Add((AcaoComBola.Conduzir, jogador.Tecnica));
            }
            else
            {
                acoes.Add((AcaoComBola.Conduzir, jogador.Tecnica));
                acoes.Add((AcaoComBola.Passar, jogador.PrecisaoPasse / 2));
            }

            // Todos têm chance de ficar parado (raro, mas pode simular hesitação)
            acoes.Add((AcaoComBola.FicarParado, 5));

            return Sorteia(acoes);
        }

        private AcaoComBola Sorteia(List<(AcaoComBola acao, int peso)> acoes)
        {
            int total = acoes.Sum(a => a.peso);
            int roleta = _rand.Next(0, total);
            int acumulado = 0;

            foreach (var (acao, peso) in acoes)
            {
                acumulado += peso;
                if (roleta < acumulado)
                    return acao;
            }

            return AcaoComBola.FicarParado;
        }
    }



    public class DecisorSemBola
    {
        private readonly Random _rand = new();

        public AcaoSemBola EscolherAcaoSemBola(Jogador jogador, Zona zonaAtual, PosicaoCampo bolaPosicao)
        {
            // Exemplo: se estiver longe da bola, mais chances de se mover
            int distancia = jogador.PosicaoCampo.CalcularDistancia(bolaPosicao);

            var acoes = new List<(AcaoSemBola acao, int peso)>
            {
                (AcaoSemBola.MoverSe, distancia > 1 ? 90 : 50),
                (AcaoSemBola.FicarParado, 100 - (distancia > 1 ? 90 : 50))
            };

            return Sorteia(acoes);
        }

        private AcaoSemBola Sorteia(List<(AcaoSemBola acao, int peso)> acoes)
        {
            int total = acoes.Sum(a => a.peso);
            int roleta = _rand.Next(0, total);
            int acumulado = 0;

            foreach (var (acao, peso) in acoes)
            {
                acumulado += peso;
                if (roleta < acumulado)
                    return acao;
            }

            return AcaoSemBola.FicarParado;
        }
    }
}