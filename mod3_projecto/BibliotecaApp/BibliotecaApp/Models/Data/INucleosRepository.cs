using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public interface INucleosRepository
    {
        IEnumerable<Nucleo> GetAllNucleos();
        Nucleo GetNucleosById(int id);
        void CreateNucleo(Nucleo nucleo);
        void RemoveNucleo(int id);
    }
}
