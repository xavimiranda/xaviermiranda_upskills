using System;
using System.Collections.Generic;
using System.Text;

namespace FP.BLL
{
    public interface ITelemovelRepository
    {
        public int GetSize();
        public void Insert(List<Telemovel> lista);
        public void Insert(Telemovel tlm);
        public List<Telemovel> DeleteFrom(Func<Telemovel, bool> regra);
        public List<Telemovel> SelectAll();
        public List<Telemovel> Select(Func<Telemovel, bool> regra);
    }
}
