using M17AB_TrabalhoModelo_202223.Classes;
using M17AB_TrabalhoModelo_202223.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace M17AB_TrabalhoModelo_2022_23.Admin
{
    /// <summary>
    /// Summary description for Servicos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class Servicos : System.Web.Services.WebService
    {

        [WebMethod]
        public string ListaLivros()
        {
            Livro livro = new Livro();
            DataTable dados = livro.ListaTodosLivros();
            List<Livro> Livros= new List<Livro>(); 
            for(int i=0;i<dados.Rows.Count;i++)
            {
                Livro novo = new Livro();
                novo.nlivro = int.Parse(dados.Rows[i]["nlivro"].ToString());
                novo.nome = dados.Rows[i]["nome"].ToString();
                novo.preco = Decimal.Parse(dados.Rows[i]["preco"].ToString());
                Livros.Add(novo);
            }
            return new JavaScriptSerializer().Serialize(Livros);
        }
        public class Dados
        {
            public string perfil;
            public int contagem;
        }
        [WebMethod(EnableSession = true)]
        public List<Dados> DadosUtilizadores()
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                return null;
            List<Dados> devolver = new List<Dados>();
            BaseDados bd = new BaseDados();
            DataTable dados = bd.devolveSQL(@"SELECT count(*), case 
                                                   when perfil=0 then 'Admin'
                                                   when perfil=1 then 'User'
                                                end as [perfil]
                                                FROM utilizadores GROUP BY perfil");
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                Dados novo = new Dados();
                novo.perfil = dados.Rows[i][1].ToString();
                novo.contagem = int.Parse(dados.Rows[i][0].ToString());
                devolver.Add(novo);
            }
            return devolver;
        }
    }
}
