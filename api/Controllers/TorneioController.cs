using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/torneios")]
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
            .Torneios.Select(t => new TorneioDTO { Id = t.Id, Nome = t.Nome })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TorneioDTO>> GetTorneio(int id)
    {
        var torneio = await _context.Torneios.FirstOrDefaultAsync(t => t.Id == id);
        if (torneio == null)
        {
            return NotFound();
        }
        return new TorneioDTO { Id = torneio.Id, Nome = torneio.Nome };
    }

    [HttpPost]
    public async Task<ActionResult<Torneio>> CreateTorneio(TorneioDTO torneioDTO)
    {
        var torneio = new Torneio { Nome = torneioDTO.Nome };
        _context.Torneios.Add(torneio);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTorneio), new { id = torneio.Id }, torneio);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTorneio(int id, TorneioDTO torneioDTO)
    {
        var torneio = await _context.Torneios.FindAsync(id);
        if (torneio == null)
        {
            return NotFound();
        }
        torneio.Nome = torneioDTO.Nome;
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
