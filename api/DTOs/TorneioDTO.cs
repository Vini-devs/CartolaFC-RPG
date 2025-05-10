using System.Collections.Generic;

public class TorneioDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public List<int> TimesParticipantesIds { get; set; } = new List<int>();
    public string Status { get; set; }
}
