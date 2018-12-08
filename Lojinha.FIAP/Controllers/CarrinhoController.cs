using Lojinha.FIAP.Core.Models;
using Lojinha.FIAP.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.FIAP.Controllers
{
    [Authorize]
    public class CarrinhoController:Controller
    {

        private readonly IProdutoServices _produtoServices;
        private readonly ICarrinhoServices _carrinhoService;

        public CarrinhoController(IProdutoServices produtoServices,ICarrinhoServices carrinhoService)
        {
            _produtoServices = produtoServices;
            _carrinhoService = carrinhoService;
        }

        public  IActionResult Add(string id) {
            var usuario = HttpContext.User.Identity.Name;
            Carrinho carrinho =  _carrinhoService.Obter(usuario);
            carrinho.Add(_produtoServices.ObterProduto(id));
            _carrinhoService.Salvar(usuario, carrinho);

            return PartialView("Index",carrinho);
        }



        public IActionResult Finalizar() {
            var usuario = HttpContext.User.Identity.Name;
            var carrinho =  _carrinhoService.Obter(usuario);


            _carrinhoService.Limpar(usuario);

            return View(carrinho);
        }
    }
}
