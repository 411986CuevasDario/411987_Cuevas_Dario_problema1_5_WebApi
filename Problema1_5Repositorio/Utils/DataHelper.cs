

using Microsoft.Data.SqlClient;
using System.Data;

namespace Problema1_5.Utils
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(Problema1_5Repositorio.Properties.Resources.connectionString);
        }

        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }
        public  SqlConnection GetConnection()
        {
            return _connection;
        }

        public DataTable ExecuteSPQuery(string sp)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());


            }
            catch (SqlException)
            {
                dt = null;
            }
            finally
            {
                _connection.Close();

            }
            return dt;

        }
        public DataTable GetById(string sp,int id)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                dt.Load(cmd.ExecuteReader());


            }
            catch (SqlException)
            {
                dt = null;
            }
            finally
            {
                _connection.Close();

            }
            return dt;

        }
        public bool ExecuteSPQuery(SqlCommand sp)
        {

            try
            {
                _connection.Open();
                sp.Connection = _connection;
                sp.ExecuteNonQuery();

            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                _connection.Close();

            }
                return true;


            }
        }

}
