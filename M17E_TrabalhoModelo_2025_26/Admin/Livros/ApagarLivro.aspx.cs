using M17AB_TrabalhoModelo_202223.Classes;
using M17AB_TrabalhoModelo_202223.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2022_23.Admin.Livros
{
    public partial class ApagarLivro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //validar sessão
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
            {
                Response.Redirect("~/index.aspx");
            }
            try
            {
                //querystring nlivro
                int nlivro = int.Parse(Request["nlivro"].ToString());

                Livro lv = new Livro();
                DataTable dados = lv.devolveDadosLivro(nlivro);
                if (dados==null || dados.Rows.Count == 0)
                {
                    //o nlivro não existe na tabela dos livros
                    throw new Exception("Livro não existe.");
                }
                //mostrar os dados livro
                lbNlivro.Text = dados.Rows[0]["nlivro"].ToString();
                lbNome.Text = dados.Rows[0]["nome"].ToString();
                imgCapa.ImageUrl = @"~\Public\Imagens\" + nlivro + ".jpg";
                imgCapa.Width = 300;
            }
            catch 
            {
                Response.Redirect("~/Admin/Livros/livros.aspx");
            }
        }

        protected void btVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Livros/livros.aspx");
        }

        protected void btRemover_Click(object sender, EventArgs e)
        {
            try
            {
                int nlivro = int.Parse(Request["nlivro"].ToString());
                Livro lv = new Livro();
                lv.removerLivro(nlivro);
                //apagar a capa
                string ficheiro = Server.MapPath(@"~\Public\Imagens\") + nlivro + ".jpg";
                if (File.Exists(ficheiro))
                    File.Delete(ficheiro);
                //Response.Redirect("~/Admin/Livros/livros.aspx");
                lbErro.Text = "O livro foi removido com sucesso";
                ScriptManager.RegisterStartupScript(this, typeof(Page),
                    "Redirecionar", "returnMain('livros.aspx')", true);
            }
            catch
            {
                Response.Redirect("~/Admin/Livros/livros.aspx");

            }
        }
    }
}