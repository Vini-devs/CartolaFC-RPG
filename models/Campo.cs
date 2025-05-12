using CartolaFCRPG.utils;

namespace CartolaFCRPG.Models
{
    public class Campo
    {
        public List<Jogador> JogadoresTime1 { get; private set; }
        public List<Jogador> JogadoresTime2 { get; private set; }
        public Posse posse { get; private set; }
        public Random random { get; private set; }

        public Campo(List<Jogador> jogadoresTime1, List<Jogador> jogadoresTime2, Random random)
        {
            JogadoresTime1 = jogadoresTime1;
            JogadoresTime2 = jogadoresTime2;
            this.random = random;
            posse = new Posse();
        }

        public PosicaoCampo GetPosicaoJogador(Jogador jogador)
        {
            return jogador.PosicaoAtual;
        }

        public void AtualizarPosicaoJogador(Jogador jogador, PosicaoCampo novaPosicao)
        {
            jogador.PosicaoAtual = novaPosicao;
        }

        public List<Jogador> ObterTodosJogadores()
        {
            return JogadoresTime1.Concat(JogadoresTime2).ToList();
        }

        public Jogador? ObterJogadorNaPosicao(PosicaoCampo posicao)
        {
            return ObterTodosJogadores()
                .FirstOrDefault(j =>
                    j.PosicaoAtual.Zona == posicao.Zona && j.PosicaoAtual.Faixa == posicao.Faixa
                );
        }

        public override string ToString()
        {
            var jogadores = ObterTodosJogadores()
                .Select(j => $"{j.Nome}: {j.PosicaoAtual}")
                .ToList();

            return string.Join("\n", jogadores);
        }
    }

    public enum ZonaCampoVertical
    {
        GrandeAreaPropria = 0,
        ForaGrandeAreaPropria = 1,
        MeioCampo = 2,
        ForaGrandeAreaAdversario = 3,
        GrandeAreaAdversario = 4,
    }

    public enum FaixaLateral
    {
        Esquerda = 0,
        Centro = 1,
        Direita = 2,
    }

    //Representação da Posição em Campo
    public class PosicaoCampo
    {
        public ZonaCampoVertical Zona { get; set; }
        public FaixaLateral Faixa { get; set; }

        public PosicaoCampo(ZonaCampoVertical zona, FaixaLateral faixa)
        {
            Zona = zona;
            Faixa = faixa;
        }

        public PosicaoCampo(int zona, int faixa)
        {
            Zona = (ZonaCampoVertical)zona;
            Faixa = (FaixaLateral)faixa;
        }

        public int CalcularDistancia(PosicaoCampo posicao1)
        {
            int diferencaZona = Math.Abs((int)this.Zona - (int)posicao1.Zona);
            int diferencaFaixa = Math.Abs((int)this.Faixa - (int)posicao1.Faixa);

            // Soma as diferenças para obter uma métrica simples de distância
            return diferencaZona + diferencaFaixa;
        }

        public override string ToString() => $"{Zona} - {Faixa}";
    }
}
