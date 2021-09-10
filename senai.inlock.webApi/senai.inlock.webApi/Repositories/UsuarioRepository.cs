using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        //Julia
        //private string stringConexao = @"Data Source= DESKTOP-6TRVTDV\SQLEXPRESS; initial catalog = inlock_games_tarde; user Id=sa; pwd=Senai@132";

        //Fabio 
        private string stringConexao = @"Data Source= LAPTOP-DF9B3HCO\SQLEXPRESS; initial catalog = inlock_games_tarde; user Id=sa; pwd=senai@132";

        public void AtualizarIdUrl(int idUsuario, UsuarioDomain UsuarioAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE usuarios SET email = @email, senha = @senha  WHERE idUsuario = @idUsuario";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@senha", UsuarioAtualizado.senha);
                    cmd.Parameters.AddWithValue("@email", UsuarioAtualizado.email);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UsuarioDomain BuscarPorId(int idUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT email, idUsuario FROM usuarios WHERE idUsuario = @idUsuario";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        UsuarioDomain UsuarioBuscado = new UsuarioDomain
                        {
                            idUsuario = Convert.ToInt32(reader["idUsuario"]),

                            email = reader["email"].ToString()
                        };

                        return UsuarioBuscado;
                    }

                    return null;
                }
            }
        }

        public void Cadastrar(UsuarioDomain novoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string queryInsert = "INSERT INTO usuarios (idTipoUsuario,email,senha) VALUES (@idTipoUsuario,@email,@senha)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    
                   // cmd.Parameters.AddWithValue("@idUsuario", novoUsuario.idUsuario);
                    cmd.Parameters.AddWithValue("@idTipoUsuario", novoUsuario.idTipoUsuario); 
                    cmd.Parameters.AddWithValue("@email", novoUsuario.email);
                    cmd.Parameters.AddWithValue("@senha", novoUsuario.senha);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idUsuario)
        {


            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string queryDelete = "DELETE FROM usuarios WHERE idUsuario = @idUsuario";


                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {

                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string querySelect = @"SELECT email,senha, idUsuario,usuarios.idTipoUsuario , titulo FROM usuarios 
                INNER JOIN tiposUsuarios ON usuarios.idTipoUsuario = tiposUsuarios.idTipoUsuario 
                                     WHERE email = @email and senha = @senha ";

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    //caso os dados tenham sido obtidos
                    if (rdr.Read())
                    {
                        //cria um objeto do tipo UsuarioDomain
                        UsuarioDomain usuarioBuscado = new UsuarioDomain
                        {
                            //atribui às propriedades os valores das colunas do banco de dados
                            idUsuario = Convert.ToInt32(rdr["idUsuario"]),
                            tipoDeUsuario = new TipoDeUsuarioDomain(){titulo = rdr["titulo"].ToString()},
                            idTipoUsuario = Convert.ToInt32(rdr["idTipoUsuario"]),
                            email = rdr["email"].ToString(),
                            senha = rdr["senha"].ToString()
                        };

                        //jwt.ex=  

                        //retorna o usuario buscado
                        return usuarioBuscado;

                    }

                    //Caso não encontre um email e senha correspondente, retorna null;
                    return null;

                }
            }
        }
        public List<UsuarioDomain> ListarTodos()
        {
            List<UsuarioDomain> listaUsuario = new List<UsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idUsuario, idTipoUsuario, email, senha FROM usuarios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        UsuarioDomain Usuario = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr[0]),
                            idTipoUsuario = Convert.ToInt32(rdr[1]),
                            email = rdr[2].ToString(),
                            senha = rdr[3].ToString(),                           
                        };


                        listaUsuario.Add(Usuario);
                    }
                }
            }
            return listaUsuario;
        }
    }
}
