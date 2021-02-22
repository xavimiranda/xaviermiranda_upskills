using BibliotecaApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Components
{
    public class LoginInfo : ViewComponent
    {
        private readonly UserManager<Leitor> _userManager;
        private readonly SignInManager<Leitor> _signInManager;

        public LoginInfo(UserManager<Leitor> userManager, SignInManager<Leitor> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IViewComponentResult Invoke()
        {
            Leitor leitor = _userManager.GetUserAsync(HttpContext.User).Result;
            return View(leitor);
        }
    }
}
