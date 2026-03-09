using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_2022_23.Admin
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //validar sessão
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
            {
                Response.Redirect("~/index.aspx");
            }
            //CSRF
            if (!IsPostBack)
            {
                //gerar o token
                string antiforgerytoken = Helper.GenerateAntiForgeryToken();
                //guardar nas variáveis de sessão
                Session["AntiForgeryToken"] = antiforgerytoken;
                //inserir na página
                AntiForgeryToken.Value = antiforgerytoken;
            }
            else
            {
                string token = AntiForgeryToken.Value;
                if (Helper.ValidateAntiForgeryToken(Session,token)==false)
                {
                    // Token inválido
                    Response.StatusCode = 403; // Forbidden
                    Response.Write("Invalid request: Anti-forgery token validation failed.");
                    Response.End(); 
                    return;
                }
                else
                {
                    //Atualiza o token
                    AntiForgeryToken.Value = Session["AntiForgeryToken"].ToString();
                }
            }
            
        }
    }
}