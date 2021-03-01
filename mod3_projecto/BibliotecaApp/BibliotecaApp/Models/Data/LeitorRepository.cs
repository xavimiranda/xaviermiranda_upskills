using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public class LeitorRepository : ILeitorRepository
    {
        private readonly UserManager<Leitor> _userManager;
        private readonly BibliotecaDbContext _dbContext;

        public LeitorRepository(UserManager<Leitor> userManager, BibliotecaDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task SuspenderLeitorAsync(string id)
        {
            var leitor = await _userManager.FindByIdAsync(id);
            SuspenderLeitor(leitor);
        }

        public void SuspenderLeitor(Leitor leitor)
        {
            leitor.Suspenso = true;
            _dbContext.SaveChanges();
        }

        public async Task ReactivarLeitorAsync(string id)
        {
            var leitor = await _userManager.FindByIdAsync(id);
            ReactivarLeitor(leitor);
        }

        public void ReactivarLeitor(Leitor leitor)
        {
            leitor.Suspenso = false;
            leitor.Atrasos = 0;
            _dbContext.SaveChanges();
        }

        public async Task ApagarLeitorAsync(string id)
        {
            var leitor = await _userManager.FindByIdAsync(id);
            ApagarLeitor(leitor);

        }

        public void ApagarLeitor(Leitor leitor)
        {
            _userManager.DeleteAsync(leitor);
        }
    }
}
