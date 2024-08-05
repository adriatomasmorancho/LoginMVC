using LoginMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LoginMVC.DAL
{
    public class UsuarioDAL
    {
        private DbConnection connection;
        public UsuarioDAL()
        {
            connection = new DbConnection();
        }

        public List<LoginViewModel> GetUserListFromDB()
        {
            List<LoginViewModel> jobs = new List<LoginViewModel>();

            connection.Open();
            try
            {

                string query = "SELECT * FROM Usuarios;";
                SqlCommand cmd = new SqlCommand(query, connection.GetConnection());

                // Recuperamos un lector...
                SqlDataReader records = cmd.ExecuteReader();

                while (records.Read())
                {
                    LoginViewModel user = new LoginViewModel();
                    user.Email = records.GetString(records.GetOrdinal("Email"));
                    user.Password = records.GetString(records.GetOrdinal("Password"));

                    // Agrega más campos según la estructura de tu tabla y tu clase Job
                    jobs.Add(user);
                }
                connection.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return jobs;
        }

        public void InsertUserToDB(LoginViewModel newUser)
        {
            try
            {
                this.connection.Open();
                string sql = @"INSERT INTO Usuarios( 
                                             Email, 
                                             Password) 

                                VALUES (@Email, 
                                        @Password
                                        )";


                using (SqlCommand cmd = new SqlCommand(sql, this.connection.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Email", newUser.Email);
                    cmd.Parameters.AddWithValue("@Password", newUser.Password);


                    cmd.ExecuteNonQuery();
                }
                this.connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}