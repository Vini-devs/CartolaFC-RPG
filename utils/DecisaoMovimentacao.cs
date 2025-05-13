using CartolaFCRPG.Models;

namespace CartolaFCRPG.utils
{
    public class MapaMovimentacao
    {
        public const int ALTURA_CAMPO = 5; // 0 = grande área própria, 4 = grande área adversária
        public const int LARGURA_CAMPO = 3; // 0 = esquerda, 1 = centro, 2 = direita

        // [linha, coluna] => peso de movimentação (0 = nunca se move para lá, quanto maior, mais probabilidade)
        public int[,] PesosComBola { get; set; } = new int[ALTURA_CAMPO, LARGURA_CAMPO];
        public int[,] PesosSemBola { get; set; } = new int[ALTURA_CAMPO, LARGURA_CAMPO];
    }

    public static class DecisorDeMovimentacao
    {
        public static PosicaoCampo? DecidirNovaPosicao(
            Jogador jogador,
            bool timeTemPosse,
        )
        {
            if (jogador.Time == campo.TimeCasa)
            {
                var mapa = TaticasMovimentacao.Mapas[jogador.PerfilTatico];
            } else {

            }

            var pesos = timeTemPosse ? mapa.PesosComBola : mapa.PesosSemBola;

            var alternativas = new List<(PosicaoCampo pos, int peso)>();

            for (int i = 0; i < MapaMovimentacao.ALTURA_CAMPO; i++)
            {
                for (int j = 0; j < MapaMovimentacao.LARGURA_CAMPO; j++)
                {
                    int peso = pesos[i, j];
                    if (peso > 0)
                        alternativas.Add(
                            (new PosicaoCampo((ZonaCampoVertical)i, (FaixaLateral)j), peso)
                        );
                }
            }

            if (!alternativas.Any())
                return null;

            int totalPeso = alternativas.Sum(a => a.peso);
            int roleta = random.Next(0, totalPeso);
            int acumulado = 0;

            foreach (var alt in alternativas)
            {
                acumulado += alt.peso;
                if (roleta < acumulado)
                    return alt.pos;
            }

            return null;
        }
    }

