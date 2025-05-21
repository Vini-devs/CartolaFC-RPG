using System.Collections.Generic;

public class TimeDTO
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Sigla { get; set; }
    public List<int> JogadorIds { get; set; } = new List<int>();
}
