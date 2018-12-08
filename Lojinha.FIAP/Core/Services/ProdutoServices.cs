using Lojinha.FIAP.Core.Models;
using Lojinha.FIAP.Infrastructure.Redis;
using Lojinha.FIAP.Infrastructure.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.FIAP.Core.Services
{
    public class ProdutoServices : IProdutoServices
    {
        private readonly IRedisCache _redis;
        private readonly IAzureStorage _storage;
        public ProdutoServices(IRedisCache redis, IAzureStorage storage)//cache aside
        {
            _redis = redis;
            _storage = storage;
        }
        public async Task<List<Produto>> ObterProdutos() {

            var key = "produtos";
            var value = _redis.Get(key);
            if (!string.IsNullOrEmpty(value)) {
                var produtos = await _storage.ObterProdutos();

                _redis.Set(key, JsonConvert.SerializeObject(produtos));
                return produtos;
            }


            return JsonConvert.DeserializeObject<List<Produto>>(value);
        }
        Produto IProdutoServices.ObterProduto(string id)
        {
            return  _storage.ObterProduto(id).Result;
        }
    }

    
}
