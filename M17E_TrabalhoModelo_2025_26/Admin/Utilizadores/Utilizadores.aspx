<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Utilizadores.aspx.cs" Inherits="M17AB_TrabalhoModelo_2022_23.Admin.Utilizadores.Utilizadores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Utilizadores</h1>
    <asp:GridView CssClass="table" ID="gvUtilizadores" runat="server"></asp:GridView>
    <h1>Adicionar Utilizador</h1>
    Nome:<asp:TextBox CssClass="form-control" runat="server" ID="tb_nome"></asp:TextBox><br />
    Email:<asp:TextBox CssClass="form-control" runat="server" ID="tb_email"></asp:TextBox><br />
    Morada:<asp:TextBox CssClass="form-control" runat="server" ID="tb_morada"></asp:TextBox><br />
    Nif:<asp:TextBox CssClass="form-control" runat="server" ID="tb_nif"></asp:TextBox><br />
    Password:<asp:TextBox CssClass="form-control" runat="server" ID="tb_password" TextMode="Password"></asp:TextBox><br />
    Perfil:<asp:DropDownList CssClass="form-control" runat="server" ID="dd_perfil">
                <asp:ListItem Value="0">Admin</asp:ListItem>
                <asp:ListItem Value="1">Leitor</asp:ListItem>
           </asp:DropDownList>
    <br />
    <asp:Button CssClass="btn btn-lg btn-danger" runat="server" ID="bt_guardar" Text="Adicionar" OnClick="bt_guardar_Click" /><br />
    <asp:Label runat="server" ID="lb_erro"></asp:Label>
</asp:Content>
