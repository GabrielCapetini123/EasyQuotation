using EasyQuotation.BLL;
using EasyQuotation.DAL;
using EasyQuotation.Models.Entities;
using System;
using System.Configuration;
using System.Web.UI;

namespace EasyQuotation.Pages
{
    public partial class Produtos : System.Web.UI.Page
    {
        private ProdutoBLL _bll;

        protected void Page_Load(object sender, EventArgs e)
        {
            _bll = new ProdutoBLL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);

            if (!IsPostBack)
                CarregarGrid();
            else
                HandlePostBackEvent();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string nome = txtNome.Text.Trim();

                var produto = new Produto
                {
                    Nome = nome
                };

                _bll.SalvarProduto(produto);

                txtNome.Text = "";
                CarregarGrid();
                MostrarToast("Produto salvo com sucesso!", "success");
            }
            catch (Exception ex)
            {
                var logDal = new LogDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
                logDal.RegistrarLog("btnSalvar_Click - Produtos", ex);
                MostrarToast(ex.Message, "danger");
            }
        }

        private void HandlePostBackEvent()
        {
            string eventTarget = (Request["__EVENTTARGET"] ?? "").TrimEnd(',');
            string eventArgument = (Request["__EVENTARGUMENT"] ?? "").TrimEnd(',');

            if (eventTarget == "ExcluirProduto" && int.TryParse(eventArgument, out int idProduto))
            {
                try
                {
                    _bll.ExcluirProduto(idProduto);
                    CarregarGrid();
                    MostrarToast("Produto excluído com sucesso!", "success");
                }
                catch (Exception ex)
                {
                    var logDal = new LogDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
                    logDal.RegistrarLog("HandlePostBackEvent - Produtos", ex);
                    MostrarToast(ex.Message, "danger");
                }
            }
        }

        private void CarregarGrid()
        {
            try
            {
                gvProdutos.DataSource = _bll.ListarProdutos();
                gvProdutos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarToast($"Erro ao carregar produtos: {ex.Message}", "danger");
            }
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
    }
}
