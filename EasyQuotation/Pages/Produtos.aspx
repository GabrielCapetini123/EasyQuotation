<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produtos.aspx.cs" Inherits="EasyQuotation.Pages.Produtos" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Cadastro de Produtos</title>
    <meta charset="utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <style>
        /* ===== Layout base ===== */
        body {
            background: linear-gradient(135deg, #f1f8f3, #f8f9fa);
            font-family: 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;
            color: #2d3436;
        }

        /* ===== Botão Voltar ===== */
        .btn-voltar {
            position: fixed;
            top: 20px;
            left: 20px;
            border-radius: 10px;
            font-weight: 600;
            padding: 10px 18px;
            background: #ffffff;
            border: 1px solid #d1d5db;
            color: #198754;
            text-decoration: none;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
            transition: all 0.25s ease-in-out;
            z-index: 1050;
        }

        .btn-voltar:hover {
            background-color: #198754;
            color: #fff;
            transform: translateY(-2px);
            box-shadow: 0 4px 10px rgba(25, 135, 84, 0.25);
        }

        /* ===== Card principal ===== */
        .card {
            border: none;
            border-radius: 14px;
            box-shadow: 0 5px 20px rgba(0, 0, 0, 0.08);
            transition: all 0.3s ease-in-out;
            background: #fff;
        }

        .card:hover {
            transform: translateY(-2px);
        }

        h2 {
            font-weight: 700;
            color: #198754;
        }

        label {
            font-weight: 600;
            color: #495057;
        }

        .form-control {
            border-radius: 8px;
            border: 1px solid #ced4da;
            transition: border-color 0.2s, box-shadow 0.2s;
        }

        .form-control:focus {
            border-color: #198754;
            box-shadow: 0 0 0 0.2rem rgba(25, 135, 84, 0.2);
        }

        /* ===== Botões ===== */
        .btn-primary {
            background-color: #198754;
            border: none;
            border-radius: 8px;
            font-weight: 600;
            box-shadow: 0 3px 10px rgba(25, 135, 84, 0.25);
            transition: all 0.25s ease-in-out;
        }

        .btn-primary:hover {
            background-color: #157347;
            transform: translateY(-1px);
            box-shadow: 0 5px 15px rgba(25, 135, 84, 0.35);
        }

        /* ===== Grid principal ===== */
        .grid-container {
            background: #ffffff;
            border-radius: 12px;
            padding: 25px 30px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.05);
        }

        .grid-container h4 {
            font-weight: 700;
            color: #198754;
        }

        .gridview {
            border-collapse: collapse;
            width: 100%;
            border-radius: 8px;
            overflow: hidden;
        }

        .gridview th {
            background-color: #198754;
            color: white;
            font-weight: 600;
            padding: 12px;
            text-align: center;
            border: none;
        }

        .gridview td {
            padding: 10px;
            border: none;
            border-bottom: 1px solid #e9ecef;
            text-align: center;
        }

        .gridview tr:nth-child(even) {
            background-color: #f8f9fa;
        }

        .gridview tr:hover {
            background-color: #edf7f0;
        }

        /* ===== Botão excluir ===== */
        .btn-trash {
            border: none;
            background: none;
            font-size: 1.3rem;
            color: #dc3545;
            transition: all 0.2s ease;
        }

        .btn-trash:hover {
            color: #b02a37;
            transform: scale(1.15);
        }

        /* ===== Toast ===== */
        .toast-container {
            position: fixed;
            top: 20px;
            right: 20px;
            z-index: 1060;
        }

        /* ===== Modal ===== */
        .modal-content {
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
        }

        .modal-header {
            background-color: #e9f7ef;
            border-bottom: none;
        }

        .modal-title {
            color: #198754;
        }

        /* ===== Animação de entrada ===== */
        .fade-in {
            animation: fadeIn 0.8s ease-in-out;
        }

        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(10px); }
            to { opacity: 1; transform: translateY(0); }
        }

        hr {
            border: none;
            height: 1px;
            background: linear-gradient(to right, transparent, #cde3d6, transparent);
            margin: 3rem 0;
        }
    </style>
</head>

<body>
    <a href="../Default.aspx" class="btn-voltar">⬅️ Voltar</a>

    <form id="form1" runat="server" class="container my-5 fade-in">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <!-- Card principal -->
        <div class="card p-4 mx-auto mb-5" style="max-width: 550px;">
            <div class="card-body text-center">
                <h2>📦 Cadastro de Produtos</h2>
                <p class="text-muted mb-4">Adicione novos produtos para suas cotações de forma rápida e organizada.</p>

                <div class="text-start">
                    <label for="txtNome">Nome do Produto</label>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Digite o nome do produto"></asp:TextBox>
                </div>

                <asp:Button ID="btnSalvar" runat="server" Text="💾 Salvar Produto"
                    CssClass="btn btn-primary w-100 mt-4"
                    OnClientClick="return validarFormulario();"
                    OnClick="btnSalvar_Click" />
            </div>
        </div>

        <hr />

        <!-- Lista de produtos -->
        <div class="grid-container mt-4">
            <h4 class="mb-3">📋 Produtos Cadastrados</h4>

            <asp:GridView ID="gvProdutos" runat="server" AutoGenerateColumns="False" CssClass="gridview mb-3">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Nome" HeaderText="Nome do Produto" />
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <button type="button" class="btn-trash" title="Excluir produto" onclick='abrirModalExclusao(<%# Eval("Id") %>)'>🗑️</button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <!-- Modal de exclusão -->
        <div class="modal fade" id="modalConfirmarExclusao" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title fw-semibold">Confirmar exclusão</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body text-center">
                        <p class="mb-3 fs-5 text-muted">Tem certeza que deseja excluir este produto?</p>
                    </div>
                    <div class="modal-footer border-0">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnConfirmarExclusao" runat="server" Text="Excluir" CssClass="btn btn-danger px-4" />
                    </div>
                </div>
            </div>
        </div>

        <input type="hidden" id="__EVENTTARGET" name="__EVENTTARGET" />
        <input type="hidden" id="__EVENTARGUMENT" name="__EVENTARGUMENT" />
    </form>

    <div class="toast-container"></div>

    <script>
        let produtoExcluirId = null;

        function abrirModalExclusao(id) {
            produtoExcluirId = id;
            const modal = new bootstrap.Modal(document.getElementById('modalConfirmarExclusao'));
            modal.show();
        }

        window.onload = function () {
            const btnExcluir = document.getElementById('<%= btnConfirmarExclusao.ClientID %>');
            if (btnExcluir) {
                btnExcluir.addEventListener('click', function () {
                    document.getElementById("__EVENTTARGET").value = "ExcluirProduto";
                    document.getElementById("__EVENTARGUMENT").value = produtoExcluirId;
                    __doPostBack("__EVENTTARGET", "__EVENTARGUMENT");
                });
            }
        };

        function validarFormulario() {
            const nome = document.getElementById('<%= txtNome.ClientID %>').value.trim();
            if (!nome) {
                exibirToast("Por favor, informe o nome do produto antes de salvar.", "danger");
                return false;
            }
            return true;
        }

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
