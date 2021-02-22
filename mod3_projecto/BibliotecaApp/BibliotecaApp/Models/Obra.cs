using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models
{
    public class Obra
    {
        [Key]
        [BindNever]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public List<Autor> Autores { get; set; } = new List<Autor>();

        [Required]
        public int AnoPublicação { get; set; }

        public string Imagem { get; set; }

        public string Isbn10 { get; set; }

        public string Isbn13 { get; set; }
        [DefaultValue("getutcdate()")]
        public DateTime AdicionadoEm { get; set; }

        public Classificacao Classificacao { get; set; }

        public List<Nucleo> Nucleos { get; set; } = new List<Nucleo>();
    }
}
