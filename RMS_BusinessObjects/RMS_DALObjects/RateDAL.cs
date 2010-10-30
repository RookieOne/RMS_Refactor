using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using RMS_BusinessObjects;

namespace RMS_DALObjects
{
	/// <summary>
	/// Summary description for RateDAL.
	/// </summary>
	public class RateDAL : baseDALObject
	{
		#region "Variables"

		CodesDAL var_CodesDAL;
		PassThrusDAL var_PassThrusDAL;
		CodesManager var_CodesManager;

		static int fld_RateSeqNum = 0;
		static int fld_RateSchedSeqNum = 1;

		static int fld_AddtnlDayRate = 2;
		//static int fld_ExcludePassThruInd = 3;
		static int fld_InOutPatientInd = 4;
		static int fld_LOSNum = 5;
		static int fld_RateCatgryDescr = 6;
		static int fld_RateName = 7;
		static int fld_RateTypeDescr = 8;
		static int fld_RateValue = 9;
		static int fld_ThreshldNum = 10;

		#endregion

		#region "Constructors"

		public RateDAL(ref CodesManager in_CodesManager, int rateScheduleID)	
		{
			this.CodesMngr = in_CodesManager;

			this.CodesData = new CodesDAL();
			this.PassThrusData = new PassThrusDAL(rateScheduleID);
		}

		#endregion

		#region "Properties"

		public CodesDAL CodesData
		{
			get{return var_CodesDAL;}
			set{var_CodesDAL = value;}
		}

		public PassThrusDAL PassThrusData
		{
			get{return var_PassThrusDAL;}
			set{var_PassThrusDAL = value;}
		}

		public CodesManager CodesMngr
		{
			get{return var_CodesManager;}
			set{var_CodesManager = value;}
		}

		#endregion

		#region "Methods"

		public RateBO getRate(int RateID)
		{
			DataSet oDataSet = base.getDataSet("SELECT * FROM Rate WHERE RateSeqNum=" + RateID);

			if (oDataSet.Tables[0].Rows.Count>0)
			{
				return getRate(oDataSet.Tables[0].Rows[0]);
			}
			else
			{	return null;	}
		}

