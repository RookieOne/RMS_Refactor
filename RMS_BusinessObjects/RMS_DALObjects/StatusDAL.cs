using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace RMS_DALObjects
{
	/// <summary>
	/// Summary description for statusTypeDAL.
	/// </summary>
	public class StatusDAL : BaseDALObject
	{
		public StatusDAL(){}

		#region "Methods"

		public ArrayList getArrayOfStatusCodes()
		{
			SqlDataReader sqlDataRdr = this.GetDataReader("SELECT StatusTypeCode FROM StatusType");

			ArrayList statusList = new ArrayList();

			while(sqlDataRdr.Read())
			{
				statusList.Add(sqlDataRdr["StatusTypeCode"].ToString());
			}

			this.CloseConnection();

			return statusList;
		}



		public DataTable getStatusTable()
		{
			SqlDataReader sqlDataRdr = this.GetDataReader("SELECT StatusTypeCode, StatusTypeDescr FROM StatusType_View");

			DataTable tableToReturn = new DataTable();
			tableToReturn.Columns.Add("StatusTypeCode", Type.GetType("System.String"));
			tableToReturn.Columns.Add("Status", Type.GetType("System.String"));
				
			DataRow newRow;
			while(sqlDataRdr.Read())
			{
				newRow = tableToReturn.NewRow();

				newRow["StatusTypeCode"] = sqlDataRdr["StatusTypeCode"].ToString();
				newRow["Status"] = sqlDataRdr["StatusTypeDescr"].ToString();
				
				tableToReturn.Rows.Add(newRow);
			}

			this.CloseConnection();

			return tableToReturn;
		}

		public string getStatus(string statusTypeCode)
		{
			SqlDataReader sqlDataRdr = this.GetDataReader("SELECT StatusTypeDescr FROM StatusType_View WHERE StatusTypeCode='" + statusTypeCode + "'");

			string statusToReturn;
			if (sqlDataRdr.Read())
			{
				statusToReturn = sqlDataRdr["StatusTypeDescr"].ToString();
			}
			else
			{ statusToReturn = "";	}

			this.CloseConnection();

			return statusToReturn;
		}

		#endregion
	}
}
