using System.Collections.Generic;

public class Torneio
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public TipoTorneio Tipo { get; set; }
    public List<Partida> Partidas { get; set; } = new List<Partida>();
    public List<Time> TimesParticipantes { get; set; } = new List<Time>();
    public StatusTorneio Status { get; set; }
}

public enum TipoTorneio
{
    Liga,
    Copa,
}

public enum StatusTorneio
{
    EmAndamento,
    Finalizado,
}