		public RateBO getRate(DataRow rateRow)
		{
			switch(rateRow["RateCatgryDescr"].ToString())
			{
				case "StopLoss" :
					Rate_StopLossBO stopLossToAdd = new Rate_StopLossBO();

					stopLossToAdd.ID = (int) rateRow["RateSeqNum"];
					stopLossToAdd.RateScheduleID = (int) rateRow["RateSchedSeqNum"];
					stopLossToAdd.InOut = Convert.ToChar(rateRow["InOutPatientInd"]);
					stopLossToAdd.RateCategory = rateRow["RateCatgryDescr"].ToString();
					stopLossToAdd.RateType = rateRow["RateTypeDescr"].ToString();
					stopLossToAdd.Name = rateRow["RateName"].ToString();
					stopLossToAdd.Codes = this.CodesData.getCodes(ref this.var_CodesManager, (int) rateRow["RateSeqNum"]);

					stopLossToAdd.Threshold = Convert.ToDouble(rateRow["RateValue"]);
					stopLossToAdd.POC = Convert.ToDouble(rateRow["ThreshldNum"]);
					stopLossToAdd.DailyCap = Convert.ToDouble(rateRow["AddtnlDayRate"]);
					stopLossToAdd.PassThrus = this.PassThrusData.getPassThrus((int) rateRow["RateSeqNum"]);

					return stopLossToAdd;

				case "LessorOf" :
					Rate_LessorOfBO lessorOfToAdd = new Rate_LessorOfBO();

					lessorOfToAdd.ID = (int) rateRow["RateSeqNum"];
					lessorOfToAdd.RateScheduleID = (int) rateRow["RateSchedSeqNum"];
					lessorOfToAdd.InOut = Convert.ToChar(rateRow["InOutPatientInd"]);
					lessorOfToAdd.RateCategory = rateRow["RateCatgryDescr"].ToString();
					lessorOfToAdd.RateType = rateRow["RateTypeDescr"].ToString();
					lessorOfToAdd.Name = rateRow["RateName"].ToString();
					lessorOfToAdd.Codes = this.CodesData.getCodes(ref this.var_CodesManager, (int) rateRow["RateSeqNum"]);

					return lessorOfToAdd;

				case "Ignore" :
					Rate_IgnoreBO ignoreToAdd = new Rate_IgnoreBO();

					ignoreToAdd.ID = (int) rateRow["RateSeqNum"];
					ignoreToAdd.RateScheduleID = (int) rateRow["RateSchedSeqNum"];
					ignoreToAdd.InOut = Convert.ToChar(rateRow["InOutPatientInd"]);
					ignoreToAdd.RateCategory = rateRow["RateCatgryDescr"].ToString();
					ignoreToAdd.RateType = rateRow["RateTypeDescr"].ToString();
					ignoreToAdd.Name = rateRow["RateName"].ToString();
					ignoreToAdd.Codes = this.CodesData.getCodes(ref this.var_CodesManager, (int) rateRow["RateSeqNum"]);

					return ignoreToAdd;

				case "Floor" :
					Rate_FloorBO floorToAdd = new Rate_FloorBO();

					floorToAdd.ID = (int) rateRow["RateSeqNum"];
					floorToAdd.RateScheduleID = (int) rateRow["RateSchedSeqNum"];
					floorToAdd.InOut = Convert.ToChar(rateRow["InOutPatientInd"]);
					floorToAdd.RateCategory = rateRow["RateCatgryDescr"].ToString();
					floorToAdd.RateType = rateRow["RateTypeDescr"].ToString();
					floorToAdd.Name = rateRow["RateName"].ToString();
					floorToAdd.Codes = this.CodesData.getCodes(ref this.var_CodesManager, (int) rateRow["RateSeqNum"]);

					floorToAdd.Rate = Convert.ToDouble(rateRow["RateValue"]);

					return floorToAdd;

				case "Ceiling" :
					Rate_CeilingBO ceilingToAdd = new Rate_CeilingBO();

					ceilingToAdd.ID = (int) rateRow["RateSeqNum"];
					ceilingToAdd.RateScheduleID = (int) rateRow["RateSchedSeqNum"];
					ceilingToAdd.InOut = Convert.ToChar(rateRow["InOutPatientInd"]);
					ceilingToAdd.RateCategory = rateRow["RateCatgryDescr"].ToString();
					ceilingToAdd.RateType = rateRow["RateTypeDescr"].ToString();
					ceilingToAdd.Name = rateRow["RateName"].ToString();
					ceilingToAdd.Codes = this.CodesData.getCodes(ref this.var_CodesManager, (int) rateRow["RateSeqNum"]);

					ceilingToAdd.Rate = Convert.ToDouble(rateRow["RateValue"]);

					return ceilingToAdd;

				case "PerDiem" :
					Rate_PerDiemBO perDiemToAdd = new Rate_PerDiemBO();

					perDiemToAdd.ID = (int) rateRow["RateSeqNum"];
					perDiemToAdd.RateScheduleID = (int) rateRow["RateSchedSeqNum"];
					perDiemToAdd.InOut = Convert.ToChar(rateRow["InOutPatientInd"]);
					perDiemToAdd.RateCategory = rateRow["RateCatgryDescr"].ToString();
					perDiemToAdd.RateType = rateRow["RateTypeDescr"].ToString();
					perDiemToAdd.Name = rateRow["RateName"].ToString();
					perDiemToAdd.Codes = this.CodesData.getCodes(ref this.var_CodesManager, (int) rateRow["RateSeqNum"]);

					perDiemToAdd.Rate = Convert.ToDouble(rateRow["RateValue"]);

					return perDiemToAdd;

				case "BaseRate" :
					Rate_BaseRateBO baseRateToAdd = new Rate_BaseRateBO();

					baseRateToAdd.ID = (int) rateRow["RateSeqNum"];
					baseRateToAdd.RateScheduleID = (int) rateRow["RateSchedSeqNum"];
					baseRateToAdd.InOut = Convert.ToChar(rateRow["InOutPatientInd"]);
					baseRateToAdd.RateCategory = rateRow["RateCatgryDescr"].ToString();
					baseRateToAdd.RateType = rateRow["RateTypeDescr"].ToString();
					baseRateToAdd.Name = rateRow["RateName"].ToString();
					baseRateToAdd.Codes = this.CodesData.getCodes(ref this.var_CodesManager, baseRateToAdd.ID);

					baseRateToAdd.Rate = Convert.ToDouble(rateRow["RateValue"]);
					baseRateToAdd.PassThrus = this.PassThrusData.getPassThrus(baseRateToAdd.ID);

					baseRateToAdd.WeightTable = this.CodesData.getWeightTable(baseRateToAdd.ID);

					return baseRateToAdd;

					
				case "FFS" :

				switch (rateRow["RateTypeDescr"].ToString())
				{
					case "CaseRate" :

						if (rateRow["InOutPatientInd"].ToString()=="I")
						{
							Rate_FFS_In_CaseRateBO ffsCaseRateToAdd = new Rate_FFS_In_CaseRateBO();

							ffsCaseRateToAdd.ID = (int) rateRow["RateSeqNum"];
							ffsCaseRateToAdd.RateScheduleID = (int) rateRow["RateSchedSeqNum"];
							ffsCaseRateToAdd.InOut = Convert.ToChar(rateRow["InOutPatientInd"]);
							ffsCaseRateToAdd.RateCategory = rateRow["RateCatgryDescr"].ToString();
							ffsCaseRateToAdd.RateType = rateRow["RateTypeDescr"].ToString();
							ffsCaseRateToAdd.Name = rateRow["RateName"].ToString();
							ffsCaseRateToAdd.Codes = this.CodesData.getCodes(ref this.var_CodesManager, (int) rateRow["RateSeqNum"]);

							ffsCaseRateToAdd.Rate = Convert.ToDouble(rateRow["RateValue"]);
							ffsCaseRateToAdd.AddtnlDayRate = Convert.ToDouble(rateRow["AddtnlDayRate"]);
							ffsCaseRateToAdd.LOS = Convert.ToUInt16(rateRow["LOSNum"]);
							ffsCaseRateToAdd.PassThrus = this.PassThrusData.getPassThrus((int) rateRow["RateSeqNum"]);

							return ffsCaseRateToAdd;
						}
						else
						{
							Rate_FFS_Out_CaseRateBO ffsOutCaseRateToAdd = new Rate_FFS_Out_CaseRateBO();

							ffsOutCaseRateToAdd.ID = (int) rateRow["RateSeqNum"];
							ffsOutCaseRateToAdd.RateScheduleID = (int) rateRow["RateSchedSeqNum"];
							ffsOutCaseRateToAdd.InOut = Convert.ToChar(rateRow["InOutPatientInd"]);
							ffsOutCaseRateToAdd.RateCategory = rateRow["RateCatgryDescr"].ToString();
							ffsOutCaseRateToAdd.RateType = rateRow["RateTypeDescr"].ToString();
							ffsOutCaseRateToAdd.Name = rateRow["RateName"].ToString();
							ffsOutCaseRateToAdd.Codes = this.CodesData.getCodes(ref this.var_CodesManager, (int) rateRow["RateSeqNum"]);

							ffsOutCaseRateToAdd.Rate = Convert.ToDouble(rateRow["RateValue"]);
							ffsOutCaseRateToAdd.Priority = Convert.ToUInt16(rateRow["LOSNum"]);
							ffsOutCaseRateToAdd.PassThrus = this.PassThrusData.getPassThrus((int) rateRow["RateSeqNum"]);

							return ffsOutCaseRateToAdd;
						}

						
					case "POC" :
						Rate_FFS_POCBO ffsPOCToAdd = new Rate_FFS_POCBO();

						ffsPOCToAdd.ID = (int) rateRow["RateSeqNum"];
						ffsPOCToAdd.RateScheduleID = (int) rateRow["RateSchedSeqNum"];
						ffsPOCToAdd.InOut = Convert.ToChar(rateRow["InOutPatientInd"]);
						ffsPOCToAdd.RateCategory = rateRow["RateCatgryDescr"].ToString();
						ffsPOCToAdd.RateType = rateRow["RateTypeDescr"].ToString();
						ffsPOCToAdd.Name = rateRow["RateName"].ToString();
						ffsPOCToAdd.Codes = this.CodesData.getCodes(ref this.var_CodesManager, (int) rateRow["RateSeqNum"]);

						ffsPOCToAdd.Rate = Convert.ToDouble(rateRow["RateValue"]);
						ffsPOCToAdd.PassThrus = this.PassThrusData.getPassThrus((int) rateRow["RateSeqNum"]);

						return ffsPOCToAdd;

					default: break;
				}
					break;


				case "PassThru" :
					Rate_PassThruBO passThruToAdd = new Rate_PassThruBO();

					passThruToAdd.ID = (int) rateRow["RateSeqNum"];
					passThruToAdd.RateScheduleID = (int) rateRow["RateSchedSeqNum"];
					passThruToAdd.InOut = Convert.ToChar(rateRow["InOutPatientInd"]);
					passThruToAdd.RateCategory = rateRow["RateCatgryDescr"].ToString();
					passThruToAdd.RateType = rateRow["RateTypeDescr"].ToString();
					passThruToAdd.Name = rateRow["RateName"].ToString();
					passThruToAdd.Codes = this.CodesData.getCodes(ref this.var_CodesManager, (int) rateRow["RateSeqNum"]);

					passThruToAdd.Rate = Convert.ToDouble(rateRow["RateValue"]);
					passThruToAdd.Threshold = Convert.ToDouble(rateRow["ThreshldNum"]);

					return passThruToAdd;

				default: 
					return null;
			}

			return null;
		}

