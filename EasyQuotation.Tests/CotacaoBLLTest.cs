using System;
using System.Collections.Generic;
using EasyQuotation.BLL;
using EasyQuotation.Interfaces;
using EasyQuotation.Models.Entities;
using EasyQuotation.Models.ViewModels;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EasyQuotation.Tests
{
    [TestFixture]
    public class CotacaoBLLTests
    {
        private Mock<ICotacaoDAL> _mockDal;
        private CotacaoBLL _bll;

        [SetUp]
        public void Setup()
        {
            _mockDal = new Mock<ICotacaoDAL>();
            _bll = new CotacaoBLL(_mockDal.Object);
        }

        [Test]
        public void SalvarCotacao_DeveLancarExcecao_SePrecoForZeroOuNegativo()
        {
            var cotacao = new Cotacao
            {
                Id = 1,
                FornecedorId = 1,
                ProdutoId = 1,
                Preco = 0 
            };

            Action act = () => _bll.SalvarCotacao(cotacao);

            act.Should().Throw<Exception>()
                .WithMessage("O preço deve ser maior que zero.");
        }

        [Test]
        public void SalvarCotacao_DeveChamarDal_SeCotacaoValida()
        {
            var cotacao = new Cotacao
            {
                Id = 1,
                FornecedorId = 2,
                ProdutoId = 3,
                Preco = 120.50m
            };

            _bll.SalvarCotacao(cotacao);

            _mockDal.Verify(d => d.InserirCotacao(It.IsAny<Cotacao>()), Times.Once);
        }

        [Test]
        public void ListarCotacoes_DeveRetornarListaDoDal()
        {
            var listaEsperada = new List<CotacaoViewModel>
            {
                new CotacaoViewModel { Id = 1, FornecedorNome = "Fornecedor A", ProdutoNome = "Produto A", Preco = 100 },
                new CotacaoViewModel { Id = 2, FornecedorNome = "Fornecedor B", ProdutoNome = "Produto B", Preco = 150 }
            };

            _mockDal.Setup(d => d.ListarCotacoes()).Returns(listaEsperada);

            var resultado = _bll.ListarCotacoes();

            resultado.Should().HaveCount(2);
            resultado.Should().BeEquivalentTo(listaEsperada);
        }

        [Test]
        public void ConsultarMenorPrecoPorProduto_DeveRetornarListaDoDal()
        {
            var listaEsperada = new List<MenorPrecoViewModel>
            {
                new MenorPrecoViewModel { ProdutoNome = "Produto X", FornecedorNome = "Fornecedor 1", Preco = 90 },
                new MenorPrecoViewModel { ProdutoNome = "Produto Y", FornecedorNome = "Fornecedor 2", Preco = 120 }
            };

            _mockDal.Setup(d => d.ConsultarMenorPrecoPorProduto()).Returns(listaEsperada);

            var resultado = _bll.ConsultarMenorPrecoPorProduto();

            resultado.Should().HaveCount(2);
            resultado.Should().BeEquivalentTo(listaEsperada);
        }

        [Test]
        public void ExcluirCotacao_DeveChamarDalCorretamente()
        {
            _bll.ExcluirCotacao(10);

            _mockDal.Verify(d => d.ExcluirCotacao(10), Times.Once);
        }
    }
}
