using M17AB_TrabalhoModelo_202223.Classes;
using M17AB_TrabalhoModelo_202223.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2022_23.Admin.Livros
{
    public partial class EditarLivro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //validar sessão
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
            {
                Response.Redirect("~/index.aspx");
            }
            if (IsPostBack) return;
            try
            {
                //querystring nlivro
                int nlivro = int.Parse(Request["nlivro"].ToString());

                Livro lv = new Livro();
                DataTable dados = lv.devolveDadosLivro(nlivro);
                if (dados == null || dados.Rows.Count == 0)
                {
                    //o nlivro não existe na tabela dos livros
                    throw new Exception("Livro não existe.");
                }
                //mostrar os dados livro
                tbNome.Text = dados.Rows[0]["nome"].ToString();
                tbAno.Text = dados.Rows[0]["ano"].ToString();
                tbAutor.Text = dados.Rows[0]["autor"].ToString();
                tbPreco.Text = dados.Rows[0]["preco"].ToString();
                tbData.Text = DateTime.Parse(dados.Rows[0]["data_aquisicao"].ToString()).ToString("yyyy-MM-dd");
                dpTipo.Text = dados.Rows[0]["tipo"].ToString();
                Random rnd=new Random();
                imgCapa.ImageUrl = @"~\Public\Imagens\" + nlivro + ".jpg?"+rnd.Next(9999);
                imgCapa.Width = 300;
            }
            catch
            {
                Response.Redirect("~/Admin/Livros/livros.aspx");
            }
        }

        protected void btAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string nome = tbNome.Text;
                if (nome.Trim().Length < 3)
                {
                    throw new Exception("O nome é muito pequeno.");
                }
                int ano = int.Parse(tbAno.Text);
                if (ano > DateTime.Now.Year || ano < 0)
                {
                    throw new Exception("O ano não é válido");
                }
                DateTime data = DateTime.Parse(tbData.Text);
                if (data > DateTime.Now)
                {
                    throw new Exception("A data tem de ser inferior à data atual");
                }
                Decimal preco = Decimal.Parse(tbPreco.Text);
                if (preco < 0 || preco > 100)
                {
                    throw new Exception("O preço deve estar entre 0 e 100");
                }
                string autor = tbAutor.Text;
                if (autor.Trim().Length < 3)
                {
                    throw new Exception("O nome do autor é muito pequeno");
                }
                string tipo = dpTipo.SelectedValue;
                if (tipo == "")
                {
                    throw new Exception("O tipo não é válido");
                }
                Livro livro = new Livro();
                livro.nome = nome;
                livro.preco = preco;
                livro.ano = ano;
                livro.data_aquisicao = data;
                livro.autor = autor;
                livro.tipo = tipo;
                livro.estado = 1;
                livro.nlivro = int.Parse(Request["nlivro"].ToString());
                livro.atualizaLivro();

                if (fuCapa.HasFile)
                {
                    string ficheiro = Server.MapPath(@"~\Public\Imagens\");
                    ficheiro = ficheiro + livro.nlivro + ".jpg";
                    fuCapa.SaveAs(ficheiro);
                }

                lbErro.Text = "O livro foi atualizado com sucesso";
                ScriptManager.RegisterStartupScript(this, typeof(Page),
                    "Redirecionar", "returnMain('livros.aspx')", true);
            }
            catch (Exception ex)
            {
                lbErro.Text = "Ocorreu o seguinte erro: " + ex.Message;
                return;
            }

        }
    }
}