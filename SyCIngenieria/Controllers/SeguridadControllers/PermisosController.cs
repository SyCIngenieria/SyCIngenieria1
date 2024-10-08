using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyCIngenieria.Models;
using SyCIngenieria.Models.Seguridad;

namespace SyCIngenieria.Controllers.SeguridadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisosController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public PermisosController(SyCIngenieriaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Permisos>>> listaPermisos()
        {
            var permiso = await _context.Permisos.ToListAsync();

            return Ok(permiso);
        }
        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> crearPermiso(Permisos permisos)
        {
            await _context.Permisos.AddAsync(permisos);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> eliminarPermiso(int Id)
        {
            var PermisoBorrado = await _context.Permisos.FindAsync(Id);

            _context.Permisos.Remove(PermisoBorrado);
            await _context.SaveChangesAsync();

            return Ok();

        }
        [HttpGet]
        [Route("consultarRol")]
        public async Task<IActionResult> ConsultarRol(int numRol, int moduloId)
        {
            var relModulosRoles = await _context.RelacionalModulosRoles
                .Include(rmr => rmr.Rol)
                .Include(rmr => rmr.Modulo)
                .Include(rmr => rmr.Permisos)
                .FirstOrDefaultAsync(rmr => rmr.FkRol == numRol && rmr.FkModulo == moduloId);

            if (relModulosRoles == null)
            {
                return NotFound(new
                {
                    Estado = "no",
                    Mensaje = "No se encontró el rol o el módulo asociado"
                });
            }
            var permisos = relModulosRoles.Permisos;

            return Ok(new
            {
                Estado = "ok",
                Permisos = new
                {
                    Lectura = permisos.FirstOrDefault()?.Lectura,
                    Editar = permisos.FirstOrDefault()?.Editar,
                    Actualizar = permisos.FirstOrDefault()?.Actualizar,
                    Insertar = permisos.FirstOrDefault()?.Insertar,
                    Eliminar = permisos.FirstOrDefault()?.Eliminar,
                    Exportar = permisos.FirstOrDefault()?.Exportar,
                    Importar = permisos.FirstOrDefault()?.Importar
                }
            });
        }


    }
}
