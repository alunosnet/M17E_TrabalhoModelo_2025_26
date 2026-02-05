using M17AB_TrabalhoModelo_202223.Classes;
using M17AB_TrabalhoModelo_202223.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2022_23.User.Emprestimos
{
    public partial class emprestimos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserLogin.ValidarSessao(Session, Request, "1") == false)
            {
                Response.Redirect("~/index.aspx");
            }
            ConfigurarGrid();
            AtualizarGrid();

        }

        private void AtualizarGrid()
        {
            gvlivros.Columns.Clear();
            gvlivros.DataSource = null;
            gvlivros.DataBind();

            Livro livro=new Livro();
            gvlivros.DataSource = livro.listaLivrosDisponiveis();

            //botão reservar
            ButtonField bt = new ButtonField();
            bt.HeaderText = "Reservar";
            bt.Text = "Reservar";
            bt.ButtonType= ButtonType.Button;
            bt.CommandName = "reservar";
            bt.ControlStyle.CssClass = "btn btn-danger";
            gvlivros.Columns.Add(bt);

            gvlivros.DataBind();
        }

        private void ConfigurarGrid()
        {
            gvlivros.RowCommand += Gvlivros_RowCommand;
        }

        private void Gvlivros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int linha=int.Parse(e.CommandArgument.ToString());
            int idlivro = int.Parse(gvlivros.Rows[linha].Cells[1].Text);
            int idutilizador = int.Parse(Session["id"].ToString());
            if(e.CommandName=="reservar")
            {
                Emprestimo emp=new Emprestimo();
                emp.adicionarReserva(idlivro,idutilizador,DateTime.Now.AddDays(7));
                AtualizarGrid();
            }
        }
    }
}