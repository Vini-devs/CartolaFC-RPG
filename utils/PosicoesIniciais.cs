using CartolaFCRPG.Models;

namespace CartolaFCRPG.utils
{
    public static class PosicoesIniciais
    {
        public static readonly Dictionary<PerfilTatico, PosicaoCampo> Posições = new()
        {
            [PerfilTatico.Zagueiro] = new PosicaoCampo((ZonaCampoVertical)0, (FaixaLateral)1), // Grande área, centro
            [PerfilTatico.LateralEsquerdo] = new PosicaoCampo(
                (ZonaCampoVertical)0,
                (FaixaLateral)0
            ), // Grande área, esquerda
            [PerfilTatico.LateralDireito] = new PosicaoCampo((ZonaCampoVertical)0, (FaixaLateral)2), // Grande área, direita
            [PerfilTatico.Volante] = new PosicaoCampo((ZonaCampoVertical)1, (FaixaLateral)1),
            [PerfilTatico.MeiaCentral] = new PosicaoCampo((ZonaCampoVertical)1, (FaixaLateral)1),
            [PerfilTatico.AlaEsquerdo] = new PosicaoCampo((ZonaCampoVertical)1, (FaixaLateral)0),
            [PerfilTatico.AlaDireito] = new PosicaoCampo((ZonaCampoVertical)1, (FaixaLateral)2),
            [PerfilTatico.PontaEsquerda] = new PosicaoCampo((ZonaCampoVertical)2, (FaixaLateral)0),
            [PerfilTatico.PontaDireita] = new PosicaoCampo((ZonaCampoVertical)2, (FaixaLateral)2),
            [PerfilTatico.Atacante] = new PosicaoCampo((ZonaCampoVertical)2, (FaixaLateral)1),
        };

        public static PosicaoCampo GetPosicaoInicial(PerfilTatico perfil) => Posições[perfil];
    }
}
