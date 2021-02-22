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

        public Nucleo GetNucleosById(int id) => _dbContext.Nucleos.Find(id);

        public void RemoveNucleo(int id)
        {
            var nucleo = GetNucleosById(id);
            _dbContext.Nucleos.Remove(nucleo);
            _dbContext.SaveChanges();
        }
    }
}
