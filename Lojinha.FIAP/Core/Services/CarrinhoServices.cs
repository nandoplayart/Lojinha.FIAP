using Lojinha.FIAP.Core.Models;
using Lojinha.FIAP.Infrastructure.Redis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.FIAP.Core.Services
{
    public class CarrinhoServices : ICarrinhoServices
    {
        private readonly string _key = "RM330731";
        private readonly IRedisCache _redis;

        public CarrinhoServices(IRedisCache redis)
        {
            _redis = redis;
        }

        public void Limpar(string usuario) {
            _redis.Set($"{ _key}:carrinho:{usuario}", null);
        }

        public void Salvar(string usuario, Carrinho carrinho) {
            _redis.Set($"{_key}:carrinho:{usuario}", JsonConvert.SerializeObject(carrinho));
        }

        public Carrinho Obter(string usuario) {
            var value = _redis.Get($"{_key}:carrinho:{usuario}");
            if (string.IsNullOrEmpty(value)) {
                var carrinho = new Carrinho();
                Salvar(usuario, carrinho);
                return carrinho;
            }

            return JsonConvert.DeserializeObject<Carrinho>(value);
        }
    }
}
