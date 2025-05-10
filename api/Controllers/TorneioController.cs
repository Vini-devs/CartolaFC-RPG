using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TorneioController : ControllerBase
{
    private readonly CartolaDbContext _context;

    public TorneioController(CartolaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TorneioDTO>>> GetTorneios()
    {
        return await _context
            .Torneios.Select(t => new TorneioDTO
            {
                Id = t.Id,
                Nome = t.Nome,
                Tipo = t.Tipo.ToString(),
                TimesParticipantesIds = t.TimesParticipantes.Select(tp => tp.Id).ToList(),
                Status = t.Status.ToString(),
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TorneioDTO>> GetTorneio(int id)
    {
        var torneio = await _context
            .Torneios.Include(t => t.TimesParticipantes)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (torneio == null)
        {
            return NotFound();
        }

        return new TorneioDTO
        {
            Id = torneio.Id,
            Nome = torneio.Nome,
            Tipo = torneio.Tipo.ToString(),
            TimesParticipantesIds = torneio.TimesParticipantes.Select(tp => tp.Id).ToList(),
            Status = torneio.Status.ToString(),
        };
    }

    [HttpPost]
    public async Task<ActionResult<Torneio>> CreateTorneio(TorneioDTO torneioDTO)
    {
        if (torneioDTO.TimesParticipantesIds.Count < 2)
        {
            return BadRequest("A torneio must have at least 2 teams.");
        }

        var times = await _context
            .Times.Where(t => torneioDTO.TimesParticipantesIds.Contains(t.Id))
            .ToListAsync();

        if (times.Count != torneioDTO.TimesParticipantesIds.Count)
        {
            return BadRequest("Invalid team IDs.");
        }

        var torneio = new Torneio
        {
            Nome = torneioDTO.Nome,
            Tipo = Enum.Parse<TipoTorneio>(torneioDTO.Tipo),
            TimesParticipantes = times,
            Status = StatusTorneio.EmAndamento,
        };

        _context.Torneios.Add(torneio);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTorneio), new { id = torneio.Id }, torneio);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTorneio(int id, TorneioDTO torneioDTO)
    {
        var torneio = await _context
            .Torneios.Include(t => t.TimesParticipantes)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (torneio == null)
        {
            return NotFound();
        }

        if (torneioDTO.TimesParticipantesIds.Count < 2)
        {
            return BadRequest("A torneio must have at least 2 teams.");
        }

        var times = await _context
            .Times.Where(t => torneioDTO.TimesParticipantesIds.Contains(t.Id))
            .ToListAsync();

        if (times.Count != torneioDTO.TimesParticipantesIds.Count)
        {
            return BadRequest("Invalid team IDs.");
        }

        torneio.Nome = torneioDTO.Nome;
        torneio.Tipo = Enum.Parse<TipoTorneio>(torneioDTO.Tipo);
        torneio.TimesParticipantes = times;
        torneio.Status = Enum.Parse<StatusTorneio>(torneioDTO.Status);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTorneio(int id)
    {
        var torneio = await _context.Torneios.FindAsync(id);

        if (torneio == null)
        {
            return NotFound();
        }

        _context.Torneios.Remove(torneio);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
