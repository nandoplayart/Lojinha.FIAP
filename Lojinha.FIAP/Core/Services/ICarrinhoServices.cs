using Lojinha.FIAP.Core.Models;

namespace Lojinha.FIAP.Core.Services
{
    public interface ICarrinhoServices
    {
        void Limpar(string usuario);
        Carrinho Obter(string usuario);
        void Salvar(string usuario, Carrinho carrinho);
    }
}