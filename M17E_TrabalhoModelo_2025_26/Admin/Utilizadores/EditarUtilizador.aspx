<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditarUtilizador.aspx.cs" Inherits="M17AB_TrabalhoModelo_2022_23.Admin.Utilizadores.EditarUtilizador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Editar Utilizador</h1>
    <br />Nome:<asp:TextBox CssClass="form-control" runat="server" ID="tbNome"></asp:TextBox>
    <br />Morada:<asp:TextBox CssClass="form-control" runat="server" ID="tbMorada"></asp:TextBox>
    <br />Nif:<asp:TextBox CssClass="form-control" runat="server" ID="tbNif"></asp:TextBox>
    <br /><asp:Button CssClass="btn btn-lg btn-danger" OnClick="btEditar_Click" runat="server" ID="btEditar" Text="Editar" />
    <asp:Button CssClass="btn btn-lg btn-info" 
        runat="server" 
        ID="btVoltar" 
        Text="Voltar" 
        PostBackUrl="~/Admin/Utilizadores/utilizadores.aspx"/>
     <br /><asp:Label runat="server" ID="lbErro" Text="" />
</asp:Content>
