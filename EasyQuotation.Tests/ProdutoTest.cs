using System;
using System.Collections.Generic;
using EasyQuotation.BLL;
using EasyQuotation.DAL;
using EasyQuotation.Models.Entities;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EasyQuotation.Tests
{
    [TestFixture]
    public class ProdutoBLLTests
    {
        private Mock<IProdutoDAL> _mockDal;
        private ProdutoBLL _bll;

        [SetUp]
        public void Setup()
        {
            _mockDal = new Mock<IProdutoDAL>();
            _bll = new ProdutoBLL(_mockDal.Object);
        }

        [Test]
        public void SalvarProduto_DeveLancarExcecao_SeProdutoForNulo()
        {
            Produto produto = null;
            Action act = () => _bll.SalvarProduto(produto);
            act.Should().Throw<Exception>()
                .WithMessage("Erro ao salvar produto: O produto não pode ser nulo.*");
        }

        [Test]
        public void SalvarProduto_DeveLancarExcecao_SeNomeForVazio()
        {
            var produto = new Produto { Nome = " ", Id = 1 };

            Action act = () => _bll.SalvarProduto(produto);

            act.Should().Throw<Exception>()
                .WithMessage("Erro ao salvar produto: O nome do produto é obrigatório.*");
        }

        [Test]
        public void SalvarProduto_DeveLancarExcecao_SeNomeForMenorQue3Caracteres()
        {
            var produto = new Produto { Nome = "AB", Id = 1 };

            Action act = () => _bll.SalvarProduto(produto);

            act.Should().Throw<Exception>()
                .WithMessage("Erro ao salvar produto: O nome do produto deve conter ao menos 3 caracteres.*");
        }

        [Test]
        public void SalvarProduto_DeveChamarDal_QuandoProdutoValido()
        {
            var produto = new Produto { Nome = "Monitor Gamer", Id = 1 };

            _bll.SalvarProduto(produto);

            _mockDal.Verify(d => d.InserirProduto(It.IsAny<Produto>()), Times.Once);
        }

        [Test]
        public void ListarProdutos_DeveRetornarListaCorreta()
        {
            var produtosEsperados = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Teclado Mecânico" },
                new Produto { Id = 2, Nome = "Mouse Gamer" }
            };

            _mockDal.Setup(d => d.ListarProdutos()).Returns(produtosEsperados);

            var resultado = _bll.ListarProdutos();

            resultado.Should().BeEquivalentTo(produtosEsperados);
            _mockDal.Verify(d => d.ListarProdutos(), Times.Once);
        }

        [Test]
        public void ListarProdutos_DeveLancarExcecao_QuandoDalFalhar()
        {
            _mockDal.Setup(d => d.ListarProdutos()).Throws(new Exception("Falha de banco"));

            Action act = () => _bll.ListarProdutos();

            act.Should().Throw<Exception>()
                .WithMessage("Erro ao listar os produtos: Falha de banco*");
        }

        [Test]
        public void ExcluirProduto_DeveLancarExcecao_SeIdForInvalido()
        {
            Action act = () => _bll.ExcluirProduto(0);

            act.Should().Throw<Exception>()
                .WithMessage("Erro ao excluir produto: ID de produto inválido.*");
        }

        [Test]
        public void ExcluirProduto_DeveChamarDal_QuandoIdValido()
        {
            _bll.ExcluirProduto(5);

            _mockDal.Verify(d => d.ExcluirProduto(5), Times.Once);
        }

        [Test]
        public void ExcluirProduto_DeveLancarExcecao_QuandoDalFalhar()
        {
            _mockDal.Setup(d => d.ExcluirProduto(It.IsAny<int>()))
                .Throws(new Exception("Violação de chave estrangeira"));

            Action act = () => _bll.ExcluirProduto(10);

            act.Should().Throw<Exception>()
                .WithMessage("Erro ao excluir produto: Violação de chave estrangeira*");
        }
    }
}
