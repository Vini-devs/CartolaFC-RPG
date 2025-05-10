using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TimeController : ControllerBase
{
    private readonly CartolaDbContext _context;

    public TimeController(CartolaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TimeDTO>>> GetTimes()
    {
        return await _context
            .Times.Select(t => new TimeDTO
            {
                Id = t.Id,
                Nome = t.Nome,
                JogadoresIds = t.Jogadores.Select(j => j.Id).ToList(),
                GolsFeitos = t.GolsFeitos,
                GolsSofridos = t.GolsSofridos,
                Vitorias = t.Vitorias,
                Derrotas = t.Derrotas,
                Empates = t.Empates,
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TimeDTO>> GetTime(int id)
    {
        var time = await _context
            .Times.Include(t => t.Jogadores)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (time == null)
        {
            return NotFound();
        }

        return new TimeDTO
        {
            Id = time.Id,
            Nome = time.Nome,
            JogadoresIds = time.Jogadores.Select(j => j.Id).ToList(),
            GolsFeitos = time.GolsFeitos,
            GolsSofridos = time.GolsSofridos,
            Vitorias = time.Vitorias,
            Derrotas = time.Derrotas,
            Empates = time.Empates,
        };
    }

    [HttpPost]
    public async Task<ActionResult<Time>> CreateTime(TimeDTO timeDTO)
    {
        var jogadores = await _context
            .Jogadores.Where(j => timeDTO.JogadoresIds.Contains(j.Id))
            .ToListAsync();

        if (jogadores.Count != timeDTO.JogadoresIds.Count)
        {
            return BadRequest("Invalid Jogador IDs.");
        }

        var time = new Time
        {
            Nome = timeDTO.Nome,
            Jogadores = jogadores,
            GolsFeitos = timeDTO.GolsFeitos,
            GolsSofridos = timeDTO.GolsSofridos,
            Vitorias = timeDTO.Vitorias,
            Derrotas = timeDTO.Derrotas,
            Empates = timeDTO.Empates,
        };

        _context.Times.Add(time);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTime), new { id = time.Id }, time);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTime(int id, TimeDTO timeDTO)
    {
        var time = await _context
            .Times.Include(t => t.Jogadores)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (time == null)
        {
            return NotFound();
        }

        var jogadores = await _context
            .Jogadores.Where(j => timeDTO.JogadoresIds.Contains(j.Id))
            .ToListAsync();

        if (jogadores.Count != timeDTO.JogadoresIds.Count)
        {
            return BadRequest("Invalid Jogador IDs.");
        }

        time.Nome = timeDTO.Nome;
        time.Jogadores = jogadores;
        time.GolsFeitos = timeDTO.GolsFeitos;
        time.GolsSofridos = timeDTO.GolsSofridos;
        time.Vitorias = timeDTO.Vitorias;
        time.Derrotas = timeDTO.Derrotas;
        time.Empates = timeDTO.Empates;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTime(int id)
    {
        var time = await _context.Times.FindAsync(id);

        if (time == null)
        {
            return NotFound();
        }

        _context.Times.Remove(time);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
