using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Exercicios
{
    public class Biblioteca
    {
        public Biblioteca()
        {
            Livros = new List<Livro>();
        }
        public List<Livro> Livros { get; private set; }
        public void AdicionarLivros(IEnumerable<Livro> novosLivros)
        {
            Livros = Livros.Concat(novosLivros).ToList();
        }
        public void AdicionarLivro(Livro novoLivro)
        {
            Livros.Add(novoLivro);
        }
        public int QuantosLivrosDeAutor(string autor)
        {
            return Livros.Where(l => l.Autores.Contains(autor)).Count();
        }
    }
}
