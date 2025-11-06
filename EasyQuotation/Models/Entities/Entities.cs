using System;
using System.Data.Linq.Mapping;

namespace EasyQuotation.Models.Entities
{
    [Table(Name = "Fornecedores")]
    public class Fornecedor
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Nome { get; set; }

        [Column]
        public string CNPJ { get; set; }
    }

    [Table(Name = "Produtos")]
    public class Produto
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Nome { get; set; }
    }

    [Table(Name = "Cotacoes")]
    public class Cotacao
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public int FornecedorId { get; set; }

        [Column]
        public int ProdutoId { get; set; }

        [Column]
        public decimal Preco { get; set; }

        [Column]
        public DateTime Data { get; set; }
    }
}
