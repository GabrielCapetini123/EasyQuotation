using EasyQuotation.Models.Entities;
using EasyQuotation.Models.ViewModels;
using System.Collections.Generic;

namespace EasyQuotation.Interfaces
{
    public interface ICotacaoDAL
    {
        void InserirCotacao(Cotacao cotacao);
        List<CotacaoViewModel> ListarCotacoes();
        List<MenorPrecoViewModel> ConsultarMenorPrecoPorProduto();
        void ExcluirCotacao(int id);
    }
}
