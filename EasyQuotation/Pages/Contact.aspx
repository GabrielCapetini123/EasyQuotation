<%@ Page Title="Contato" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="EasyQuotation.Pages.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <a href="Default.aspx" class="btn-voltar">
        ⬅️ Voltar
    </a>

    <main class="container my-5" aria-labelledby="title">
        <div class="card shadow-lg border-0 rounded-4">
            <div class="card-body p-5">
                <div class="text-center mb-5">
                    <h1 class="fw-bold text-primary mb-2">Entre em Contato</h1>
                    <p class="text-muted fs-5">Fique à vontade para me contatar através dos canais abaixo 👇</p>
                    <hr class="w-50 mx-auto mt-4" />
                </div>

                <div class="row justify-content-center text-center gy-4">
                    <div class="col-md-5">
                        <div class="p-4 border rounded-4 shadow-sm bg-light h-100">
                            <h4 class="fw-semibold text-dark mb-3">📞 Telefone</h4>
                            <p class="fs-5 mb-0">
                                <a href="tel:+5521971781577" class="text-decoration-none text-primary fw-bold">
                                    (21) 97178-1577
                                </a>
                            </p>
                        </div>
                    </div>

                    <div class="col-md-5">
                        <div class="p-4 border rounded-4 shadow-sm bg-light h-100">
                            <h4 class="fw-semibold text-dark mb-3">📧 E-mail</h4>
                            <p class="fs-5 mb-0">
                                <a href="mailto:gabrielcapetini18@outlook.com" class="text-decoration-none text-primary fw-bold">
                                    gabrielcapetini18@outlook.com
                                </a>
                            </p>
                        </div>
                    </div>

                    <div class="col-md-5">
                        <div class="p-4 border rounded-4 shadow-sm bg-light h-100">
                            <h4 class="fw-semibold text-dark mb-3">💼 LinkedIn</h4>
                            <p class="fs-5 mb-0">
                                <a href="https://linkedin.com/in/gabrielcapetini" target="_blank" class="text-decoration-none text-primary fw-bold">
                                    linkedin.com/in/gabrielcapetini
                                </a>
                            </p>
                        </div>
                    </div>

                    <div class="col-md-5">
                        <div class="p-4 border rounded-4 shadow-sm bg-light h-100">
                            <h4 class="fw-semibold text-dark mb-3">👨‍💻 GitHub</h4>
                            <p class="fs-5 mb-0">
                                <a href="https://github.com/GabrielCapetini123" target="_blank" class="text-decoration-none text-primary fw-bold">
                                    github.com/GabrielCapetini123
                                </a>
                            </p>
                        </div>
                    </div>
                </div>

                <div class="text-center mt-5">
                    <p class="text-muted mb-0 fs-6">
                        Desenvolvido por <strong>Gabriel Capetini</strong> — © <%: DateTime.Now.Year %> EasyQuotation
                    </p>
                </div>
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
            background-color: white;
            border-radius: 16px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }

        h1, h4 {
            color: #0d6efd;
        }

        .shadow-sm:hover {
            transform: scale(1.03);
            transition: all 0.2s ease-in-out;
        }

        a.text-primary:hover {
            color: #0a58ca;
        }

        .border {
            border-color: #dee2e6 !important;
        }
    </style>
</asp:Content>
