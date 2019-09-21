using System.Collections.Generic;
using Cantina.Models;

namespace Cantina.Repositorio
{
    public interface IUsuarioRepository
    {
         void Add(Usuario user);
        IEnumerable<Usuario> GetAll();

        Usuario Autenticar(string usuario, string senha);
        
        Usuario Find(long id);
        void Remove(long id);
        void Update(Usuario user);
    }
}