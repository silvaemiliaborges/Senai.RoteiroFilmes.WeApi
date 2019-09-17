using Senai.RoteiroFilmess.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.RoteiroFilmess.WebApi.Repositories
{
    public class FilmesRepository
    {
        
            private string StringConexao = "Data Source=.\\SqlExpress; Initial Catalog=T_RoteiroFilmes;User Id=sa;Pwd=132";

            public List<Filmes> Listar()
            {
                List<Filmes> filmes = new List<Filmes>();

                string Query = "select F.IdFilme, F.Titulo, G.IdGenero, G.Nome as NomeGenero from Filmes F inner join Generos G on F.IdGenero = G.IdGenero;";

                using (SqlConnection con = new SqlConnection(StringConexao))
                {
                    con.Open();
                    SqlDataReader sdr;

                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        // executar a query 
                        sdr = cmd.ExecuteReader();

                        //percorrer os dados 
                        while (sdr.Read())
                        {
                            Filmes filme = new Filmes
                            {
                                IdFilme = Convert.ToInt32(sdr["IdFilme"]),
                                Titulo = sdr["Titulo"].ToString(),
                                Genero = new Generos
                                {
                                    IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                    Nome = sdr["NomeGenero"].ToString()

                                }
                            };
                            filmes.Add(filme);
                        }
                    }
                }
                return filmes;

            }

            public void Cadastrar(Filmes filmesDomain)
            {
                string Query = "insert into Filmes (Titulo, IdGenero) values (@Titulo, @IdGenero)";

                using (SqlConnection con = new SqlConnection(StringConexao))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(Query, con);

                    cmd.Parameters.AddWithValue("@Titulo", filmesDomain.Titulo);
                    cmd.Parameters.AddWithValue("@IdGenero", filmesDomain.IdGenero);

                    cmd.ExecuteNonQuery();
                }
            }

            public Filmes BuscarPorId(int id)
            {
                string Query = "select F.IdFilme, F.Titulo, G.IdGenero, G.Nome as NomeGenero from Filmes F inner join Generos G on F.IdGenero = G.IdGenero where IdFilme = @IdFilme";

                using (SqlConnection con = new SqlConnection(StringConexao))
                {
                    con.Open();
                    SqlDataReader sdr;

                    using (SqlCommand cmd = new SqlCommand(Query, con))
                    {
                        cmd.Parameters.AddWithValue("@IdFilme", id);
                        sdr = cmd.ExecuteReader();

                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                Filmes filme = new Filmes
                                {
                                    IdFilme = Convert.ToInt32(sdr["IdFilme"]),
                                    Titulo = sdr["Titulo"].ToString(),
                                    Genero = new Generos
                                    {
                                        IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                        Nome = sdr["NomeGenero"].ToString()
                                    }
                                };
                                return filme;

                            }
                        }
                    }
                    return null;
                }
            }

            public void Deletar(int id)
            {
                string Query = "delete from Filmes where IdFilme = @id";
                using (SqlConnection con = new SqlConnection(StringConexao))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(Query, con);

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }

