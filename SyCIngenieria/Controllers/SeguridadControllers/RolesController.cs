using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyCIngenieria.Models;
using SyCIngenieria.Models.Seguridad;

namespace SyCIngenieria.Controllers.SeguridadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public RolesController(SyCIngenieriaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Roles>>> ListaRoles()
        {
            var rol = await _context.Roles.ToListAsync();

            return Ok(rol);
        }
        [HttpPost]
        [Route("crear")]

        public async Task<IActionResult> CrearRol(Roles rol)
        {
            await _context.Roles.AddAsync(rol);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarRol(int Id)
        {
            var RolBorrado = await _context.Roles.FindAsync(Id);

            _context.Roles.Remove(RolBorrado);

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
