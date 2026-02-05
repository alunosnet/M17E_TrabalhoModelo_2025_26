using M17AB_TrabalhoModelo_2022_23.User.Emprestimos;
using M17AB_TrabalhoModelo_202223.Classes;
using M17AB_TrabalhoModelo_202223.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2022_23.Admin.Emprestimos
{
    public partial class Emprestimos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //validar sessão
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
            {
                Response.Redirect("~/index.aspx");
            }

            ConfigurarGrid();

            if (IsPostBack) return;

            AtualizarGrid();
            AtualizarDDLivros();
            AtualizarDDLeitores();

        }

        private void AtualizarDDLeitores()
        {
            Utilizador u = new Utilizador();
            dd_leitor.Items.Clear();
            DataTable dados = u.listaTodosUtilizadoresDisponiveis();
            foreach (DataRow linha in dados.Rows)
                dd_leitor.Items.Add(
                    new ListItem(linha["nome"].ToString(), linha["id"].ToString())
                );
        }

        private void AtualizarDDLivros()
        {
            Livro lv = new Livro();
            dd_livro.Items.Clear();
            DataTable dados = lv.listaLivrosDisponiveis();
            foreach (DataRow linha in dados.Rows)
                dd_livro.Items.Add(
                    new ListItem(linha["nome"].ToString(), linha["nlivro"].ToString())
                );
        }

        private void AtualizarGrid()
        {
            Emprestimo emp = new Emprestimo();

            DataTable dados;
            if (cb_livros_emprestados.Checked)
                dados = emp.listaTodosEmprestimosPorConcluirComNomes();
            else
                dados = emp.listaTodosEmprestimosComNomes();

            gv_emprestimos.Columns.Clear();
            gv_emprestimos.DataSource = null;
            gv_emprestimos.DataBind();
            if (dados == null || dados.Rows.Count == 0) return;
            //botões de comando
            //receber
            ButtonField bfReceber = new ButtonField();
            bfReceber.HeaderText = "Receber Livro";
            bfReceber.Text = "Receber";
            bfReceber.ButtonType = ButtonType.Button;
            bfReceber.ControlStyle.CssClass = "btn btn-info";
            bfReceber.CommandName = "receber";
            gv_emprestimos.Columns.Add(bfReceber);
            //enviar um email
            ButtonField bfEmail = new ButtonField();
            bfEmail.HeaderText = "Enviar email";
            bfEmail.Text = "Email";
            bfEmail.ButtonType = ButtonType.Button;
            bfEmail.ControlStyle.CssClass = "btn btn-danger";
            bfEmail.CommandName = "email";
            gv_emprestimos.Columns.Add(bfEmail);

            gv_emprestimos.DataSource = dados;
            gv_emprestimos.AutoGenerateColumns = true;
            gv_emprestimos.DataBind();
        }

        private void ConfigurarGrid()
        {
            //paginação
            gv_emprestimos.AllowPaging = true;
            gv_emprestimos.PageSize = 5;
            gv_emprestimos.PageIndexChanging += Gv_emprestimos_PageIndexChanging;
            //botões de comando
            gv_emprestimos.RowCommand += Gv_emprestimos_RowCommand;
            gv_emprestimos.RowDataBound += GvEmprestimos_RowDataBound;
        }

        private void Gv_emprestimos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //mudar de página
            if (e.CommandName == "Page") return;

            //linha
            int linha=int.Parse(e.CommandArgument.ToString());

            //id do empréstimo
            int idemprestimo = int.Parse(gv_emprestimos.Rows[linha].Cells[2].Text);
            Emprestimo emp = new Emprestimo();
            if(e.CommandName=="receber")
            {
                emp.alterarEstadoEmprestimo(idemprestimo);
                AtualizarDDLeitores();
                AtualizarDDLivros();
                AtualizarGrid();
            }
            if(e.CommandName=="email")
            {
                string email = ConfigurationManager.AppSettings["MeuEmail"];
                string password = ConfigurationManager.AppSettings["MinhaPassword"];
                string assunto = "Empréstimo de livro fora de prazo";
                string texto = "Caro leitor deve devolver o livro que tem emprestado.";
                DataTable dados = emp.devolveDadosEmprestimo(idemprestimo);
                int idleitor = int.Parse(dados.Rows[0]["idutilizador"].ToString());
                DataTable dadosLeitor = new Utilizador().devolveDadosUtilizador(idleitor);
                string emailLeitor = dadosLeitor.Rows[0]["email"].ToString();
                Helper.enviarMail(email,password,emailLeitor,assunto,texto);
                AtualizarDDLeitores();
                AtualizarDDLivros();
                AtualizarGrid();
            }
        }

        private void Gv_emprestimos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_emprestimos.PageIndex = e.NewPageIndex;
            AtualizarGrid();
        }

        protected void bt_registar_Click(object sender, EventArgs e)
        {
            try
            {
                Emprestimo emp = new Emprestimo();
                int nlivro = int.Parse(dd_livro.SelectedValue);
                int nleitor = int.Parse(dd_leitor.SelectedValue);
                DateTime data = DateTime.Parse(tb_data.Text);
                emp.adicionarEmprestimo(nlivro, nleitor, data);
                
                lb_erro.Text = "O empréstimo foi registado com sucesso.";
                lb_erro.CssClass = "";
            }catch(Exception erro)
            {
                lb_erro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lb_erro.CssClass = "alert alert-danger";
            }
            AtualizarDDLeitores();
            AtualizarDDLivros();
            AtualizarGrid();
        }

        protected void cb_livros_emprestados_CheckedChanged(object sender, EventArgs e)
        {
            AtualizarGrid();
        }
        //executado para cada linha adicionada à grid
        private void GvEmprestimos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime datadevolve = DateTime.Parse(e.Row.Cells[6].Text); 
                int estado = int.Parse(e.Row.Cells[7].Text);                
                if (estado != 0)
                {
                    e.Row.Cells[0].Controls[0].Visible = true;
                    if (datadevolve < DateTime.Now)
                        e.Row.Cells[1].Controls[0].Visible = true;
                    else
                        e.Row.Cells[1].Controls[0].Visible = false;

                }
                else
                {
                    e.Row.Cells[0].Controls[0].Visible = false;             
                    e.Row.Cells[1].Controls[0].Visible = false;
                }
                

            }
        }
    }
}