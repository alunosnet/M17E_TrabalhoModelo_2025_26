<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="detalheslivro.aspx.cs" Inherits="M17AB_TrabalhoModelo_2022_23.detalheslivro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Image CssClass="img-fluid" ID="imgCapa" runat="server" /><br />
    <asp:Label ID="lbNome" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lbAutor" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lbAno" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lbPreco" runat="server" Text=""></asp:Label><br />
    <%if (Session["perfil"] != null && Session["perfil"].Equals("1")){ %>
    <asp:Button CssClass="btn btn-danger" ID="btReservar" runat="server" Text="Reservar" OnClick="btReservar_Click" />
    <asp:Label runat="server" ID="lbErro" />
    <% } %>
    <br /><a href="/index.aspx">Voltar</a>
</asp:Content>
