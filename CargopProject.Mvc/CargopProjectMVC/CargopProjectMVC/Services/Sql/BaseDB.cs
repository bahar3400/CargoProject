using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace CargopProjectMVC.Services.Sql
{
    public class BaseDB
    {

        #region Bağlantı
        private SqlConnection sqlConnection;
        private string connection;


        public BaseDB()
        {
            connection = ConfigurationManager.ConnectionStrings["DpConnection"].ConnectionString;
        }

        public SqlConnection GetSqlConnection()
        {
            return sqlConnection = new SqlConnection(connection);
        }

        #endregion

        #region CRUD
        public bool Crud(string spName, params SqlParameter[] parameters)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(spName, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);
                try
                {
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally { sqlConnection.Close(); }
            }
        }
        public DataTable GetAll(string spName, params SqlParameter[] sqlParameters)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(spName, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sqlParameters);
                try
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = cmd;
                    sqlDataAdapter.Fill(dataTable);
                }
                catch
                {
                    return null;
                }
                finally { sqlConnection.Close(); }
            }

            return dataTable;
        }
        #endregion

        #region IdGetAll
        public DataTable GetAllId(string text, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            DataTable dataTable = new DataTable();

            using(SqlConnection sqlConnection = GetSqlConnection()) 
            {
                SqlCommand cmd = new SqlCommand(text, sqlConnection);
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(sqlParameters);
                try
                {
                    sqlConnection.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = cmd;
                    sqlDataAdapter.Fill(dataTable);
                }
                catch { return null; }
                finally { sqlConnection.Close(); }
            }
            return dataTable;
        }
        #endregion
    }
}