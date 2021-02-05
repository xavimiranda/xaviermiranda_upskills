using System;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public bool Ativo { get; set; }
    }
}
