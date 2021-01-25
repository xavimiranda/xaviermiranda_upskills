using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercicios
{
    public class Livro
    {
        public Livro(string titulo, params string[] autores)
        {
            Titulo = titulo;
            Autores = autores.ToList();
        }
        public string Titulo { get;}
        public List<string> Autores { get;}
        public int NumAutores
        {
            get => Autores.Count;
            
        }
        public override string ToString() => Titulo;
    }
}
