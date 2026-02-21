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
    public class clsTransactionsData
    {
        public static DataTable GetAllTransactions()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetAllTransactions", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle error
            }
            return dt;
        }
        public static Transactions GetTransactionsByAccountID(int AccountId)
        {
           
            Transactions transaction = null;
            Account account = null;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command =
                   new SqlCommand("SP_GetTransactionsByAccountID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AccountId", AccountId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        transaction = new Transactions();
                        transaction.account = new Account();

                        transaction.TransactionId = (int)reader["TransactionId"];
                        transaction.account.AccountId = (int)reader["AccountId"];
                        transaction.TransactionDate = (DateTime)reader["TransactionDate"];
                        transaction.Amount = (decimal)reader["Amount"];
                        transaction.Description = reader["Description"].ToString();
                        transaction.FromAccountId = (int)reader["FromAccountId"];
                        transaction.ToAccountId = (int)reader["ToAccountId"];
                        transaction.TypeName = reader["TypeName"].ToString();
                        transaction.StatusName = reader["StatusName"].ToString();


                    }
                }
            }

            return transaction;
        }


        public static bool AddTransaction(Transactions transaction)
        {
            transaction.TransactionId = -1;  

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_AddTransaction", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Input parameters
                    command.Parameters.AddWithValue("@TypeId", transaction.TypeId);
                    command.Parameters.AddWithValue("@Amount", transaction.Amount);
                    command.Parameters.AddWithValue("@FromAccountId", (object)transaction.FromAccountId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ToAccountId", (object)transaction.ToAccountId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Description", transaction.Description);

                    // Output parameter
                    SqlParameter outputParam = new SqlParameter("@TransactionId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    // Return value parameter
                    SqlParameter returnParam = new SqlParameter
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(returnParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    // Get the output parameter value
                    transaction.TransactionId = (int)outputParam.Value;

                    // Get the return value (1 or 0)
                    int returnValue = (int)returnParam.Value;

                    return returnValue == 1;  // true if success, false if failure
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
