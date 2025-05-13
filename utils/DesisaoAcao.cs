using CartolaFCRPG.Models;

namespace CartolaFCRPG.utils
{
    // no caso da movimentação, as probabilidades dependem do perfiltatico e de ter a posse de bola
    public static class DecisorComBola
    {
        public static AcaoComBola EscolherAcaoComBola(Jogador jogador, Random random)
        {
            var acoes = new List<(AcaoComBola acao, int peso)>();

            // Exemplo de lógica contextual:
            if (jogador.PosicaoAtual.Zona == (ZonaCampoVertical)1) //1
            {
                acoes.Add(
                    (AcaoComBola.Chutar, jogador.PrecisaoFinalizacao + jogador.Mentalidade / 2)
                );
                acoes.Add((AcaoComBola.Passar, jogador.PrecisaoPasse));
                acoes.Add((AcaoComBola.Conduzir, jogador.Tecnica / 2));
            }
            else if (jogador.PosicaoAtual.Zona == (ZonaCampoVertical)2)
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

            return Sorteia(acoes, random);
        }

        private static AcaoComBola Sorteia(List<(AcaoComBola acao, int peso)> acoes, Random random)
        {
            int total = acoes.Sum(a => a.peso);
            int roleta = random.Next(0, total);
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

        public AcaoSemBola EscolherAcaoSemBola(Jogador jogador, PosicaoCampo bolaPosicao)
        {
            // Exemplo: se estiver longe da bola, mais chances de se mover
            int distancia = jogador.PosicaoAtual.CalcularDistancia(bolaPosicao);

            var acoes = new List<(AcaoSemBola acao, int peso)>
            {
                (AcaoSemBola.MoverSe, distancia > 1 ? 90 : 50),
                (AcaoSemBola.FicarParado, 100 - (distancia > 1 ? 90 : 50)),
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
