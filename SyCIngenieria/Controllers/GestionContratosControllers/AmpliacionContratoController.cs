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
    public class AmpliacionContratoController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public AmpliacionContratoController(SyCIngenieriaContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearAmpliacionContrato(AmpliacionContrato ampliacionContrato)
        {
            await _context.AmpliacionContratos.AddAsync(ampliacionContrato);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<AmpliacionContrato>>> ListaAmplicionContrato()
        {
            var ampliacionContrato = await _context.AmpliacionContratos.ToListAsync();

            return Ok(ampliacionContrato);
        }
        [HttpGet]
        [Route("ver")]
        public async Task<IActionResult> verAmpliacionContrato(int Id)
        {
            AmpliacionContrato ampliacionContrato = await _context.AmpliacionContratos.FindAsync(Id);

            if (ampliacionContrato == null)
            {
                return NotFound();
            }
            return Ok(ampliacionContrato);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> ActualizarAmpliacionContrato(int Id, AmpliacionContrato ampliacionContrato)
        {
            var AmpliacionContratoExistente = await _context.AmpliacionContratos.FindAsync(Id);

            AmpliacionContratoExistente.ContratoId = ampliacionContrato.ContratoId;
            AmpliacionContratoExistente.FechaInicioAmpliacion = ampliacionContrato.FechaInicioAmpliacion;
            AmpliacionContratoExistente.FechaFinAmpliacion = ampliacionContrato.FechaFinAmpliacion;
            AmpliacionContratoExistente.ValorAmpliacion = ampliacionContrato.ValorAmpliacion;
            AmpliacionContratoExistente.NumeroDiasAmpliacion = ampliacionContrato.NumeroDiasAmpliacion;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarAmpliacionContrato(int Id)
        {
            var AmpliacionContratoBorrado = await _context.AmpliacionContratos.FindAsync(Id);

            _context.AmpliacionContratos.Remove(AmpliacionContratoBorrado);

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}