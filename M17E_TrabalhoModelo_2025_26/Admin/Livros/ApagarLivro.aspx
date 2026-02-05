<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ApagarLivro.aspx.cs" Inherits="M17AB_TrabalhoModelo_2022_23.Admin.Livros.ApagarLivro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Confirmar apagar livro</h1>
    Nº Livro:<asp:Label  runat="server" ID="lbNlivro" CssClass="form-control"></asp:Label>
    <br />Nome:<asp:Label runat="server" ID="lbNome" CssClass="form-control"></asp:Label>
    <br />Capa<asp:Image CssClass="figure-img" runat="server" ID="imgCapa" />
    <br />
    <asp:Button CssClass="btn btn-lg btn-danger" runat="server" ID="btRemover" Text="Remover" OnClick="btRemover_Click" />
    <asp:Button CssClass="btn btn-lg btn-info" runat="server" ID="btVoltar" Text="Voltar" OnClick="btVoltar_Click" />
    <br /><asp:Label runat="server" ID="lbErro"></asp:Label>

</asp:Content>
