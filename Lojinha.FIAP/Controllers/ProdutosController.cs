using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lojinha.FIAP.Models;
using Microsoft.AspNetCore.Authorization;
using Lojinha.FIAP.Core.Models;
using Lojinha.FIAP.Infrastructure.Storage;
using Lojinha.FIAP.Core.Services;
using AutoMapper;
using Lojinha.FIAP.Core.ViewModels;

namespace Lojinha.FIAP.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly IProdutoServices _produtoServices;
        private readonly IMapper _mapper;
        public ProdutosController(IProdutoServices produtoServices, IMapper mapper)
        {
            _produtoServices = produtoServices;
            _mapper = mapper;
        }
        public IActionResult Create()
        {
            var produto = new Produto
            {
                Id = 330731,
                Nome = "Motorola V3",
                Fabricante = new Fabricante
                {
                    Id = 999,
                    Nome = "Motorola",
                },
                ImagemPrincipalUrl = "https://cache.olhardigital.com.br/uploads/acervo_imagens/2014/07/20140702145535_660_420.jpg",
                Preco = 100,
                Tags = new string[] { "reliquia", "celular", "motorola" },
                Imagens = new Imagens[] {
                        new Imagens() {
                           ImagemUrl = "https://http2.mlstatic.com/celular-motorola-v3-original-pronta-entrega-D_NQ_NP_16576-MLB20123510664_072014-O.jpg",
                            Id=1,
                            ProdutoId =1
                       }
                   }
            };

           // _produtoServices.AddProduto(produto);

            return Content("Ok");
        }

        public async Task<IActionResult> List() {
            var lista = await _produtoServices.ObterProdutos();
            var vm = _mapper.Map<List<ProdutoViewModel>>(lista);
            return View(vm);
        }

        public IActionResult Details(string id) {
            var produto =  _produtoServices.ObterProduto(id);
            var vm = _mapper.Map<ProdutoViewModel>(produto);
            return View(vm);
        }
    }
}
