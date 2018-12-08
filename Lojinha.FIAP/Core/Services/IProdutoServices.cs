using System.Collections.Generic;
using System.Threading.Tasks;
using Lojinha.FIAP.Core.Models;

namespace Lojinha.FIAP.Core.Services
{
    public interface IProdutoServices
    {
        Task<List<Produto>> ObterProdutos();
        Produto ObterProduto(string id);
    }
}