    public static class TaticasMovimentacao
    {
        public static Dictionary<PerfilTatico, MapaMovimentacao> Mapas = new()
        {
            [PerfilTatico.Zagueiro] = new MapaMovimentacao
            {
                PesosComBola = new int[5, 3]
                {
                    { 10, 20, 10 },
                    { 5, 10, 5 },
                    { 0, 2, 0 },
                    { 0, 0, 0 },
                    { 0, 0, 0 },
                },
                PesosSemBola = new int[5, 3]
                {
                    { 20, 30, 20 },
                    { 10, 15, 10 },
                    { 0, 2, 0 },
                    { 0, 0, 0 },
                    { 0, 0, 0 },
                },
            },

            [PerfilTatico.LateralEsquerdo] = new MapaMovimentacao
            {
                PesosComBola = new int[5, 3]
                {
                    { 15, 0, 0 },
                    { 20, 0, 0 },
                    { 25, 0, 0 },
                    { 15, 0, 0 },
                    { 5, 0, 0 },
                },
                PesosSemBola = new int[5, 3]
                {
                    { 20, 0, 0 },
                    { 25, 0, 0 },
                    { 15, 0, 0 },
                    { 5, 0, 0 },
                    { 0, 0, 0 },
                },
            },

            [PerfilTatico.LateralDireito] = new MapaMovimentacao
            {
                PesosComBola = new int[5, 3]
                {
                    { 0, 0, 15 },
                    { 0, 0, 20 },
                    { 0, 0, 25 },
                    { 0, 0, 15 },
                    { 0, 0, 5 },
                },
                PesosSemBola = new int[5, 3]
                {
                    { 0, 0, 20 },
                    { 0, 0, 25 },
                    { 0, 0, 15 },
                    { 0, 0, 5 },
                    { 0, 0, 0 },
                },
            },

            [PerfilTatico.Volante] = new MapaMovimentacao
            {
                PesosComBola = new int[5, 3]
                {
                    { 5, 10, 5 },
                    { 10, 20, 10 },
                    { 15, 30, 15 },
                    { 5, 10, 5 },
                    { 0, 0, 0 },
                },
                PesosSemBola = new int[5, 3]
                {
                    { 10, 15, 10 },
                    { 20, 25, 20 },
                    { 15, 20, 15 },
                    { 5, 10, 5 },
                    { 0, 0, 0 },
                },
            },

            [PerfilTatico.MeiaCentral] = new MapaMovimentacao
            {
                PesosComBola = new int[5, 3]
                {
                    { 0, 0, 0 },
                    { 5, 10, 5 },
                    { 15, 30, 15 },
                    { 15, 30, 15 },
                    { 5, 10, 5 },
                },
                PesosSemBola = new int[5, 3]
                {
                    { 0, 0, 0 },
                    { 5, 10, 5 },
                    { 10, 20, 10 },
                    { 10, 20, 10 },
                    { 5, 10, 5 },
                },
            },

            [PerfilTatico.AlaEsquerdo] = new MapaMovimentacao
            {
                PesosComBola = new int[5, 3]
                {
                    { 0, 0, 0 },
                    { 5, 0, 0 },
                    { 20, 0, 0 },
                    { 25, 0, 0 },
                    { 10, 0, 0 },
                },
                PesosSemBola = new int[5, 3]
                {
                    { 0, 0, 0 },
                    { 10, 0, 0 },
                    { 15, 0, 0 },
                    { 15, 0, 0 },
                    { 5, 0, 0 },
                },
            },

            [PerfilTatico.AlaDireito] = new MapaMovimentacao
            {
                PesosComBola = new int[5, 3]
                {
                    { 0, 0, 0 },
                    { 0, 0, 5 },
                    { 0, 0, 20 },
                    { 0, 0, 25 },
                    { 0, 0, 10 },
                },
                PesosSemBola = new int[5, 3]
                {
                    { 0, 0, 0 },
                    { 0, 0, 10 },
                    { 0, 0, 15 },
                    { 0, 0, 15 },
                    { 0, 0, 5 },
                },
            },

            [PerfilTatico.PontaEsquerda] = new MapaMovimentacao
            {
                PesosComBola = new int[5, 3]
                {
                    { 0, 0, 0 },
                    { 0, 0, 0 },
                    { 10, 0, 0 },
                    { 25, 0, 0 },
                    { 15, 0, 0 },
                },
                PesosSemBola = new int[5, 3]
                {
                    { 0, 0, 0 },
                    { 0, 0, 0 },
                    { 5, 0, 0 },
                    { 20, 0, 0 },
                    { 10, 0, 0 },
                },
            },

            [PerfilTatico.PontaDireita] = new MapaMovimentacao
            {
                PesosComBola = new int[5, 3]
                {
                    { 0, 0, 0 },
                    { 0, 0, 0 },
                    { 0, 0, 10 },
                    { 0, 0, 25 },
                    { 0, 0, 15 },
                },
                PesosSemBola = new int[5, 3]
                {
                    { 0, 0, 0 },
                    { 0, 0, 0 },
                    { 0, 0, 5 },
                    { 0, 0, 20 },
                    { 0, 0, 10 },
                },
            },

            [PerfilTatico.Atacante] = new MapaMovimentacao
            {
                PesosComBola = new int[5, 3]
                {
                    { 0, 0, 0 },
                    { 0, 5, 0 },
                    { 5, 15, 5 },
                    { 15, 30, 15 },
                    { 20, 30, 20 },
                },
                PesosSemBola = new int[5, 3]
                {
                    { 0, 0, 0 },
                    { 0, 5, 0 },
                    { 5, 10, 5 },
                    { 10, 25, 10 },
                    { 15, 30, 15 },
                },
            },
        };
    

    }
}
