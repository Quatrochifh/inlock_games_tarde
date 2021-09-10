using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class JogoRepository : IJogosRepository
    {
        //Julia
        //private string stringConexao = @"Data Source= DESKTOP-6TRVTDV\SQLEXPRESS; initial catalog = inlock_games_tarde; user Id=sa; pwd=Senai@132";

        //Fabio 
        private string stringConexao = @"Data Source= LAPTOP-DF9B3HCO\SQLEXPRESS; initial catalog = inlock_games_tarde; user Id=sa; pwd=senai@132";

        public void AtualizarIdUrl(int idJogo, JogosDomain JogoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE jogos SET nomeJogo = @nomeJogo, descricao = @descricao, valor = @valor WHERE idJogo = @idJogo";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@nomeJogo", JogoAtualizado.nomeJogo);
                    cmd.Parameters.AddWithValue("@descricao", JogoAtualizado.descricao);
                    cmd.Parameters.AddWithValue("@valor", JogoAtualizado.valor);
                    cmd.Parameters.AddWithValue("@idJogo", idJogo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public JogosDomain BuscarPorId(int idJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idJogo, idEstudio, nomeJogo, descricao, dataLancamento,valor FROM jogos WHERE idJogo = @idJogo";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idJogo", idJogo);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        JogosDomain JogoBuscado = new JogosDomain
                        {
                            idJogo = Convert.ToInt32(reader["idJogo"]),
                            idEstudio = Convert.ToInt32(reader["idEstudio"]),
                            nomeJogo = reader["nomeJogo"].ToString(),
                            descricao = reader["descricao"].ToString(),
                            dataLancamento = Convert.ToDateTime(reader["dataLancamento"]),
                            valor = reader["valor"].ToString()
                        };

                        return JogoBuscado;
                    }

                    return null;
                }
            }
        }

        public void Cadastrar(JogosDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string queryInsert = "INSERT INTO jogos (idEstudio,nomeJogo,descricao,dataLancamento,valor) VALUES (@idEstudio,@nomeJogo,@descricao,@dataLancamento,@valor)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idEstudio", novoJogo.idEstudio);
                    cmd.Parameters.AddWithValue("@nomeJogo", novoJogo.nomeJogo);
                    cmd.Parameters.AddWithValue("@descricao", novoJogo.descricao);
                    cmd.Parameters.AddWithValue("@dataLancamento", novoJogo.dataLancamento);
                    cmd.Parameters.AddWithValue("@valor", novoJogo.valor);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idJogo)
        {

            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string queryDelete = "DELETE FROM jogos WHERE idJogo = @idJogo";


                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {

                    cmd.Parameters.AddWithValue("@idJogo", idJogo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogosDomain> ListarTodos()
        {
            List<JogosDomain> listaJogo = new List<JogosDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idJogo, idEstudio, nomeJogo, descricao, dataLancamento, valor FROM jogos";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        JogosDomain Jogo = new JogosDomain()
                        {
                            idJogo = Convert.ToInt32(rdr[0]),
                            idEstudio = Convert.ToInt32(rdr[1]),
                            nomeJogo = rdr[2].ToString(),
                            descricao = rdr[3].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr[4]),
                            valor = rdr[5].ToString()
                        };


                        listaJogo.Add(Jogo);
                    }
                }
            }
            return listaJogo;
        }
    }
}
    

