namespace CartolaFCRPG.utils
{
    public static class PosicoesIniciais
    {
        public static readonly Dictionary<PerfilTatico, PosicaoCampo> Posições = new()
        {
            [PerfilTatico.Zagueiro] = new PosicaoCampo(0, 1), // Grande área, centro
            [PerfilTatico.LateralEsquerdo] = new PosicaoCampo(0, 0), // Grande área, esquerda
            [PerfilTatico.LateralDireito] = new PosicaoCampo(0, 2), // Grande área, direita
            [PerfilTatico.Volante] = new PosicaoCampo(1, 1),
            [PerfilTatico.MeiaCentral] = new PosicaoCampo(2, 1),
            [PerfilTatico.AlaEsquerdo] = new PosicaoCampo(2, 0),
            [PerfilTatico.AlaDireito] = new PosicaoCampo(2, 2),
            [PerfilTatico.PontaEsquerda] = new PosicaoCampo(3, 0),
            [PerfilTatico.PontaDireita] = new PosicaoCampo(3, 2),
            [PerfilTatico.Atacante] = new PosicaoCampo(3, 1)
        };

        public static PosicaoCampo GetPosicaoInicial(PerfilTatico perfil) => Posições[perfil];
    }
}