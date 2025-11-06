<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produtos.aspx.cs" Inherits="EasyQuotation.Pages.Produtos" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Cadastro de Produtos</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <style>
        body {
            background-color: #f8f9fa;
            font-family: 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;
        }

        .btn-voltar {
            position: fixed;
            top: 20px;
            left: 20px;
            z-index: 1050;
            border-radius: 12px;
            font-weight: 600;
            padding: 10px 20px;
            transition: all 0.2s ease-in-out;
            font-size: 1rem;
            text-decoration: none;
            background-color: white;
            border: 1px solid #6c757d;
            color: #343a40;
        }

        .btn-voltar:hover {
            background-color: #6c757d;
            color: white;
            transform: scale(1.05);
        }

        .card {
            border-radius: 16px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }

        h2 {
            font-weight: 700;
            color: #343a40;
            margin-bottom: 20px;
        }

        label {
            font-weight: 600;
            margin-top: 10px;
            color: #495057;
        }

        .form-control {
            border-radius: 8px;
            height: 42px;
            font-size: 1rem;
        }

        .btn-primary {
            border-radius: 10px;
            padding: 10px 25px;
            font-weight: 600;
        }

        .btn-trash {
            border: none;
            background: none;
            font-size: 1.2rem;
            color: #dc3545;
            transition: transform 0.2s ease, color 0.2s ease;
            cursor: pointer;
        }

        .btn-trash:hover {
            color: #b02a37;
            transform: scale(1.2);
        }

        .toast-container {
            position: fixed;
            top: 20px;
            right: 20px;
            z-index: 1055;
        }

        .grid-container {
            background: #fff;
            border-radius: 12px;
            padding: 20px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05);
        }

        .gridview {
            border-collapse: collapse;
            width: 100%;
        }

        .gridview th, .gridview td {
            padding: 10px;
            border: 1px solid #dee2e6;
        }

        .gridview th {
            background-color: #007bff;
            color: white;
        }

        .gridview tr:hover {
            background-color: #f1f1f1;
        }
    </style>
</head>
<body>
    <a href="../Default.aspx" class="btn-voltar">
        ⬅️ Voltar
    </a>

    <form id="form1" runat="server" class="container my-5">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div class="card p-4 mx-auto" style="max-width: 500px;">
            <div class="card-body text-center">
                <h2 class="mb-4">📦 Cadastro de Produtos</h2>

                <div class="mb-3 text-start">
                    <label for="txtNome" class="form-label">Nome do Produto:</label>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Digite o nome do produto"></asp:TextBox>
                </div>

                <asp:Button ID="btnSalvar" runat="server" Text="💾 Salvar Produto"
                    CssClass="btn btn-primary w-100 mt-3"
                    OnClientClick="return validarFormulario();"
                    OnClick="btnSalvar_Click" />
            </div>
        </div>

        <div class="grid-container mt-5">
            <h4>📋 Produtos Cadastrados</h4>
            <asp:GridView ID="gvProdutos" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Nome" HeaderText="Nome do Produto" />
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <button type="button" class="btn-trash" title="Excluir produto" onclick='abrirModalExclusao(<%# Eval("Id") %>)'>
                                🗑️
                            </button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="modal fade" id="modalConfirmarExclusao" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow-lg">
                    <div class="modal-header bg-warning-subtle border-0">
                        <h5 class="modal-title fw-semibold">Confirmar exclusão</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body text-center">
                        <p class="mb-3">Tem certeza que deseja excluir este produto?</p>
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

    <div class="toast-container position-fixed top-0 end-0 p-3"></div>

    <script>
        let produtoExcluirId = null;

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

        function abrirModalExclusao(id) {
            produtoExcluirId = id;
            const modal = new bootstrap.Modal(document.getElementById('modalConfirmarExclusao'));
            modal.show();
        }

        function exibirToast(mensagem, tipo = "success") {
            const toastContainer = document.querySelector('.toast-container');
            const toast = document.createElement('div');
            toast.classList.add('toast', 'align-items-center', `text-bg-${tipo}`, 'border-0');
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

        function validarFormulario() {
            const nome = document.getElementById('<%= txtNome.ClientID %>').value.trim();

            if (!nome) {
                exibirToast("Preencha o nome do produto antes de salvar.", "danger");
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
