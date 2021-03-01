using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public interface IRequisicoesRepository
    {
        IEnumerable<Requisicao> GetAllRequisicoes();
        IEnumerable<Requisicao> GetAllRequisicoesEagerLoading();

        Requisicao GetRequisicaoById(int id);
        Requisicao GetRequisicaoByIdEagerLoading(int id);

        int CreateRequisicao(Leitor leitor, Obra obra, Nucleo nucleo);

        void CloseRequisicao(int requisicaoId, int nucleoId);

        IEnumerable<Requisicao> GetRequisicoesFromLeitor(string leitorId);

        IEnumerable<Requisicao> GetRequisicoesActivas(string leitor);
        IEnumerable<Requisicao> GetRequisicoesEntregues(string leitor);


    }
}
