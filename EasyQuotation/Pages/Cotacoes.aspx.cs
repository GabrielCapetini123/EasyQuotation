using EasyQuotation.DAL;
using EasyQuotation.Models;
using System;
using System.Configuration;
using System.Web.UI;

namespace EasyQuotation.Pages
{
    public partial class Cotacoes : System.Web.UI.Page
    {
        private CotacaoDAL _cotacaoDal;
        private FornecedorDAL _fornecedorDal;
        private ProdutoDAL _produtoDal;

        protected void Page_Load(object sender, EventArgs e)
        {
            _cotacaoDal = new CotacaoDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
            _fornecedorDal = new FornecedorDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
            _produtoDal = new ProdutoDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);

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
            ddlFornecedor.DataSource = _fornecedorDal.ListarFornecedores();
            ddlFornecedor.DataTextField = "Nome";
            ddlFornecedor.DataValueField = "Id";
            ddlFornecedor.DataBind();

            ddlProduto.DataSource = _produtoDal.ListarProdutos();
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

                if (!decimal.TryParse(precoTexto, System.Globalization.NumberStyles.Any,
                    new System.Globalization.CultureInfo("pt-BR"), out decimal preco))
                {
                    MostrarToast("Valor inválido! Verifique o formato (ex: 4,34).", "danger");
                    return;
                }

                var cotacao = new EasyQuotation.Models.Entities.Cotacao
                {
                    Data = DateTime.Now,
                    FornecedorId = int.Parse(ddlFornecedor.SelectedValue),
                    ProdutoId = int.Parse(ddlProduto.SelectedValue),
                    Preco = preco
                };

                _cotacaoDal.InserirCotacao(cotacao);
                txtPreco.Text = "";
                CarregarGrid();

                MostrarToast("Cotação salva com sucesso!", "success");
            }
            catch (Exception ex)
            {
                var logDal = new LogDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
                logDal.RegistrarLog("btnSalvar_Click - Cotacoes", ex);
                MostrarToast("Ocorreu um erro ao salvar a cotação. Tente novamente mais tarde.", "danger");
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
                    _cotacaoDal.ExcluirCotacao(idCotacao);
                    CarregarGrid();
                    MostrarToast("Cotação excluída com sucesso ✅", "success");
                }
                catch (Exception ex)
                {
                    var logDal = new LogDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
                    logDal.RegistrarLog("HandlePostBackEvent - Cotacoes", ex);
                    MostrarToast("Ocorreu um erro ao excluir a cotação. Tente novamente mais tarde.", "danger");
                }
            }
        }

        protected void btnConsultarMenorPreco_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = _cotacaoDal.ConsultarMenorPrecoPorProduto();
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
                MostrarToast("Erro ao consultar o menor preço. Tente novamente mais tarde.", "danger");
            }
        }


        private void CarregarGrid()
        {
            gvCotacoes.DataSource = _cotacaoDal.ListarCotacoes();
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
