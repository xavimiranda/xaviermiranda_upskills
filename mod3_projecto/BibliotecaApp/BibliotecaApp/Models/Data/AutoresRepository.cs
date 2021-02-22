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

        public Autor GetAutorById(int id) => _dbContext.Autores.Find(id);
    }
}
