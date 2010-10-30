using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using RMS_BusinessObjects;

namespace RMS_DALObjects
{
	/// <summary>
	/// Summary description for contractDAL.
	/// </summary>
	public class ContractDAL : baseDALObject
	{
		#region "Data Fields"

		// ContrctID Fields
		static int fld_ContrctIDNum = 0;
		static int fld_ContrctIDDescr = 1;

		// Contrct_RateSched Fields
		static int fld_Contrct_RateSched_ContrctIDNum = 0;
		static int fld_Contrct_RateSched_RateSchedSeqNum = 1;

		#endregion

		#region "Constructors"

		public ContractDAL()		{		}

		#endregion

		#region "Methods"

		public ContractBO getContract(int id)
		{
			SqlDataReader sqlDataRdr = this.getDataReader("SELECT ContrctIDNum, ContrctIDDescr FROM ContrctID WHERE ContrctIDNum=" + id);

			ContractBO contractToReturn;

			if (sqlDataRdr.Read())
			{
				contractToReturn =  new ContractBO((int)sqlDataRdr["ContrctIDNum"], sqlDataRdr["ContrctIDDescr"].ToString());	
			}
			else
			{	contractToReturn = new ContractBO();	}


			sqlDataRdr = this.getDataReader("SELECT RateSched.RateSchedSeqNum as RateSchedSeqNum, RateSchedName FROM Contrct_RateSched, RateSched WHERE Contrct_RateSched.RateSchedSeqNum=RateSched.RateSchedSeqNum AND ContrctIDNum=" + id);

			while(sqlDataRdr.Read())
			{
				contractToReturn.addRateSchedule(Convert.ToInt16(sqlDataRdr["RateSchedSeqNum"]), sqlDataRdr["RateSchedName"].ToString());
			}

			this.closeConnection();

			return contractToReturn;
		}


		public Contract_Collection getContractsByStatusType(string statusType)
		{
			SqlDataReader sqlDataRdr= this.getDataReader("SELECT ContrctID.ContrctIDNum as ContrctIDNum, ContrctID.ContrctIDDescr as ContrctIDDescr, RateSched.RateSchedSeqNum as RateSchedSeqNum, RateSchedName FROM ContrctID, Contrct_RateSched, RateSched WHERE ContrctID.ContrctIDNum=Contrct_RateSched.ContrctIDNum AND Contrct_RateSched.RateSchedSeqNum=RateSched.RateSchedSeqNum AND StatusTypeCode='" + statusType + "' GROUP BY ContrctID.ContrctIDNum, ContrctID.ContrctIDDescr, RateSched.RateSchedSeqNum, RateSchedName ORDER BY ContrctID.ContrctIDDescr ASC");
			
			Contract_Collection Contracts = new Contract_Collection();

			ContractBO newContract;

			if (sqlDataRdr.Read())
			{
				newContract = new ContractBO((int)sqlDataRdr["ContrctIDNum"], sqlDataRdr["ContrctIDDescr"].ToString());
				newContract.addRateSchedule((int)sqlDataRdr["RateSchedSeqNum"], sqlDataRdr["RateSchedName"].ToString());

				while(sqlDataRdr.Read())
				{
          if (! (newContract.ID==(int)sqlDataRdr["ContrctIDNum"]) )
            {
							Contracts.addContract(newContract);
							newContract = new ContractBO((int)sqlDataRdr["ContrctIDNum"], sqlDataRdr["ContrctIDDescr"].ToString());	
						}

					newContract.addRateSchedule((int)sqlDataRdr["RateSchedSeqNum"], sqlDataRdr["RateSchedName"].ToString());
				}

				Contracts.addContract(newContract);
			}
			
			this.closeConnection();

			return Contracts;
		}

		public RateSchedule_Collection getRateSchedulesForContract(int contractID)
		{
			SqlDataReader sqlDataRdr= this.getDataReader("SELECT RateSched.RateSchedSeqNum as RateSchedSeqNum, RateSchedName FROM RateSched, Contrct_RateSched WHERE Contrct_RateSched.RateSchedSeqNum=RateSched.RateSchedSeqNum AND ContrctIDNum='" + contractID + "' ORDER BY RateSchedName ASC");
			
			RateSchedule_Collection RateSchedules = new RateSchedule_Collection();
			
			while(sqlDataRdr.Read())
			{
				RateSchedules.addRateSchedule(new RateScheduleBO((int)sqlDataRdr["RateSchedSeqNum"], sqlDataRdr["RateSchedName"].ToString()));
			}
			
			this.closeConnection();

			return RateSchedules;

		}


		#endregion

		#region "Database Methods"

		public int insertContract(ContractBO contract)
		{
			return updateContract(contract);
		}

		public int updateContract(ContractBO contract)
		{
			SqlParameter[] sqlParams = base.getParameters("UpdateContrctID");

			if (contract.ID==0)
			{
				sqlParams[fld_ContrctIDNum].Value = null;
			}
			else
			{
				sqlParams[fld_ContrctIDNum].Value = contract.ID;
			}

			sqlParams[fld_ContrctIDDescr].Value = contract.ContractName;

			base.executeUpdate("UpdateContrctID", sqlParams);

			return Convert.ToInt16(sqlParams[fld_ContrctIDNum].Value);
		}

		public void deleteContract(ContractBO contract)
		{
			SqlParameter[] sqlParams;

			sqlParams = base.getParameters("DeleteContrct_RateSched");

			RateScheduleDAL rateScheduleData = new RateScheduleDAL();

			for(int k=0; k<contract.rateScheduleCount(); k++)
			{
				sqlParams[fld_Contrct_RateSched_ContrctIDNum].Value = contract.ID;
				sqlParams[fld_Contrct_RateSched_RateSchedSeqNum].Value = ((RateScheduleStruct) contract.getRateScheduleAt(k)).ID;

				base.executeDelete("DeleteContrct_RateSched", sqlParams);

				rateScheduleData.deleteRateSchedule(((RateScheduleStruct) contract.getRateScheduleAt(k)).ID);	
			}

			sqlParams = base.getParameters("DeleteContrctID");

      sqlParams[fld_ContrctIDNum].Value = contract.ID;

			base.executeDelete("DeleteContrctID", sqlParams);
		}

		#endregion
	}
}
