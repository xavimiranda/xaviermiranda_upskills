using FP.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace FP.BLL
{
    public class JsonTelemovelRepository : ITelemovelRepository
    {
        private readonly BD _baseDados;
        public JsonTelemovelRepository(string path)
        {
            _baseDados = new BD(path);
        }
        public List<Telemovel> DeleteFrom(Func<Telemovel, bool> regra) => _baseDados.DeleteFrom(regra);
        public int GetSize() => _baseDados.GetSize<Telemovel>();
        public void Insert(List<Telemovel> lista) => _baseDados.Insert(lista);
        public void Insert(Telemovel tlm) => _baseDados.Insert(tlm);
        public List<Telemovel> Select(Func<Telemovel, bool> regra) => _baseDados.Select(regra);
        public List<Telemovel> SelectAll() => _baseDados.SelectAll<Telemovel>();
    }
}
