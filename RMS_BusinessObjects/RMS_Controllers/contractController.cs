using System;
using System.Data;

using RMS_BusinessObjects;
using RMS_DALObjects;

namespace RMS_Controllers
{
	/// <summary>
	/// Summary description for contractController.
	/// </summary>
	public class ContractController
	{
		#region "Variables"

		#endregion

		#region "Constructors"

		public ContractController()
		{
		}


		#endregion

		#region "Methods"

		public string getStatus(string statusTypeCode)
		{
			StatusDAL myStatusDAL = new StatusDAL();

			return myStatusDAL.getStatus(statusTypeCode);
		}

		public DataTable getContracts(string statusTypeCode)
		{
			ContractDAL myContractDAL = new ContractDAL();

			Contract_Collection myContract_Collection = myContractDAL.getContractsByStatusType(statusTypeCode);

			return myContract_Collection.ToDataTable();
		}

		public DataTable getRateSchedules(string contractID)
		{
			ContractDAL myContractDAL = new ContractDAL();

			RateSchedule_Collection myRateSchedules = myContractDAL.getRateSchedulesForContract(contractID);

			return myRateSchedules.toDataTable();
		}

		#endregion
	}
}
