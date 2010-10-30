using System;
using System.Data;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for Contract_Collection.
	/// </summary>
	public class Contract_Collection 
	{
		#region "Variables"

		ArrayList Contracts;

		#endregion

		#region "Constructors"

		public Contract_Collection()	
		{
			Contracts = new ArrayList();
		}

		#endregion

		#region "Methods"

		public void addContract(ContractBO contractToAdd)
		{
			Contracts.Add(contractToAdd);
		}

		public DataTable ToDataTable()
		{
			DataTable contractDataTable = new DataTable();

			contractDataTable.Columns.Add("ContractID", Type.GetType("System.String"));
			contractDataTable.Columns.Add("ContractName", Type.GetType("System.String"));

			DataRow contractRow;
			foreach (ContractBO Contract in Contracts)
			{
				contractRow = contractDataTable.NewRow();
				contractRow["ContractID"] = Contract.ID;
        contractRow["ContractName"] = Contract.ContractName;
				contractDataTable.Rows.Add(contractRow);
			}

			return contractDataTable;
		}

		public ContractBO getContractAt(int index)
		{	return (ContractBO) Contracts[index];	}

		public int Count()
		{	return Contracts.Count;	}


		#endregion
	}
}
