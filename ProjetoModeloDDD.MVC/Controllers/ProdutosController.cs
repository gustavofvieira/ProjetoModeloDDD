
using AutoMapper;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.MVC.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjetoModeloDDD.MVC.Controllers
{
    public class ProdutosController : Controller
    {
        //private readonly produtoRepository _produtoRepository = new produtoRepository(); // Isso é gabiarra, não está no padrão a camada de apresentação se associar ao dominio
        private readonly IProdutoAppService _produtoApp;
        private readonly IClienteAppService _clienteApp;

        //public ProdutosController(IProdutoAppService produtoApp, IClienteAppService clienteApp)
        //{
        //    _produtoApp = produtoApp;
        //    _clienteApp = clienteApp;
        //}

        public ProdutosController(IProdutoAppService produtoApp)
        {
            _produtoApp = produtoApp;
            
        }

        // GET: Produtos
        public ActionResult Index()
        {
            var produtoViewModel = Mapper.Map<IEnumerable<Produto>, IEnumerable<ProdutoViewModel>>(_produtoApp.GetAll());
            //var produtoViewModel = Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(_clienteApp.GetAll());
            return View(produtoViewModel);
        }  


        // GET: Produtos/Details/5
        public ActionResult Details(int id)
        {
            var produto = _produtoApp.GetById(id);
            //transforma produto para viewmodel
            var ProdutoViewModel = Mapper.Map<Produto, ProdutoViewModel>(produto);
            return View(ProdutoViewModel);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(_clienteApp.GetAll(), "ClienteId", "Nome");
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProdutoViewModel produto)
        {
            if (ModelState.IsValid)
            {
                var clientDomain = Mapper.Map<ProdutoViewModel, Produto>(produto);
                _produtoApp.Add(clientDomain);

                return RedirectToAction("Index");
            }
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int id)
        {
            var produto = _produtoApp.GetById(id);
            //transforma produto para viewmodel
            var ProdutoViewModel = Mapper.Map<Produto, ProdutoViewModel>(produto);
            return View(ProdutoViewModel);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProdutoViewModel produto)
        {
            if (ModelState.IsValid)
            {
                var produtoDomain = Mapper.Map<ProdutoViewModel, Produto>(produto);
                _produtoApp.Update(produtoDomain);
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(_clienteApp.GetAll(), "ClienteId", "Nome", produto.ClienteId);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int id)
        {
            var produto = _produtoApp.GetById(id);
            //transforma produto para viewmodel
            var ProdutoViewModel = Mapper.Map<Produto, ProdutoViewModel>(produto);
            return View(ProdutoViewModel);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var produto = _produtoApp.GetById(id);
            _produtoApp.Remove(produto);
            return RedirectToAction("Index");
        }
    }
}