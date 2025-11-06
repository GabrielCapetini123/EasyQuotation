<%@ Page Title="Sistema de Cotações de Produtos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EasyQuotation._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container text-center mt-5">
        <h1 class="display-4 mb-4 fw-bold">🧾 Sistema de Cotações de Produtos</h1>
        <p class="lead text-muted mb-5">
            Gerencie fornecedores, produtos e registre cotações de forma prática e organizada.
        </p>

        <!-- Abas de navegação -->
        <ul class="nav nav-tabs justify-content-center" id="quotationTabs" role="tablist">
            <li class="nav-item" role="presentation">
                <a class="nav-link active" id="fornecedor-tab" data-bs-toggle="tab" href="#fornecedor" role="tab" aria-controls="fornecedor" aria-selected="true">
                    🏢 Fornecedores
                </a>
            </li>
            <li class="nav-item" role="presentation">
                <a class="nav-link" id="produto-tab" data-bs-toggle="tab" href="#produto" role="tab" aria-controls="produto" aria-selected="false">
                    📦 Produtos
                </a>
            </li>
            <li class="nav-item" role="presentation">
                <a class="nav-link" id="cotacao-tab" data-bs-toggle="tab" href="#cotacao" role="tab" aria-controls="cotacao" aria-selected="false">
                    💰 Cotações
                </a>
            </li>
        </ul>
        <div class="tab-content mt-4" id="quotationTabsContent">
            <div class="tab-pane fade show active" id="fornecedor" role="tabpanel" aria-labelledby="fornecedor-tab">
                <h4 class="fw-semibold mb-3">Gerencie seus fornecedores</h4>
                <p>Cadastre novos fornecedores e visualize os já existentes.</p>
                <a href='<%= ResolveUrl("~/Pages/Fornecedores.aspx") %>' class="btn btn-primary mt-2">Ir para Fornecedores</a>
            </div>
            <div class="tab-pane fade" id="produto" role="tabpanel" aria-labelledby="produto-tab">
                <h4 class="fw-semibold mb-3">Controle seus produtos</h4>
                <p>Cadastre e mantenha a lista de produtos atualizada.</p>
                <a href='<%= ResolveUrl("~/Pages/Produtos.aspx") %>' class="btn btn-primary mt-2">Ir para Produtos</a>
            </div>
            <div class="tab-pane fade" id="cotacao" role="tabpanel" aria-labelledby="cotacao-tab">
                <h4 class="fw-semibold mb-3">Registre e consulte cotações</h4>
                <p>Insira novas cotações e visualize o menor preço por produto.</p>
                <a href='<%= ResolveUrl("~/Pages/Cotacoes.aspx") %>' class="btn btn-primary mt-2">Ir para Cotações</a>
            </div>

        </div>
    </div>

</asp:Content>
