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
            if (cotacao.Preco <= 0)
                throw new Exception("O preço deve ser maior que zero.");

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
            _dal.ExcluirCotacao(id);
        }
    }
}
