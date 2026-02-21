using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SecureBank.Models;


namespace SecureBank.DataAccess
{
    public class clsClientsData
    {
        public static DataTable GetAllClients()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllClients", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log error
            }
            return dt;
        }

        public static int AddNewClient(Client client)
        {
            int newClientId = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_AddClients", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FirstName", client.FirstName);
                    command.Parameters.AddWithValue("@LastName", client.LastName);
                    command.Parameters.AddWithValue("@Email", client.Email);
                    command.Parameters.AddWithValue("@Phone", client.Phone);
                    command.Parameters.AddWithValue("@Address", client.Address);
                    command.Parameters.AddWithValue("@UserName", client.UserName);
                    command.Parameters.AddWithValue("@PasswordHash", client.PasswordHash);

                    SqlParameter outputPersonId = new SqlParameter("@NewPersonID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputPersonId);

                    SqlParameter outputClientId = new SqlParameter("@NewClientID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputClientId);

                    connection.Open();
                    command.ExecuteNonQuery();

                    client.PersonID = (int)command.Parameters["@NewPersonID"].Value;
                    newClientId = (int)command.Parameters["@NewClientID"].Value;
                }
            }
            catch (Exception ex)
            {
               
                throw;
            }

            return newClientId; 
        }
        public static Client GetClientById(int ClientId)
        {
            Client client = null;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command =
                   new SqlCommand("SP_GetClientById", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClientID", ClientId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Client(); 

                        client.ClientID = (int)reader["ClientID"];
                        client.FirstName = reader["FirstName"].ToString();
                        client.LastName = reader["LastName"].ToString();
                        client.Email = reader["Email"].ToString();
                        client.Phone = reader["Phone"].ToString();
                        client.Address = reader["Address"].ToString();
                        client.IsActive = (bool)reader["IsActive"];
                    }
                }
            }

            return client;
        }

        public static bool UpdateClient(Client client)
        {
            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command =
                   new SqlCommand("SP_UpdateClients", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ClientID", client.ClientID);
                command.Parameters.AddWithValue("@FirstName", client.FirstName);
                command.Parameters.AddWithValue("@LastName", client.LastName);
                command.Parameters.AddWithValue("@Email", client.Email);
                command.Parameters.AddWithValue("@Phone", client.Phone);
                command.Parameters.AddWithValue("@Address", client.Address);

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool SoftDeleteClient(int clientId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_DeleteClients", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClientID", clientId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0; 
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }


        public static bool ActivateClient(int clientId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_ActivateClients", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClientID", clientId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }


    }
}