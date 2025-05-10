using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class JogadorController : ControllerBase
{
    private readonly CartolaDbContext _context;

    public JogadorController(CartolaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JogadorDTO>>> GetJogadores()
    {
        return await _context
            .Jogadores.Select(j => new JogadorDTO
            {
                Id = j.Id,
                Nome = j.Nome,
                Tecnica = j.Tecnica,
                Defesa = j.Defesa,
                PrecisaoFinalizacao = j.PrecisaoFinalizacao,
                PrecisaoPasse = j.PrecisaoPasse,
                AptidaoFisica = j.AptidaoFisica,
                Agilidade = j.Agilidade,
                Mentalidade = j.Mentalidade,
                Posicao = j.Posicao.Nome,
                TimeId = j.TimeId,
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JogadorDTO>> GetJogador(int id)
    {
        var jogador = await _context
            .Jogadores.Include(j => j.Posicao)
            .FirstOrDefaultAsync(j => j.Id == id);

        if (jogador == null)
        {
            return NotFound();
        }

        return new JogadorDTO
        {
            Id = jogador.Id,
            Nome = jogador.Nome,
            Tecnica = jogador.Tecnica,
            Defesa = jogador.Defesa,
            PrecisaoFinalizacao = jogador.PrecisaoFinalizacao,
            PrecisaoPasse = jogador.PrecisaoPasse,
            AptidaoFisica = jogador.AptidaoFisica,
            Agilidade = jogador.Agilidade,
            Mentalidade = jogador.Mentalidade,
            Posicao = jogador.Posicao.Nome,
            TimeId = jogador.TimeId,
        };
    }

    [HttpPost]
    public async Task<ActionResult<Jogador>> CreateJogador(JogadorDTO jogadorDTO)
    {
        var time = await _context.Times.FindAsync(jogadorDTO.TimeId);

        if (time == null)
        {
            return BadRequest("Invalid Time ID");
        }

        var jogador = new Jogador
        {
            Nome = jogadorDTO.Nome,
            Tecnica = jogadorDTO.Tecnica,
            Defesa = jogadorDTO.Defesa,
            PrecisaoFinalizacao = jogadorDTO.PrecisaoFinalizacao,
            PrecisaoPasse = jogadorDTO.PrecisaoPasse,
            AptidaoFisica = jogadorDTO.AptidaoFisica,
            Agilidade = jogadorDTO.Agilidade,
            Mentalidade = jogadorDTO.Mentalidade,
            Posicao = new PosicaoCampo { Nome = jogadorDTO.Posicao },
            TimeId = jogadorDTO.TimeId,
        };

        _context.Jogadores.Add(jogador);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetJogador), new { id = jogador.Id }, jogador);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJogador(int id, JogadorDTO jogadorDTO)
    {
        var jogador = await _context
            .Jogadores.Include(j => j.Posicao)
            .FirstOrDefaultAsync(j => j.Id == id);

        if (jogador == null)
        {
            return NotFound();
        }

        var time = await _context.Times.FindAsync(jogadorDTO.TimeId);

        if (time == null)
        {
            return BadRequest("Invalid Time ID");
        }

        jogador.Nome = jogadorDTO.Nome;
        jogador.Tecnica = jogadorDTO.Tecnica;
        jogador.Defesa = jogadorDTO.Defesa;
        jogador.PrecisaoFinalizacao = jogadorDTO.PrecisaoFinalizacao;
        jogador.PrecisaoPasse = jogadorDTO.PrecisaoPasse;
        jogador.AptidaoFisica = jogadorDTO.AptidaoFisica;
        jogador.Agilidade = jogadorDTO.Agilidade;
        jogador.Mentalidade = jogadorDTO.Mentalidade;
        jogador.Posicao.Nome = jogadorDTO.Posicao;
        jogador.TimeId = jogadorDTO.TimeId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJogador(int id)
    {
        var jogador = await _context.Jogadores.FindAsync(id);

        if (jogador == null)
        {
            return NotFound();
        }

        _context.Jogadores.Remove(jogador);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
