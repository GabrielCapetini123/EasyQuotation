using EasyQuotation.Interfaces;
using EasyQuotation.Models.Entities;
using EasyQuotation.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace EasyQuotation.BLL
{
    public class CotacaoBLL
    {
        private readonly ICotacaoDAL _dal;

        public CotacaoBLL(ICotacaoDAL cotacaoDal)
        {
            _dal = cotacaoDal ?? throw new ArgumentNullException(nameof(cotacaoDal));
        }

        public void SalvarCotacao(Cotacao cotacao)
        {
            if (cotacao == null)
                throw new Exception("A cotação não pode ser nula.");

            if (cotacao.Preco <= 0)
                throw new Exception("O preço da cotação deve ser maior que zero.");

            if (cotacao.FornecedorId <= 0)
                throw new Exception("É necessário informar um fornecedor válido.");

            if (cotacao.ProdutoId <= 0)
                throw new Exception("É necessário informar um produto válido.");

            _dal.InserirCotacao(cotacao);
        }

        public List<CotacaoViewModel> ListarCotacoes()
        {
            return _dal.ListarCotacoes();
        }

        public List<MenorPrecoViewModel> ConsultarMenorPrecoPorProduto()
        {
            return _dal.ConsultarMenorPrecoPorProduto();
        }

        public void ExcluirCotacao(int id)
        {
            if (id <= 0)
                throw new Exception("O ID da cotação é inválido.");

            _dal.ExcluirCotacao(id);
        }
    }
}
