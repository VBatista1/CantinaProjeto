using System.Collections.Generic;
using System.Linq;
using CantinaProjeto.Models;
using CantinaProjeto.Repositorio;

namespace Cantina.Repositorio
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioDbContext _contexto;

        public UsuarioRepository(UsuarioDbContext ctx)
        {
            _contexto = ctx;
        }
        public void Add(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
            _contexto.SaveChanges();
        }

        public Usuario Find(long id)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
        }

        public IEnumerable<Usuario> GetAll()
        {
             return _contexto.Usuarios.ToList();
        }

        public Usuario Autenticar(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
 
            var user = _contexto.Usuarios.SingleOrDefault(x => x.Nome == username && x.Senha==password);
 
            // check if username exists
            if (user == null)
                return null;
 
            // check if password is correct
            // if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            //     return null;
 
            // authentication successful
            return user;
        }

        public void Remove(long id)
        {
            var entity = _contexto.Usuarios.First(u=> u.UsuarioId == id);
            _contexto.Usuarios.Remove(entity);
            _contexto.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
            _contexto.SaveChanges();
        }
    }
}