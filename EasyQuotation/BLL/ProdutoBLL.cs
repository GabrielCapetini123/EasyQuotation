using EasyQuotation.DAL;
using EasyQuotation.Models.Entities;
using System;
using System.Collections.Generic;

namespace EasyQuotation.BLL
{
    public class ProdutoBLL
    {
        private readonly IProdutoDAL _produtoDal;

        public ProdutoBLL(IProdutoDAL produtoDal)
        {
            _produtoDal = produtoDal ?? throw new ArgumentNullException(nameof(produtoDal), "A dependência ProdutoDAL não pode ser nula.");
        }

        public void SalvarProduto(Produto produto)
        {
            if (produto == null)
                throw new Exception("O produto não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(produto.Nome))
                throw new Exception("O nome do produto é obrigatório.");

            if (produto.Nome.Trim().Length < 3)
                throw new Exception("O nome do produto deve conter ao menos 3 caracteres.");

            _produtoDal.InserirProduto(produto);
        }

        public List<Produto> ListarProdutos()
        {
            return _produtoDal.ListarProdutos();
        }

        public void ExcluirProduto(int id)
        {
            if (id <= 0)
                throw new Exception("ID de produto inválido.");

            _produtoDal.ExcluirProduto(id);
        }
    }
}
