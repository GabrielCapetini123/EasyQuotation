<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cotacoes.aspx.cs" Inherits="EasyQuotation.Pages.Cotacoes" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Registro de Cotações</title>
    <meta charset="utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <style>
        body {
            background: linear-gradient(135deg, #fffef9, #f8f9fa);
            font-family: 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;
            color: #2d3436;
        }

        .btn-voltar {
            position: fixed;
            top: 20px;
            left: 20px;
            border-radius: 8px;
            font-weight: 600;
            padding: 10px 20px;
            background: #ffffff;
            border: 1px solid #dee2e6;
            color: #e0a800;
            text-decoration: none;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
            transition: all 0.2s ease-in-out;
            z-index: 1050;
        }

        .btn-voltar:hover {
            background-color: #ffc107;
            color: #fff;
            transform: translateY(-1px);
            box-shadow: 0 4px 12px rgba(255, 193, 7, 0.3);
        }

        .card {
            border-radius: 14px;
            border: none;
            box-shadow: 0 4px 14px rgba(0, 0, 0, 0.08);
        }

        h2 {
            font-weight: 700;
            color: #e0a800;
        }

        .subtitulo {
            color: #6c757d;
            font-size: 0.95rem;
        }

        label {
            font-weight: 600;
            color: #495057;
        }

        .form-control {
            border-radius: 8px;
            border: 1px solid #ced4da;
        }

        .form-control:focus {
            border-color: #ffc107;
            box-shadow: 0 0 0 0.2rem rgba(255, 193, 7, 0.2);
        }

        .btn-primary {
            background-color: #ffc107;
            border: none;
            border-radius: 8px;
            font-weight: 600;
            color: #212529;
            box-shadow: 0 3px 8px rgba(255, 193, 7, 0.25);
            transition: all 0.25s ease-in-out;
        }

        .btn-primary:hover {
            background-color: #e0a800;
            color: #fff;
            transform: translateY(-1px);
            box-shadow: 0 5px 15px rgba(224, 168, 0, 0.35);
        }

        .btn-outline-success {
            border-radius: 8px;
            font-weight: 600;
            border-color: #ffc107;
            color: #ffc107;
        }

        .btn-outline-success:hover {
            background-color: #ffc107;
            color: #212529;
        }

        .btn-outline-primary {
            border-radius: 8px;
            font-weight: 600;
            border-color: #ffc107;
            color: #ffc107;
        }

        .btn-outline-primary:hover {
            background-color: #ffc107;
            color: #212529;
        }

        .grid-container {
            background: #ffffff;
            border-radius: 12px;
            padding: 25px;
            box-shadow: 0 4px 16px rgba(0, 0, 0, 0.05);
        }

        .grid-container h4 {
            font-weight: 700;
            color: #e0a800;
        }

        .gridview {
            width: 100%;
            border-collapse: collapse;
            overflow: hidden;
            border-radius: 8px;
        }

        .gridview th {
            background: #ffc107;
            color: #212529;
            font-weight: 600;
            padding: 12px;
            text-align: center;
            border: none;
        }

        .gridview td {
            border-top: 1px solid #e9ecef;
            text-align: center;
            padding: 10px;
        }

        .gridview tr:nth-child(even) {
            background: #fffdf5;
        }

        .gridview tr:hover {
            background: #fff8e1;
        }

        .btn-trash {
            background: none;
            border: none;
            font-size: 1.3rem;
            color: #dc3545;
            cursor: pointer;
            transition: all 0.2s ease-in-out;
        }

        .btn-trash:hover {
            color: #b02a37;
            transform: scale(1.1);
        }

        .card-relatorio {
            border-radius: 12px;
            border: none;
            background: #ffffff;
            box-shadow: 0 3px 12px rgba(0, 0, 0, 0.05);
            padding: 25px;
        }

        .card-relatorio h5 {
            color: #e0a800;
            font-weight: 700;
        }

        .toast-container {
            position: fixed;
            top: 20px;
            right: 20px;
            z-index: 1060;
        }

        hr {
            border: none;
            height: 1px;
            background: linear-gradient(to right, transparent, #ffe082, transparent);
            margin: 3rem 0;
        }

        .modal-header {
            background-color: #fff8e1;
        }

        .modal-title {
            color: #e0a800;
        }

        .fade-in {
            animation: fadeIn 0.8s ease-in-out;
        }

        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(10px); }
            to { opacity: 1; transform: translateY(0); }
        }
    </style>
</head>

