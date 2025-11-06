using EasyQuotation.Models.Entities;
using System.Collections.Generic;

namespace EasyQuotation.DAL
{
    public interface IProdutoDAL
    {
        void InserirProduto(Produto produto);
        List<Produto> ListarProdutos();
        void ExcluirProduto(int id);
    }
}
