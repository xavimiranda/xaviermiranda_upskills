using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FP.BLL;
using Newtonsoft.Json;

namespace FP.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            //criar bd e verificar que tem 0 objectos
            BD bd = new BD("BD.json");
            Console.WriteLine($"A base de dados tem {bd.GetSize()} objectos");

            //importa duas lista de telemoveis de ficheiros diferents.
            //a class telemóvel não deve ser responsável por retornar uma coleção de telemóveis

            //Assim sendo deserializa-se uma lista de atributos dos ficheiro json
            var listaAtributos1 = new List<AtributosTelemovel>();
            var listaAtributos2 = new List<AtributosTelemovel>();
            if (File.Exists("lista1.json")) //se o ficheiro existe...
            {
                string lista1 = File.ReadAllText("lista1.json"); //transforma em string
                listaAtributos1 = JsonConvert.DeserializeObject<List<AtributosTelemovel>>(lista1); //deserializa
            }
            else
                throw new FileNotFoundException("lista1.json não existe"); //se não existe dispara excepção

            if (File.Exists("lista2.json")) 
            {
                string lista2 = File.ReadAllText("lista2.json");
                listaAtributos2 = JsonConvert.DeserializeObject<List<AtributosTelemovel>>(lista2);
            }
            else
                throw new FileNotFoundException("lista2.json não existe"); 

            //Iniciam-se as listas de telemóveis em memória
            List<Telemovel> tm1 = new List<Telemovel>();
            List<Telemovel> tm2 = new List<Telemovel>();

            //finalmente populam-se as listas
            foreach (var atributo in listaAtributos1) //para cara atributo na lista...
            {
                tm1.Add(new Telemovel(atributo)); //...cria-se um novo telemóvel
            }

            foreach (var atributo in listaAtributos2)
            {
                tm2.Add(new Telemovel(atributo));
            }

            //para acrescentar a funcionalidade MergeWith à classe de sistema List<T> tem que se
            //recorrer a um Extension Method. isto pode ser feito simplesmente com a função já existente em sistema tm1.AddRange(tm2)
            tm1.MergeWith(tm2);

            //inserir a lista na bd
            bd.Insert(tm1);
            Console.WriteLine($"A base de dados tem {bd.GetSize()} objectos");


            //aqui estou a passar uma funçao anónima para ter liberdade na regra que se passa.
            //alternativamente pode ser feito parseando uma string no formato "Atributo=Valor"
            List<Telemovel> tmbd = bd.DeleteFrom(t => t.Atributos.Marca == "Apple"); //Lê-se para um telemovel t onde t.Atributos.Marca é igual a "Apple"
            foreach(var telemovel in tmbd)
            {
                Console.WriteLine($"Modelo: {telemovel.Atributos.Modelo}");
            }
            Console.WriteLine($"Foram eliminados {tmbd.Count} objectos.");


            tmbd = bd.Select(t => t.Atributos.Modelo == "Huawei");
            foreach (var telemovel in tmbd)
            {
                Console.WriteLine($"Modelo: {telemovel.Atributos.Modelo}");
            }
            Console.WriteLine($"Foram selecionados {tmbd.Count} objectos");

            //Mudar nome para SelectAll ???
            tmbd = bd.Select();
            foreach (var telemovel in tmbd)
            {
                Console.WriteLine($"Modelo: {telemovel.Atributos.Modelo}");
            }
            Console.WriteLine($"Foram selecionados {tmbd.Count} objectos");

            Console.Write("Enter para sair");
            Console.ReadLine();
        }


    }
    static class ListTelemovelExtensions
    {
        public static void MergeWith(this List<Telemovel> tm1, List<Telemovel> tm2) => tm1.AddRange(tm2);

    }
}
    