using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        //Julia
        //private string stringConexao = @"Data Source= DESKTOP-6TRVTDV\SQLEXPRESS; initial catalog = inlock_games_tarde; user Id=sa; pwd=Senai@132";

        //Fabio 
        private string stringConexao = @"Data Source= LAPTOP-DF9B3HCO\SQLEXPRESS; initial catalog = inlock_games_tarde; user Id=sa; pwd=senai@132";

        public void AtualizarIdUrl(int idEstudio, EstudioDomain EstudioAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE estudios SET nomeEstudio = @nomeEstudio WHERE idEstudio = @idEstudio";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@nomeEstudio", EstudioAtualizado.nomeEstudio);
                    cmd.Parameters.AddWithValue("@idEstudio", idEstudio);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        public EstudioDomain BuscarPorId(int idEstudio)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT nomeEstudio, idEstudio FROM estudios WHERE idEstudio = @idEstudio";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idEstudio", idEstudio);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        EstudioDomain EstudioBuscado = new EstudioDomain
                        {
                            idEstudio = Convert.ToInt32(reader["idEstudio"]),

                            nomeEstudio = reader["nomeEstudio"].ToString()
                        };

                        return EstudioBuscado;
                    }

                    return null;
                }
            }
        }

        public void Cadastrar(EstudioDomain novoEstudio)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string queryInsert = "INSERT INTO estudios (nomeEstudio) VALUES (@nomeEstudio)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@nomeEstudio", novoEstudio.nomeEstudio);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idEstudio)
        {
           
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
              
                string queryDelete = "DELETE FROM estudios WHERE idEstudio = @idEstudio";

              
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                   
                    cmd.Parameters.AddWithValue("@idEstudio", idEstudio);

                    con.Open();
                
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<EstudioDomain> ListarTodos()
        {
            List<EstudioDomain> listaEstudio = new List<EstudioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idEstudio, nomeEstudio FROM estudios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                       
                        EstudioDomain estudio = new EstudioDomain()
                        {
                            idEstudio = Convert.ToInt32(rdr[0]),
                            
                            nomeEstudio = rdr[1].ToString()
                        };


                        listaEstudio.Add(estudio);
                    }
                }
            }
             return listaEstudio;
        }
    }
}
