using EasyQuotation.BLL;
using EasyQuotation.DAL;
using EasyQuotation.Models.Entities;
using System;
using System.Configuration;
using System.Globalization;
using System.Web.UI;

namespace EasyQuotation.Pages
{
    public partial class Cotacoes : System.Web.UI.Page
    {
        private CotacaoBLL _cotacaoBll;
        private FornecedorBLL _fornecedorBll;
        private ProdutoBLL _produtoBll;

        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString;

            _cotacaoBll = new CotacaoBLL(connectionString);
            _fornecedorBll = new FornecedorBLL(connectionString);
            _produtoBll = new ProdutoBLL(connectionString);

            if (!IsPostBack)
            {
                CarregarDropdowns();
                CarregarGrid();
            }
            else
            {
                string eventTarget = Request["__EVENTTARGET"];
                if (!string.IsNullOrEmpty(eventTarget))
                    HandlePostBackEvent();
            }
        }

        private void CarregarDropdowns()
        {
            ddlFornecedor.DataSource = _fornecedorBll.ListarFornecedores();
            ddlFornecedor.DataTextField = "Nome";
            ddlFornecedor.DataValueField = "Id";
            ddlFornecedor.DataBind();

            ddlProduto.DataSource = _produtoBll.ListarProdutos();
            ddlProduto.DataTextField = "Nome";
            ddlProduto.DataValueField = "Id";
            ddlProduto.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string precoTexto = txtPreco.Text
                    .Replace("R$", "")
                    .Trim();

                if (!decimal.TryParse(precoTexto, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal preco))
                {
                    MostrarToast("Valor inválido! Use o formato 0,00.", "danger");
                    return;
                }

                var cotacao = new Cotacao
                {
                    Data = DateTime.Now,
                    FornecedorId = int.Parse(ddlFornecedor.SelectedValue),
                    ProdutoId = int.Parse(ddlProduto.SelectedValue),
                    Preco = preco
                };

                _cotacaoBll.SalvarCotacao(cotacao);
                txtPreco.Text = "";
                CarregarGrid();

                MostrarToast("Cotação salva com sucesso!", "success");
            }
            catch (Exception ex)
            {
                var logDal = new LogDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
                logDal.RegistrarLog("btnSalvar_Click - Cotacoes", ex);
                MostrarToast(ex.Message, "danger");
            }
        }

        private void HandlePostBackEvent()
        {
            string eventTarget = (Request["__EVENTTARGET"] ?? "").TrimEnd(',');
            string eventArgument = (Request["__EVENTARGUMENT"] ?? "").TrimEnd(',');

            if (eventTarget == "ExcluirCotacao" && int.TryParse(eventArgument, out int idCotacao))
            {
                try
                {
                    _cotacaoBll.ExcluirCotacao(idCotacao);
                    CarregarGrid();
                    MostrarToast("Cotação excluída com sucesso ✅", "success");
                }
                catch (Exception ex)
                {
                    var logDal = new LogDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
                    logDal.RegistrarLog("HandlePostBackEvent - Cotacoes", ex);
                    MostrarToast(ex.Message, "danger");
                }
            }
        }

        protected void btnConsultarMenorPreco_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = _cotacaoBll.ConsultarMenorPrecoPorProduto();
                gvMenorPreco.DataSource = lista;
                gvMenorPreco.DataBind();

                if (lista.Count == 0)
                    MostrarToast("Nenhuma cotação encontrada para exibir menor preço.", "warning");
                else
                    MostrarToast("Consulta realizada com sucesso!", "success");
            }
            catch (Exception ex)
            {
                var logDal = new LogDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
                logDal.RegistrarLog("btnConsultarMenorPreco_Click - Cotacoes", ex);
                MostrarToast(ex.Message, "danger");
            }
        }

        private void CarregarGrid()
        {
            gvCotacoes.DataSource = _cotacaoBll.ListarCotacoes();
            gvCotacoes.DataBind();
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
