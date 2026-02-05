<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="M17AB_TrabalhoModelo_2022_23.Admin.admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Public/chartistJS/chartist.css" rel="stylesheet" />
    <script src="/Public/chartistJS/chartist.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Área de administrador</h1>
    <button id="btnlista" class="btn btn-info">Carregar dados</button>
    <button id="btnCriaGrafico" class="btn btn-success">Dados de utilizadores</button>
    <div id="divLista"></div>
    <div class="ct-chart ct-golden-section"></div>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const btnLista = document.getElementById('btnlista');
            btnLista.addEventListener('click', function (e) {
                e.preventDefault();

                fetch('Servicos.asmx/ListaLivros', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                })
                .then(response => {
                        if (!response.ok) {
                            throw new Error('Erro na resposta da rede');
                        }
                        return response.json();
                    })
                .then(data => {
                        console.log(data.d);
                        const listaLivros = JSON.parse(data.d);
                        const divLista = document.getElementById('divLista');
                    divLista.innerHTML = "";
                        listaLivros.forEach(livro => {
                            const br = document.createElement('br');
                            divLista.appendChild(document.createTextNode(livro.nome));
                            divLista.appendChild(br);
                        });
                    })
                .catch(error => {
                        console.error('Erro:', error);
                        alert('Alguma coisa errada correu mal');
                    });
            });

            const btnGrafico = document.getElementById('btnCriaGrafico');
            btnGrafico.addEventListener('click', function (e) {
                e.preventDefault();

                const pData = [];
                const jsonData = JSON.stringify({ pData: pData });

                fetch('Servicos.asmx/DadosUtilizadores', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    },
                    body: jsonData
                    })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Erro na resposta da rede');
                        }
                        return response.json();
                    })
                    .then(response => {
                        const aData = response.d;
                        const arrLabels = [];
                        const arrSeries = [];

                        aData.forEach(item => {
                            arrLabels.push(item.perfil);
                            arrSeries.push(item.contagem);
                        });

                        const data = {
                            labels: arrLabels,
                            series: arrSeries
                        };

                        // Cria o gráfico PIE CHART
                        new Chartist.Pie('.ct-chart', data);
                    })
                    .catch(error => {
                        alert('Alguma coisa correu mal');
                        console.log(error);
                    });
            });
        });
    </script>
</asp:Content>
