using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace FP.DAL
{
    public class BD
    {
        /// <summary>
        /// Caminho para o ficheiro json no sistema
        /// </summary>
        public string Path { get; private set; }
        public BD(string path)
        {
            Path = path;
            if (!File.Exists(Path))
                File.WriteAllText(Path, "[]");
        }
        /// <summary>
        /// Retorna o número de Objectos na BD
        /// </summary>
        /// <typeparam name="T">Tipo de Objectos guardados na BD</typeparam>
        public int GetSize<T>() => GetFromFile<T>().Count;
        /// <summary>
        /// Insere uma lista de tipo genérico na BD
        /// <param name="lista">Lista para inserir</param>
        public void Insert<T>(List<T> lista)
        {
            var temp = GetFromFile<T>();
            temp.AddRange(lista);
            SaveToFile(temp);
        }
        public void Insert<T>(T item)
        {
            var temp = GetFromFile<T>();
            temp.Add(item);
            SaveToFile<T>(temp);
        }
        /// <summary>
        /// Remove objectos da BD que obedeçam à regra passada
        /// </summary>
        public List<T> DeleteFrom<T>(Func<T, bool> regra)
        {
            //retira todos os objectos da BD
            var temp = GetFromFile<T>();
            //guarda os telemoveis que vão ser removidos para retornar no fim;
            var removedObjs = temp.Where(regra).ToList();
            //remove os telemoveis
            temp.RemoveAll(new Predicate<T>(regra));
            SaveToFile(temp);
            return removedObjs;
        }

        /// <summary>
        /// Retorna todos os objectos que obedeçam à regra passada
        /// </summary>
        public List<T> Select<T>(Func<T, bool> regra)
        {
            return GetFromFile<T>().Where(regra).ToList();
        }

        /// <summary>
        /// Retorna todos os objectos da BD
        /// </summary>
        public List<T> SelectAll<T>()
        {
            return GetFromFile<T>();
        }
        private void SaveToFile<T>(List<T> lista)
        {
            string json = JsonConvert.SerializeObject(lista);
            File.WriteAllText(Path, json);
        }
        private List<T> GetFromFile<T>()
        {
            var result = new List<T>();
            if (File.Exists(Path))
            {
                string json = File.ReadAllText(Path);
                if (!string.IsNullOrEmpty(json))
                    result = JsonConvert.DeserializeObject<List<T>>(json);
            }
            return result;
        }
    }
}
