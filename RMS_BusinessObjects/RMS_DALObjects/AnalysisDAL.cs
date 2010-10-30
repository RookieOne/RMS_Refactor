using System;

using RMS_BusinessObjects;


namespace RMS_DALObjects
{
	/// <summary>
	/// Summary description for AnalysisDAL.
	/// </summary>
	public class AnalysisDAL : baseDALObject
	{
		public AnalysisDAL()		{	}


		#region "Methods"

		public EncounterDataBO getEncounterData(string DataSetID)
		{
			return this.getEncounterData(DataSetID, "", "");
		}

		public EncounterDataBO getEncounterData(string DataSetID, string entityFilter, string insPlanFilter)
		{
			string strSQL = "SELECT EncntrNum as EncNum, ChrgAmt as Charges, CompanyCode as Company, CostAmt as Cost, DRGRateCode as DRG, InOutInd as InOut, InsurncPlanCode as InsPlan, LOSNum as LOS, NetRevenueAmt as NetRev, PmtAmt as Payment, PrincplProcdrCode as PrincplProcdrCode, PrincplDiagCode as PrincplDiagCode  FROM Encntr WHERE DatasetSeqNum=" + DataSetID;

			if ( ! (entityFilter=="") )
			{	strSQL += " AND CompanyCode in (" + entityFilter + ")";	}
			
			if ( ! (insPlanFilter=="") )
			{	strSQL += " AND InsurncPlanCode In (" + insPlanFilter + ")";	}

			EncounterDataBO encData = new EncounterDataBO();

			encData.Data = base.getDataSet(strSQL);

			return encData;
		}

		#endregion

	}
}
