
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace TelemovelJson
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = File.ReadAllText("lista.json");
            
            List<AtributosTelemovel> atrbts = JsonConvert.DeserializeObject<List<AtributosTelemovel>>(json);

            List<Telemovel> telemoveis = new List<Telemovel>();
            telemoveis.

            foreach(var atr in atrbts)
            {
                telemoveis.Add(new Telemovel(atr));
            }

            var t = new Telemovel("single.json");

        }
    }
}