		#endregion

		#region "Database Methods"
		

		public int insertRate(RateBO rate)
		{
      return updateRate(rate);
		}

		public int updateRate(RateBO rate)
		{
			SqlParameter[] sqlParams = base.getParameters("UpdateRate");

			RateDataRow inputRow = rate.getRateAsRateDataRow();
			// RateSeqNum
			if (inputRow.RateID==0)
			{  sqlParams[fld_RateSeqNum].Value = null;	}
			else
			{	sqlParams[fld_RateSeqNum].Value = inputRow.RateID;	}

			// RateSchedSeqNum
			sqlParams[fld_RateSchedSeqNum].Value = inputRow.RateScheduleID;

			// AddtnlDayRate
			sqlParams[fld_AddtnlDayRate].Value = inputRow.AddtnlDayRate;

			// InOutPatientInd
			sqlParams[fld_InOutPatientInd].Value = inputRow.InOut;

			// RateName
			sqlParams[fld_RateName].Value = inputRow.Name;

			// RateCatgryDescr
			sqlParams[fld_RateCatgryDescr].Value = inputRow.RateCategory;

			// RateTypeDescr
			sqlParams[fld_RateTypeDescr].Value = inputRow.RateType;

			// RateValue
			sqlParams[fld_RateValue].Value = inputRow.Rate;

			// LOS
			sqlParams[fld_LOSNum].Value = inputRow.LOS;

			// Threshold
			sqlParams[fld_ThreshldNum].Value = inputRow.Threshold;

			base.executeUpdate("UpdateRate", sqlParams);

			int rateID = Convert.ToInt16(sqlParams[fld_RateSeqNum].Value);

			rate.ID = rateID;

			rate.Codes.RateID = rateID;
			rate.Codes.RateScheduleID = rate.RateScheduleID;

			this.CodesData.insertCodes(rate.Codes);


			if (rate.HasPassThrus)
			{
				this.PassThrusData.insertPassThrus( ((Rate_W_PassThruBO) rate).PassThrus, rate);
			}

			if (inputRow.RateCategory=="BaseRate")
			{
				this.CodesData.insertWeightTable(rate, ((Rate_BaseRateBO) rate).WeightTable);

			}


			return rateID;
		}

		public void deleteRate(RateBO rate)
		{
			SqlParameter[] sqlParams = base.getParameters("DeleteRateCodeAll");

			// RateSeqNum
			sqlParams[fld_RateSeqNum].Value = rate.ID;

			base.executeDelete("DeleteRateCodeAll", sqlParams);
		}

		#endregion
	}
}
