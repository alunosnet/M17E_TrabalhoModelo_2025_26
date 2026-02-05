<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Consultas.aspx.cs" Inherits="M17AB_TrabalhoModelo_2022_23.Admin.Consultas.Consultas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Consultas</h2>
    <asp:DropDownList ID="ddConsultas" CssClass="form-control" AutoPostBack="true" 
        OnSelectedIndexChanged="ddConsultas_SelectedIndexChanged" runat="server">
        <asp:ListItem Value="0">Top de livros mais requisitados</asp:ListItem>
        <asp:ListItem Value="1">Top de leitores</asp:ListItem>
        <asp:ListItem Value="2">Top de livros mais requisitados do último mês</asp:ListItem>
        <asp:ListItem Value="3">Lista de utilizadores com livros fora de prazo</asp:ListItem>
        <asp:ListItem Value="4">Livros da última semana - novidades</asp:ListItem>
        <asp:ListItem Value="5">Tempo médio de empréstimo</asp:ListItem>
        <asp:ListItem Value="6">Nº de livros por autor</asp:ListItem>
        <asp:ListItem Value="7">Nº de utilizadores bloqueados</asp:ListItem>
        <asp:ListItem Value="8">Nº de tipos de livro por utilizador</asp:ListItem>
        <asp:ListItem Value="9">Nº de empréstimos por mês</asp:ListItem>
        <asp:ListItem Value="10">Lista dos utilizadores que requisitaram o livro mais caro</asp:ListItem>
        <asp:ListItem Value="11">Lista dos livros cujo preço é superior à média</asp:ListItem>
        <asp:ListItem Value="12">Lista dos utilizadores com a mesma password</asp:ListItem>
    </asp:DropDownList>
    <asp:GridView CssClass="table" ID="gvConsultas" runat="server"></asp:GridView>
</asp:Content>
