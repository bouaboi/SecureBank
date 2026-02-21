using SecureBank.DataAccess;
using SecureBank.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureBank.DataAccess
{
    public class clsLoginData
    {
        public static User ValidateUser(string username, string passwordHash)  // Changed parameters
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_ValidateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User user = new User();  // Create new object
                            user.UserID = (int)reader["UserID"];
                            user.UserName = (string)reader["UserName"];
                            user.FirstName = (string)reader["FirstName"];
                            user.LastName = (string)reader["LastName"];

                            return user;  // Return the object
                        }
                        else
                        {
                            return null;  // Not found
                        }
                    }
                }
            }
            catch
            {
                return null;  // Error
            }
        }
        public static Client ValidateClient(string username, string passwordHash)  // Changed parameters
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_ValidateClient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Client client = new Client();  // Create new object
                            client.ClientID = (int)reader["ClientId"];
                            client.UserName = (string)reader["UserName"];
                            client.FirstName = (string)reader["FirstName"];
                            client.LastName = (string)reader["LastName"];
                            client.Email = (string)reader["Email"];

                            return client;  // Return the object
                        }
                        else
                        {
                            return null;  // Not found
                        }
                    }
                }
            }
            catch
            {
                return null;  // Error
            }
        }

    }
}
