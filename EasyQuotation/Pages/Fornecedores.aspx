<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fornecedores.aspx.cs" Inherits="EasyQuotation.Pages.Fornecedores" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Cadastro de Fornecedores</title>
    <meta charset="utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <style>
        body {
            background: linear-gradient(135deg, #f8f9fa, #e9ecef);
            font-family: 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;
            color: #2d3436;
        }

        .btn-voltar {
            position: fixed;
            top: 20px;
            left: 20px;
            z-index: 1050;
            border-radius: 10px;
            font-weight: 600;
            padding: 10px 18px;
            background: #ffffff;
            border: 1px solid #dee2e6;
            color: #0d6efd;
            text-decoration: none;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
            transition: all 0.25s ease-in-out;
        }

        .btn-voltar:hover {
            background-color: #0d6efd;
            color: #fff;
            transform: translateY(-2px);
            box-shadow: 0 4px 10px rgba(13, 110, 253, 0.25);
        }

        .card {
            border: none;
            border-radius: 14px;
            box-shadow: 0 5px 20px rgba(0, 0, 0, 0.08);
            background: #fff;
            transition: all 0.3s ease-in-out;
        }

        .card:hover {
            transform: translateY(-2px);
        }

        h2 {
            font-weight: 700;
            color: #0d6efd;
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
            border-color: #0d6efd;
            box-shadow: 0 0 0 0.2rem rgba(13, 110, 253, 0.15);
        }

        /* ===== Botão Primário ===== */
        .btn-primary {
            background-color: #0d6efd;
            border: none;
            border-radius: 8px;
            font-weight: 600;
            box-shadow: 0 3px 10px rgba(13, 110, 253, 0.25);
            transition: all 0.25s ease-in-out;
        }

        .btn-primary:hover {
            background-color: #0b5ed7;
            transform: translateY(-1px);
            box-shadow: 0 5px 15px rgba(13, 110, 253, 0.35);
        }

        .grid-container {
            background: #ffffff;
            border-radius: 12px;
            padding: 25px 30px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.05);
        }

        .grid-container h4 {
            font-weight: 700;
            color: #0d6efd;
        }

        .gridview {
            border-collapse: collapse;
            width: 100%;
            border-radius: 8px;
            overflow: hidden;
        }

        .gridview th {
            background-color: #0d6efd;
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
            background-color: #eef3ff;
        }

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

        .toast-container {
            position: fixed;
            top: 20px;
            right: 20px;
            z-index: 1060;
        }

        .modal-content {
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
        }

        .modal-header {
            background-color: #e7f0ff;
            border-bottom: none;
        }

        .modal-title {
            color: #0d6efd;
        }

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
            background: linear-gradient(to right, transparent, #bcd3ff, transparent);
            margin: 3rem 0;
        }
    </style>
</head>

<body>
<a href="~/Pages/Default.aspx" runat="server" class="btn-voltar">⬅️ Voltar</a>
    <form id="form1" runat="server" class="container my-5 fade-in">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div class="card p-4 mx-auto mb-5" style="max-width: 600px;">
            <div class="card-body text-center">
                <h2>🏢 Cadastro de Fornecedores</h2>
                <p class="text-muted mb-4">Cadastre fornecedores e mantenha suas informações organizadas.</p>

                <div class="text-start mb-3">
                    <label for="txtNome">Nome do Fornecedor</label>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Digite o nome do fornecedor"></asp:TextBox>
                </div>

                <div class="text-start mb-3">
                    <label for="txtCNPJ">CNPJ</label>
                    <asp:TextBox ID="txtCNPJ" runat="server" CssClass="form-control" placeholder="00.000.000/0000-00"></asp:TextBox>
                </div>

                <asp:Button ID="btnSalvar" runat="server" Text="💾 Salvar Fornecedor"
                    CssClass="btn btn-primary w-100 mt-3"
                    OnClientClick="return validarFormulario();"
                    OnClick="btnSalvar_Click" />
            </div>
        </div>

        <hr />

        <div class="grid-container mt-4">
            <h4 class="mb-3">📋 Fornecedores Cadastrados</h4>

            <asp:GridView ID="gvFornecedores" runat="server" AutoGenerateColumns="False" CssClass="gridview mb-3">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Nome" HeaderText="Nome" />
                    <asp:BoundField DataField="CNPJ" HeaderText="CNPJ" />
                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <button type="button" class="btn-trash" title="Excluir fornecedor" onclick='abrirModalExclusao(<%# Eval("Id") %>)'>🗑️</button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="modal fade" id="modalConfirmarExclusao" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title fw-semibold">Confirmar exclusão</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body text-center">
                        <p class="mb-3 fs-5 text-muted">Tem certeza que deseja excluir este fornecedor?</p>
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
        let fornecedorExcluirId = null;

        function abrirModalExclusao(id) {
            fornecedorExcluirId = id;
            const modal = new bootstrap.Modal(document.getElementById('modalConfirmarExclusao'));
            modal.show();
        }

        window.onload = function () {
            const btnExcluir = document.getElementById('<%= btnConfirmarExclusao.ClientID %>');
            if (btnExcluir) {
                btnExcluir.addEventListener('click', function () {
                    document.getElementById("__EVENTTARGET").value = "ExcluirFornecedor";
                    document.getElementById("__EVENTARGUMENT").value = fornecedorExcluirId;
                    __doPostBack("__EVENTTARGET", "__EVENTARGUMENT");
                });
            }
        };

        document.addEventListener("DOMContentLoaded", function () {
            const cnpjInput = document.getElementById('<%= txtCNPJ.ClientID %>');
            cnpjInput.addEventListener('input', function (e) {
                let value = e.target.value.replace(/\D/g, '');
                if (value.length <= 14) {
                    value = value.replace(/^(\d{2})(\d)/, "$1.$2");
                    value = value.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3");
                    value = value.replace(/\.(\d{3})(\d)/, ".$1/$2");
                    value = value.replace(/(\d{4})(\d)/, "$1-$2");
                    e.target.value = value;
                }
            });
        });

        function validarFormulario() {
            const nome = document.getElementById('<%= txtNome.ClientID %>').value.trim();
            const cnpj = document.getElementById('<%= txtCNPJ.ClientID %>').value.trim();
            const regexCNPJ = /^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$/;

            if (!nome || !cnpj) {
                exibirToast("Preencha todos os campos antes de salvar.", "danger");
                return false;
            }

            if (!regexCNPJ.test(cnpj)) {
                exibirToast("CNPJ inválido! Use o formato 00.000.000/0000-00.", "danger");
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
