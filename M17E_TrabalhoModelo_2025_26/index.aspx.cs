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

namespace M17AB_TrabalhoModelo_2022_23
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"]!=null)
                divLogin.Visible=false;

            //ordenar os livros?
            int? ordenar = 0;
            try
            {
                if (Request["ordenada"] != null)
                    ordenar = int.Parse(Request["ordena"].ToString());
            }
            catch
            {
                ordenar = null;
            }
            //atualizar grelha livros
            atualizaListaLivros(null, ordenar);
        }
        protected void btPesquisar_Click(object sender, EventArgs e)
        {
            atualizaListaLivros(tbPesquisa.Text);
        }
        private void atualizaListaLivros(string pesquisa = null, int? ordena = null)
        {
            Livro livro = new Livro();
            DataTable dados;
            if (pesquisa == null)
            {
                //se existir o cookie ultimolivro listar os livros do mesmo autor
                HttpCookie httpCookie = Request.Cookies["ultimolivro"];
                if (httpCookie == null)
                    dados = livro.listaLivrosDisponiveis(ordena);
                else
                {
                    dados = livro.listaLivrosDoAutor(Server.UrlDecode(httpCookie.Value));
                    dados.Merge(livro.listaLivrosDisponiveis(ordena));
                }
            }
            else
            {
                dados = livro.listaLivrosDisponiveis(pesquisa, ordena);
            }
            gerarIndex(dados);
        }
        private void gerarIndex(DataTable dados)
        {
            if (dados == null || dados.Rows.Count == 0)
            {
                divLivros.InnerHtml = "";
                return;
            }
            string grelha = "<div class='container-fluid'>";
            grelha += "<div class='row'>";
            foreach (DataRow livro in dados.Rows)
            {
                grelha += "<div class='col'>";
                grelha += "<img src='/Public/Imagens/" + livro[0].ToString() +
                    ".jpg' class='img-fluid'/>";
                grelha += "<p/><span class='stat-title'>" + livro[1].ToString()
                    + "</span>";
                grelha += "<span class='stat-title'>" +
                    String.Format(" | {0:C}", Decimal.Parse(livro["preco"].ToString()))
                    + "</span>";
                grelha += "<br/><a href='detalheslivro.aspx?id=" + livro[0].ToString()
                    + "'>Detalhes</a>";
                grelha += "</div>";
            }
            grelha += "</div></div>";
            divLivros.InnerHtml = grelha;
        }
        protected void bt_recuperar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_Email.Text.Trim().Length == 0)
                {
                    throw new Exception("Indique um email");
                }
                string email=tb_Email.Text.Trim();
                Utilizador utilizador= new Utilizador();
                DataTable dados = utilizador.devolveDadosUtilizador(email);
                if(dados==null || dados.Rows.Count != 1)
                {
                    throw new Exception("Foi enviado um email para recuperação da palavra passe");
                }
                Guid guid=Guid.NewGuid();
                utilizador.recuperarPassword(email, guid.ToString());

                string mensagem = "Clique no link para recuperar a sua password.<br/>";
                mensagem += "<a href='http://" + Request.Url.Authority + "/recuperarpassword.aspx?";
                mensagem += "id=" + Server.UrlEncode(guid.ToString()) + "'>Clique aqui</a>";
                
                string meuemail = ConfigurationManager.AppSettings["MeuEmail"];
                string minhapassword = ConfigurationManager.AppSettings["MinhaPassword"];
                Helper.enviarMail(meuemail, minhapassword, email, "Recuperação de password", mensagem);

                lb_erro.Text = "Foi enviado um email para recuperação da palavra passe";
            }
            catch(Exception ex)
            {
                lb_erro.Text= ex.Message;
            }
        }

        protected void bt_login_Click(object sender, EventArgs e)
        {
            try
            {
                string email=tb_Email.Text;
                string password=tb_Password.Text;
                UserLogin user = new UserLogin();
                DataTable dados=user.VerificaLogin(email, password);
                if (dados == null)
                    throw new Exception("Login falhou");
                //iniciar sessão
                Session["nome"] = dados.Rows[0]["nome"].ToString();
                Session["id"] = dados.Rows[0]["id"].ToString();
                //autorização
                Session["perfil"] = dados.Rows[0]["perfil"].ToString();
                Session["ip"] = Request.UserHostAddress;
                Session["useragent"] = Request.UserAgent;
                //redirecionar
                if (Session["perfil"].ToString() == "0")
                    Response.Redirect("~/Admin/Admin.aspx");
                if (Session["perfil"].ToString() == "1")
                    Response.Redirect("~/User/User.aspx");
            }
            catch
            {
                lb_erro.Text = "Login falhou. Tente novamente";
            }
        }
    }
}