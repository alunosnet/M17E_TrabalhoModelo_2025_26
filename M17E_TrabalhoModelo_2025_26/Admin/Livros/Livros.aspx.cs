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
    public partial class Livros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //validar sessão
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
            {
                Response.Redirect("~/index.aspx");
            }

            ConfigurarGrid();

            if (!IsPostBack)
            {
                AtualizarGrid();
            }
        }
        /// <summary>
        /// Configuração da grelha dos livros
        /// </summary>
        private void ConfigurarGrid()
        {
            gvLivros.AllowPaging= true;
            gvLivros.PageSize = 5;
            gvLivros.PageIndexChanging += GvLivros_PageIndexChanging;
        }

        private void GvLivros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLivros.PageIndex = e.NewPageIndex;
            AtualizarGrid();
        }

        /// <summary>
        /// Adicionar umlivro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bt_Click(object sender, EventArgs e)
        {

            try
            {
                string nome = tbNome.Text;
                if (nome.Trim().Length < 3)
                {
                    throw new Exception("O nome é muito pequeno.");
                }
                int ano = int.Parse(tbAno.Text);
                if (ano>DateTime.Now.Year || ano < 0)
                {
                    throw new Exception("O ano não é válido");
                }
                DateTime data = DateTime.Parse(tbData.Text);
                if (data>DateTime.Now)
                {
                    throw new Exception("A data tem de ser inferior à data atual");
                }
                Decimal preco = Decimal.Parse(tbPreco.Text);
                if (preco<0 || preco > 100)
                {
                    throw new Exception("O preço deve estar entre 0 e 100");
                }
                string autor = tbAutor.Text;
                if (autor.Trim().Length < 3)
                {
                    throw new Exception("O nome do autor é muito pequeno");
                }
                string tipo = dpTipo.SelectedValue;
                if (tipo=="")
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
                int nlivro = livro.Adicionar();

                if (fuCapa.HasFile)
                {
                    string ficheiro = Server.MapPath(@"~\Public\Imagens\");
                    ficheiro = ficheiro + nlivro + ".jpg";
                    fuCapa.SaveAs(ficheiro);
                }
            }catch(Exception ex)
            {
                lbErro.Text = "Ocorreu o seguinte erro: " + ex.Message;
                return;
            }
            //limpar form
            tbAno.Text = "";
            tbNome.Text = "";
            tbPreco.Text = "";
            tbAutor.Text = "";
            dpTipo.SelectedIndex = 0;
            tbData.Text = DateTime.Now.ToShortDateString();
            //atualizar grid
            AtualizarGrid();
        }
        /// <summary>
        /// Atualiza a grid dos livros
        /// </summary>
        private void AtualizarGrid()
        {
           /* 
            Livro lv = new Livro();
            gvLivros.DataSource = lv.ListaTodosLivros();
            gvLivros.DataBind();
           */
            gvLivros.Columns.Clear();
            Livro lv=new Livro();
            DataTable dados = lv.ListaTodosLivros();

            DataColumn dcEditar = new DataColumn();
            dcEditar.ColumnName = "Editar";
            dcEditar.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcEditar);

            DataColumn dcApagar = new DataColumn();
            dcApagar.ColumnName = "Apagar";
            dcApagar.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcApagar);

            //colunas da gridview
            gvLivros.DataSource= dados;
            gvLivros.AutoGenerateColumns = false;

            //Editar
            HyperLinkField hlEditar= new HyperLinkField();
            hlEditar.HeaderText = "Editar";
            hlEditar.DataTextField = "Editar";
            hlEditar.Text = "Editar...";
            hlEditar.DataNavigateUrlFormatString = "EditarLivro.aspx?nlivro={0}";
            hlEditar.DataNavigateUrlFields = new string[] { "nlivro" };
            gvLivros.Columns.Add(hlEditar);
            //Apagar
            HyperLinkField hlApagar = new HyperLinkField();
            hlApagar.HeaderText = "Apagar";
            hlApagar.DataTextField = "Apagar";
            hlApagar.Text = "Apagar...";
            hlApagar.DataNavigateUrlFormatString = "ApagarLivro.aspx?nlivro={0}";
            hlApagar.DataNavigateUrlFields = new string[] { "nlivro" };
            gvLivros.Columns.Add(hlApagar);
            //nlivro
            BoundField bfnlivro= new BoundField();
            bfnlivro.HeaderText = "Nº Livro";
            bfnlivro.DataField = "nlivro";
            gvLivros.Columns.Add(bfnlivro);
            //nome
            BoundField bfnome=new BoundField();
            bfnome.HeaderText = "Nome";
            bfnome.DataField = "nome";
           // bfnome.ControlStyle.CssClass = "batatas";
            //bfnome.HeaderStyle.CssClass = "classe do thead";
            gvLivros.Columns.Add(bfnome);
            //ano
            BoundField bfano = new BoundField();
            bfano.HeaderText = "Ano";
            bfano.DataField = "ano";
            gvLivros.Columns.Add(bfano);
            //data aquisição
            BoundField bfdata=new BoundField();
            bfdata.HeaderText = "Data aquisição";
            bfdata.DataField = "data_aquisicao";
            bfdata.DataFormatString = "{0:dd-MM-yyyy}";
            gvLivros.Columns.Add(bfdata);
            //Preço
            BoundField bfpreco=new BoundField();
            bfpreco.HeaderText = "Preço";
            bfpreco.DataField = "preco";
            bfpreco.DataFormatString = "{0:C}";
            gvLivros.Columns.Add(bfpreco);
            //Autor
            BoundField bfautor = new BoundField();
            bfautor.HeaderText = "Autor";
            bfautor.DataField = "autor";
            gvLivros.Columns.Add(bfautor);
            //Tipo
            BoundField bftipo = new BoundField();
            bftipo.HeaderText = "Tipo";
            bftipo.DataField = "tipo";
            gvLivros.Columns.Add(bftipo);
            //Estado
            BoundField bfestado=new BoundField();
            bfestado.HeaderText = "Estado";
            bfestado.DataField = "estado";
            gvLivros.Columns.Add(bfestado);
            //Capa
            ImageField ifcapa=new ImageField();
            ifcapa.HeaderText = "Capa";
            int aleatorio = new Random().Next(99999);
            ifcapa.DataImageUrlFormatString = "~/Public/Imagens/{0}.jpg?"+aleatorio;
            ifcapa.DataImageUrlField = "nlivro";
            ifcapa.ControlStyle.Width = 200;
            gvLivros.Columns.Add(ifcapa);

            gvLivros.DataBind();
        }
    }
}