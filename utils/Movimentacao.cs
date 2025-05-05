using CartolaFCRPG.Models;

namespace CartolaFCRPG.utils
{
    public class Movimentacao
    {
        // Métodos para Mover Jogadores
        public static void MoverJogadorVertical(Jogador jogador, int direcao)
        {
            int novaZona = (int)jogador.PosicaoAtual.Zona + direcao;

            if (novaZona >= 0 && novaZona <= 4)
            {
                jogador.PosicaoAtual.Zona = (ZonaCampoVertical)novaZona;
                Console.WriteLine($"{jogador.Nome} se moveu para {jogador.PosicaoAtual.Zona}");
            }
            else
            {
                Console.WriteLine($"{jogador.Nome} não pode se mover mais nessa direção.");
            }
        }

        public static void MoverJogadorLateral(Jogador jogador, int direcao)
        {
            int novaFaixa = (int)jogador.PosicaoAtual.Faixa + direcao;

            if (novaFaixa >= 0 && novaFaixa <= 2)
            {
                jogador.PosicaoAtual.Faixa = (FaixaLateral)novaFaixa;
                Console.WriteLine($"{jogador.Nome} se moveu para {jogador.PosicaoAtual.Faixa}");
            }
            else
            {
                Console.WriteLine($"{jogador.Nome} não pode ir mais para essa faixa.");
            }
        }
    }
}
