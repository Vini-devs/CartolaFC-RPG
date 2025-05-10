using System;

public class Partida
{
    public int Id { get; set; }
    public Time TimeCasa { get; set; }
    public Time TimeFora { get; set; }
    public int PlacarCasa { get; set; }
    public int PlacarFora { get; set; }
    public DateTime Data { get; set; }
    public bool Finalizada { get; set; }
    public int TorneioId { get; set; }
    public Torneio Torneio { get; set; }
}
