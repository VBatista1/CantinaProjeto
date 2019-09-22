using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CantinaProjeto.Models;
using CantinaProjeto.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CantinaProjeto.Controllers
{
    [Route("api/[controller]")]
    [Authorize()]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepositorio;
        public UsuariosController(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepositorio = usuarioRepo;
        }
            
       [HttpGet]
       public IEnumerable<Usuario> GetAll()
       {
            return _usuarioRepositorio.GetAll();
       }

       [HttpGet("{id}", Name = "GetUsuario")]
       public IActionResult GetById(long id)
       {
           var usuario = _usuarioRepositorio.Find(id);
           if (usuario == null)
           {
                 return NotFound();
           }
          return new ObjectResult(usuario);
       }

        [AllowAnonymous]
        [HttpPost("autenticar")]
        public IActionResult Autenticar([FromBody] Usuario usuario)
        {
            var _usuario = _usuarioRepositorio.Autenticar(usuario.Nome, usuario.Senha);
 
            if (_usuario == null)
                return Unauthorized();
 
          return new ObjectResult(_usuario);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }

            _usuarioRepositorio.Add(usuario);

            return CreatedAtRoute("GetUsuario", new { id = usuario.UsuarioId }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Usuario usuario)
        {
            if (usuario == null || usuario.UsuarioId != id)
            {
                return BadRequest();
            }

            var _usuario = _usuarioRepositorio.Find(id);
            if (_usuario == null)
            {
                return NotFound();
            }

            _usuario.Email = usuario.Email;
            _usuario.Nome = usuario.Nome;

            _usuarioRepositorio.Update(_usuario);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var usuario = _usuarioRepositorio.Find(id);

            if (usuario == null)
                    return NotFound();

            _usuarioRepositorio.Remove(id);

             return new NoContentResult();
        }
    }
}