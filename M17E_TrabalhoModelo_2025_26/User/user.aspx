<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="M17AB_TrabalhoModelo_2022_23.User.user" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Área de utilizador</h1>
    <div runat="server" id="divPerfil">
        Nome: <asp:Label  CssClass="form-control" runat="server" ID="lbNome"></asp:Label>
        <br />Morada: <asp:Label  CssClass="form-control" runat="server" ID="lbMorada"></asp:Label>
        <br />Nif: <asp:Label  CssClass="form-control" runat="server" ID="lbnif"></asp:Label>
        <br />
        <asp:Button CssClass="btn btn-lg btn-danger" runat="server" ID="btEditar" Text="Editar Perfil" OnClick="btEditar_Click" />
    </div>
    <div runat="server" id="divEditar">
        Nome:<asp:TextBox  CssClass="form-control" runat="server" ID="tbNome"></asp:TextBox>
        <br />Morada<asp:TextBox  CssClass="form-control" runat="server" ID="tbMorada"></asp:TextBox>
        <br />Nif<asp:TextBox  CssClass="form-control" runat="server" ID="tbNif"></asp:TextBox>
        <br />
        <asp:Button CssClass="btn btn-lg btn-danger" runat="server" ID="btAtualizar" Text="Atualizar Perfil" OnClick="btAtualizar_Click" />
        <asp:Button CssClass="btn btn-lg btn-info"  runat="server" ID="btCancelar" Text="Cancelar" OnClick="btCancelar_Click" />
    </div>
</asp:Content>
