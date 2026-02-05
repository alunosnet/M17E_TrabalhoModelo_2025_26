using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace M17AB_TrabalhoModelo_202223.Classes
{
    public class UserLogin
    {
        BaseDados bd;
        public UserLogin()
        {
            bd = new BaseDados();
        }
        public DataTable VerificaLogin(string email, string password)
        {
            DataTable dados;
            string sql = @"SELECT * FROM Utilizadores WHERE email=@email AND 
                         password=HASHBYTES('SHA2_512',concat(@password,sal))
                        AND estado=1";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@email",
                    SqlDbType=SqlDbType.VarChar,
                    Value=email
                },
                new SqlParameter()
                {
                    ParameterName="@password",
                    SqlDbType=SqlDbType.VarChar,
                    Value=password
                },
            };
            dados = bd.devolveSQL(sql, parametros);
            if (dados == null || dados.Rows.Count == 0)
                return null;
            return dados;
        }


        public static bool ValidarSessao(HttpSessionState estadoSessao,HttpRequest pedido,string perfil)
        {
            if (estadoSessao["perfil"] == null ||
                estadoSessao["perfil"].ToString() != perfil ||
                estadoSessao["ip"].ToString() != pedido.UserHostAddress ||
                estadoSessao["useragent"].ToString() != pedido.UserAgent)
            {
                estadoSessao.Clear();
                return false;
            }
            return true;
        }
    }
}