using EasyQuotation.Models.Data;
using EasyQuotation.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyQuotation.DAL
{
    public class FornecedorDAL
    {
        private readonly EasyQuotationDataContext _context;

        public FornecedorDAL(string connectionString)
        {
            _context = new EasyQuotationDataContext(connectionString);
        }

        public void InserirFornecedor(Fornecedor fornecedor)
        {
            try
            {
                _context.Fornecedores.InsertOnSubmit(fornecedor);
                _context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir fornecedor no banco de dados.", ex);
            }
        }

        public List<Fornecedor> ListarFornecedores()
        {
            try
            {
                return _context.Fornecedores
                    .OrderBy(f => f.Nome)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar fornecedores.", ex);
            }
        }

        public void ExcluirFornecedor(int id)
        {
            try
            {
                var fornecedor = _context.Fornecedores.FirstOrDefault(f => f.Id == id);
                if (fornecedor == null)
                    throw new Exception("Fornecedor não encontrado.");

                _context.Fornecedores.DeleteOnSubmit(fornecedor);
                _context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir fornecedor.", ex);
            }
        }
    }
}
