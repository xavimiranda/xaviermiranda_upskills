using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public interface IObrasRepository
    {
        IEnumerable<Obra> GetAllObras();
        IEnumerable<Obra> GetAllObrasFromNucleo(int nucleoId);
         Obra GetObraById(int id);
        int CreateObra(Obra obra);
        void RemoveObra(int id);
        IEnumerable<Obra> Search(string? titulo, string? autor, int? ano);
        IEnumerable<Obra> SearchInNucleo(int nucleoId, string? titulo, string? autor, int? ano);
    }
}
