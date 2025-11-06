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
    public class FornecedorBLLTests
    {
        private Mock<IFornecedorDAL> _mockDal;
        private FornecedorBLL _bll;

        [SetUp]
        public void Setup()
        {
            _mockDal = new Mock<IFornecedorDAL>();
            _bll = new FornecedorBLL(_mockDal.Object);
        }

        [Test]
        public void SalvarFornecedor_DeveLancarExcecao_SeNomeForVazio()
        {
            var fornecedor = new Fornecedor { Nome = "", CNPJ = "12.345.678/0001-90" };

            Action act = () => _bll.SalvarFornecedor(fornecedor);
            act.Should().Throw<Exception>()
                .WithMessage("O nome do fornecedor é obrigatório.");
        }

        [Test]
        public void SalvarFornecedor_DeveLancarExcecao_SeCNPJForVazio()
        {
            var fornecedor = new Fornecedor { Nome = "Fornecedor A", CNPJ = "" };

            Action act = () => _bll.SalvarFornecedor(fornecedor);

            act.Should().Throw<Exception>()
                .WithMessage("O CNPJ é obrigatório.");
        }

        [Test]
        public void SalvarFornecedor_DeveLancarExcecao_SeCNPJForInvalido()
        {
            var fornecedor = new Fornecedor { Nome = "Fornecedor B", CNPJ = "12.345.678/0001-99" };

            Action act = () => _bll.SalvarFornecedor(fornecedor);

            act.Should().Throw<Exception>()
                .WithMessage("CNPJ inválido!");
        }

        [Test]
        public void SalvarFornecedor_DeveChamarDal_SeFornecedorValido()
        {
            var fornecedor = new Fornecedor
            {
                Nome = "Fornecedor Teste",
                CNPJ = "45.723.174/0001-10"
            };

            _bll.SalvarFornecedor(fornecedor);

            _mockDal.Verify(d => d.InserirFornecedor(It.IsAny<Fornecedor>()), Times.Once);
        }

        [Test]
        public void ListarFornecedores_DeveRetornarListaDoDal()
        {
            var listaEsperada = new List<Fornecedor>
            {
                new Fornecedor { Id = 1, Nome = "Fornecedor 1" },
                new Fornecedor { Id = 2, Nome = "Fornecedor 2" }
            };

            _mockDal.Setup(d => d.ListarFornecedores()).Returns(listaEsperada);

            var resultado = _bll.ListarFornecedores();

            resultado.Should().HaveCount(2);
            resultado.Should().BeEquivalentTo(listaEsperada);
        }

        [Test]
        public void ExcluirFornecedor_DeveChamarDalCorretamente()
        {
            _bll.ExcluirFornecedor(5);

            _mockDal.Verify(d => d.ExcluirFornecedor(5), Times.Once);
        }
    }
}
