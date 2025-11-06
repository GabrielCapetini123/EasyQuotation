using System;

namespace EasyQuotation.Models.ViewModels
{
    public class CotacaoViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Preco { get; set; }
        public int FornecedorId { get; set; }
        public int ProdutoId { get; set; }
        public string FornecedorNome { get; set; }
        public string ProdutoNome { get; set; }
    }
}