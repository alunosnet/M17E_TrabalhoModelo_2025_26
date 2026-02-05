using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2022_23.Admin.Consultas
{
    public partial class Consultas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //validar sessão
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
                Response.Redirect("~/index.aspx");

            AtualizaGrelhaConsulta();
        }
        protected void ddConsultas_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaGrelhaConsulta();
        }
        private void AtualizaGrelhaConsulta()
        {
            gvConsultas.Columns.Clear();
            int iconsulta = int.Parse(ddConsultas.SelectedValue);
            DataTable dados;
            string sql = "";
            switch (iconsulta)
            {
                //Top de livros mais requisitados
                case 0:
                    sql = @"SELECT Nome,count(Emprestimos.nlivro) as [Nº de empréstimos] FROM Livros 
                            INNER JOIN Emprestimos ON Livros.nlivro=Emprestimos.nlivro 
                            GROUP BY Livros.nlivro,Livros.Nome
                            ORDER BY count(Emprestimos.nlivro) DESC";
                    break;
                //Top de leitores
                case 1:
                    sql = @"SELECT Nome,count(idutilizador) as [Nº de empréstimos] FROM Utilizadores 
                            INNER JOIN Emprestimos ON Utilizadores.id=Emprestimos.idutilizador 
                            GROUP BY Utilizadores.id,Utilizadores.Nome
                            ORDER BY count(idutilizador) DESC";
                    break;
                //Top de livros mais requisitados do último mês
                case 2:
                    sql = @"SELECT TOP 3 nome AS [Livro], COUNT(emprestimos.nlivro) AS [Nº de Requisições] 
                            FROM Livros, emprestimos
                            WHERE Livros.nlivro = emprestimos.nlivro 
                                AND DATEDIFF(DAY, emprestimos.data_emprestimo, GETDATE()) < 30
                            GROUP BY Livros.nlivro,Livros.nome
                            ORDER BY COUNT(emprestimos.nlivro) DESC";
                    break;
                //Lista de utilizadores com livros fora de prazo 
                case 3:
                    sql = @"SELECT utilizadores.nome as [Nome do Leitor],livros.nome as [Nome Livro],
                            data_emprestimo,data_devolve,DATEDIFF(DAY,emprestimos.data_devolve,getdate()) as [Dias fora de prazo]
                            FROM Emprestimos 
                            INNER JOIN livros 
                                ON emprestimos.nlivro=livros.nlivro
                            INNER JOIN utilizadores 
                                ON emprestimos.idutilizador=utilizadores.id 
                            WHERE DATEDIFF(DAY,emprestimos.data_devolve,getdate()) >= 1 AND emprestimos.estado = 1";
                    break;
                //Livros da última semana - novidades 
                case 4:
                    sql = @"SELECT nome
                            FROM Livros
                            WHERE DATEDIFF(DAY,data_aquisicao,GETDATE()) <= 7";
                    break;
                //Tempo médio de empréstimo 
                case 5:
                    sql = @"SELECT AVG(datediff(day, data_emprestimo, data_devolve)) as [Média de duração dos Empréstimos]
                            FROM Emprestimos";
                    break;
                //Nº de livros por autor
                case 6:
                    sql = @"SELECT autor, count(nlivro) as [Nrº de livros] 
                            FROM Livros 
                            GROUP BY autor";
                    break;
                //Nº de utilizadores bloqueados
                case 7:
                    sql = @"SELECT count(*) as [Nº de utilizadores bloqueados] 
                            FROM Utilizadores
                            WHERE estado = 0";
                    break;
                //Nº de tipos de livro por utilizador
                case 8:
                    sql = @"SELECT utilizadores.nome, Livros.tipo, count(*) as [Nº de Empréstimos] 
                            FROM utilizadores
                            INNER JOIN emprestimos on utilizadores.id=emprestimos.idutilizador
                            INNER JOIN Livros on emprestimos.nlivro=Livros.nlivro
                            GROUP BY utilizadores.id,utilizadores.nome,Livros.tipo
                            ORDER BY utilizadores.id";
                    break;
                //Nº de empréstimos por mês
                case 9:
                    sql = @"SELECT MONTH(data_emprestimo) as [Mês],Count(nemprestimo) as [Nº de empréstimo] 
                            FROM emprestimos
                            GROUP BY MONTH(data_emprestimo)";
                    break;
                //Lista dos utilizadores que requisitaram o livro mais caro
                case 10:
                    sql = @"SELECT DISTINCT Utilizadores.nome 
                            FROM utilizadores
                            INNER JOIN emprestimos on emprestimos.idutilizador = utilizadores.id
                            WHERE emprestimos.nlivro = (SELECT TOP 1 livros.nlivro FROM livros ORDER BY preco DESC)";
                    break;
                //Lista dos livros cujo preço é superior à média
                case 11:
                    sql = @"SELECT * FROM Livros WHERE preco>(SELECT AVG(preco) FROM Livros)";
                    break;
                //11- Lista dos utilizadores com a mesma password
                case 12:
                    sql = @"SELECT Nome, 
                                (SELECT count(*) FROM Utilizadores as UT WHERE U.password=UT.password)
                            as [Nº de utilizadores com a mesma password]
                            FROM Utilizadores as U";
                    break;
            }
            BaseDados bd = new BaseDados();
            dados = bd.devolveSQL(sql);
            gvConsultas.DataSource = dados;
            gvConsultas.DataBind();
        }
    }
}