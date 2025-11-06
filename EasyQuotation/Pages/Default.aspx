<%@ Page Title="Sistema de Cotações de Produtos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EasyQuotation.Pages._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <div class="container text-center mt-5 fade-in">
        <div class="mb-5">
            <h1 class="display-5 fw-bold text-primary">🧾 EasyQuotation</h1>
            <p class="lead text-muted mt-3">
                Simplifique o gerenciamento de <strong>fornecedores</strong>, <strong>produtos</strong> e <strong>cotações</strong> — tudo em um só lugar.
            </p>
        </div>

        <div class="row justify-content-center g-4">
            <div class="col-md-4">
                <div class="card border-0 shadow-lg rounded-4 h-100 card-hover">
                    <div class="card-body p-4 d-flex flex-column align-items-center">
                        <div class="icon-circle bg-primary text-white mb-3">
                            🏢
                        </div>
                        <h4 class="fw-semibold mb-2 text-dark">Fornecedores</h4>
                        <p class="text-muted small mb-4">
                            Cadastre, visualize e gerencie seus fornecedores de forma simples.
                        </p>
                        <a href='<%= ResolveUrl("~/Pages/Fornecedores.aspx") %>' class="btn btn-outline-primary px-4 py-2 fw-semibold">
                            Acessar
                        </a>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="card border-0 shadow-lg rounded-4 h-100 card-hover">
                    <div class="card-body p-4 d-flex flex-column align-items-center">
                        <div class="icon-circle bg-success text-white mb-3">
                            📦
                        </div>
                        <h4 class="fw-semibold mb-2 text-dark">Produtos</h4>
                        <p class="text-muted small mb-4">
                            Mantenha o catálogo de produtos sempre atualizado e organizado.
                        </p>
                        <a href='<%= ResolveUrl("~/Pages/Produtos.aspx") %>' class="btn btn-outline-success px-4 py-2 fw-semibold">
                            Acessar
                        </a>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="card border-0 shadow-lg rounded-4 h-100 card-hover">
                    <div class="card-body p-4 d-flex flex-column align-items-center">
                        <div class="icon-circle bg-warning text-white mb-3">
                            💰
                        </div>
                        <h4 class="fw-semibold mb-2 text-dark">Cotações</h4>
                        <p class="text-muted small mb-4">
                            Registre preços, consulte o menor valor e otimize suas compras.
                        </p>
                        <a href='<%= ResolveUrl("~/Pages/Cotacoes.aspx") %>' class="btn btn-outline-warning px-4 py-2 fw-semibold text-dark">
                            Acessar
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div class="mt-5 text-muted small">
            <hr class="w-50 mx-auto mb-3" />
            <p>Desenvolvido por <strong>Gabriel Capetini</strong> — EasyQuotation © <%: DateTime.Now.Year %></p>
        </div>
    </div>

    <style>
        body {
            background-color: #f8f9fa;
            font-family: 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;
        }

        .fade-in {
            animation: fadeIn 0.9s ease-in-out;
        }

        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(10px); }
            to { opacity: 1; transform: translateY(0); }
        }

        .card-hover {
            transition: all 0.25s ease-in-out;
        }

        .card-hover:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.15);
        }

        .icon-circle {
            width: 70px;
            height: 70px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 2rem;
            box-shadow: 0 3px 10px rgba(0, 0, 0, 0.1);
        }

        .btn {
            border-radius: 10px;
            transition: all 0.2s ease-in-out;
        }

        .btn:hover {
            transform: scale(1.05);
        }

        .display-5 {
            letter-spacing: 0.5px;
        }

        hr {
            opacity: 0.2;
        }
    </style>
</asp:Content>
