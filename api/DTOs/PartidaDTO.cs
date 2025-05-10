using System;

public class PartidaDTO
{
    public int Id { get; set; }
    public int TimeCasaId { get; set; }
    public int TimeForaId { get; set; }
    public int PlacarCasa { get; set; }
    public int PlacarFora { get; set; }
    public DateTime Data { get; set; }
    public bool Finalizada { get; set; }
    public int TorneioId { get; set; }
}
