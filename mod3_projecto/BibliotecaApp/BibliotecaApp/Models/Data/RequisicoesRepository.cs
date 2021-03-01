using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public class RequisicoesRepository : IRequisicoesRepository
    {
        private readonly BibliotecaDbContext _dbContext;

        public RequisicoesRepository(BibliotecaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateRequisicao(Leitor leitor, Obra obra, Nucleo nucleo)
        {
            var requisicao = new Requisicao
            {
                Leitor = leitor,
                Obra = obra,
                Nucleo = nucleo,
                DataRequisicao = DateTime.UtcNow,
                DataLimite = DateTime.UtcNow + TimeSpan.FromDays(15)
            };
            leitor.UltimaRequesicao = DateTime.UtcNow;
            var result = _dbContext.Requisicoes.Add(requisicao);
            var obrasNucleo = _dbContext.Set<ObrasNucleo>().FirstOrDefault(on => on.NucleoId == nucleo.Id && on.ObraId == obra.Id);
            obrasNucleo.NumCopias--;
            _dbContext.SaveChanges();
            return result.Entity.Id;
        }

        public void CloseRequisicao(int requisicaoId, int nucleoId)
        {
            var requisicao = GetRequisicaoByIdEagerLoading(requisicaoId);
            requisicao.DataEntregue = DateTime.UtcNow;
            var obrasNucleo = _dbContext.Set<ObrasNucleo>().FirstOrDefault(on => on.NucleoId == nucleoId && on.ObraId == requisicao.Obra.Id);
            
            if (obrasNucleo != null)
            {
                obrasNucleo.NumCopias++;
            }
            else
            {
                obrasNucleo = new ObrasNucleo
                {
                    NucleoId = nucleoId,
                    ObraId = requisicao.Obra.Id,
                    NumCopias = 1
                };

                _dbContext.Set<ObrasNucleo>().Add(obrasNucleo);
            }

            if (requisicao.DataEntregue > requisicao.DataLimite)
            {
                requisicao.Leitor.Atrasos++;
            }

            _dbContext.SaveChanges();
        }

        public IEnumerable<Requisicao> GetAllRequisicoes() => _dbContext.Requisicoes;

        public IEnumerable<Requisicao> GetAllRequisicoesEagerLoading()
        {
            return _dbContext.Requisicoes.Include(r => r.Leitor)
                                         .Include(r => r.Obra)
                                         .Include(r => r.Nucleo);
        }

        public Requisicao GetRequisicaoById(int id) => _dbContext.Requisicoes.Find(id);

        public Requisicao GetRequisicaoByIdEagerLoading(int id)
        {
            return _dbContext.Requisicoes.Include(r => r.Leitor)
                                         .Include(r => r.Obra)
                                         .Include(r => r.Nucleo)
                                         .FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Requisicao> GetRequisicoesFromLeitor(string leitorId)
        {
            return _dbContext.Requisicoes.Where(r => r.Leitor.Id == leitorId)
                                         .Include(r => r.Obra)
                                         .Include(r => r.Nucleo)
                                         .OrderBy(r => r.DataLimite);
        }

        public IEnumerable<Requisicao> GetRequisicoesActivas(string leitor) 
        {
            return _dbContext.Requisicoes.Where(r => r.Leitor.Id == leitor)
                                         .Where(r => r.DataEntregue == null)
                                         .Include(r => r.Obra)
                                         .Include(r => r.Nucleo)
                                         .OrderBy(r => r.DataLimite);
        }

        public IEnumerable<Requisicao> GetRequisicoesEntregues(string leitor)
        {
            return _dbContext.Requisicoes.Where(r => r.Leitor.Id == leitor)
                             .Where(r => r.DataEntregue != null)
                             .Include(r => r.Obra)
                             .Include(r => r.Nucleo)
                             .OrderBy(r => r.DataLimite); ;
        }
    }
}
