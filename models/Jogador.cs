using CartolaFCRPG.utils;

namespace CartolaFCRPG.Models
{
    public class Jogador
    {
        public int Id { get; set; }
        public Time Time { get; set; }
        public string Nome { get; set; }
        public PerfilTatico PerfilTatico { get; set; }
        public int Tecnica { get; set; }
        public int Defesa { get; set; }
        public int PrecisaoFinalizacao { get; set; }
        public int PrecisaoPasse { get; set; }
        public int Forca { get; set; }
        public int Aptidao { get; set; } // aka ritmo
        public int Mentalidade { get; set; }
        public PosicaoCampo PosicaoAtual { get; set; } // fica so no DTO

        public Jogador(
            int id,
            Time time,
            string nome,
            PerfilTatico perfilTatico,
            int tecnica,
            int defesa,
            int finalizacao,
            int passe,
            int forca,
            int aptidao,
            int mentalidade
        )
        {
            Id = id;
            Time = time;
            Nome = nome;
            PerfilTatico = perfilTatico;
            PosicaoCampo posicaoInicial = PosicoesIniciais.GetPosicaoInicial(perfilTatico);
            PosicaoAtual = posicaoInicial;
            Tecnica = tecnica;
            Defesa = defesa;
            PrecisaoFinalizacao = finalizacao;
            PrecisaoPasse = passe;
            Forca = forca;
            Aptidao = aptidao;
            Mentalidade = mentalidade;
        }

        public override string ToString() => $"{Nome} em {PosicaoAtual}";
    }
}
