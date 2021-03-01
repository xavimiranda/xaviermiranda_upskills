using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public interface INucleosRepository
    {
        IEnumerable<Nucleo> GetAllNucleos();
        Nucleo GetNucleoById(int id);
        void CreateNucleo(Nucleo nucleo);
        void RemoveNucleo(int id);

        Dictionary<int, int> GetNumCopiasTodasObras(int nucleoId);
        Dictionary<int, int> GetNumCopiasTodasObras(Nucleo nucleo);

        int GetNumCopiasObra(Nucleo nucleo, int obraId);
        void UpdateNumCopias(Nucleo nucleo, int obraId, int numCopias);
    }
}
