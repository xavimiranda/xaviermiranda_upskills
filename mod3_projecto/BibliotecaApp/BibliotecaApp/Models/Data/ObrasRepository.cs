using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public class ObrasRepository : IObrasRepository
    {

        private readonly BibliotecaDbContext _dbContext;

        public ObrasRepository(BibliotecaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateObra(Obra obra)
        {
            _dbContext.Obras.Add(obra);
            _dbContext.SaveChanges();
            return obra.Id;
        }

        public IEnumerable<Obra> GetAllObras() => _dbContext.Obras.Include(o => o.Autores);

        public Obra GetObraById(int id) => _dbContext.Obras.Include(o => o.Autores).FirstOrDefault(o => o.Id == id);

        public void RemoveObra(int id)
        {
            var obra = GetObraById(id);
            _dbContext.Obras.Remove(obra);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Obra> Search(string? titulo, string? autor, int? ano)
        {
            var result = new List<Obra>();

            if (autor != null)
            {
                result.AddRange(_dbContext.Obras.Include(o => o.Autores.Where(a => a.Nome.ToLower().Contains(autor))));
            }
            if (titulo != null)
            {
                result = result.Count == 0 ? 
                    _dbContext.Obras.Include(o => o.Autores).Where(o => o.Titulo.ToLower().Contains(titulo)).ToList() :
                    result.Where(o => o.Titulo.Contains(titulo)).ToList();
            }
            if (ano != null)
                result = result.Count == 0 ?
                    _dbContext.Obras.Where(o => o.AnoPublicação == ano).ToList() :
                    result.Where(o => o.AnoPublicação == ano).ToList();

            return result;
        }
    }
}
