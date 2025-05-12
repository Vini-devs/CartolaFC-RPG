using System.Collections.Generic;
using CartolaFCRPG.Models;
using CartolaFCRPG.utils;

//ver se o jogador está se movendo aleatoriamente pelo campo ou se esta de um em um bloco

public class Program
{
    public static void Main(string[] args)
    {
        var time1 = new Time
        {
            Id = 1,
            Nome = "Time 1",
            Sigla = "T1",
            UrlEscudo = "",
        };

        var time2 = new Time
        {
            Id = 2,
            Nome = "Time 2",
            Sigla = "T2",
            UrlEscudo = "",
        };

        // Criação de jogadores para o Time 1
        var jogador1Time1 = new Jogador(
            1,
            time1,
            "Jogador 1 T1",
            PerfilTatico.Atacante,
            100,
            100,
            100,
            100,
            100,
            100,
            100
        );

        var jogador2Time1 = new Jogador(
            2,
            time1,
            "Jogador 2 T1",
            PerfilTatico.Volante,
            100,
            100,
            100,
            100,
            100,
            100,
            100
        );

        // Criação de jogadores para o Time 2
        var jogador1Time2 = new Jogador(
            3,
            time2,
            "Jogador 1 T2",
            PerfilTatico.Zagueiro,
            100,
            100,
            100,
            100,
            100,
            100,
            100
        );

        var jogador2Time2 = new Jogador(
            4,
            time2,
            "Jogador 2 T2",
            PerfilTatico.MeiaCentral,
            100,
            100,
            100,
            100,
            100,
            100,
            100
        );

        // Criação do campo e inicialização da posse
        var campo = new Campo(
            new List<Jogador> { jogador1Time1, jogador2Time1 },
            new List<Jogador> { jogador1Time2, jogador2Time2 },
            new Random()
        );
        campo.posse.AtualizarPosse(jogador1Time1);

        // Criação da lista de jogadores
        var jogadores = new List<Jogador>
        {
            jogador1Time1,
            jogador2Time1,
            jogador1Time2,
            jogador2Time2,
        };

        // Inicialização do simulador
        var simulador = new SimuladorPartida(campo, jogadores);
        simulador.IniciarSimulacao();
    }
}
