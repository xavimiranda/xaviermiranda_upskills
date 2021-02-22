using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public class AutoresRepository : IAutoresRepository
    {
        private readonly BibliotecaDbContext _dbContext;

        public AutoresRepository(BibliotecaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateAutor(Autor autor)
        {
            _dbContext.Autores.Add(autor);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Autor> GetAllAutores() => _dbContext.Autores;
        public IEnumerable<Autor> GetAllAutoresWithObras() => _dbContext.Autores.Include(a => a.Obras);

        public Autor GetAutorById(int id) => _dbContext.Autores.Find(id);

        public Autor GetAutorByIdWithObras(int id) => _dbContext.Autores.Include(a => a.Obras).FirstOrDefault(a => a.Id == id);

        public bool IsAuthorOf(Autor autor, int obraId)
        {
            var obra = autor.Obras.Find(o => o.Id == obraId);
            return obra == null ? false : true;

        }

        public void AddObra(Autor autor, int obraId)
        {
            var obra = _dbContext.Obras.Find(obraId);
            autor.Obras.Add(obra);
            _dbContext.SaveChanges();
        }
        public void RemoveObra(Autor autor, int obraId)
        {
            var obra = _dbContext.Obras.Find(obraId);
            autor.Obras.Remove(obra);
            _dbContext.SaveChanges();
        }
    }
}
