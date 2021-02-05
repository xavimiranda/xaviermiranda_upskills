using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace TelemovelJson
{
    public class AtributosTelemovel
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Ecra { get; set; }
        public int Memoria { get; set; }
        public int Disco { get; set; }
        public string Processador { get; set; }
        public string OS { get; set; }
    }
    public class Telemovel
    {
        public Telemovel(AtributosTelemovel atb)
        {
            Atributos = atb;
        }

        public Telemovel(string path)
        {
            string json = File.ReadAllText(path);
            Atributos = JsonConvert.DeserializeObject<AtributosTelemovel>(json);
        }

        public AtributosTelemovel Atributos { get; private set; } = new AtributosTelemovel();
    }
}
