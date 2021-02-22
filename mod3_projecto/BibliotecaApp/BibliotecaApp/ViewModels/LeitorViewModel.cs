using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.ViewModels
{
    /// <summary>
    /// Model for the Admin Create View model validation.
    /// After validation it is created a Leitor in the database
    /// </summary>
    public class LeitorViewModel
    {
        [Required, Display(Name = "Nome Usuário")]
        public string NomeUser { get; set; }
        [Required, Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }
        
        [Required, DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "O E-mail introduzido não é válido")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As password não são iguais")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Morada { get; set; }
        
        [Required]
        public int CC { get; set; }

        public int NucleoRegisto { get; set; }

        public IEnumerable<SelectListItem> Nucleos { get; set; }
    }
}
