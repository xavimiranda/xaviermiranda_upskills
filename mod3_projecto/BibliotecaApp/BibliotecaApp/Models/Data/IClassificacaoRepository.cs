using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public interface IClassificacaoRepository
    {
        IEnumerable<Classificacao> GetAllClassificacoes();
        Classificacao GetClassificacaoById(int id);
        Classificacao GetClassificacaoByCDD(int cdd);
    }
}
