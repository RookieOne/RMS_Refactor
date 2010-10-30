using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using RMS_BusinessObjects;

namespace RMS_DALObjects
{
	/// <summary>
	/// Summary description for PassThrusDAL.
	/// </summary>
	public class PassThrusDAL : baseDALObject
	{

		#region "Variables"

		private RateSchedulePassThrusBO var_RateSchedulePassThrus;

		#endregion


		#region "Constructors"

		public PassThrusDAL()	{}
		public PassThrusDAL(int rateScheduleID)	
		{
			this.loadRateSchedulePassThrus(rateScheduleID);
		}

		#endregion

		#region "Properties"

		public RateSchedulePassThrusBO RateSchedulePassThrus
		{
			get{return var_RateSchedulePassThrus;}
			set{var_RateSchedulePassThrus = value;}
		}

		#endregion

		#region "Methods"

		public void loadRateSchedulePassThrus(int rateScheduleID)
		{
			this.RateSchedulePassThrus = new RateSchedulePassThrusBO();

			SqlDataReader sqlDataRdr = base.getDataReader("SELECT RateSeqNum, RateName FROM Rate WHERE RateCatgryDescr='PassThru' AND RateSchedSeqNum=" + rateScheduleID);

			while(sqlDataRdr.Read())
			{
				this.RateSchedulePassThrus.addPassThru(Convert.ToInt32(sqlDataRdr["RateSeqNum"]), sqlDataRdr["RateName"].ToString());
			}

			base.closeConnection();
		}

		public PassThrusBO getPassThrus(int rateID)
		{
			SqlDataReader sqlDataRdr = base.getDataReader("SELECT RateCode FROM RateCode WHERE (RateTypeCode='PassThru') AND RateSeqNum=" + rateID);

			PassThrusBO myPassThrus = new PassThrusBO(this.RateSchedulePassThrus);

			string rateCode; int passThruRateID;
			while(sqlDataRdr.Read())
			{
				rateCode = sqlDataRdr["RateCode"].ToString();
				
				if ( (rateCode=="All") || (rateCode=="None") )
				{	myPassThrus.addPassThru(new PassThruStruct(0, rateCode));	}
				else
				{	
					passThruRateID = Convert.ToInt32(rateCode);
					myPassThrus.addPassThru(new PassThruStruct(passThruRateID, this.RateSchedulePassThrus.getPassThruName(passThruRateID)));	
				}	
			}

			base.closeConnection();

			return myPassThrus;
		}



		#endregion

		#region "Database Methods"

		public void insertPassThrus(PassThrusBO passThrus, RateBO rate)
		{
			updatePassThrus(passThrus, rate);
		}

		public void updatePassThrus(PassThrusBO passThrus, RateBO rate)
		{
			SqlParameter[] sqlParams;

			ArrayList passThrusList = passThrus.getArrayListOfPassThrus();

			ArrayList passThrusCodeList = new ArrayList();

			PassThruStruct passThru;
			for(int k=0; k<passThrusList.Count; k++)
			{
				passThru = (PassThruStruct) passThrusList[k];

				if ( (passThru.RateName=="All") || (passThru.RateName=="None") )
				{	passThrusCodeList.Add(passThru.RateName);	}
				else
				{	passThrusCodeList.Add(passThru.RateID.ToString());	}
			}

			DataSet oDataSet = base.getDataSet("SELECT * FROM RateCode WHERE RateTypeCode='PassThru' AND RateSeqNum=" + rate.ID);

			sqlParams = base.getParameters("DeleteRateCode");

			foreach(DataRow dRow in oDataSet.Tables[0].Rows)
			{
				if (! passThrusCodeList.Contains(dRow["RateCode"].ToString()) )
				{
					sqlParams[0].Value = dRow["RateCodeSeqNum"];
					sqlParams[1].Value = dRow["RateSeqNum"];
					sqlParams[2].Value = dRow["RateSchedSeqNum"];
					base.executeDelete("DeleteRateCode", sqlParams);
				}
				else
				{	passThrusCodeList.Remove(dRow["RateCode"].ToString());	}
			}


			sqlParams = base.getParameters("UpdateRateCode");

			for(int k=0; k<passThrusCodeList.Count; k++)
			{
				foreach(SqlParameter sqlParam in sqlParams)
				{
					switch(sqlParam.ParameterName)
					{
						case "@RateCodeSeqNum" :	sqlParam.Value = DBNull.Value; break;
						case "@RateSeqNum" : sqlParam.Value = rate.ID; break;
						case "@RateSchedSeqNum" : sqlParam.Value = rate.RateScheduleID; break;
						case "@RateTypeCode" : sqlParam.Value = "PassThru"; break;
						case "@RateCode" : sqlParam.Value = passThrusCodeList[k].ToString(); break;
						default : break;
					}
				}
				base.executeUpdate("UpdateRateCode", sqlParams);
			}
		}

		#endregion
	}
}
