using M17AB_TrabalhoModelo_202223.Classes;
using M17AB_TrabalhoModelo_202223.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2022_23.Admin.Utilizadores
{
    public partial class EditarUtilizador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //validar sessão
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
            {
                Response.Redirect("~/index.aspx");
            }
            //consultar a bd para recolher os dados do utilizador
            if (IsPostBack) return;
            try
            {
                int id = int.Parse(Request["id"].ToString());
                Utilizador utilizador = new Utilizador();
                DataTable dados = utilizador.devolveDadosUtilizador(id);
                if (dados == null || dados.Rows.Count != 1)
                {
                    throw new Exception("Utilizador não existe");
                }
                tbNome.Text = dados.Rows[0]["nome"].ToString();
                tbNif.Text = dados.Rows[0]["nif"].ToString();
                tbMorada.Text = dados.Rows[0]["morada"].ToString();
            }
            catch
            {
                lbErro.Text = "O utilizador indicado não existe.";
                ScriptManager.RegisterStartupScript(this, typeof(Page),
                    "Redirecionar", "returnMain('/Admin/Utilizadores/Utilizadores.aspx')", true);
            }
        }
        protected void btEditar_Click(object sender, EventArgs e)
        {
            try
            {
                string nome = tbNome.Text.Trim();
                if (string.IsNullOrEmpty(nome) || nome.Length < 3)
                {
                    throw new Exception("O nome não é válido.");
                }
                string morada = tbMorada.Text.Trim();
                if (string.IsNullOrEmpty(morada) || morada.Length < 3)
                {
                    throw new Exception("A morada não é válida.");
                }
                string nif = tbNif.Text.Trim();
                if (string.IsNullOrEmpty(nif) || nif.Length != 9)
                {
                    throw new Exception("O nif não é válido.");
                }
                int id = int.Parse(Request["id"].ToString());
                Utilizador utilizador = new Utilizador();
                utilizador.id = id;
                utilizador.nome = nome;
                utilizador.morada = morada;
                utilizador.nif = nif;
                utilizador.atualizarUtilizador();
            }
            catch (Exception error)
            {
                lbErro.Text = "Ocorreu um erro: " + error.Message;
                return;
            }
            lbErro.Text = "Utilizador atualizado com sucesso.";
            ScriptManager.RegisterStartupScript(this, typeof(Page),
                    "Redirecionar", "returnMain('/Admin/Utilizadores/Utilizadores.aspx')", true);
        }
    }
}