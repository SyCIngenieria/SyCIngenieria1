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
    public class ContratosController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public ContratosController(SyCIngenieriaContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearContrato(Contrato contrato)
        {
            await _context.Contratos.AddAsync(contrato);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Contrato>>> ListaContratos()
        {
            var contratos = await _context.Contratos.ToListAsync();

            return Ok(contratos);
        }
        [HttpGet]
        [Route("ver")]
        public async Task<IActionResult> verContrato(int Id)
        {
            Contrato contrato = await _context.Contratos.FindAsync(Id);

            if (contrato == null)
            {
                return NotFound();
            }
            return Ok(contrato);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> ActualizarContrato(int Id, Contrato contrato)
        {
            var ContratoExistente = await _context.Contratos.FindAsync(Id);

            ContratoExistente.NumeroContrato = contrato.NumeroContrato;
            ContratoExistente.EmpresasId = contrato.EmpresasId;
            ContratoExistente.NombrePersonaContrato = contrato.NombrePersonaContrato;
            ContratoExistente.NumeroDocumentoPersonaContrato = contrato.NumeroDocumentoPersonaContrato;
            ContratoExistente.EmailPersonaContrato = contrato.EmailPersonaContrato;
            ContratoExistente.TelefonoPersonaContrato = contrato.TelefonoPersonaContrato;
            ContratoExistente.ValorContrato = contrato.ValorContrato;
            ContratoExistente.DuracionDiascontrato = contrato.DuracionDiascontrato;
            ContratoExistente.Fecha_Inicio = contrato.Fecha_Inicio;
            ContratoExistente.Fecha_Fin = contrato.Fecha_Fin;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarContrato(int Id)
        {
            var ContratoBorrado = await _context.Proyectos.FindAsync(Id);

            _context.Proyectos.Remove(ContratoBorrado);

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}