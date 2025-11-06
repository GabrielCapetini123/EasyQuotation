using EasyQuotation.Interfaces;
using EasyQuotation.Models.Data;
using EasyQuotation.Models.Entities;
using EasyQuotation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyQuotation.DAL
{
    public class CotacaoDAL : ICotacaoDAL
    {
        private readonly EasyQuotationDataContext _context;

        public CotacaoDAL(string connectionString)
        {
            _context = new EasyQuotationDataContext(connectionString);
        }

        public void InserirCotacao(Cotacao cotacao)
        {
            try
            {
                _context.Cotacoes.InsertOnSubmit(cotacao);
                _context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir cotação no banco de dados.", ex);
            }
        }

        public List<CotacaoViewModel> ListarCotacoes()
        {
            try
            {
                var query = from c in _context.Cotacoes
                            join f in _context.Fornecedores on c.FornecedorId equals f.Id
                            join p in _context.Produtos on c.ProdutoId equals p.Id
                            orderby c.Id descending
                            select new CotacaoViewModel
                            {
                                Id = c.Id,
                                Data = c.Data,
                                Preco = c.Preco,
                                FornecedorId = f.Id,
                                ProdutoId = p.Id,
                                FornecedorNome = f.Nome,
                                ProdutoNome = p.Nome
                            };

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar cotações.", ex);
            }
        }

        public void ExcluirCotacao(int id)
        {
            try
            {
                var cotacao = _context.Cotacoes.FirstOrDefault(c => c.Id == id);

                if (cotacao == null)
                    throw new Exception("Cotação não encontrada.");

                _context.Cotacoes.DeleteOnSubmit(cotacao);
                _context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir cotação.", ex);
            }
        }

        public List<MenorPrecoViewModel> ConsultarMenorPrecoPorProduto()
        {
            try
            {
                var query = from c in _context.Cotacoes
                            group c by c.ProdutoId into g
                            let menorPreco = g.Min(x => x.Preco)
                            join p in _context.Produtos on g.Key equals p.Id
                            select new MenorPrecoViewModel
                            {
                                ProdutoNome = p.Nome,
                                Preco = menorPreco,
                                FornecedorNome = (
                                    from c2 in _context.Cotacoes
                                    join f in _context.Fornecedores on c2.FornecedorId equals f.Id
                                    where c2.ProdutoId == p.Id && c2.Preco == menorPreco
                                    select f.Nome
                                ).FirstOrDefault()
                            };

                return query.OrderBy(x => x.ProdutoNome).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar menor preço por produto.", ex);
            }
        }
    }
}
