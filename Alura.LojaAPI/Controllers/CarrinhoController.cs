using Alura.LojaAPI.DAO;
using Alura.LojaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alura.LojaAPI.Controllers
{
    public class CarrinhoController : ApiController
    {
        //[Route("api/carrinho/{idCarrinho}/produto/{idProduto}")]
        //public HttpResponseMessage Put([FromBody]Produto produto, [FromUri] int idCarrinho, [FromUri] int idProduto)
        //{
        //    var dao = new CarrinhoDAO();
        //    var carrinho = dao.Busca(idCarrinho);

        //    carrinho.Troca(produto);

        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        [Route("api/carrinho/{idCarrinho}/produto/{idProduto}/quantidade")]
        public HttpResponseMessage Put([FromBody]Produto produto, [FromUri] int idCarrinho, [FromUri] int idProduto)
        {
            var dao = new CarrinhoDAO();
            var carrinho = dao.Busca(idCarrinho);

            carrinho.TrocaQuantidade(produto);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/carrinho/{idCarrinho}/produto/{idProduto}")]
        public HttpResponseMessage Delete([FromUri] int idCarrinho, [FromUri] int idProduto)
        {
            CarrinhoDAO dao = new CarrinhoDAO();
            Carrinho carrinho = dao.Busca(idCarrinho);
            carrinho.Remove(idProduto);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Get(int id)
        {
            try
            {
                CarrinhoDAO dao = new CarrinhoDAO();
                Carrinho carrinho = dao.Busca(id);
                return Request.CreateResponse(HttpStatusCode.OK, carrinho); ;
            }
            catch (KeyNotFoundException)
            {
                string mensagem = string.Format("O carrinho {0} não foi encontrado", id);
                HttpError error = new HttpError(mensagem);
                return Request.CreateResponse(HttpStatusCode.NotFound, error);
            }

        }

        public HttpResponseMessage Post([FromBody]Carrinho carrinho)
        {
            CarrinhoDAO dao = new CarrinhoDAO();
            dao.Adiciona(carrinho);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

            string location = Url.Link("DefaultApi", new { controller = "carrinho", id = carrinho.Id });
            response.Headers.Location = new Uri(location);

            return response;
        }
    }
}
