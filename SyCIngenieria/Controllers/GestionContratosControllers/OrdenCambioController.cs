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
    public class OrdenCambioController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public OrdenCambioController(SyCIngenieriaContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearOrdenCambio(OrdenCambio ordenCambio)
        {
            await _context.ordenCambios.AddAsync(ordenCambio);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<OrdenCambio>>> ListaOrdenCambio()
        {
            var ordenCambio = await _context.ordenCambios.ToListAsync();

            return Ok(ordenCambio);
        }
        [HttpGet]
        [Route("ver")]
        public async Task<IActionResult> verOrdenCambio(int Id)
        {
            OrdenCambio ordenCambio = await _context.ordenCambios.FindAsync(Id);

            if (ordenCambio == null)
            {
                return NotFound();
            }
            return Ok(ordenCambio);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> ActualizarOrdenCambio(int Id, OrdenCambio ordenCambio)
        {
            var OrdenCambioExistente = await _context.ordenCambios.FindAsync(Id);

            OrdenCambioExistente.ContratoId = ordenCambio.ContratoId;
            OrdenCambioExistente.AmpliacionContratoId = ordenCambio.AmpliacionContratoId;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarOrdenCambio(int Id)
        {
            var OrdenCambioBorrado = await _context.ordenCambios.FindAsync(Id);

            _context.ordenCambios.Remove(OrdenCambioBorrado);

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
