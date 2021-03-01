using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaApp.ViewModels;
using BibliotecaApp.Models;
using BibliotecaApp.Models.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BibliotecaApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<Leitor> _userManager;
        private readonly SignInManager<Leitor> _signInManager;
        private readonly INucleosRepository _nucleosRepository;

        public AccountController(UserManager<Leitor> userManager, SignInManager<Leitor> signInManager, RoleManager<IdentityRole> roleManager, INucleosRepository nucleosRepository)
        {
           
            _userManager = userManager;
            _signInManager = signInManager;
            _nucleosRepository = nucleosRepository;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            
            var login = new LoginViewModel();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        [AllowAnonymous]
        public IActionResult Suspenso()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if(ModelState.IsValid)
            {
                Leitor leitor = await _userManager.FindByNameAsync(login.NomeUser);
                
                if (leitor != null)
                {
                    if (leitor.Suspenso)
                        return RedirectToAction("Suspenso");
                    await _signInManager.SignOutAsync();
                    var signInResult = await _signInManager.PasswordSignInAsync(leitor, login.Password, false, false);

                    if (signInResult.Succeeded)
                        return Redirect(login.ReturnUrl ?? "/");
                }
                ModelState.AddModelError(nameof(login.NomeUser), "Login Failed: Invalid Email or password");
            }
            return View(login);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            var leitorViewModel = new LeitorViewModel
            {
                Nucleos = _nucleosRepository.GetAllNucleos().Select(n => new SelectListItem { Text = n.Nome, Value = n.Id.ToString() })
            };

            return View(leitorViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LeitorViewModel leitorModel)
        {
            if (ModelState.IsValid)
            {
                var novoLeitor = new Leitor
                {
                    UserName = leitorModel.NomeUser,
                    Nome = leitorModel.NomeCompleto,
                    Email = leitorModel.Email,
                    Morada = leitorModel.Morada,
                    CC = leitorModel.CC,
                    NucleoRegisto = _nucleosRepository.GetNucleoById(leitorModel.NucleoRegisto)
                };
                IdentityResult createUserResult = await _userManager.CreateAsync(novoLeitor, leitorModel.Password);

                if (createUserResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(novoLeitor, "User");
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (IdentityError error in createUserResult.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            leitorModel.Nucleos = _nucleosRepository.GetAllNucleos().Select(n => new SelectListItem { Text = n.Nome, Value = n.Id.ToString() });
            return View(leitorModel);
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Test()
        {
            return RedirectToAction("Index", "Obras");
        }
    }
}
