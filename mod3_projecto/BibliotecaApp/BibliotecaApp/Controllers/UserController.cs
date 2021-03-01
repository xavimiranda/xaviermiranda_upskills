using BibliotecaApp.Models;
using BibliotecaApp.Models.Data;
using BibliotecaApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<Leitor> _userManager;
        private readonly IRequisicoesRepository _requisicoesRepository;
        private readonly IObrasRepository _obrasRepository;
        private readonly INucleosRepository _nucleosRepository;
        private readonly int _requisicoesMax;

        public UserController(UserManager<Leitor> userManager, 
                              IRequisicoesRepository requisicoesRepository, 
                              IObrasRepository obrasRepository,
                              INucleosRepository nucleosRepository,
                              IConfiguration configuration )
        {
            _userManager = userManager;
            _requisicoesRepository = requisicoesRepository;
            _obrasRepository = obrasRepository;
            _nucleosRepository = nucleosRepository;
            _requisicoesMax = int.Parse(configuration.GetSection("Biblioteca")["RequisicoesMax"]) ;
        }
        
        public async Task<IActionResult> Index(string username)
        {
            if (username != User.Identity.Name && !User.IsInRole("Admin"))
                return RedirectToAction("Index", new { username = User.Identity.Name });
            Leitor leitor = await _userManager.FindByNameAsync(username);

            var viewModel = new UserViewModel
            {
                Leitor = leitor,
                RequisicoesActivas = _requisicoesRepository.GetRequisicoesActivas(leitor.Id),
                RequisicoesEntregues = _requisicoesRepository.GetRequisicoesEntregues(leitor.Id)
            };
            return View(viewModel);
        }
        //TODO: implementar limite de 4 requisições total e verivifar se fica pelo menos uma obra no núcleo
        public async Task<IActionResult> Requisitar(int obraId, int nucleoId)
        {
            Leitor leitor = await _userManager.FindByNameAsync(User.Identity.Name);
            var requisicoesActivas = _requisicoesRepository.GetRequisicoesActivas(leitor.Id);

           
            if (requisicoesActivas.Count() >= _requisicoesMax)
            {
                TempData["ErrorMessage"] = $"Não pode ter mais de {_requisicoesMax} requisições activas.";
                return RedirectToAction("Index", new { username = leitor.UserName });
            }

            Obra obra = _obrasRepository.GetObraById(obraId);
            Nucleo nucleo = _nucleosRepository.GetNucleoById(nucleoId);

            var numCopiasDisponiveis = _nucleosRepository.GetNumCopiasObra(nucleo, obraId);
            if (numCopiasDisponiveis < 2)
            {
                TempData["ErrorMessage"] = "De momento só existe uma cópia para consulta presencial.";
                return RedirectToAction("Index", "Obras");
            }
            _requisicoesRepository.CreateRequisicao(leitor, obra, nucleo);
            return RedirectToAction("Index", new { username = leitor.UserName });
        }


        public IActionResult Devolver(int requisicaoId, int nucleoId)
        {
            _requisicoesRepository.CloseRequisicao(requisicaoId, nucleoId);

            var requisicao = _requisicoesRepository.GetRequisicaoById(requisicaoId);
            if (requisicao.DataEntregue > requisicao.DataLimite)
            {
                TempData["ErrorMessage"] = "Entregou a cópia com atraso. O limite de atrasos é 3 antes da suspensão de conta.";
            }
            return RedirectToAction("Index", new { username = requisicao.Leitor.UserName });
        }
         
    }
}
