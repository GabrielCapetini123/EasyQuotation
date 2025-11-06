<%@ Page Title="Sobre o EasyQuotation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="EasyQuotation.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <a href="Default.aspx" class="btn-voltar">
        ⬅️ Voltar
    </a>

    <main class="container my-5" aria-labelledby="title">
        <div class="card shadow-lg border-0 rounded-4">
            <div class="card-body p-5">
                <div class="text-center mb-4">
                    <h1 class="fw-bold text-primary mb-3">EasyQuotation</h1>
                    <h4 class="text-muted">Sistema de Gestão de Cotações</h4>
                    <hr class="w-50 mx-auto my-4" />
                </div>

                <section class="mb-4">
                    <h3 class="text-dark fw-semibold mb-3">📘 Sobre o Sistema</h3>
                    <p class="lead text-justify">
                        O <strong>EasyQuotation</strong> é um sistema desenvolvido com o objetivo de facilitar o 
                        <strong>gerenciamento de cotações comerciais</strong>. 
                        Ele permite o <strong>cadastro de fornecedores</strong>, <strong>produtos</strong> 
                        e suas respectivas <strong>cotações</strong>, além de disponibilizar uma 
                        funcionalidade inteligente para a <strong>consulta do menor preço por produto</strong>.
                    </p>
                </section>

                <section class="mb-4">
                    <h3 class="text-dark fw-semibold mb-3">⚙️ Funcionalidades Principais</h3>
                    <ul class="list-group list-group-flush fs-6">
                        <li class="list-group-item">✅ Cadastro e gerenciamento de fornecedores</li>
                        <li class="list-group-item">✅ Cadastro de produtos com validações inteligentes</li>
                        <li class="list-group-item">✅ Registro de cotações com cálculo e exibição automática</li>
                        <li class="list-group-item">✅ Consulta do menor preço por produto</li>
                        <li class="list-group-item">✅ Interface intuitiva e responsiva para melhor usabilidade</li>
                    </ul>
                </section>

                <section class="mt-5">
                    <h3 class="text-dark fw-semibold mb-3">💻 Tecnologias Utilizadas</h3>
                    <div class="row text-center">
                        <div class="col-md-3 col-6 mb-3">
                            <div class="p-3 border rounded-4 shadow-sm bg-light">
                                <h5 class="fw-bold text-primary mb-1">.NET Framework</h5>
                                <small>Versão 4.7</small>
                            </div>
                        </div>
                        <div class="col-md-3 col-6 mb-3">
                            <div class="p-3 border rounded-4 shadow-sm bg-light">
                                <h5 class="fw-bold text-primary mb-1">ASP.NET Web Forms</h5>
                                <small>Interface dinâmica</small>
                            </div>
                        </div>
                        <div class="col-md-3 col-6 mb-3">
                            <div class="p-3 border rounded-4 shadow-sm bg-light">
                                <h5 class="fw-bold text-primary mb-1">LINQ</h5>
                                <small>Acesso e manipulação de dados</small>
                            </div>
                        </div>
                        <div class="col-md-3 col-6 mb-3">
                            <div class="p-3 border rounded-4 shadow-sm bg-light">
                                <h5 class="fw-bold text-primary mb-1">SQL Server 2022</h5>
                                <small>Banco de dados relacional</small>
                            </div>
                        </div>
                    </div>
                </section>

                <section class="mt-5 text-center">
                    <h3 class="text-dark fw-semibold mb-3">👨‍💻 Desenvolvedor</h3>
                    <p class="lead mb-1">
                        Este sistema foi idealizado e desenvolvido por <strong>Gabriel Capetini</strong>.
                    </p>
                    <p class="text-muted">
                        Profissional de tecnologia com foco em desenvolvimento de soluções web modernas, 
                        seguras e escaláveis.
                    </p>
                </section>
            </div>
        </div>
    </main>

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
            background: white;
            border-radius: 16px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }

        .text-justify {
            text-align: justify;
        }

        .list-group-item {
            border: none;
            padding: 10px 0;
            font-size: 1.05rem;
        }

        .list-group-item::before {
            content: "• ";
            color: #007bff;
            font-weight: bold;
        }

        .shadow-sm:hover {
            transform: scale(1.03);
            transition: all 0.2s ease-in-out;
        }
    </style>
</asp:Content>
