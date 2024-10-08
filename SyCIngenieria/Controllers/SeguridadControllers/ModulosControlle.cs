using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyCIngenieria.Models;
using SyCIngenieria.Models.Seguridad;

namespace SyCIngenieria.Controllers.SeguridadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulosController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public ModulosController(SyCIngenieriaContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Roles>>> ListaModulos()
        {
            var rol = await _context.Modulos.ToListAsync();

            return Ok(rol);
        }
        [HttpPost]
        [Route("crear")]

        public async Task<IActionResult> CrearModulo(Modulos modulos)
        {
            await _context.Modulos.AddAsync(modulos);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
