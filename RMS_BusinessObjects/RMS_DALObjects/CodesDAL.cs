using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using RMS_BusinessObjects;

namespace RMS_DALObjects
{
	/// <summary>
	/// Summary description for CodesDAL.
	/// </summary>
	public class CodesDAL : baseDALObject
	{
		#region "Data Fields"

		static int fld_RateCodeSeqNum = 0;
		static int fld_RateSeqNum = 1;
		static int fld_RateSchedSeqNum = 2;
		static int fld_RateCode = 3;
		static int fld_RateTypeCode = 4;

		#endregion


		#region "Constructors"

		public CodesDAL()	{	}

		#endregion


		#region "Methods"

		public CodesBO getCodes(ref CodesManager codesMngr, int rateID)
		{
			SqlDataReader sqlDataRdr = base.getDataReader("SELECT RateCode, RateTypeCode FROM RateCode WHERE (NOT RateTypeCode='PassThru') AND (NOT RateTypeCode='Table') AND RateSeqNum=" + rateID + " ORDER BY RateTypeCode ASC");

			CodesBO myCodes = new CodesBO(ref codesMngr);
			string codeType = "";
			ArrayList codesList = new ArrayList();
			bool first = true;


			while(sqlDataRdr.Read())
			{
				if (first)
				{
					first = false;
					codeType = sqlDataRdr["RateTypeCode"].ToString();
					codesList.Add(sqlDataRdr["RateCode"]);
				}
				else if (codeType==sqlDataRdr["RateTypeCode"].ToString())
				{
					codesList.Add(sqlDataRdr["RateCode"]);
				}
				else
				{
					myCodes.addCodes(codeType, codesList);
					codesList.Clear();

					codeType = sqlDataRdr["RateTypeCode"].ToString();
					codesList.Add(sqlDataRdr["RateCode"]);
				}
			}

			if (! (codeType=="") )
			{	myCodes.addCodes(codeType, codesList); }

			base.closeConnection();

			return myCodes;
		}


		public CodesManager getCodesManager()
		{
			CodesManager myCodesManager = new CodesManager();

			myCodesManager.addCodeList("DRG", getCodeList("DRG"));
			myCodesManager.addCodeList("RevCode", getCodeList("RevCode"));
			myCodesManager.addCodeList("CPT", getCodeList("CPT"));
			myCodesManager.addCodeList("ICD9", getCodeList("ICD9"));
			myCodesManager.addCodeList("ICD9D", getCodeList("ICD9D"));

			return myCodesManager;
		}

    private ArrayList getCodeList(string codeType)
    {
      SqlDataReader sqlDataRdr = base.getDataReader("SELECT * FROM " + codeType + "Rate");

      ArrayList codeList = new ArrayList();
      while(sqlDataRdr.Read())
      {	codeList.Add(sqlDataRdr[codeType + "RateCode"]);		}

      return codeList;
     }


		
		public WeightTableStruct getWeightTable(int rateID)
		{
      SqlDataReader sqlDataRdr = base.getDataReader("SELECT RateCode, RateTypeCode FROM RateCode WHERE RateTypeCode='Table' AND RateSeqNum=" + rateID + " ORDER BY RateTypeCode ASC");

			WeightTableStruct weightTable = new WeightTableStruct(1, "DEFAULT");
      
			if(sqlDataRdr.Read())
			{ 
				weightTable.TableID = Convert.ToInt16(sqlDataRdr["RateCode"]);

				sqlDataRdr = base.getDataReader("SELECT DRGWgtName FROM DRGWgtID WHERE DRGWgtIDSeqNum=" + weightTable.TableID);

				if (sqlDataRdr.Read())
				{	weightTable.TableName = sqlDataRdr["DRGWgtName"].ToString();	}
			}

			base.closeConnection();

			return weightTable;
		}

		public ArrayList getWeightTables()
		{
			SqlDataReader sqlDataRdr = base.getDataReader("SELECT DRGWgtIDSeqNum, DRGWgtName FROM DRGWgtID");

			ArrayList weightTables = new ArrayList();
      
			while(sqlDataRdr.Read())
			{ 
				weightTables.Add(new WeightTableStruct(Convert.ToInt16(sqlDataRdr["DRGWgtIDSeqNum"]), sqlDataRdr["DRGWgtName"].ToString()));
			}

			base.closeConnection();

			return weightTables;
		}
		

		#endregion

		#region "Database Methods"

		
		public void insertCodes(CodesBO codes)
		{
			updateCodes(codes);
		}

