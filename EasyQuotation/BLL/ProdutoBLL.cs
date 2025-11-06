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
            try
            {
                if (produto == null)
                    throw new Exception("O produto não pode ser nulo.");

                if (string.IsNullOrWhiteSpace(produto.Nome))
                    throw new Exception("O nome do produto é obrigatório.");

                if (produto.Nome.Trim().Length < 3)
                    throw new Exception("O nome do produto deve conter ao menos 3 caracteres.");

                _produtoDal.InserirProduto(produto);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar produto: " + ex.Message, ex);
            }
        }

        public List<Produto> ListarProdutos()
        {
            try
            {
                return _produtoDal.ListarProdutos();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar os produtos: " + ex.Message, ex);
            }
        }

        public void ExcluirProduto(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("ID de produto inválido.");

                _produtoDal.ExcluirProduto(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir produto: " + ex.Message, ex);
            }
        }
    }
}
