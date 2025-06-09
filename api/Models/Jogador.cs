using System.ComponentModel.DataAnnotations;

public class Jogador
{
    public int Id { get; set; }

    [Required]
    public string? Nome { get; set; }

    public string? Posicao { get; set; }

    public int TimeId { get; set; }
}
