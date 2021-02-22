using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public interface IAutoresRepository
    {
        IEnumerable<Autor> GetAllAutores();
        Autor GetAutorById(int id);
        void CreateAutor(Autor autor);
    }
}
