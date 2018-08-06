using ExemploMVC02.Models;
using ExemploMVC02.Repositorio.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExemploMVC02.Repositorio
{
    public class RecrutadoraRepositorio
    {

        public List<Recrutadora> ObterTodos()
        {

            List<Recrutadora> recrutadoras = new List<Recrutadora>();

            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "SELECT id, nome, cpf, tempo_empresa, salário FROM recrutadoras";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                Recrutadora recrutadora = new Recrutadora()

                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Nome = linha[1].ToString(),
                    CPF = linha[2].ToString(),
                    TempoEmpresa = Convert.ToByte(linha[3].ToString()),
                    Salário = Convert.ToDecimal(linha[4].ToString())
                };
                recrutadoras.Add(recrutadora);
            }

            return recrutadoras;
        }

        public int Cadastrar(Recrutadora recrutadora)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "INSERT INTO recrutadoras (nome, salário, cpf, tempo_empresa)OUTPUT INSERTED.ID VALUES (@NOME, @SALARIO, @CPF, @TEMPO_EMPRESA)";
            comando.Parameters.AddWithValue("@NOME", recrutadora.Nome);
            comando.Parameters.AddWithValue("@SALARIO", recrutadora.Salário);
            comando.Parameters.AddWithValue("@CPF", recrutadora.CPF);
            comando.Parameters.AddWithValue("@TEMPO_EMPRESA", recrutadora.TempoEmpresa);
            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            return id;
        }



        public bool Alterar(Recrutadora recrutadora)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "UPDATE recrutadoras SET nome = @NOME, cpf = @CPF, tempo_empresa = @TEMPO_EMPRESA, salário = @SALARIO WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", recrutadora.Nome);
            comando.Parameters.AddWithValue("@CPF", recrutadora.CPF);
            comando.Parameters.AddWithValue("@TEMPO_EMPRESA", recrutadora.TempoEmpresa);
            comando.Parameters.AddWithValue("@SALARIO", recrutadora.Salário);
            comando.Parameters.AddWithValue("@ID", recrutadora.Id);
            return comando.ExecuteNonQuery() == 1;
        }



        public bool Excluir(int id)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "DELETE FROM recrutadoras WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            return comando.ExecuteNonQuery() == 1;
        }



        public Recrutadora ObterPeloId(int id)
        {
            Recrutadora recrutadora = null;
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "SELECT nome, cpf, tempo_empresa, salário FROM recrutadoras WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            if (tabela.Rows.Count == 1)
            {
                recrutadora = new Recrutadora();
                recrutadora.Id = id;
                recrutadora.Nome = tabela.Rows[0][0].ToString();
                recrutadora.CPF = tabela.Rows[0][1].ToString();
                recrutadora.TempoEmpresa = Convert.ToByte(tabela.Rows[0][2].ToString());
                recrutadora.Salário = Convert.ToDecimal(tabela.Rows[0][3].ToString());

            }
            return recrutadora;
        }
    }
}