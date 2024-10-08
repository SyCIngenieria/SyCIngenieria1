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
    public class ODSController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public ODSController(SyCIngenieriaContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearContrato(ODS oDS)
        {
            await _context.oDs.AddAsync(oDS);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<ODS>>> ListaODS()
        {
            var ods = await _context.oDs.ToListAsync();

            return Ok(ods);
        }
        [HttpGet]
        [Route("ver")]
        public async Task<IActionResult> verODS(int Id)
        {
            ODS oDS = await _context.oDs.FindAsync(Id);

            if (oDS == null)
            {
                return NotFound();
            }
            return Ok(oDS);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> ActualizarODS(int Id, ODS oDS)
        {
            var odsExistente = await _context.oDs.FindAsync(Id);

            odsExistente.EmpesasId = oDS.EmpesasId;
            odsExistente.Descripcion = oDS.Descripcion;
            odsExistente.ValorInicalODS = oDS.ValorInicalODS;
            odsExistente.DiasEjecuCionODS = oDS.DiasEjecuCionODS;
            odsExistente.FechaInicioODS = oDS.FechaInicioODS;
            odsExistente.FechaFinODS = oDS.FechaFinODS;
            odsExistente.OrdenesCambioId = oDS.OrdenesCambioId;
            odsExistente.SuspensionFechaInicio = oDS.SuspensionFechaInicio;
            odsExistente.SuspensionFechaFin = oDS.SuspensionFechaFin;
            odsExistente.ValorActual = oDS.ValorActual;
            odsExistente.EstadoODS = oDS.EstadoODS;
            odsExistente.Supervisor = oDS.Supervisor;
            odsExistente.SolicitanteCliente = oDS.SolicitanteCliente;
            odsExistente.RecursoODS = oDS.RecursoODS;
            odsExistente.ProyectosId = oDS.ProyectosId;
            odsExistente.TroncalesId = oDS.TroncalesId;
            odsExistente.EmpesasId = oDS.EmpesasId;
            odsExistente.PlantaSistema = oDS.PlantaSistema;
            odsExistente.ConexoObra = oDS.ConexoObra;

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
