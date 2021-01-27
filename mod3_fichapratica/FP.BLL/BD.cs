using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace FP.BLL
{
    public class BD
    {
        /// <summary>
        /// Caminho para o ficheiro json no sistema
        /// </summary>
        public string Path { get; private set; }
        /// <summary>
        /// Lista em Memória dos dispositivos.
        /// </summary>
        private List<Telemovel> _telemoveis = new List<Telemovel>();

        public BD(string path)
        {
            Path = path;
            //verifica se o ficheiro existe
            if (!File.Exists(path))
                //se não existe cria o ficheiro
                File.Create(path);
        }
        //devolve o tamanho da lista em memória
        public int GetSize() => _telemoveis.Count;

        /// <summary>
        /// Adiciona uma lista à base de dados
        /// </summary>
        /// <param name="lista">Lista a adicionar</param>
        public void Insert(List<Telemovel> lista)
        {
            _telemoveis.AddRange(lista);
            SaveToFile();
        }

        public List<Telemovel> DeleteFrom(Func<Telemovel, bool> regra)
        {
            //guarda os telemoveis que vão ser removidos para retornar no fim;
            //remove os telemoveis
            var removedObjs = _telemoveis.Where(regra).ToList();
            _telemoveis.RemoveAll(new Predicate<Telemovel>(regra));
            SaveToFile();
            return removedObjs;
        }
       
        public List<Telemovel> Select(Func<Telemovel, bool> regra)
        {
            return _telemoveis.Where(regra).ToList();
        }
        public List<Telemovel> Select()
        {
            return _telemoveis;
        }

        private void SaveToFile()
        {
            string json = JsonConvert.SerializeObject(_telemoveis.Select(t => t.Atributos));
            File.WriteAllText(Path, json);
        }
    }
}
