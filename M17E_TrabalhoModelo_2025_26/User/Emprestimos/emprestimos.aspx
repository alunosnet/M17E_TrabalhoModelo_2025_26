<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="emprestimos.aspx.cs" Inherits="M17AB_TrabalhoModelo_2022_23.User.Emprestimos.emprestimos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Reservar livro</h2>
    <asp:GridView CssClass="table" EmptyDataText="Não existem livros disponíveis para emprestar" runat="server" ID="gvlivros"></asp:GridView>
</asp:Content>
