using BibliotecaApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.ViewModels
{
    public class CreateAutorViewModel
    {
        public string RedirectUrl { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime Nascimento { get; set; }

        public DateTime? Obito { get; set; }

    }
}
