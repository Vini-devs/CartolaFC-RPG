using CartolaFCRPG.Models;

// A MATURAR IDEIA

namespace CartolaFCRPG.utils
{
    public static class DecisorDeFinalizacao
    {
        public static bool DecidirFinalizacao(
            Jogador jogador,
            PosicaoCampo posicaoAtual,
            Random random
        )
        {
            // Pesos baseados no perfil tático e posição no campo
            int pesoBase = TaticasFinalizacao.Mapas[jogador.PerfilTatico][(int)posicaoAtual.Zona];

            // Ajuste baseado no atributo de finalização do jogador
            int pesoFinalizacao = pesoBase + jogador.PrecisaoFinalizacao;

            // Ajuste baseado na força do jogador (chutes de longa distância)
            if (posicaoAtual.Zona == ZonaCampoVertical.MeioCampo)
            {
                pesoFinalizacao += jogador.Forca / 2;
            }

            // Determinar probabilidade de finalização
            int roleta = random.Next(0, 100);
            return roleta < pesoFinalizacao;
        }
    }

    public static class TaticasFinalizacao
    {
        public static Dictionary<PerfilTatico, int[]> Mapas = new()
        {
            [PerfilTatico.Atacante] = new int[] { 10, 20, 50, 80, 100 },
            [PerfilTatico.MeiaCentral] = new int[] { 5, 10, 30, 50, 70 },
            [PerfilTatico.Volante] = new int[] { 0, 5, 10, 20, 30 },
            [PerfilTatico.Zagueiro] = new int[] { 0, 0, 5, 10, 20 },
            [PerfilTatico.LateralEsquerdo] = new int[] { 0, 5, 10, 20, 30 },
            [PerfilTatico.LateralDireito] = new int[] { 0, 5, 10, 20, 30 },
            [PerfilTatico.PontaEsquerda] = new int[] { 0, 10, 20, 40, 60 },
            [PerfilTatico.PontaDireita] = new int[] { 0, 10, 20, 40, 60 },
        };
    }
}
