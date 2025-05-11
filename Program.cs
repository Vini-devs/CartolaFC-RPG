using CartolaFCRPG.Models;
using CartolaFCRPG.utils;

public class Program
{
    public static void Main(string[] args)
    {
        // Exemplo de uso
        var jogador = new Jogador(
            "João",
            new PosicaoCampo(ZonaCampoVertical.MeioCampo, FaixaLateral.Centro),
            80,
            70
        );

        Console.WriteLine(jogador);

        foreach (var jogador in todosOsJogadores)
        {
            jogador.PosicaoAtual = PosicoesIniciais.GetPosicaoInicial(jogador.Perfil);
        }

        // Movimentações
        Movimentacao.MoverJogadorVertical(jogador, 1); // Avançar uma zona
        Movimentacao.MoverJogadorLateral(jogador, -1); // Ir para a esquerda
        Movimentacao.MoverJogadorVertical(jogador, 1); // Avançar de novo
        Movimentacao.MoverJogadorLateral(jogador, 1); // Voltar ao centro
    }
}
