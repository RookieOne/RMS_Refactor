using System;
using System.Data;
using System.Data.SqlClient;

using RMS_BusinessObjects;

namespace RMS_DALObjects
{
	/// <summary>
	/// Summary description for RateScheduleDAL.
	/// </summary>
	public class RateScheduleDAL : baseDALObject
	{

		#region "Data Fields"

		// RateSched Fields
		static int fld_RateSchedSeqNum = 0;
		static int fld_EffEndDate = 1;
		static int fld_EffStartDate = 2;
		static int fld_RateSchedName = 3;
		static int fld_StatusTypeCode = 4;

		// Contrct_RateSched Fields
		static int fld_Contrct_RateSched_ContrctIDNum = 0;
		static int fld_Contrct_RateSched_RateSchedSeqNum = 1;

		#endregion

		public RateScheduleDAL()		{}

		#region "Methods"

		public RateScheduleBO getRateScheduleWithoutRates(int rateScheduleID)
		{
			// Fill out Rate Schedule info

			SqlDataReader sqlDataRdr = base.getDataReader("SELECT * FROM RateSched WHERE RateSchedSeqNum=" + rateScheduleID);

			RateScheduleBO rateSchedule = new RateScheduleBO();

			if (sqlDataRdr.Read())
			{
				rateSchedule.ID = rateScheduleID;
				rateSchedule.RateScheduleName = sqlDataRdr["RateSchedName"].ToString();
				rateSchedule.Status = sqlDataRdr["StatusTypeCode"].ToString();

				CoverageDAL coverageData = new CoverageDAL();
				rateSchedule.Coverage = coverageData.getRateScheduleCoverage(rateScheduleID);
			}
			
			return rateSchedule;
		}

		public RateScheduleBO getRateSchedule(ref CodesManager codesMngr, int rateScheduleID)
		{
			RateScheduleBO rateSchedule = getRateScheduleWithoutRates(rateScheduleID);

			rateSchedule.Rates = getRates(ref codesMngr, rateScheduleID);
			
			return rateSchedule;
		}


		public Rate_Collection getRates(ref CodesManager codesMngr, int rateScheduleID)
		{
			// Fill out Rate Collection

			DataSet oDataSet = base.getDataSet("SELECT * FROM Rate WHERE RateSchedSeqNum=" + rateScheduleID);

			Rate_Collection Rates = new Rate_Collection();
			RateDAL rateData = new RateDAL(ref codesMngr, rateScheduleID);

			Rate_ASCRateBO ascRate = new Rate_ASCRateBO();


			foreach(DataRow dRow in oDataSet.Tables[0].Rows)
			{
				// ASC Rates are handled as an exception
				if (dRow["RateCatgryDescr"].ToString() == "ASC")
				{
					switch(dRow["RateTypeDescr"].ToString())
					{
						case "Main" :
							ascRate.ID = Convert.ToInt32(dRow["RateSeqNum"]);
							ascRate.RateScheduleID = Convert.ToInt32(dRow["RateSchedSeqNum"]);
							ascRate.Name = "ASC Rates";

							ascRate.RateCategory = dRow["RateCatgryDescr"].ToString();
							ascRate.RateType = dRow["RateTypeDescr"].ToString();

							ascRate.GroupTableID = Convert.ToInt16(dRow["RateValue"]);
							ascRate.GroupName = dRow["RateName"].ToString();

							ascRate.Priority = Convert.ToInt32(dRow["LOSNum"]);
							ascRate.Threshold = Convert.ToDouble(dRow["ThreshldNum"]);

							
							if (Convert.ToInt16(dRow["AddtnlDayRate"])==1)
							{	ascRate.StandardCPTs = true;		}
							else
							{	ascRate.StandardCPTs = false;		}

						
							ascRate.Codes = rateData.CodesData.getCodes(ref codesMngr, ascRate.ID);

							break;

						case "Group" :

							ascRate.addASCRate(dRow["RateName"].ToString(), Convert.ToDouble(dRow["RateValue"]), Convert.ToDouble(dRow["ThreshldNum"]));

							break;

						case "Rate" :

							ascRate.addRateReimbursement(Convert.ToDouble(dRow["RateValue"]));

							break;

						default: break;
					}
				}
				else	// Load rate normally
        {	Rates.addRate(rateData.getRate(dRow));		}
			}

			if ( ! (ascRate.ID==0) )
			{	Rates.addRate(ascRate);		}

			return Rates;
		}


		#endregion

		#region "Database Methods"

		public int insertRateSchedule(RateScheduleBO rateSchedule)
		{
			return updateRateSchedule(rateSchedule);
		}

		public int updateRateSchedule(RateScheduleBO rateSchedule)
		{
			SqlParameter[] sqlParams = base.getParameters("UpdateRateSched");

			if (rateSchedule.ID==0)
			{	sqlParams[fld_RateSchedSeqNum].Value = 0;	}
			else
			{	sqlParams[fld_RateSchedSeqNum].Value = rateSchedule.ID;	}

			sqlParams[fld_EffStartDate].Value = rateSchedule.Coverage.StartDate;
			sqlParams[fld_EffEndDate].Value = rateSchedule.Coverage.EndDate;

			sqlParams[fld_RateSchedName].Value = rateSchedule.RateScheduleName;
			sqlParams[fld_StatusTypeCode].Value = rateSchedule.Status;

			base.executeUpdate("UpdateRateSched", sqlParams);

			rateSchedule.ID = Convert.ToInt16(sqlParams[fld_RateSchedSeqNum].Value);
      
			sqlParams = base.getParameters("UpdateContrct_RateSched");

			sqlParams[fld_Contrct_RateSched_ContrctIDNum].Value = rateSchedule.ContractID;
			sqlParams[fld_Contrct_RateSched_RateSchedSeqNum].Value = rateSchedule.ID;

			base.executeUpdate("UpdateContrct_RateSched", sqlParams);
			
			return rateSchedule.ID;
		}

		public void deleteRateSchedule(RateScheduleBO rateSchedule)
		{
			deleteRateSchedule(rateSchedule.ID);
		}

		public void deleteRateSchedule(int rateScheduleID)
		{
			SqlParameter[] sqlParams;

			SqlDataReader sqlDataRdr = base.getDataReader("SELECT * FROM Contrct_RateSched WHERE RateSchedSeqNum=" + rateScheduleID);

			sqlParams = base.getParameters("DeleteContrct_RateSched");

			while (sqlDataRdr.Read())
			{
				sqlParams[0].Value = sqlDataRdr["ContrctIDNum"];
				sqlParams[1].Value = rateScheduleID;
				base.executeDelete("DeleteContrct_RateSched", sqlParams);
			}


			sqlParams = base.getParameters("DeleteRateAll");
			sqlParams[0].Value = rateScheduleID;
			base.executeDelete("DeleteRateAll", sqlParams);

			CoverageDAL coverageData = new CoverageDAL();
			coverageData.deleteCoverage(rateScheduleID);

			sqlParams = base.getParameters("DeleteRateSched");
			sqlParams[0].Value = rateScheduleID;
			base.executeDelete("DeleteRateSched", sqlParams);
		}


		#endregion

	}
}
