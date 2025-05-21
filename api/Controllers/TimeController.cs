using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/times")]
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
        var times = await _context.Times.ToListAsync();
        var timeDTOs = new List<TimeDTO>();
        foreach (var t in times)
        {
            var jogadorIds = await _context
                .Jogadores.Where(j => j.TimeId == t.Id)
                .Select(j => j.Id)
                .ToListAsync();
            timeDTOs.Add(
                new TimeDTO
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    Sigla = t.Sigla,
                    JogadorIds = jogadorIds,
                }
            );
        }
        return timeDTOs;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TimeDTO>> GetTime([FromRoute] int id)
    {
        var time = await _context.Times.FirstOrDefaultAsync(t => t.Id == id);
        if (time == null)
        {
            return NotFound();
        }
        var jogadorIds = await _context
            .Jogadores.Where(j => j.TimeId == time.Id)
            .Select(j => j.Id)
            .ToListAsync();
        return new TimeDTO
        {
            Id = time.Id,
            Nome = time.Nome,
            Sigla = time.Sigla,
            JogadorIds = jogadorIds,
        };
    }

    [HttpPost]
    public async Task<ActionResult<Time>> CreateTime(TimeDTO timeDTO)
    {
        var time = new Time { Nome = timeDTO.Nome, Sigla = timeDTO.Sigla };
        _context.Times.Add(time);
        await _context.SaveChangesAsync();
        // Atualiza os jogadores para o novo time
        var jogadores = await _context
            .Jogadores.Where(j => timeDTO.JogadorIds.Contains(j.Id))
            .ToListAsync();
        foreach (var jogador in jogadores)
        {
            jogador.TimeId = time.Id;
        }
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTime), new { id = time.Id }, time);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTime(int id, TimeDTO timeDTO)
    {
        var time = await _context.Times.FirstOrDefaultAsync(t => t.Id == id);
        if (time == null)
        {
            return NotFound();
        }
        time.Nome = timeDTO.Nome;
        time.Sigla = timeDTO.Sigla;
        // Atualiza os jogadores para o time
        var jogadores = await _context
            .Jogadores.Where(j => timeDTO.JogadorIds.Contains(j.Id))
            .ToListAsync();
        foreach (var jogador in jogadores)
        {
            jogador.TimeId = time.Id;
        }
        // Remove jogadores antigos que não estão mais na lista
        var jogadoresAntigos = await _context
            .Jogadores.Where(j => j.TimeId == time.Id && !timeDTO.JogadorIds.Contains(j.Id))
            .ToListAsync();
        foreach (var jogador in jogadoresAntigos)
        {
            jogador.TimeId = 0; // ou null, se permitido
        }
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTime(int id)
    {
        var time = await _context.Times.FirstOrDefaultAsync(t => t.Id == id);

        if (time == null)
        {
            return NotFound();
        }

        try
        {
            _context.Times.Remove(time);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar: {ex.Message}");
        }
    }
}
