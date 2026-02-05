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
    public partial class historico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(UserLogin.ValidarSessao(Session,Request,"1")==false)
            {
                Response.Redirect("~/index.aspx");
            }

            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            gvhistorico.Columns.Clear();
            gvhistorico.DataSource = null;
            gvhistorico.DataBind();

            int idutilizador = int.Parse(Session["id"].ToString());
            Emprestimo emp = new Emprestimo();
            gvhistorico.DataSource = emp.listaTodosEmprestimosComNomes(idutilizador);
            gvhistorico.DataBind();
        }
    }
}