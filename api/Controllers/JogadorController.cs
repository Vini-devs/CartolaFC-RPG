using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/jogadores")]
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
                Posicao = j.Posicao,
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
            Posicao = jogador.Posicao,
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
            Posicao = jogadorDTO.Posicao,
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
        jogador.Posicao = jogadorDTO.Posicao;
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
