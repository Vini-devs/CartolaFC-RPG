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
                TimeCasaId = p.TimeCasa.Id,
                TimeForaId = p.TimeFora.Id,
                PlacarCasa = p.PlacarCasa,
                PlacarFora = p.PlacarFora,
                Data = p.Data,
                Finalizada = p.Finalizada,
                TorneioId = p.TorneioId,
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartidaDTO>> GetPartida(int id)
    {
        var partida = await _context
            .Partidas.Include(p => p.TimeCasa)
            .Include(p => p.TimeFora)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (partida == null)
        {
            return NotFound();
        }

        return new PartidaDTO
        {
            Id = partida.Id,
            TimeCasaId = partida.TimeCasa.Id,
            TimeForaId = partida.TimeFora.Id,
            PlacarCasa = partida.PlacarCasa,
            PlacarFora = partida.PlacarFora,
            Data = partida.Data,
            Finalizada = partida.Finalizada,
            TorneioId = partida.TorneioId,
        };
    }

    [HttpPost]
    public async Task<ActionResult<Partida>> CreatePartida(PartidaDTO partidaDTO)
    {
        var timeCasa = await _context.Times.FindAsync(partidaDTO.TimeCasaId);
        var timeFora = await _context.Times.FindAsync(partidaDTO.TimeForaId);
        var torneio = await _context.Torneios.FindAsync(partidaDTO.TorneioId);

        if (timeCasa == null || timeFora == null || torneio == null)
        {
            return BadRequest("Invalid Time or Torneio ID");
        }

        var partida = new Partida
        {
            TimeCasa = timeCasa,
            TimeFora = timeFora,
            PlacarCasa = partidaDTO.PlacarCasa,
            PlacarFora = partidaDTO.PlacarFora,
            Data = partidaDTO.Data,
            Finalizada = partidaDTO.Finalizada,
            Torneio = torneio,
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

        var timeCasa = await _context.Times.FindAsync(partidaDTO.TimeCasaId);
        var timeFora = await _context.Times.FindAsync(partidaDTO.TimeForaId);
        var torneio = await _context.Torneios.FindAsync(partidaDTO.TorneioId);

        if (timeCasa == null || timeFora == null || torneio == null)
        {
            return BadRequest("Invalid Time or Torneio ID");
        }

        partida.TimeCasa = timeCasa;
        partida.TimeFora = timeFora;
        partida.PlacarCasa = partidaDTO.PlacarCasa;
        partida.PlacarFora = partidaDTO.PlacarFora;
        partida.Data = partidaDTO.Data;
        partida.Finalizada = partidaDTO.Finalizada;
        partida.Torneio = torneio;

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
