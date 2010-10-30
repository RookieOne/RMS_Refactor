using System;
using System.Data;
using System.Data.SqlClient;
//using Microsoft.ApplicationBlocks.Data;

namespace RMS_DALObjects
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class baseDALObject
	{
		#region "Variables"

		private string strConn = "connection_string";
		private SqlConnection sqlConn;

		#endregion

		#region "Constructors"

		public baseDALObject(){}

		#endregion

		#region "Methods"

		public DataSet getDataSet(string strSQL)
		{
			try
			{
			    return null;
			    //return SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSQL);
			}
			catch (SqlException e)
			{
				string msg = e.Message;
				return null;	
			}
		}

		public SqlDataReader getDataReader(string strSQL)
		{
			try
			{
				sqlConn = new SqlConnection(strConn);
				sqlConn.Open();

				SqlCommand sqlCmd = new SqlCommand(strSQL, sqlConn);

				return sqlCmd.ExecuteReader();
			}
			catch (SqlException e)
			{
				string msg = e.Message;
				return null;	
			}
		}

		public void closeConnection()
		{
			try
			{
				sqlConn.Close();
			}
			catch (SqlException e)
			{
				string msg = e.Message;
			}
		}




		public SqlParameter[] getParameters(string storedProcedureName)
		{
            return null; //SqlHelperParameterCache.GetSpParameterSet(strConn, storedProcedureName);	
           }

		public void executeUpdate(string storedProcedureName, SqlParameter[] sqlParams)
		{
			try
			{
				sqlParams[sqlParams.Length-3].Value = "RMS";
				//SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, storedProcedureName, sqlParams);
			}
			catch (SqlException e)
			{	string msg = e.Message;	}
		}

		public void executeDelete(string storedProcedureName, SqlParameter[] sqlParams)
		{
			try
			{
				//SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, storedProcedureName, sqlParams);
			}
			catch(SqlException e)
			{	string msg = e.Message;	}
		}

		#endregion

	}
}
