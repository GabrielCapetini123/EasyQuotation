using EasyQuotation.Models.Data;
using EasyQuotation.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyQuotation.DAL
{
    public class ProdutoDAL : IProdutoDAL
    {
        private readonly EasyQuotationDataContext _context;

        public ProdutoDAL(string connectionString)
        {
            _context = new EasyQuotationDataContext(connectionString);
        }

        public void InserirProduto(Produto produto)
        {
            try
            {
                _context.Produtos.InsertOnSubmit(produto);
                _context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir produto no banco de dados.", ex);
            }
        }

        public List<Produto> ListarProdutos()
        {
            try
            {
                return _context.Produtos
                    .OrderBy(p => p.Nome)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar produtos.", ex);
            }
        }

        public void ExcluirProduto(int id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

                if (produto == null)
                    throw new Exception("Produto não encontrado.");

                _context.Produtos.DeleteOnSubmit(produto);
                _context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir produto.", ex);
            }
        }
    }
}
