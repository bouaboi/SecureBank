using SecureBank.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SecureBank.DataAccess
{
    public class clsAccountData
    {
        public static DataTable GetAllAccounts()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllAccounts", connection))
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

        public static int AddNewAccount(Account account)
        {
            int NewAccountID = -1;

            try
            {
                using (SqlConnection connection =
                       new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_AddAcounts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

         
                    command.Parameters.AddWithValue("@PinCodeHash", account.PinCodeHash);
                    command.Parameters.AddWithValue("@ClientId", account.client.ClientID);

                    // Output parameter
                    SqlParameter outputId = new SqlParameter("@NewAccountID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputId);

                    connection.Open();
                    command.ExecuteNonQuery();


                    NewAccountID = Convert.ToInt32(command.Parameters["@NewAccountID"].Value);
                }
            }

            catch(Exception ex)
            {
                throw;
            }

            return NewAccountID;
        }

        
        public static Account GetAccountByClientId(int ClientId)
        {
            Account account = null;
            Client client = null;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command =
                   new SqlCommand("SP_GetAccountByClientID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClientID", ClientId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        account = new Account();
                        account.client = new Client();  

                        account.client.ClientID = (int)reader["ClientId"];
                        account.AccountId = (int)reader["AccountId"];
                        account.client.FirstName = reader["FirstName"].ToString();
                        account.client.LastName = reader["LastName"].ToString();
                        account.AccountNumber = reader["AccountNumber"].ToString();
                        account.PinCodeHash = reader["PinCodeHash"].ToString();
                        account.Balance = (decimal)reader["Balance"];
                        account.IsActive = (bool)reader["IsActive"];
                    }
                }
            }

            return account;
        }

        public static bool UpdateAccount(Account account)
        {
            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command =
                   new SqlCommand("SP_UpdateAccount", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AccountId", account.AccountId);
                command.Parameters.AddWithValue("@PinCodeHash", account.PinCodeHash);
               

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        public static bool SoftDeleteAccount(int AccountId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_DeleteAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AccountId", AccountId);

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
        public static bool DoesAccountExist(int accountId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Accounts WHERE AccountId = @AccountId AND IsActive = 1", connection))
                {
                    command.Parameters.AddWithValue("@AccountId", accountId);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
