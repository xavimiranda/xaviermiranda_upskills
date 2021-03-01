using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public class NucleosRepository : INucleosRepository
    {
        private readonly BibliotecaDbContext _dbContext;

        public NucleosRepository(BibliotecaDbContext db)
        {
            _dbContext = db;
        }

        public void CreateNucleo(Nucleo nucleo)
        {
            _dbContext.Nucleos.Add(nucleo);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Nucleo> GetAllNucleos() => _dbContext.Nucleos;

        public Nucleo GetNucleoById(int id) => _dbContext.Nucleos.Include(n => n.Obras).First(n => n.Id == id);

        public Dictionary<int, int> GetNumCopiasTodasObras(int nucleoId)
        {
            return GetNumCopiasTodasObras(_dbContext.Nucleos.Include(n => n.Obras).First(n => n.Id == nucleoId));
        }

        public Dictionary<int, int> GetNumCopiasTodasObras(Nucleo nucleo)
        {
            return _dbContext.Set<ObrasNucleo>().Where(on => on.NucleoId == nucleo.Id).ToDictionary(on => on.ObraId, on => on.NumCopias);
        }

        public int GetNumCopiasObra(Nucleo nucleo, int obraId)
        {
            return _dbContext.Set<ObrasNucleo>().FirstOrDefault(on => on.NucleoId == nucleo.Id && on.ObraId == obraId).NumCopias;
        }

        public void UpdateNumCopias(Nucleo nucleo, int obraId, int numCopias)
        {
            var result = _dbContext.Set<ObrasNucleo>().FirstOrDefault(on => on.NucleoId == nucleo.Id && on.ObraId == obraId);
            if (result != null)
                result.NumCopias = numCopias;
            else
                _dbContext.Set<ObrasNucleo>().Add(new ObrasNucleo { NucleoId = nucleo.Id, ObraId = obraId, NumCopias = numCopias });

            _dbContext.SaveChanges();
        }

        public void RemoveNucleo(int id)
        {
            var nucleo = GetNucleoById(id);
            _dbContext.Nucleos.Remove(nucleo);
            _dbContext.SaveChanges();
        }

    }
}
