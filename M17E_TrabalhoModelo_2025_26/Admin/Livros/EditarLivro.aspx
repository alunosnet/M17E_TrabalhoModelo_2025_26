<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditarLivro.aspx.cs" Inherits="M17AB_TrabalhoModelo_2022_23.Admin.Livros.EditarLivro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Editar livro</h1>
    <div class="from-group">
        <label for="ContentPlaceHolder1_tbNome">Nome:</label>
        <asp:TextBox CssClass="form-control" ID="tbNome" runat="server" MaxLength="100" Required placeholder="Nome do livro" /><br />
    </div>
    <div class="from-group">
        <label for="ContentPlaceHolder1_tbAno">Ano:</label>
        <asp:TextBox CssClass="form-control" ID="tbAno" runat="server" TextMode="Number" /><br />
    </div>
    <div class="from-group">
        <label for="ContentPlaceHolder1_tbData">Data de Aquisição:</label>
        <asp:TextBox CssClass="form-control" ID="tbData" runat="server" TextMode="Date" /><br />
    </div>
    <div class="from-group">
        <label for="ContentPlaceHolder1_tbPreco">Preço:</label>
        <asp:TextBox ID="tbPreco" CssClass="form-control" runat="server"  /><br />
    </div>
    <div class="from-group">
        <label for="ContentPlaceHolder1_tbAutor">Autor:</label>
        <asp:TextBox ID="tbAutor" CssClass="form-control" runat="server" placeholder="Nome do autor" /><br />
    </div>
    <div class="from-group">
        <label for="ContentPlaceHolder1_dpTipo">Tipo:</label>
        <asp:DropDownList CssClass="form-select" ID="dpTipo" runat="server">
            <asp:ListItem Text="Banda desenhada" Value="bd" />
            <asp:ListItem Text="Mistério" Value="mistério" />
            <asp:ListItem Text="Romance" Value="romance" />
         </asp:DropDownList>
    </div>
    <asp:Image CssClass="figure-img" runat="server" ID="imgCapa" />
    <div class="form-group">
        <label for="ContentPlaceHolder1_fuCapa">Capa:</label>
        <asp:FileUpload ID="fuCapa" runat="server" CssClass="form-control" />
    </div> 
    <br />
    <asp:Button runat="server" ID="btAtualizar" CssClass="btn btn-lg btn-success" Text="Atualizar" OnClick="btAtualizar_Click" />
    <asp:Button runat="server" ID="btVoltar" CssClass="btn btn-lg btn-info" Text="Voltar" PostBackUrl="~/Admin/Livros/Livros.aspx" />
    <br />
    <asp:Label ID="lbErro" runat="server"></asp:Label>
</asp:Content>
