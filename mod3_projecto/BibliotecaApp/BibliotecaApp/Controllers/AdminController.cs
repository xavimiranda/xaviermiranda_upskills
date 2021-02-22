using BibliotecaApp.Models;
using BibliotecaApp.ViewModels;
using BibliotecaApp.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaApp.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<Leitor> _userManager;
        private readonly INucleosRepository _nucleosRepository;

        public AdminController(UserManager<Leitor> userManager, INucleosRepository nucleosRepository)
        {
            _userManager = userManager;
            _nucleosRepository = nucleosRepository;
        }

        public IActionResult Index() => View(_userManager.Users);

        
        public IActionResult Create()
        {
            
            var leitorViewModel = new LeitorViewModel
            {
                Nucleos = _nucleosRepository.GetAllNucleos().Select( n => new SelectListItem { Text = n.Nome, Value = n.Id.ToString()})
            };

            return View(leitorViewModel);
        }

        [HttpPost]
        public IActionResult Create(LeitorViewModel leitorModel)
        {
            if(ModelState.IsValid)
            {
                var novoLeitor = new Leitor
                {   
                    UserName = leitorModel.NomeUser,
                    Nome = leitorModel.NomeCompleto,
                    Email = leitorModel.Email,
                    Morada = leitorModel.Morada,
                    CC = leitorModel.CC,
                    NucleoRegisto = _nucleosRepository.GetNucleosById(leitorModel.NucleoRegisto)
                };
                IdentityResult createUserResult = _userManager.CreateAsync(novoLeitor, leitorModel.Password).Result;

                if( createUserResult.Succeeded)
                {
                    return RedirectToAction("Index");
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

        public IActionResult Update(string id)
        {
            var leitor = _userManager.FindByIdAsync(id).Result;
            if (leitor != null)
                return View(leitor);
            else
                return RedirectToAction("Index");
        }
    }
}