		public void updateCodes(CodesBO codes)
		{
			insertCodeType(codes.RateScheduleID, codes.RateID, codes.getCodeType("DRG"));
			insertCodeType(codes.RateScheduleID, codes.RateID, codes.getCodeType("RevCode"));
			insertCodeType(codes.RateScheduleID, codes.RateID, codes.getCodeType("CPT"));
			insertCodeType(codes.RateScheduleID, codes.RateID, codes.getCodeType("ICD9"));
			insertCodeType(codes.RateScheduleID, codes.RateID, codes.getCodeType("ICD9D"));
		}

		private void insertCodeType(int rateScheduleID, int rateID, CodeTypeBO codeType)
		{
			SqlParameter[] sqlParams;

			if (codeType.CodeCount==0)
				// If no new codes, then delete any existing codes
			{
				SqlDataReader sqlDataRdr = base.getDataReader("SELECT * FROM RateCode WHERE RateTypeCode='" + codeType.CodeType + "' AND RateSeqNum=" + rateID);

				sqlParams = base.getParameters("DeleteRateCode");
				while(sqlDataRdr.Read())
				{
					sqlParams[fld_RateCodeSeqNum].Value = sqlDataRdr["RateCodeSeqNum"];
					sqlParams[fld_RateSeqNum].Value = sqlDataRdr["RateSeqNum"];
					sqlParams[fld_RateSchedSeqNum].Value = sqlDataRdr["RateSchedSeqNum"];

					base.executeDelete("DeleteRateCode", sqlParams);
				}

				base.closeConnection();
			}
			else
			{
				ArrayList codesList = codeType.getCodesList();

				SqlDataReader sqlDataRdr = base.getDataReader("SELECT * FROM RateCode WHERE RateTypeCode='" + codeType.CodeType + "' AND RateSeqNum=" + rateID);

				sqlParams = base.getParameters("DeleteRateCode");
				while(sqlDataRdr.Read())
				{
					if (codesList.Contains(sqlDataRdr["RateCode"].ToString()))
						// If Code already in database then remove from codes to be entered
					{
						codesList.Remove(sqlDataRdr["RateCode"].ToString());
					}
					else 
						// If Code is in the Database but not in the new Codes list, then Delete Rate Code
					{
						sqlParams[fld_RateCodeSeqNum].Value = sqlDataRdr["RateCodeSeqNum"];
						sqlParams[fld_RateSeqNum].Value = sqlDataRdr["RateSeqNum"];
						sqlParams[fld_RateSchedSeqNum].Value = sqlDataRdr["RateSchedSeqNum"];

						base.executeDelete("DeleteRateCode", sqlParams);
					}

				}
				base.closeConnection();

				sqlParams = base.getParameters("UpdateRateCode");

				for(int k=0; k<codesList.Count; k++)
				{
					sqlParams[fld_RateCodeSeqNum].Value = null;
					sqlParams[fld_RateSeqNum].Value = rateID;
					sqlParams[fld_RateSchedSeqNum].Value = rateScheduleID;

					sqlParams[fld_RateCode].Value = codesList[k].ToString();
					sqlParams[fld_RateTypeCode].Value = codeType.CodeType;

					base.executeUpdate("UpdateRateCode", sqlParams);
				}
			}
		}


		public void deleteRate(RateDataRow deleteRow)
		{
			SqlParameter[] sqlParams = base.getParameters("DeleteRateCodeAll");

			// RateSeqNum
			sqlParams[fld_RateSeqNum].Value = deleteRow.RateID;

			base.executeDelete("DeleteRateCodeAll", sqlParams);
		}


		public void insertWeightTable(RateBO rate, WeightTableStruct weightTable)
		{
			SqlParameter[] sqlParams;

			SqlDataReader sqlDataRdr = base.getDataReader("SELECT * FROM RateCode WHERE RateTypeCode='Table' AND RateSeqNum=" + rate.ID);

			sqlParams = base.getParameters("DeleteRateCode");
			while(sqlDataRdr.Read())
			{
				sqlParams[fld_RateCodeSeqNum].Value = sqlDataRdr["RateCodeSeqNum"];
				sqlParams[fld_RateSeqNum].Value = sqlDataRdr["RateSeqNum"];
				sqlParams[fld_RateSchedSeqNum].Value = sqlDataRdr["RateSchedSeqNum"];

				base.executeDelete("DeleteRateCode", sqlParams);
			}

			base.closeConnection();


			sqlParams = base.getParameters("UpdateRateCode");

			sqlParams[fld_RateCodeSeqNum].Value = null;
      sqlParams[fld_RateSeqNum].Value = rate.ID;
      sqlParams[fld_RateSchedSeqNum].Value = rate.RateScheduleID;

      sqlParams[fld_RateCode].Value = weightTable.TableID;
      sqlParams[fld_RateTypeCode].Value = "Table";

      base.executeUpdate("UpdateRateCode", sqlParams);
		}

		#endregion
	}
}
