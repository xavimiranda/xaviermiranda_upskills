using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public class ClassificacaoRepository : IClassificacaoRepository
    {
        private readonly BibliotecaDbContext _dbContext;

        public ClassificacaoRepository(BibliotecaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Classificacao> GetAllClassificacoes() => _dbContext.Classificacoes;

        public Classificacao GetClassificacaoByCDD(int cdd)
        {
            throw new NotImplementedException();
        }

        public Classificacao GetClassificacaoById(int id) => _dbContext.Classificacoes.Find(id);
    }
}