<body>
<a href="~/Pages/Default.aspx" runat="server" class="btn-voltar">⬅️ Voltar</a>

    <form id="form1" runat="server" class="container my-5 fade-in">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div class="card p-4 mx-auto mb-5" style="max-width: 650px;">
            <div class="card-body text-center">
                <h2>🧾 Registro de Cotações</h2>
                <p class="subtitulo mb-4">Preencha os dados abaixo para cadastrar uma nova cotação.</p>

                <div class="text-start">
                    <label for="ddlFornecedor">Fornecedor</label>
                    <asp:DropDownList ID="ddlFornecedor" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="text-start mt-3">
                    <label for="ddlProduto">Produto</label>
                    <asp:DropDownList ID="ddlProduto" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="text-start mt-3">
                    <label for="txtPreco">Valor (R$)</label>
                    <asp:TextBox ID="txtPreco" runat="server" CssClass="form-control" placeholder="Ex: R$ 250,00"></asp:TextBox>
                </div>

                <asp:Button ID="btnSalvar" runat="server" Text="💾 Salvar Cotação"
                    CssClass="btn btn-primary w-100 mt-4"
                    OnClientClick="return validarFormulario();"
                    OnClick="btnSalvar_Click" />
            </div>
        </div>

        <hr />

        <div class="grid-container mt-4">
            <div class="d-flex justify-content-between align-items-center mb-3 flex-wrap gap-2">
                <h4 class="mb-0">📋 Cotações Registradas</h4>
                <asp:Button ID="btnConsultarMenorPreco" runat="server"
                    Text="🔍 Consultar Menor Preço"
                    CssClass="btn btn-outline-success"
                    OnClick="btnConsultarMenorPreco_Click" />
            </div>

            <asp:GridView ID="gvCotacoes" runat="server" AutoGenerateColumns="False" CssClass="gridview mb-4">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="FornecedorNome" HeaderText="Fornecedor" />
                    <asp:BoundField DataField="ProdutoNome" HeaderText="Produto" />
                    <asp:BoundField DataField="Preco" HeaderText="Valor (R$)" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <button type="button" class="btn-trash" onclick='abrirModalExclusao(<%# Eval("Id") %>)' title="Excluir">🗑️</button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="card-relatorio mt-4">
                <asp:Panel ID="pnlMenorPrecoHeader" runat="server" Visible="false">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h5>💰 Menor Preço por Produto</h5>
                        <asp:Button ID="btnExportarExcel" runat="server"
                            Text="📊 Exportar Excel"
                            CssClass="btn btn-outline-primary"
                            Visible="false"
                            OnClick="btnExportarExcel_Click" />
                    </div>
                </asp:Panel>

                <asp:GridView ID="gvMenorPreco" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                    <Columns>
                        <asp:BoundField DataField="ProdutoNome" HeaderText="Produto" />
                        <asp:BoundField DataField="FornecedorNome" HeaderText="Fornecedor" />
                        <asp:BoundField DataField="Preco" HeaderText="Menor Valor (R$)" DataFormatString="{0:C}" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="modal fade" id="modalConfirmarExclusao" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow-lg">
                    <div class="modal-header">
                        <h5 class="modal-title fw-semibold">Confirmar exclusão</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body text-center">
                        <p class="mb-3 fs-5 text-muted">🗑️ Deseja realmente excluir esta cotação?</p>
                    </div>
                    <div class="modal-footer border-0">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnConfirmarExclusao" runat="server" Text="Excluir" CssClass="btn btn-danger px-4" />
                    </div>
                </div>
            </div>
        </div>

        <input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" />
        <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" />
    </form>

    <div class="toast-container"></div>

    <script>
        let cotacaoExcluirId = null;

        function abrirModalExclusao(id) {
            cotacaoExcluirId = id;
            const modal = new bootstrap.Modal(document.getElementById('modalConfirmarExclusao'));
            modal.show();
        }

        window.onload = function () {
            const btnExcluir = document.getElementById('<%= btnConfirmarExclusao.ClientID %>');
            if (btnExcluir) {
                btnExcluir.addEventListener('click', function () {
                    document.getElementById("__EVENTTARGET").value = "ExcluirCotacao";
                    document.getElementById("__EVENTARGUMENT").value = cotacaoExcluirId;
                    __doPostBack("__EVENTTARGET", "__EVENTARGUMENT");
                });
            }
        };

        function validarFormulario() {
            const fornecedor = document.getElementById('<%= ddlFornecedor.ClientID %>').value;
            const produto = document.getElementById('<%= ddlProduto.ClientID %>').value;
            const preco = document.getElementById('<%= txtPreco.ClientID %>').value.trim();

            if (!fornecedor || !produto || !preco) {
                exibirToast("Preencha todos os campos obrigatórios.", "danger");
                return false;
            }

            const valorNumerico = preco.replace(/[R$\s.]/g, '').replace(',', '.');
            if (isNaN(valorNumerico) || parseFloat(valorNumerico) <= 0) {
                exibirToast("Digite um valor válido.", "danger");
                return false;
            }

            return true;
        }

        document.addEventListener("DOMContentLoaded", () => {
            const precoInput = document.getElementById('<%= txtPreco.ClientID %>');
            precoInput.addEventListener('input', e => {
                let valor = e.target.value.replace(/\D/g, '');
                valor = (valor / 100).toFixed(2) + '';
                valor = valor.replace(".", ",");
                valor = valor.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
                e.target.value = 'R$ ' + valor;
            });
        });

        function exibirToast(mensagem, tipo = "success") {
            const toastContainer = document.querySelector('.toast-container');
            const toast = document.createElement('div');
            toast.className = `toast align-items-center text-bg-${tipo} border-0`;
            toast.innerHTML = `
                <div class="d-flex">
                    <div class="toast-body fw-semibold">${mensagem}</div>
                    <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
                </div>`;
            toastContainer.appendChild(toast);
            const bsToast = new bootstrap.Toast(toast);
            bsToast.show();
            setTimeout(() => toast.remove(), 4000);
        }
    </script>
</body>
</html>
