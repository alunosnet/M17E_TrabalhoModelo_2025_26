using M17AB_TrabalhoModelo_202223.Classes;
using M17AB_TrabalhoModelo_202223.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2022_23.User
{
    public partial class user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //validar sessão
            if (UserLogin.ValidarSessao(Session, Request, "1") == false)
            {
                Response.Redirect("~/index.aspx");
            }
            if(!IsPostBack)
            {
                divEditar.Visible= false;
                MostrarPerfil();
            }
        }
        void MostrarPerfil()
        {
            int id = int.Parse(Session["id"].ToString());
            Utilizador utilizador= new Utilizador();
            DataTable dados=utilizador.devolveDadosUtilizador(id);
            if(divPerfil.Visible == true)
            {
                lbNome.Text = dados.Rows[0]["nome"].ToString();
                lbMorada.Text = dados.Rows[0]["morada"].ToString();
                lbnif.Text = dados.Rows[0]["nif"].ToString();
            }
            else
            {
                tbNome.Text = dados.Rows[0]["nome"].ToString();
                tbMorada.Text = dados.Rows[0]["morada"].ToString();
                tbNif.Text = dados.Rows[0]["nif"].ToString();
            }
        }
        protected void btEditar_Click(object sender, EventArgs e)
        {
            divPerfil.Visible = false;
            divEditar.Visible = true;
            MostrarPerfil();
        }

        protected void btAtualizar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Session["id"].ToString());
            
           
            string nome = tbNome.Text.Trim();
            if (nome.Length < 3)
            {
                throw new Exception("O nome tem de ter pelo menos 3 letras");
            }

            string morada = tbMorada.Text.Trim();
            if (morada.Length < 3)
            {
                throw new Exception("A morada tem de ter pelo menos 3 letras");
            }
            string nif = tbNif.Text.Trim();
            if (nif.Length != 9)
            {
                throw new Exception("O nif tem de ter 9 números");
            }
            Utilizador utilizador= new Utilizador();
            utilizador.nome = nome;
            utilizador.morada= morada;
            utilizador.nif= nif;
            utilizador.id = id;
            utilizador.atualizarUtilizador();
            btCancelar_Click(sender, e);
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            divPerfil.Visible = true;
            divEditar.Visible = false;
            MostrarPerfil();
        }
    }
}