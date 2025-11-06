using EasyQuotation.DAL;
using EasyQuotation.Models.Entities;
using System;
using System.Collections.Generic;

namespace EasyQuotation.BLL
{
    public class FornecedorBLL
    {
        private readonly IFornecedorDAL _dal;

        public FornecedorBLL(IFornecedorDAL fornecedorDal)
        {
            _dal = fornecedorDal ?? throw new ArgumentNullException(nameof(fornecedorDal));
        }

        public void SalvarFornecedor(Fornecedor fornecedor)
        {
            if (string.IsNullOrWhiteSpace(fornecedor.Nome))
                throw new Exception("O nome do fornecedor é obrigatório.");

            if (string.IsNullOrWhiteSpace(fornecedor.CNPJ))
                throw new Exception("O CNPJ é obrigatório.");

            if (!ValidarCNPJ(fornecedor.CNPJ))
                throw new Exception("CNPJ inválido!");

            _dal.InserirFornecedor(fornecedor);
        }

        public List<Fornecedor> ListarFornecedores()
        {
            return _dal.ListarFornecedores();
        }

        public void ExcluirFornecedor(int id)
        {
            _dal.ExcluirFornecedor(id);
        }

        private bool ValidarCNPJ(string cnpj)
        {
            cnpj = System.Text.RegularExpressions.Regex.Replace(cnpj, "[^0-9]", "");
            if (cnpj.Length != 14) return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            string digito = resto.ToString();
            tempCnpj += digito;

            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}
