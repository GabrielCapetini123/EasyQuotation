using EasyQuotation.Models.Entities;
using System.Collections.Generic;

namespace EasyQuotation.DAL
{
    public interface IFornecedorDAL
    {
        void InserirFornecedor(Fornecedor fornecedor);
        List<Fornecedor> ListarFornecedores();
        void ExcluirFornecedor(int id);
    }
}
