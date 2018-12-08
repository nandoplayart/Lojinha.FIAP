using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Lojinha.FIAP.Core.Models;
using Lojinha.FIAP.Core.Entities;

namespace Lojinha.FIAP.Infrastructure.Storage
{
    public class AzureStorage : IAzureStorage
    {
        public readonly CloudStorageAccount _account;
        private readonly CloudTableClient _tableClient;
        public AzureStorage(IConfiguration configuration)
        {
            _account = CloudStorageAccount.Parse(configuration.GetSection("Azure:Storage").Value);
            _tableClient = _account.CreateCloudTableClient();
        }

        public void AddProduto(Produto produto) {
            var json = JsonConvert.SerializeObject(produto);
            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();

            var entity = new ProdutoEntity("13net", produto.Id.ToString());//"RM330731"
            entity.Produto = json;
            TableOperation operation = TableOperation.Insert(entity);
            table.ExecuteAsync(operation);
        }

        public async Task<Produto> ObterProduto(string id)
        {
            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();
            var query = new TableQuery<ProdutoEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "13net"))
                .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));

            TableContinuationToken token = null;
            var segment = await table
                .ExecuteQuerySegmentedAsync(query, token);
            var produtoEntity = segment.FirstOrDefault();

            return JsonConvert.DeserializeObject<Produto>(produtoEntity.Produto);




        }

        public async Task<List<Produto>> ObterProdutos() {

            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();
            var query = new TableQuery<ProdutoEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "13net"));

            TableContinuationToken token = null;
            var segment = await table.ExecuteQuerySegmentedAsync(query, token);
            var produtosEntity = segment.ToList();


            return produtosEntity.Where(x=> x.Produto != null).Select(x => JsonConvert.DeserializeObject<Produto>(x.Produto)).ToList();

            


        }
    }
}
