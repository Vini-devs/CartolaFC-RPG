using System.ComponentModel.DataAnnotations;

public class Jogador
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }

    [Range(0, 100)]
    public int Tecnica { get; set; }

    [Range(0, 100)]
    public int Defesa { get; set; }

    [Range(0, 100)]
    public int PrecisaoFinalizacao { get; set; }

    [Range(0, 100)]
    public int PrecisaoPasse { get; set; }

    [Range(0, 100)]
    public int AptidaoFisica { get; set; }

    [Range(0, 100)]
    public int Agilidade { get; set; }

    [Range(0, 100)]
    public int Mentalidade { get; set; }

    public PosicaoCampo Posicao { get; set; }

    public int TimeId { get; set; }
    public Time Time { get; set; }
}

public class PosicaoCampo
{
    public int Id { get; set; }
    public string Nome { get; set; }
}
