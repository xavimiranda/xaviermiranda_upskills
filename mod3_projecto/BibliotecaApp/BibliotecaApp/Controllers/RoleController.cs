using BibliotecaApp.Models;
using BibliotecaApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Leitor> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<Leitor> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index() => View(_roleManager.Roles);

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await _roleManager.CreateAsync(new IdentityRole(name));
                if (identityResult.Succeeded)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", "Problema ao criar a função. Tente outro nome.");
            }
            return View(name);
        }

        public async Task<IActionResult> Update(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<Leitor>();
            var nonMembers = new List<Leitor>();

            foreach(var user in _userManager.Users)
            {
                bool isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                if (isInRole)
                    members.Add(user);
                else
                    nonMembers.Add(user); 
            }

            return View(new RoleUpdateViewModel
                { 
                  Role = role,
                  RoleId = role.Id,
                  Members = members,
                  NonMembers = nonMembers
                });
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleUpdateViewModel viewModel)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(viewModel.RoleId);
            
            foreach(var leitorId in viewModel.AddIds ?? new string[] { })
            {
                Leitor leitor = await _userManager.FindByIdAsync(leitorId);
                if (leitor != null)
                {
                    var addToRoleResult = await _userManager.AddToRoleAsync(leitor, role.Name);
                    if (!addToRoleResult.Succeeded)
                        ModelState.AddModelError("", $"Não foi possível adicionar {leitor.UserName} a {role.Name}.");
                }
                else
                {
                    ModelState.AddModelError("", $"O id {leitorId} não é válido");
                }
            }

            foreach(var leitorId in viewModel.RemoveIds ?? new string[] { })
            {
                Leitor leitor = await _userManager.FindByIdAsync(leitorId);

                if( leitor != null )
                {
                    var removeFromRoleResult = await _userManager.RemoveFromRoleAsync(leitor, role.Name);
                    
                    if (!removeFromRoleResult.Succeeded)
                        ModelState.AddModelError("", $"Não foi possível remover {leitor.UserName} a {role.Name}.");
                }
                else
                {
                    ModelState.AddModelError("", $"O id {leitorId} não é válido");
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction("Index");
            else
                return View(viewModel);
        }
    }
}
