using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyCIngenieria.Models;
using SyCIngenieria.Models.GestionContratos;
using SyCIngenieria.Models.Seguridad;

namespace SyCIngenieria.Controllers.SeguridadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TroncalesController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public TroncalesController(SyCIngenieriaContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("crear")]

        public async Task<IActionResult> CrearTroncales(Troncales troncales)
        {
            await _context.Troncales.AddAsync(troncales);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Troncales>>> ListaTroncales()
        {
            var troncales = await _context.Troncales.ToListAsync();

            return Ok(troncales);
        }
        [HttpGet]
        [Route("ver")]
        public async Task<IActionResult> verTroncal(int Id)
        {
            Troncales troncales = await _context.Troncales.FindAsync(Id);

            if (troncales == null)
            {
                return NotFound();
            }
            return Ok(troncales);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> ActualizarTroncal(int Id, Troncales troncales)
        {
            var TrocalExistente = await _context.Troncales.FindAsync(Id);

            TrocalExistente.NombreTroncal = troncales.NombreTroncal;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarTroncal(int Id)
        {
            var troncalBorrado = await _context.Troncales.FindAsync(Id);

            _context.Troncales.Remove(troncalBorrado);

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}

