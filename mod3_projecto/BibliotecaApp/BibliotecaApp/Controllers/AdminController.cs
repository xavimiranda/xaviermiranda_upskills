﻿using BibliotecaApp.Models;
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
        public async Task<IActionResult> Create(LeitorViewModel leitorModel)
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
                    NucleoRegisto = _nucleosRepository.GetNucleoById(leitorModel.NucleoRegisto)
                };
                IdentityResult createUserResult = await _userManager.CreateAsync(novoLeitor, leitorModel.Password);

                if( createUserResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(novoLeitor, "User");
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

        public async Task<IActionResult> Update(string id)
        {
            var leitor = await _userManager.FindByIdAsync(id);
            if (leitor != null)
                return View(leitor);
            else
                return RedirectToAction("Index");
        }
    }
}
