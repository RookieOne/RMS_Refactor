using System.Data;
using System.Data.SqlClient;

namespace RMS_DALObjects
{
    public class BaseDALObject
    {
        SqlConnection _connection;
        string _connection_string = "connection_string";

        void createConnection()
        {
            _connection = new SqlConnection(_connection_string);
        }

        public DataSet GetDataSet(string sql)
        {
            createConnection();
            var dataSet = new DataSet();
            new SqlDataAdapter(sql, _connection).Fill(dataSet);
            
            return dataSet;
        }

        public SqlDataReader GetDataReader(string sql)
        {
            createConnection();
            _connection.Open();

            return new SqlCommand(sql, _connection).ExecuteReader();
        }

        public void CloseConnection()
        {
            _connection.Close();
        }

        public SqlParameter[] GetParameters(string storedProcedureName)
        {
            return null; //SqlHelperParameterCache.GetSpParameterSet(strConn, storedProcedureName);	
        }

        public void ExecuteUpdate(string storedProcedureName, SqlParameter[] sqlParams)
        {
            sqlParams[sqlParams.Length - 3].Value = "RMS";
            //SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, storedProcedureName, sqlParams);
        }

        public void ExecuteDelete(string storedProcedureName, SqlParameter[] sqlParams)
        {
            //SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, storedProcedureName, sqlParams);
        }
    }
}