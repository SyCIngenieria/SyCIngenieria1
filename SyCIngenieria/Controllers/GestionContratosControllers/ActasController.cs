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
    public class ActasController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public ActasController(SyCIngenieriaContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearActas(Actas actas)
        {
            await _context.Actas.AddAsync(actas);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Actas>>> ListaActas()
        {
            var actas = await _context.Actas.ToListAsync();

            return Ok(actas);
        }
        [HttpGet]
        [Route("ver")]
        public async Task<IActionResult> verActas(int Id)
        {
            Actas actas = await _context.Actas.FindAsync(Id);

            if (actas == null)
            {
                return NotFound();
            }
            return Ok(actas);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> ActualizarContrato(int Id, Actas actas)
        {
            var ActaExistente = await _context.Actas.FindAsync(Id);

            ActaExistente.ODSId = actas.ODSId;
            ActaExistente.NombreActa = actas.NombreActa;
            ActaExistente.Descripcion = actas.Descripcion;
            ActaExistente.DocumentoAdjunto = actas.DocumentoAdjunto;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarODS(int Id)
        {
            var ODSBorrado = await _context.oDs.FindAsync(Id);

            _context.oDs.Remove(ODSBorrado);

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
