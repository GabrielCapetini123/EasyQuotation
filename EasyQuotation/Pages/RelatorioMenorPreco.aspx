<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioMenorPreco.aspx.cs" Inherits="EasyQuotation.Pages.RelatorioMenorPreco" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Relatório de Menor Preço</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Menor Preço por Produto</h2>

        <asp:Button ID="btnCarregar" runat="server" Text="Gerar Relatório" OnClick="btnCarregar_Click" /><br /><br />

        <asp:GridView ID="gvMenorPreco" runat="server" AutoGenerateColumns="false" CssClass="table">
            <Columns>
                <asp:BoundField DataField="Produto" HeaderText="Produto" />
                <asp:BoundField DataField="MenorPreco" HeaderText="Menor Preço (R$)" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
