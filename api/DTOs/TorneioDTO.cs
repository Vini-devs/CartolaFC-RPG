using System.Collections.Generic;

public class TorneioDTO
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public List<int> PartidaIds { get; set; } = new List<int>();
}
