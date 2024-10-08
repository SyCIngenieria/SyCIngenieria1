using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyCIngenieria.Models;
using SyCIngenieria.Models.Seguridad;

namespace SyCIngenieria.Controllers.SeguridadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelacionalModulosRolesController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public RelacionalModulosRolesController(SyCIngenieriaContext context)
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
    }
}