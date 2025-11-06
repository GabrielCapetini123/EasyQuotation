using EasyQuotation.DAL;
using EasyQuotation.Models.Entities;
using System;
using System.Collections.Generic;

namespace EasyQuotation.BLL
{
    public class ProdutoBLL
    {
        private readonly ProdutoDAL _dal;

        public ProdutoBLL(string connectionString)
        {
            _dal = new ProdutoDAL(connectionString);
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

                _dal.InserirProduto(produto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }

        public List<Produto> ListarProdutos()
        {
            try
            {
                return _dal.ListarProdutos();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar os produtos.", ex);
            }
        }

        public void ExcluirProduto(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("ID de produto inválido.");

                _dal.ExcluirProduto(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }
    }
}
