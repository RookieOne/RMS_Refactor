using System;
using System.Data;
using System.Data.OleDb;

namespace RMS_DALObjects
{
	/// <summary>
	/// Summary description for AccessDALObject.
	/// </summary>
	public class AccessDALObject
	{

		#region "Variables"

		string accessConnStr;
    OleDbConnection accessConnection;
    OleDbDataAdapter accessDataAdapter;

		#endregion

		public AccessDALObject()	{		}

		public void setDynamicAccessDB(string filePath)
		{
			accessConnStr = "PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=" + filePath + ";Mode=Share Deny None;Jet OLEDB:Database Locking Mode=0;";
			accessConnection = new OleDbConnection(accessConnStr);
		}
			
		public DataSet getDataSet(string strSQL)
		{
			DataSet tempDS = new DataSet();

			try
			{
				accessDataAdapter = new OleDbDataAdapter(strSQL, accessConnection);

				accessDataAdapter.Fill(tempDS, "Data");
			}
			catch(OleDbException e)
			{
				string msg = e.Message;
			}

			return tempDS;
		}

		public DataTable getTables()
		{
			DataTable tableToReturn = new DataTable();

			try
			{
				accessConnection.Open();
        
				tableToReturn = accessConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
			}
			catch(OleDbException e)
			{
				string msg = e.Message;
			}
			finally
			{
				accessConnection.Close();
			}

			return tableToReturn;
		}



	}
}
