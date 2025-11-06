using EasyQuotation.DAL;
using EasyQuotation.Models;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EasyQuotation.Pages
{
    public partial class Fornecedores : System.Web.UI.Page
    {
        private FornecedorDAL _dal;

        protected void Page_Load(object sender, EventArgs e)
        {
            _dal = new FornecedorDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);

            if (!IsPostBack)
                CarregarGrid();
            else
                HandlePostBackEvent();
        }

        protected void gvFornecedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                int idFornecedor = Convert.ToInt32(e.CommandArgument);

                try
                {
                    _dal.ExcluirFornecedor(idFornecedor);
                    CarregarGrid();
                    MostrarToast("Fornecedor excluído com sucesso!", "success");
                }
                catch (Exception ex)
                {
                    var logDal = new LogDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
                    logDal.RegistrarLog("gvFornecedores_RowCommand - Fornecedores", ex);
                    MostrarToast("Ocorreu um erro ao excluir o fornecedor. Tente novamente mais tarde.", "danger");
                }
            }
        }

        protected void HandlePostBackEvent()
        {
            string eventTarget = (Request["__EVENTTARGET"] ?? "").TrimEnd(',');
            string eventArgument = (Request["__EVENTARGUMENT"] ?? "").TrimEnd(',');

            if (eventTarget == "ExcluirFornecedor" && int.TryParse(eventArgument, out int idFornecedor))
            {
                try
                {
                    _dal.ExcluirFornecedor(idFornecedor);
                    CarregarGrid();
                    MostrarToast("Fornecedor excluído com sucesso!", "success");
                }
                catch (Exception ex)
                {
                    var logDal = new LogDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
                    logDal.RegistrarLog("HandlePostBackEvent - Fornecedores", ex);
                    MostrarToast("Ocorreu um erro ao excluir o fornecedor. Tente novamente mais tarde.", "danger");
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string nome = txtNome.Text.Trim();
                string cnpj = txtCNPJ.Text.Trim();

                if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(cnpj))
                {
                    MostrarToast("Preencha todos os campos antes de salvar.", "danger");
                    return;
                }

                if (!ValidarCNPJ(cnpj))
                {
                    MostrarToast("CNPJ inválido! Verifique e tente novamente.", "danger");
                    return;
                }

                var fornecedor = new EasyQuotation.Models.Entities.Fornecedor
                {
                    Nome = nome,
                    CNPJ = cnpj
                };

                _dal.InserirFornecedor(fornecedor);
                LimparCampos();
                CarregarGrid();
                MostrarToast("Fornecedor salvo com sucesso!", "success");
            }
            catch (Exception ex)
            {
                var logDal = new LogDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
                logDal.RegistrarLog("btnSalvar_Click - Fornecedores", ex);
                MostrarToast("Ocorreu um erro ao salvar o fornecedor. Tente novamente mais tarde.", "danger");
            }
        }

        private void CarregarGrid()
        {
            gvFornecedores.DataSource = _dal.ListarFornecedores();
            gvFornecedores.DataBind();
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtCNPJ.Text = "";
        }

        private void MostrarToast(string mensagem, string tipo)
        {
            mensagem = mensagem.Replace("'", "\\'");
            tipo = string.IsNullOrEmpty(tipo) ? "info" : tipo;

            ViewState["ToastMessage"] = mensagem;
            ViewState["ToastType"] = tipo;

            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(),
                $"setTimeout(function(){{ exibirToast('{mensagem}', '{tipo}'); }}, 200);", true);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            if (ViewState["ToastMessage"] != null)
            {
                string mensagem = ViewState["ToastMessage"].ToString();
                string tipo = ViewState["ToastType"].ToString();

                ScriptManager.RegisterStartupScript(this, GetType(), "DelayedToast",
                    $"setTimeout(function(){{ exibirToast('{mensagem}', '{tipo}'); }}, 300);", true);

                ViewState["ToastMessage"] = null;
                ViewState["ToastType"] = null;
            }
        }

        private bool ValidarCNPJ(string cnpj)
        {
            cnpj = Regex.Replace(cnpj, "[^0-9]", "");
            if (cnpj.Length != 14) return false;
            if (new string(cnpj[0], cnpj.Length) == cnpj) return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            int resto = (soma % 11) < 2 ? 0 : 11 - (soma % 11);
            string digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11) < 2 ? 0 : 11 - (soma % 11);
            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}
