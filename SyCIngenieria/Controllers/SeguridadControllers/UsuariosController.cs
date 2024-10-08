using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyCIngenieria.Models;
using SyCIngenieria.Models.Seguridad;

namespace SyCIngenieria.Controllers.SeguridadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public UsuariosController(SyCIngenieriaContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear")]

        public async Task<IActionResult> CrearUsuarios(Usuarios usuarios)
        {
            await _context.Usuarios.AddAsync(usuarios);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Usuarios>>> ListaUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            return Ok(usuarios);
        }
        [HttpGet]
        [Route("ver")]
        public async Task<IActionResult> verUsuario(int Id)
        {
            Usuarios usuarios = await _context.Usuarios.FindAsync(Id);

            if (usuarios == null)
            {
                return NotFound();
            }
            return Ok(usuarios);
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> ActualizarUsuario(int Id, Usuarios usuarios)
        {
            var usuariosExistente = await _context.Usuarios.FindAsync(Id);

            usuariosExistente!.NombreUsuario = usuarios.NombreUsuario;
            usuariosExistente.Contraseña = usuarios.Contraseña;
            usuariosExistente.Nombres = usuarios.Nombres;
            usuariosExistente.Apellidos = usuarios.Apellidos;
            usuariosExistente.Correo = usuarios.Correo;
            usuariosExistente.Telefono = usuarios.Telefono;
            usuariosExistente.FechaExpira = usuarios.FechaExpira;
            usuariosExistente.FkRol = usuarios.FkRol;


            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> EliminarUsuario(int Id)
        {
            var usuarioBorrado = await _context.Usuarios.FindAsync(Id);

            _context.Usuarios.Remove(usuarioBorrado);

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
