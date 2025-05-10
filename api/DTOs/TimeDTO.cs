using System.Collections.Generic;

public class TimeDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<int> JogadoresIds { get; set; } = new List<int>();
    public int GolsFeitos { get; set; }
    public int GolsSofridos { get; set; }
    public int Vitorias { get; set; }
    public int Derrotas { get; set; }
    public int Empates { get; set; }
}
