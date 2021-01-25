using System;
using System.Collections.Generic;
using System.Text;
using Exercicios;
using NUnit.Framework;

namespace Tests
{
    public class LivroTestes
    {
        [Test]
        public void LivroCanCountItsAuthors()
        {
            Livro livro1 = new Livro("titulo1");
            Livro livro2 = new Livro("titulo2", "autor1", "autor2", "autor3");
            Livro livro3 = new Livro("titulo3", new string[] { "autor4", "autor5" });

            Assert.AreEqual(0, livro1.NumAutores);
            Assert.AreEqual(3, livro2.NumAutores);
            Assert.AreEqual(2, livro3.NumAutores);
        }
        [Test]
        public void BibliotecaAdicionaLivros()
        {
            var livros = new List<Livro>()
            {
                new Livro("titulo1"),
                new Livro("titulo2", "autor1", "autor2", "autor3"),
            };

            var livros2 = new Livro[]
            {
                new Livro("titulo4", "autor6"),
                new Livro("titulo5", "autor3")
            };

            Livro livro = new Livro("titulo3", new string[] { "autor4", "autor5" });

            var biblio = new Biblioteca();

            biblio.AdicionarLivros(livros);
            biblio.AdicionarLivros(livros2);
            biblio.AdicionarLivro(livro);

            Assert.AreEqual(5, biblio.Livros.Count);
            Assert.AreEqual(2, biblio.QuantosLivrosDeAutor("autor3"));
            Assert.AreEqual(0, biblio.QuantosLivrosDeAutor("Frodo"));
        }
    }
}
