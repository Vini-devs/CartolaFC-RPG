using System.Collections.Generic;

public class Torneio
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public List<int> PartidaIds { get; set; } = new List<int>();
}
