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

            var cotacaoDal = new CotacaoDAL(connectionString);
            var fornecedorDal = new FornecedorDAL(connectionString);
            var produtoDal = new ProdutoDAL(connectionString);

            _cotacaoBll = new CotacaoBLL(cotacaoDal);
            _fornecedorBll = new FornecedorBLL(fornecedorDal);
            _produtoBll = new ProdutoBLL(produtoDal);

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

                if (lista == null || lista.Count == 0)
                {
                    pnlMenorPrecoHeader.Visible = false;
                    btnExportarExcel.Visible = false;
                    MostrarToast("Nenhuma cotação encontrada para exibir menor preço.", "warning");
                }
                else
                {
                    pnlMenorPrecoHeader.Visible = true;
                    btnExportarExcel.Visible = true;
                    MostrarToast("Consulta realizada com sucesso!", "success");
                }
            }
            catch (Exception ex)
            {
                pnlMenorPrecoHeader.Visible = false;
                btnExportarExcel.Visible = false;

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

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = _cotacaoBll.ConsultarMenorPrecoPorProduto();

                if (lista == null || lista.Count == 0)
                {
                    MostrarToast("Não há dados para exportar.", "warning");
                    return;
                }

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=MenorPrecoPorProduto.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.UTF8;

                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                hw.Write("<table border='1' style='border-collapse:collapse;'>");
                hw.Write("<tr style='background-color:#0d6efd;color:white;font-weight:bold;'>");
                hw.Write("<th>Produto</th>");
                hw.Write("<th>Fornecedor</th>");
                hw.Write("<th>Menor Valor (R$)</th>");
                hw.Write("</tr>");

                foreach (var item in lista)
                {
                    hw.Write("<tr>");
                    hw.Write($"<td>{item.ProdutoNome}</td>");
                    hw.Write($"<td>{item.FornecedorNome}</td>");
                    hw.Write($"<td style='text-align:right;'>{item.Preco.ToString("C", new System.Globalization.CultureInfo("pt-BR"))}</td>");
                    hw.Write("</tr>");
                }

                hw.Write("</table>");

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                var logDal = new LogDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
                logDal.RegistrarLog("btnExportarExcel_Click - Cotacoes", ex);
                MostrarToast("Erro ao exportar para Excel: " + ex.Message, "danger");
            }
        }

    }
}
