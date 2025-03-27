using BEseguridad.Dtos;
using BEseguridad.Models;
using BEseguridad.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEseguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SeguridadInformaticaContext _context;
        private readonly IConfiguration _config;
        public LoginController(SeguridadInformaticaContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioDto user)
        {
            try
            {
                user.Password = Encriptar.EncriptarPassword(user.Password);
                var usuario = await _context.Usuarios.Where(x => x.Username == user.Username
                                                && x.Password == user.Password).FirstOrDefaultAsync();
                if (usuario == null)
                {
                    return BadRequest(new { message = "Usuario o contraseña invalidos" });
                }
                string tokenString = JwtConfigurator.GetToken(usuario, _config);
                return Ok(new { token = tokenString });
                ///return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
