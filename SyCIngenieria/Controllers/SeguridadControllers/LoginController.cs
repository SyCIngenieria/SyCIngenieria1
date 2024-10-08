using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyCIngenieria.Models;

namespace SyCIngenieria.Controllers.SeguridadControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SyCIngenieriaContext _context;

        public LoginController(SyCIngenieriaContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("consultarUsuario")]
        public async Task<IActionResult> ConsultarUsuario(string nombreUsuario, string contraseña)
        {

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario && u.Contraseña == contraseña);


            if (usuario == null)
            {
                return NotFound(new
                {
                    Estado = "no",
                    Mensaje = "Usuario o contraseña incorrectos"
                });
            }


            var permisos = await _context.RelacionalModulosRoles
                .Where(rmr => rmr.FkRol == usuario.FkRol)
                .Include(rmr => rmr.Permisos)
                .SelectMany(rmr => rmr.Permisos)
                .ToListAsync();

            if (permisos == null || !permisos.Any())
            {
                return Ok(new
                {
                    Estado = "ok",
                    Rol = usuario.Rol.NombreRol,
                    NumRol = usuario.FkRol,
                    Permisos = new
                    {
                        Lectura = false,
                        Actualizar = false,
                        Insertar = false,
                        Eliminar = false,
                        Exportar = false
                    }
                });
            }


            return Ok(new
            {
                Estado = "ok",
                Rol = usuario.Rol.NombreRol,
                NumRol = usuario.FkRol,
                Permisos = new
                {
                    Lectura = permisos.Any(p => p.Lectura),
                    Actualizar = permisos.Any(p => p.Actualizar),
                    Insertar = permisos.Any(p => p.Insertar),
                    Eliminar = permisos.Any(p => p.Eliminar),
                    Exportar = permisos.Any(p => p.Exportar)
                }
            });
        }

    }
}
