using EasyQuotation.DAL;
using System;
using System.Configuration;

namespace EasyQuotation.Pages
{
    public partial class RelatorioMenorPreco : System.Web.UI.Page
    {
        private CotacaoDAL _cotacaoDal;

        protected void Page_Load(object sender, EventArgs e)
        {
            _cotacaoDal = new CotacaoDAL(ConfigurationManager.ConnectionStrings["EasyQuotationDB"].ConnectionString);
        }

        protected void btnCarregar_Click(object sender, EventArgs e)
        {
            var resultado = _cotacaoDal.ConsultarMenorPrecoPorProduto();
            gvMenorPreco.DataSource = resultado;
            gvMenorPreco.DataBind();
        }
    }
}