using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public interface IAutoresRepository
    {
        IEnumerable<Autor> GetAllAutores();

        IEnumerable<Autor> GetAllAutoresWithObras();

        Autor GetAutorById(int id);
        Autor GetAutorByIdWithObras(int id);

        void CreateAutor(Autor autor);

        bool IsAuthorOf(Autor autor, int obraId);

        void AddObra(Autor autor, int obraId);
        void RemoveObra(Autor autor, int obraId);
    }
}
