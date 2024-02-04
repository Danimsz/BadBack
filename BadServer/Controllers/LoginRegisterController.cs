using BadServer.DataBase;
using BadServer.DataBase.Dto;
using BadServer.DataBase.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BadServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginRegisterController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public LoginRegisterController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            
                // Aqui mira a ver si los datos del usuario son correctos para el incio de sesion
                var user = await _dbContext.Cliente.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName && u.Password == loginDto.Password);
                
                // Control por si el usuario y la contrase�a no coinciden
                if (user == null)
                {
                    return Unauthorized("Usuario o contrase�a incorrectos");
                }

               
                return Ok("Sesion iniciada correctamente");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            
                // A benito le da error aqui
                // Aqui se comprueba si el usuario ya existe para no crearlo
                if (await _dbContext.Cliente.AnyAsync(u => u.UserName == registerDto.UserName))
                {
                    return BadRequest("El nombre de usuario ya existe");
                }

                // Se crea el nuevo usuario, compruebalo bien Benito
                var newUser = new Cliente
                {
                    UserName = registerDto.UserName,
                    Password = registerDto.Password,
                    Email = registerDto.Email,
                    Address = registerDto.Address,
                    Rol = "Usuario" // Esto ultimo me dijo Amanda que lo a�adiera
                };

                _dbContext.Cliente.Add(newUser);
                await _dbContext.SaveChangesAsync();

                return Ok("El usuario se ha registrado");
            
        }
    }
}
