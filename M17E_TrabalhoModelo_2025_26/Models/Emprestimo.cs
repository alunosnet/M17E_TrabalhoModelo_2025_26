using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17AB_TrabalhoModelo_202223.Models
{
    public class Emprestimo
    {
        BaseDados bd;

        public Emprestimo()
        {
            this.bd = new BaseDados();
        }

        public void adicionarEmprestimo(int nlivro, int nleitor, DateTime dataDevolve)
        {
            string sql = "SELECT * FROM livros WHERE nlivro=@nlivro";
            List<SqlParameter> parametrosBloquear = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro }
            };
            //iniciar transação
            SqlTransaction transacao = bd.iniciarTransacao(IsolationLevel.Serializable);
            DataTable dados = bd.devolveSQL(sql, parametrosBloquear, transacao);

            try
            {
                //verificar disponibilidade do livro
                if (dados.Rows[0]["estado"].ToString() != "1")
                    throw new Exception("Livro não está disponível");
                //alterar estado do livro
                sql = "UPDATE Livros SET estado=@estado WHERE nlivro=@nlivro";
                List<SqlParameter> parametrosUpdate = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro },
                    new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Int,Value=0 },
                };
                bd.executaSQL(sql, parametrosUpdate, transacao);
                //registar empréstimo
                sql = @"INSERT INTO Emprestimos(nlivro,idutilizador,data_emprestimo,data_devolve,estado) 
                            VALUES (@nlivro,@idutilizador,@data_emprestimo,@data_devolve,@estado)";
                List<SqlParameter> parametrosInsert = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro },
                    new SqlParameter() {ParameterName="@idutilizador",SqlDbType=SqlDbType.Int,Value=nleitor },
                    new SqlParameter() {ParameterName="@data_emprestimo",SqlDbType=SqlDbType.Date,Value=DateTime.Now.Date},
                    new SqlParameter() {ParameterName="@data_devolve",SqlDbType=SqlDbType.Date,Value=dataDevolve },
                    new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Int,Value=1 },
                };
                bd.executaSQL(sql, parametrosInsert, transacao);
                //concluir transação
                transacao.Commit();
            }
            catch
            {
                transacao.Rollback();
            }
            dados.Dispose();
        }
        public void adicionarReserva(int nlivro, int nleitor, DateTime dataDevolve)
        {
            string sql = "SELECT * FROM livros WHERE nlivro=@nlivro";
            List<SqlParameter> parametrosBloquear = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro }
            };
            //iniciar transação
            SqlTransaction transacao = bd.iniciarTransacao(IsolationLevel.Serializable);
            DataTable dados = bd.devolveSQL(sql, parametrosBloquear, transacao);

            try
            {
                //testar se o livro ainda está disponível
                if (dados.Rows[0]["estado"].ToString() != "1")
                    throw new Exception("Livro não está disponível");
                //alterar estado do livro
                sql = "UPDATE Livros SET estado=@estado WHERE nlivro=@nlivro";
                List<SqlParameter> parametrosUpdate = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro },
                    new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Int,Value=2 },
                };
                bd.executaSQL(sql, parametrosUpdate, transacao);
                //registar empréstimo
                sql = @"INSERT INTO Emprestimos(nlivro,idutilizador,data_emprestimo,data_devolve,estado) 
                            VALUES (@nlivro,@idutilizador,@data_emprestimo,@data_devolve,@estado)";
                List<SqlParameter> parametrosInsert = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro },
                    new SqlParameter() {ParameterName="@idutilizador",SqlDbType=SqlDbType.Int,Value=nleitor },
                    new SqlParameter() {ParameterName="@data_emprestimo",SqlDbType=SqlDbType.Date,Value=DateTime.Now.Date},
                    new SqlParameter() {ParameterName="@data_devolve",SqlDbType=SqlDbType.Date,Value=dataDevolve },
                    new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Int,Value=2 },
                };
                bd.executaSQL(sql, parametrosInsert, transacao);
                //concluir transação
                transacao.Commit();
            }
            catch
            {
                transacao.Rollback();
            }
            dados.Dispose();
        }
        public DataTable listaTodosEmprestimosComNomes(int nleitor)
        {
            string sql = @"SELECT nemprestimo,livros.nome as nomeLivro,utilizadores.nome as nomeLeitor,data_emprestimo,data_devolve,
                        case
                            when emprestimos.estado=0 then 'Concluído'
                            when emprestimos.estado=1 then 'Em curso'
                            when emprestimos.estado=2 then 'Reservado'
                        end as estado
                        FROM Emprestimos inner join livros on emprestimos.nlivro=livros.nlivro
                        inner join utilizadores on emprestimos.idutilizador=utilizadores.id Where idutilizador=@idutilizador";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@idutilizador",SqlDbType=SqlDbType.Int,Value=nleitor }
            };
            return bd.devolveSQL(sql, parametros);
        }

        public DataTable listaTodosEmprestimosComNomes()
        {
            string sql = @"SELECT nemprestimo,livros.nome as nomeLivro,utilizadores.nome as nomeLeitor,data_emprestimo,data_devolve,emprestimos.estado
                        FROM Emprestimos inner join livros on emprestimos.nlivro=livros.nlivro
                        inner join utilizadores on emprestimos.idutilizador=utilizadores.id";
            return bd.devolveSQL(sql);
        }
        public DataTable listaTodosEmprestimosPorConcluirComNomes()
        {
            string sql = @"SELECT nemprestimo,livros.nome as nomeLivro,utilizadores.nome as nomeLeitor,data_emprestimo,data_devolve,emprestimos.estado
                        FROM Emprestimos inner join livros on emprestimos.nlivro=livros.nlivro
                        inner join utilizadores on emprestimos.idutilizador=utilizadores.id where emprestimos.estado!=0";
            return bd.devolveSQL(sql);
        }

        public void alterarEstadoEmprestimo(int nemprestimo)
        {
            DataTable dadosEmprestimo = devolveDadosEmprestimo(nemprestimo);
            int nlivro = int.Parse(dadosEmprestimo.Rows[0]["nlivro"].ToString());
            int novoestadolivro, novoestadoemprestimo;

            if (dadosEmprestimo.Rows[0]["estado"].ToString() == "2") //reserva para emprestimo
            {
                novoestadolivro = 0;
                novoestadoemprestimo = 1;
            }
            else
            {
                //emprestimo para devolucao
                novoestadoemprestimo = 0;
                novoestadolivro = 1;
            }
            string sql = "SELECT * FROM livros WHERE nlivro=@nlivro";
            List<SqlParameter> parametrosBloquear = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro }
            };
            //iniciar transação
            SqlTransaction transacao = bd.iniciarTransacao(IsolationLevel.Serializable);
            DataTable dados = bd.devolveSQL(sql, parametrosBloquear, transacao);

            try
            {
                //alterar estado do livro
                sql = "UPDATE Livros SET estado=@estado WHERE nlivro=@nlivro";
                List<SqlParameter> parametrosUpdate = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro },
                    new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Int,Value=novoestadolivro },
                };
                bd.executaSQL(sql, parametrosUpdate, transacao);
                //terminar empréstimo
                sql = @"UPDATE Emprestimos SET estado=@estado WHERE nemprestimo=@nemprestimo";
                List<SqlParameter> parametrosInsert = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName="@nemprestimo",SqlDbType=SqlDbType.Int,Value=nemprestimo },
                    new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Int,Value=novoestadoemprestimo },
                };
                bd.executaSQL(sql, parametrosInsert, transacao);
                //concluir transação
                transacao.Commit();
            }
            catch
            {
                transacao.Rollback();
            }
            dados.Dispose();
        }
        public DataTable devolveDadosEmprestimo(int nemprestimo)
        {
            string sql = "SELECT * FROM emprestimos WHERE nemprestimo=@nemprestimo";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nemprestimo",SqlDbType=SqlDbType.Int,Value=nemprestimo }
            };
            return bd.devolveSQL(sql, parametros);
        }
    }
}