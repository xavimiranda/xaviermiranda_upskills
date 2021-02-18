using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BibliotecaApp.Models
{
    public class Leitor : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        public string Morada { get; set; }

        [Required]
        public int CC { get; set; }

        public Nucleo NucleoRegisto { get; set; }

        public bool Suspenso { get; set; }

        [Range(0, 4)]
        public int Atrasos { get; set; }
    }
}
