using BibliotecaApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.ViewModels
{
    public class RoleUpdateViewModel
    {
        public IdentityRole Role { get; set; }
        public List<Leitor> Members { get; set; }
        public List<Leitor> NonMembers { get; set; }

        public string RoleId { get; set; }
        public string[] AddIds { get; set; }
        public string[] RemoveIds { get; set; }
    }
}
