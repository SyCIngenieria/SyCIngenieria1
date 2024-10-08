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
    public class ProyectosController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public ProyectosController(SyCIngenieriaContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("crear")]

        public async Task<IActionResult> CrearProyectos(Proyectos proyectos)
        {
            await _context.Proyectos.AddAsync(proyectos);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Proyectos>>> ListaProyectos()
        {
            var troncales = await _context.Proyectos.ToListAsync();

            return Ok(troncales);
        }
        [HttpGet]
        [Route("ver")]
        public async Task<IActionResult> verProyectos(int Id)
        {
            Proyectos proyectos = await _context.Proyectos.FindAsync(Id);

            if (proyectos == null)
            {
                return NotFound();
            }
            return Ok(proyectos);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> ActualizarProyectos(int Id, Proyectos proyectos)
        {
            var ProyectoExistente = await _context.Proyectos.FindAsync(Id);

            ProyectoExistente.NombreProyecto = proyectos.NombreProyecto;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarProyecto(int Id)
        {
            var ProyectoBorrado = await _context.Proyectos.FindAsync(Id);

            _context.Proyectos.Remove(ProyectoBorrado);

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
