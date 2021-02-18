using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models
{
    /// <summary>
    /// Vai associar Obra a Nucleos com um NumCopias. (Many to Many with payload)
    /// </summary>
    public class ObrasNucleo
    {
        public int ObraId { get; set; }
        public int NucleoId { get; set; }
        public int NumCopias { get; set; }
    }
}
