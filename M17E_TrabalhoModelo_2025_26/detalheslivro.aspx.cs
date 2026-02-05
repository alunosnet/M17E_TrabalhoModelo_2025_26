using M17AB_TrabalhoModelo_202223.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2022_23
{
    public partial class detalheslivro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Request["id"].ToString());
                Livro livro = new Livro();
                DataTable dados = livro.devolveDadosLivro(id);
                lbNome.Text = dados.Rows[0]["nome"].ToString();
                lbAno.Text = dados.Rows[0]["ano"].ToString();
                lbAutor.Text = dados.Rows[0]["autor"].ToString();
                lbPreco.Text = String.Format("{0:c}", Decimal.Parse(dados.Rows[0]["preco"].ToString()));
                string ficheiro = @"~\Public\Imagens\" + dados.Rows[0]["nlivro"].ToString() + ".jpg";
                imgCapa.ImageUrl = ficheiro;
                imgCapa.Width = 200;
                //criar cookie
                HttpCookie cookie = new HttpCookie("ultimolivro", Server.UrlEncode(lbAutor.Text));
                cookie.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(cookie);
            }
            catch
            {
                Response.Redirect("~/index.aspx");
            }
        }
        protected void btReservar_Click(object sender, EventArgs e)
        {
            try
            {
                int idlivro = int.Parse(Request["id"].ToString());
                int idutilizador = int.Parse(Session["id"].ToString());
                Emprestimo emprestimo = new Emprestimo();
                emprestimo.adicionarReserva(idlivro, idutilizador, DateTime.Now.AddDays(7));
                lbErro.Text = "Livro reservado com sucesso";
                ScriptManager.RegisterStartupScript(this, typeof(Page),
                    "Redirecionar", "returnMain('/index.aspx')", true);
            }
            catch
            {
                Response.Redirect("/index.aspx");
            }
        }
    }
}
