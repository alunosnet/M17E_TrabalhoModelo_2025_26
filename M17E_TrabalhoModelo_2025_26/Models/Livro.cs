using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17AB_TrabalhoModelo_202223.Models
{
    public class Livro
    {
        public int nlivro;
        public string nome;
        public int ano;
        public DateTime data_aquisicao;
        public decimal preco;
        public string autor;
        public string tipo;
        public int estado;

        BaseDados bd;

        public Livro()
        {
            bd = new BaseDados();
        }

        public int Adicionar()
        {
            string sql = @"INSERT INTO Livros(nome,ano,data_aquisicao,preco,autor,tipo,estado)
                    VALUES (@nome,@ano,@data_aquisicao,@preco,@autor,@tipo,@estado);
                    SELECT CAST(SCOPE_IDENTITY() AS INT)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@nome",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.nome
                },
                new SqlParameter()
                {
                    ParameterName="@ano",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.ano
                },
                new SqlParameter()
                {
                    ParameterName="@data_aquisicao",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=this.data_aquisicao
                },
                new SqlParameter()
                {
                    ParameterName="@preco",
                    SqlDbType=System.Data.SqlDbType.Decimal,
                    Value=this.preco
                },
                new SqlParameter()
                {
                    ParameterName="@autor",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.autor
                },
                new SqlParameter()
                {
                    ParameterName="@tipo",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.tipo
                },
                new SqlParameter()
                {
                    ParameterName="@estado",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.estado
                },
            };
            return bd.executaEDevolveSQL(sql, parametros);
        }

        internal DataTable ListaTodosLivros()
        {
            string sql = @"SELECT nlivro,nome,ano,data_aquisicao,
                    preco, autor, tipo,
                    case
                        when estado=0 then 'Emprestado'
                        when estado=1 then 'Disponível'
                        when estado=2 then 'Reservado'
                    end as estado
                    FROM Livros";
            return bd.devolveSQL(sql);
        }

        public DataTable devolveDadosLivro(int nlivro)
        {
            string sql = "SELECT * FROM Livros WHERE nlivro=@nlivro";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro }
            };
            return bd.devolveSQL(sql, parametros);
        }
        public void removerLivro(int nlivro)
        {
            string sql = "DELETE FROM Livros WHERE nlivro=@nlivro";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro }
            };
            bd.executaSQL(sql, parametros);
        }
        public void atualizaLivro()
        {
            string sql = "UPDATE Livros SET nome=@nome,ano=@ano,data_aquisicao=@data,preco=@preco,";
            sql += "autor=@autor, tipo=@tipo ";
            sql += " WHERE nlivro=@nlivro;";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value= nome},
                new SqlParameter() {ParameterName="@ano",SqlDbType=SqlDbType.Int,Value= ano},
                new SqlParameter() {ParameterName="@data",SqlDbType=SqlDbType.DateTime,Value= data_aquisicao},
                new SqlParameter() {ParameterName="@preco",SqlDbType=SqlDbType.Decimal,Value= preco},
                new SqlParameter() {ParameterName="@autor",SqlDbType=SqlDbType.VarChar,Value=autor},
                new SqlParameter() {ParameterName="@tipo",SqlDbType=SqlDbType.VarChar,Value=tipo},
                new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro}
            };
            bd.executaSQL(sql, parametros);
        }
        public DataTable listaLivrosDisponiveis(int? ordena = null)
        {
            string sql = "SELECT * FROM Livros WHERE estado=1";
            if (ordena != null && ordena == 1)
            {
                sql += " order by preco";
            }
            if (ordena != null && ordena == 2)
            {
                sql += " order by autor";
            }
            return bd.devolveSQL(sql);
        }

        internal DataTable listaLivrosDoAutor(string pesquisa)
        {
            string sql = "SELECT * FROM Livros WHERE estado=1 and autor like @nome";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {
                    ParameterName ="@nome",
                    SqlDbType =SqlDbType.VarChar,
                    Value = "%"+pesquisa+"%"},
            };
            return bd.devolveSQL(sql, parametros);
        }

        internal DataTable listaLivrosDisponiveis(string pesquisa, int? ordena = null)
        {
            string sql = "SELECT * FROM Livros WHERE estado=1 and nome like @nome";
            if (ordena != null && ordena == 1)
            {
                sql += " order by preco";
            }
            if (ordena != null && ordena == 2)
            {
                sql += " order by autor";
            }

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {
                    ParameterName ="@nome",
                    SqlDbType =SqlDbType.VarChar,
                    Value = "%"+pesquisa+"%"},
            };
            return bd.devolveSQL(sql, parametros);
        }


    }
}