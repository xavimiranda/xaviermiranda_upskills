using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaApp.Models.Data
{
    public interface ILeitorRepository
    {
        Task SuspenderLeitorAsync(string id);
        void SuspenderLeitor(Leitor leitor);
        Task ReactivarLeitorAsync(string id);
        void ReactivarLeitor(Leitor leitor);

        Task ApagarLeitorAsync(string id);
        void ApagarLeitor(Leitor leitor);
    }
}
