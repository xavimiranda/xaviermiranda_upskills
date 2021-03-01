using BibliotecaApp.Models;
using BibliotecaApp.Models.Data;
using BibliotecaApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class LeitorController : Controller
    {
        private readonly UserManager<Leitor> _userManager;
        private readonly IRequisicoesRepository _requisicoesRepository;
        private readonly ILeitorRepository _leitorRepository;

        public LeitorController(UserManager<Leitor> userManager, IRequisicoesRepository requisicoesRepository, ILeitorRepository leitorRepository)
        {
            _userManager = userManager;
            _requisicoesRepository = requisicoesRepository;
            _leitorRepository = leitorRepository;
        }

        public IActionResult Index(string viewOption)
        {
            var leitores = _userManager.Users.AsEnumerable();

            if (viewOption == "inactivos")
            {
                leitores =  leitores.Where(l => l.UltimaRequesicao < DateTime.UtcNow + TimeSpan.FromDays(365));
            }
            else if( viewOption == "suspensos")
            {
                leitores = leitores.Where(l => l.Suspenso);
            }

            var requisicoesActivas = new Dictionary<string, int>();
            foreach (var leitor in leitores)
            {
                int numRequisicoes = _requisicoesRepository.GetRequisicoesActivas(leitor.Id).Count();
                requisicoesActivas.Add(leitor.Id, numRequisicoes);
            }

            var viewModel = new LeitoresViewModel
            {
                Leitores = leitores,
                RequisicoesActivas = requisicoesActivas

            };
            return View(viewModel);
        }

        public async Task<IActionResult> Suspender(string id)
        {
            await _leitorRepository.SuspenderLeitorAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Reactivar(string id)
        {
            await _leitorRepository.ReactivarLeitorAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult Apagar(string id)
        {
            int numRequisicoesActivas = _requisicoesRepository.GetRequisicoesActivas(id).Count();
            if (numRequisicoesActivas > 0)
            {
                TempData["ErrorMessage"] = "Não é possível cancelar a inscrição porque o leitor ainda tem exemplares por entregar.";
                var leitor = _userManager.FindByIdAsync(id).Result;
                return RedirectToAction("Index", "User", new { username = leitor.UserName});
            }
            else
            {
                _leitorRepository.ApagarLeitorAsync(id).Wait();
                return RedirectToAction("Index");
            }
        }
    }
}
