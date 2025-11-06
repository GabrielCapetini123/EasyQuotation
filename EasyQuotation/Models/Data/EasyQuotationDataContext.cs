using EasyQuotation.Models.Entities;
using System.Data.Linq;

namespace EasyQuotation.Models.Data
{
    public class EasyQuotationDataContext : DataContext
    {
        public EasyQuotationDataContext(string connectionString)
            : base(connectionString)
        {
        }

        public Table<Fornecedor> Fornecedores => GetTable<Fornecedor>();
        public Table<Produto> Produtos => GetTable<Produto>();
        public Table<Cotacao> Cotacoes => GetTable<Cotacao>();
        public Table<Log> Logs => this.GetTable<Log>();

    }
}
