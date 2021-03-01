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

        public IEnumerable<Obra> GetAllObrasFromNucleo(int nucleoId)
        {
            var selection = _dbContext.Set<ObrasNucleo>().Where(on => on.NucleoId == nucleoId && on.NumCopias > 0).ToList();
            return selection.Select(s => _dbContext.Obras.Find(s.ObraId));
        }

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
                //result = _dbContext.Obras.Include(o => o.Autores.Where(a => a.Nome.ToLower().Contains(autor))).ToList();
                foreach(var obra in _dbContext.Obras.Include(o => o.Autores))
                {
                    foreach (var a in obra.Autores)
                    {
                        if (a.Nome.ToLower().Contains(autor))
                            result.Add(obra);
                    }
                }
            }
            if (titulo != null)
            {
                result = result.Count == 0 ? 
                    _dbContext.Obras.Include(o => o.Autores).Where(o => o.Titulo.ToLower().Contains(titulo)).ToList() :
                    result.Where(o => o.Titulo.ToLower().Contains(titulo)).ToList();
            }
            if (ano != null)
                result = result.Count == 0 ?
                    _dbContext.Obras.Where(o => o.AnoPublicação == ano).ToList() :
                    result.Where(o => o.AnoPublicação == ano).ToList();

            return result;
        }

        public IEnumerable<Obra> SearchInNucleo(int nucleoId, string? titulo, string? autor, int? ano)
        {
            var obras = Search(titulo, autor, ano).ToList();
            var selection = _dbContext.Set<ObrasNucleo>().Where(on => on.NucleoId == nucleoId && on.NumCopias > 0).ToList();
            var result = obras.Join(selection, o => o.Id, s => s.ObraId, (o, s) => new { o, s });
            return result.Select(r => r.o);
           
        }
    }
}
