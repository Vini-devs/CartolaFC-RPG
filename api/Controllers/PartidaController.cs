using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/partidas")]
public class PartidaController : ControllerBase
{
    private readonly CartolaDbContext _context;

    public PartidaController(CartolaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PartidaDTO>>> GetPartidas()
    {
        return await _context
            .Partidas.Select(p => new PartidaDTO
            {
                Id = p.Id,
                TimeCasaId = p.TimeIdCasa,
                TimeForaId = p.TimeIdFora,
                PlacarCasa = p.PlacarCasa,
                PlacarFora = p.PlacarFora,
                Data = p.Data,
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartidaDTO>> GetPartida(int id)
    {
        var partida = await _context.Partidas.FirstOrDefaultAsync(p => p.Id == id);
        if (partida == null)
        {
            return NotFound();
        }
        return new PartidaDTO
        {
            Id = partida.Id,
            TimeCasaId = partida.TimeIdCasa,
            TimeForaId = partida.TimeIdFora,
            PlacarCasa = partida.PlacarCasa,
            PlacarFora = partida.PlacarFora,
            Data = partida.Data,
        };
    }

    [HttpPost]
    public async Task<ActionResult<Partida>> CreatePartida(PartidaDTO partidaDTO)
    {
        var partida = new Partida
        {
            TimeIdCasa = partidaDTO.TimeCasaId,
            TimeIdFora = partidaDTO.TimeForaId,
            PlacarCasa = partidaDTO.PlacarCasa,
            PlacarFora = partidaDTO.PlacarFora,
            Data = partidaDTO.Data,
        };
        _context.Partidas.Add(partida);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPartida), new { id = partida.Id }, partida);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePartida(int id, PartidaDTO partidaDTO)
    {
        var partida = await _context.Partidas.FindAsync(id);
        if (partida == null)
        {
            return NotFound();
        }
        partida.TimeIdCasa = partidaDTO.TimeCasaId;
        partida.TimeIdFora = partidaDTO.TimeForaId;
        partida.PlacarCasa = partidaDTO.PlacarCasa;
        partida.PlacarFora = partidaDTO.PlacarFora;
        partida.Data = partidaDTO.Data;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePartida(int id)
    {
        var partida = await _context.Partidas.FindAsync(id);

        if (partida == null)
        {
            return NotFound();
        }

        _context.Partidas.Remove(partida);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
