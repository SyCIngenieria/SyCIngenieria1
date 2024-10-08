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
    public class EmpresasController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public EmpresasController(SyCIngenieriaContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("crear")]

        public async Task<IActionResult> CrearEmpresa(Empresas empresas)
        {
            await _context.Empresas.AddAsync(empresas);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Empresas>>> ListaEmpresas()
        {
            var empresas = await _context.Empresas.ToListAsync();

            return Ok(empresas);
        }
        [HttpGet]
        [Route("ver")]
        public async Task<IActionResult> verEmpresa(int Id)
        {
            Empresas empresas = await _context.Empresas.FindAsync(Id);

            if (empresas == null)
            {
                return NotFound();
            }
            return Ok(empresas);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> ActualizarEmpresa(int Id, Empresas empresas)
        {
            var EmpresaExistente = await _context.Empresas.FindAsync(Id);

            EmpresaExistente!.Nombre = empresas.Nombre;
            EmpresaExistente.NIT = empresas.NIT;


            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarEmpresa(int Id)
        {
            var EmpresaBorrado = await _context.Empresas.FindAsync(Id);

            _context.Empresas.Remove(EmpresaBorrado);

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
