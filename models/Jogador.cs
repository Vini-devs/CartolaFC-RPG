namespace CartolaFCRPG.Models
{
    public class Jogador
    {
        public PerfilTatico Perfil { get; set; }
        public Time Time { get; set; }
        public string Nome { get; set; }
        public PosicaoCampo PosicaoAtual { get; set; } // fica so no DTO

        // Apenas atributos relevantes à movimentação aqui
        public int Ritmo { get; set; } // velocidade de movimentação
        public int Conducao { get; set; } // habilidade de manter a bola

        public Jogador(string nome, PosicaoCampo posicaoInicial, int ritmo, int conducao)
        {
            Nome = nome;
            PosicaoAtual = posicaoInicial;
            Ritmo = ritmo;
            Conducao = conducao;
        }

        public override string ToString() => $"{Nome} em {PosicaoAtual}";
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

        public override string ToString() => $"{Zona} - {Faixa}";
    }
}
