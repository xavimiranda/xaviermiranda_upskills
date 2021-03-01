using BibliotecaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.ViewModels
{
    public class ObrasNucleoViewModel
    {
        public Nucleo Nucleo { get; set; }

        public List<NumCopias> NumCopias { get; set; }

        public int NucleoId { get; set; }
        public int[] Obras { get; set; }
        public int[] NovoNumeroCopias { get; set; }
    }
}
