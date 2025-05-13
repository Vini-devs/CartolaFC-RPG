using CartolaFCRPG.Models;

namespace CartolaFCRPG.utils
{
    public static class PosicoesIniciais
    {
        public static readonly Dictionary<PerfilTatico, PosicaoCampo> Posicoes = new()
        {
            [PerfilTatico.Zagueiro] = new PosicaoCampo((ZonaCampoVertical)0, (FaixaLateral)1),
            [PerfilTatico.LateralEsquerdo] = new PosicaoCampo(
                (ZonaCampoVertical)0,
                (FaixaLateral)0
            ), // Grande área, esquerda
            [PerfilTatico.LateralDireito] = new PosicaoCampo((ZonaCampoVertical)0, (FaixaLateral)2),
            [PerfilTatico.Volante] = new PosicaoCampo((ZonaCampoVertical)1, (FaixaLateral)1),
            [PerfilTatico.MeiaCentral] = new PosicaoCampo((ZonaCampoVertical)1, (FaixaLateral)1),
            [PerfilTatico.AlaEsquerdo] = new PosicaoCampo((ZonaCampoVertical)1, (FaixaLateral)0),
            [PerfilTatico.AlaDireito] = new PosicaoCampo((ZonaCampoVertical)1, (FaixaLateral)2),
            [PerfilTatico.PontaEsquerda] = new PosicaoCampo((ZonaCampoVertical)2, (FaixaLateral)0),
            [PerfilTatico.PontaDireita] = new PosicaoCampo((ZonaCampoVertical)2, (FaixaLateral)2),
            [PerfilTatico.Atacante] = new PosicaoCampo((ZonaCampoVertical)2, (FaixaLateral)1),
        };

        public static PosicaoCampo GetPosicaoInicial(Jogador jogador) => jogador.Time == campo.TimeCasa ? Posicoes[jogador.PerfilTatico] : EspelharPosicao(Posicoes[jogador.PerfilTatico]);

        public static PosicaoCampo EspelharPosicao(PosicaoCampo pos)
        {
            int novaLinha = MapaMovimentacao.ALTURA_CAMPO - 1 - (int)pos.ZonaVertical;
            int novaColuna = MapaMovimentacao.LARGURA_CAMPO - 1 - (int)pos.FaixaLateral;
            return new PosicaoCampo((ZonaCampoVertical)novaLinha, (FaixaLateral)novaColuna);
        }
    }
}
