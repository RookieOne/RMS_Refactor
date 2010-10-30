using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for AnalysisBO.
	/// </summary>
	public class AnalysisBO
	{
		#region "Variables"

		DataSet var_Data;

		string var_ContractTitle;
		string var_RateSchedulesAnalyzed;
		string var_DataSetID;
		
		double var_InChgInc;
		double var_OutChgInc;
		double var_CostInc;
		
		string var_FilterEntity;
		string var_FilterInsurancePlanCode;

		string var_BaseComments;

		#endregion

		#region "Constructors"

		public AnalysisBO()	{	}
    
		private void buildDataTable()
		{
			this.Data = new DataSet();

			this.Data.Tables.Add("RMS Encounter");

			this.Data.Tables["RMS Encounter"].Columns.Add("EncNum", Type.GetType("System.String"));
			this.Data.Tables["RMS Encounter"].Columns.Add("InOut", Type.GetType("System.String"));
			this.Data.Tables["RMS Encounter"].Columns.Add("DRG", Type.GetType("System.String"));
			this.Data.Tables["RMS Encounter"].Columns.Add("PrincplProcdr", Type.GetType("System.String"));
			this.Data.Tables["RMS Encounter"].Columns.Add("PrincplDiag", Type.GetType("System.String"));
			this.Data.Tables["RMS Encounter"].Columns.Add("drgType", Type.GetType("System.String"));
			this.Data.Tables["RMS Encounter"].Columns.Add("Company", Type.GetType("System.String"));
			this.Data.Tables["RMS Encounter"].Columns.Add("InsPlan", Type.GetType("System.String"));
			this.Data.Tables["RMS Encounter"].Columns.Add("LOS", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("medSurg", Type.GetType("System.String"));
			this.Data.Tables["RMS Encounter"].Columns.Add("RateCategory", Type.GetType("System.String"));
			this.Data.Tables["RMS Encounter"].Columns.Add("RateType", Type.GetType("System.String"));
			this.Data.Tables["RMS Encounter"].Columns.Add("RateName", Type.GetType("System.String"));
			this.Data.Tables["RMS Encounter"].Columns.Add("Model", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("NetRev", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("Charges", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("Payment", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("Cost", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("passThruCharges", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("passThruModel", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("POC", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("cPOC", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("High", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("ALOS", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("Weight", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("HiTrim", Type.GetType("System.Double"));
			this.Data.Tables["RMS Encounter"].Columns.Add("ProcessTime", Type.GetType("System.Double"));


			DataColumn[] key = new DataColumn[1];
			key[0] = this.Data.Tables["RMS Encounter"].Columns["EncNum"];
			this.Data.Tables["RMS Encounter"].PrimaryKey = key;

			
			this.Data.Tables.Add("RMS Detail");

			this.Data.Tables["RMS Detail"].Columns.Add("EncNum", Type.GetType("System.String"));
			this.Data.Tables["RMS Detail"].Columns.Add("RevCode", Type.GetType("System.String"));
			this.Data.Tables["RMS Detail"].Columns.Add("CPT", Type.GetType("System.String"));
			this.Data.Tables["RMS Detail"].Columns.Add("Charges", Type.GetType("System.Double"));
			this.Data.Tables["RMS Detail"].Columns.Add("Quantity", Type.GetType("System.Double"));
			this.Data.Tables["RMS Detail"].Columns.Add("Model", Type.GetType("System.Double"));
			this.Data.Tables["RMS Detail"].Columns.Add("POC", Type.GetType("System.Double"));
			this.Data.Tables["RMS Detail"].Columns.Add("RateCategory", Type.GetType("System.String"));
			this.Data.Tables["RMS Detail"].Columns.Add("RateType", Type.GetType("System.String"));
			this.Data.Tables["RMS Detail"].Columns.Add("RateName", Type.GetType("System.String"));


			this.Data.Tables.Add("SDS");

			this.Data.Tables["SDS"].Columns.Add("EncNum", Type.GetType("System.String"));
			this.Data.Tables["SDS"].Columns.Add("Code", Type.GetType("System.String"));
			this.Data.Tables["SDS"].Columns.Add("Sequence", Type.GetType("System.String"));
			this.Data.Tables["SDS"].Columns.Add("Group", Type.GetType("System.String"));
			this.Data.Tables["SDS"].Columns.Add("Rate", Type.GetType("System.Double"));
			this.Data.Tables["SDS"].Columns.Add("Model", Type.GetType("System.Double"));
		}


		#endregion

		#region "Properties"

		public DataSet Data
		{
			get{return var_Data;}
			set{var_Data = value;}
		}

		public string ContractTitle
		{
			get{return var_ContractTitle;}
			set{var_ContractTitle = value;}
		}

		public string RateSchedulesAnalyzed
		{
			get{return var_RateSchedulesAnalyzed;}
			set{var_RateSchedulesAnalyzed = value;}
		}
		public string DataSetID
		{
			get{return var_DataSetID;}
			set{var_DataSetID = value;}
		}


		public double InpatientChargeIncrease
		{
			get{return var_InChgInc;}
			set{var_InChgInc = value;}
		}
		public double OutpatientChargeIncrease
		{
			get{return var_OutChgInc;}
			set{var_OutChgInc = value;}
		}
		public double CostIncrease
		{
			get{return var_CostInc;}
			set{var_CostInc = value;}
		}


		public string FilterEntity
		{
			get{return var_FilterEntity;}
			set{var_FilterEntity = value;}
		}
		public string FilterInsurancePlanCode
		{
			get{return var_FilterInsurancePlanCode;}
			set{var_FilterInsurancePlanCode = value;}
		}

		public string BaseComments
		{
			get{return var_BaseComments;}
			set{var_BaseComments = value;}
		}


		#endregion

		#region "Methods"

		#region "Summary Tables"

		public DataTable getPatientTypeSummaryTable()
		{
			DataTable dTable = new DataTable("Summary");

			dTable.Columns.Add("Title", Type.GetType("System.String"));
			dTable.Columns.Add("Cases", Type.GetType("System.Double"));
			dTable.Columns.Add("Charges", Type.GetType("System.Double"));
			dTable.Columns.Add("NetRev", Type.GetType("System.Double"));
			dTable.Columns.Add("Model", Type.GetType("System.Double"));
			dTable.Columns.Add("POC", Type.GetType("System.Double"));
			dTable.Columns.Add("Var", Type.GetType("System.Double"));
			dTable.Columns.Add("Cost", Type.GetType("System.Double"));
			dTable.Columns.Add("NI", Type.GetType("System.Double"));

			double inCases = 0;
			double inCharges = 0;
			double inNetRev = 0;
			double inModel = 0;
			double inCost = 0;

			double outCases = 0;
			double outCharges = 0;
			double outNetRev = 0;
			double outModel = 0;
			double outCost = 0;
			
			foreach(DataRow dRow in this.Data.Tables[0].Rows)
			{
				if (dRow["InOut"].ToString() == "I")
				{
					inCases += 1;
					inCharges += Convert.ToDouble(dRow["Charges"]);
					inNetRev += Convert.ToDouble(dRow["NetRev"]);
					inModel += Convert.ToDouble(dRow["Model"]);
					inCost += Convert.ToDouble(dRow["Cost"]);
				}
				else
				{
					outCases += 1;
					outCharges += Convert.ToDouble(dRow["Charges"]);
					outNetRev += Convert.ToDouble(dRow["NetRev"]);
					outModel += Convert.ToDouble(dRow["Model"]);
					outCost += Convert.ToDouble(dRow["Cost"]);
				}
			}

			DataRow inRow = dTable.NewRow();
			inRow["Title"] = "Inpatient";
			inRow["Cases"] = inCases;
			inRow["Charges"] = inCharges;
			inRow["Model"] = inModel;
			inRow["NetRev"] = inNetRev;
			inRow["Cost"] = inCost;

			DataRow outRow = dTable.NewRow();
			outRow["Title"] = "Outpatient";
			outRow["Cases"] = outCases;
			outRow["Charges"] = outCharges;
			outRow["Model"] = outModel;
			outRow["NetRev"] = outNetRev;
			outRow["Cost"] = outCost;

			dTable.Rows.Add(inRow);
			dTable.Rows.Add(outRow);

			return dTable;
		}


		public DataTable getDRGTypeSummaryTable()
		{
			DataTable dTable = new DataTable("Summary");

			dTable.Columns.Add("Title", Type.GetType("System.String"));
			dTable.Columns.Add("Cases", Type.GetType("System.Double"));
			dTable.Columns.Add("Charges", Type.GetType("System.Double"));
			dTable.Columns.Add("NetRev", Type.GetType("System.Double"));
			dTable.Columns.Add("Model", Type.GetType("System.Double"));
			dTable.Columns.Add("Cost", Type.GetType("System.Double"));

			Hashtable drgTypeTable = new Hashtable();
			DataRow inputRow;

			foreach(DataRow dRow in this.Data.Tables[0].Rows)
			{
				if (dRow["InOut"].ToString() == "I")
				{
					if ( ! (drgTypeTable.ContainsKey(dRow["drgType"].ToString())) )
					{
						inputRow = dTable.NewRow();

						inputRow["Title"] = dRow["drgType"].ToString();
						inputRow["Cases"] = 1;
						inputRow["Charges"] = Convert.ToDouble(dRow["Charges"]);
						inputRow["NetRev"] = Convert.ToDouble(dRow["NetRev"]);
						inputRow["Model"] = Convert.ToDouble(dRow["Model"]);
						inputRow["Cost"] = Convert.ToDouble(dRow["Cost"]);

						dTable.Rows.Add(inputRow);

						drgTypeTable.Add(dRow["drgType"].ToString(), inputRow);
					}
					else
					{
						inputRow = (DataRow) drgTypeTable[dRow["drgType"].ToString()];

						inputRow["Cases"] = Convert.ToDouble(inputRow["Cases"]) + 1;
						inputRow["Charges"] = Convert.ToDouble(inputRow["Charges"]) + Convert.ToDouble(dRow["Charges"]);
						inputRow["NetRev"] = Convert.ToDouble(inputRow["NetRev"]) + Convert.ToDouble(dRow["NetRev"]);
						inputRow["Model"] = Convert.ToDouble(inputRow["Model"]) + Convert.ToDouble(dRow["Model"]);
						inputRow["Cost"] = Convert.ToDouble(inputRow["Cost"]) + Convert.ToDouble(dRow["Cost"]);
					}
				}
			}

			return dTable;
		}


		public DataTable getOutpatientSummaryTable()
		{
			DataTable dTable = new DataTable("Outpatient Summary");

			dTable.Columns.Add("Title", Type.GetType("System.String"));
			dTable.Columns.Add("Cases", Type.GetType("System.Double"));
			dTable.Columns.Add("Charges", Type.GetType("System.Double"));
			dTable.Columns.Add("NetRev", Type.GetType("System.Double"));
			dTable.Columns.Add("Model", Type.GetType("System.Double"));
			dTable.Columns.Add("Cost", Type.GetType("System.Double"));

			Hashtable rateCategoryTable = new Hashtable();
			DataRow inputRow;

			foreach(DataRow dRow in this.Data.Tables[0].Rows)
			{
				if (dRow["InOut"].ToString() == "O")
				{
					if ( ! (rateCategoryTable.ContainsKey(dRow["RateCategory"].ToString())) )
					{
						inputRow = dTable.NewRow();

						inputRow["Title"] = dRow["RateCategory"].ToString();
						inputRow["Cases"] = 1;
						inputRow["Charges"] = Convert.ToDouble(dRow["Charges"]);
						inputRow["NetRev"] = Convert.ToDouble(dRow["NetRev"]);
						inputRow["Model"] = Convert.ToDouble(dRow["Model"]);
						inputRow["Cost"] = Convert.ToDouble(dRow["Cost"]);

						dTable.Rows.Add(inputRow);

						rateCategoryTable.Add(dRow["RateCategory"].ToString(), inputRow);
					}
					else
					{
						inputRow = (DataRow) rateCategoryTable[dRow["RateCategory"].ToString()];

						inputRow["Cases"] = Convert.ToDouble(inputRow["Cases"]) + 1;
						inputRow["Charges"] = Convert.ToDouble(inputRow["Charges"]) + Convert.ToDouble(dRow["Charges"]);
						inputRow["NetRev"] = Convert.ToDouble(inputRow["NetRev"]) + Convert.ToDouble(dRow["NetRev"]);
						inputRow["Model"] = Convert.ToDouble(inputRow["Model"]) + Convert.ToDouble(dRow["Model"]);
						inputRow["Cost"] = Convert.ToDouble(inputRow["Cost"]) + Convert.ToDouble(dRow["Cost"]);
					}
				}
			}

			return dTable;
		}


		public DataTable getHospitalSummaryTable()
		{
			Hashtable entityDescrTable = new Hashtable();
		//	baseDALObject data = new baseDALObject();

			//DataSet oDataSet = data.getDataSet("SELECT * FROM Entity");

			DataSet oDataSet = new DataSet();

			foreach(DataRow dRow in oDataSet.Tables[0].Rows)
			{
				if (! (dRow["CompanyCode"]==System.DBNull.Value) )
				{	entityDescrTable.Add(dRow["CompanyCode"].ToString(), dRow["EntityDescr"].ToString());	}
			}


			DataTable dTable = new DataTable("Summary");

			dTable.Columns.Add("Title", Type.GetType("System.String"));
			dTable.Columns.Add("Cases", Type.GetType("System.Double"));
			dTable.Columns.Add("Charges", Type.GetType("System.Double"));
			dTable.Columns.Add("NetRev", Type.GetType("System.Double"));
			dTable.Columns.Add("Model", Type.GetType("System.Double"));
			dTable.Columns.Add("Cost", Type.GetType("System.Double"));

			Hashtable drgTypeTable = new Hashtable();
			DataRow inputRow;

			foreach(DataRow dRow in this.Data.Tables[0].Rows)
			{
				if ( ! (drgTypeTable.ContainsKey(dRow["Company"].ToString())) )
				{
					inputRow = dTable.NewRow();

					inputRow["Title"] = entityDescrTable[Convert.ToString(dRow["Company"])].ToString();
					inputRow["Cases"] = 1;
					inputRow["Charges"] = Convert.ToDouble(dRow["Charges"]);
					inputRow["NetRev"] = Convert.ToDouble(dRow["NetRev"]);
					inputRow["Model"] = Convert.ToDouble(dRow["Model"]);
					inputRow["Cost"] = Convert.ToDouble(dRow["Cost"]);

					dTable.Rows.Add(inputRow);

					drgTypeTable.Add(dRow["Company"].ToString(), inputRow);
				}
				else
				{
					inputRow = (DataRow) drgTypeTable[dRow["Company"].ToString()];

					inputRow["Cases"] = Convert.ToDouble(inputRow["Cases"]) + 1;
					inputRow["Charges"] = Convert.ToDouble(inputRow["Charges"]) + Convert.ToDouble(dRow["Charges"]);
					inputRow["NetRev"] = Convert.ToDouble(inputRow["NetRev"]) + Convert.ToDouble(dRow["NetRev"]);
					inputRow["Model"] = Convert.ToDouble(inputRow["Model"]) + Convert.ToDouble(dRow["Model"]);
					inputRow["Cost"] = Convert.ToDouble(inputRow["Cost"]) + Convert.ToDouble(dRow["Cost"]);
				}
			}

			return dTable;
		}


		public DataTable getRateBreakDownTable(string inOut)
		{
			DataTable dTable = new DataTable("Rate Breakdown Summary");

			dTable.Columns.Add("RateCategory", Type.GetType("System.String"));
			dTable.Columns.Add("RateType", Type.GetType("System.String"));
			dTable.Columns.Add("RateName", Type.GetType("System.String"));

			dTable.Columns.Add("Cases", Type.GetType("System.Double"));
			dTable.Columns.Add("Charges", Type.GetType("System.Double"));
			dTable.Columns.Add("NetRev", Type.GetType("System.Double"));
			dTable.Columns.Add("Model", Type.GetType("System.Double"));
			dTable.Columns.Add("Cost", Type.GetType("System.Double"));

			Hashtable rateCategoryTable = new Hashtable();
			Hashtable rateTypeTable, rateNameTable;
			DataRow inputRow;

			foreach(DataRow dRow in this.Data.Tables[0].Rows)
			{
				if (dRow["InOut"].ToString()==inOut)
				{

					// FIND rateTypeTable (Hashtable)
					if (rateCategoryTable.ContainsKey(dRow["RateCategory"]))
					{	rateTypeTable = (Hashtable) rateCategoryTable[dRow["RateCategory"]];	}
					else
					{	rateTypeTable = new Hashtable();	rateCategoryTable.Add(dRow["RateCategory"], rateTypeTable);	}

					// FIND rateNameTable (Hashtable)
					if (rateTypeTable.ContainsKey(dRow["RateType"]))
					{	rateNameTable = (Hashtable) rateTypeTable[dRow["RateType"]];	}
					else
					{	rateNameTable = new Hashtable();		rateTypeTable.Add(dRow["RateType"], rateNameTable);		}

					// FIND inputRow (DataRow) AND input analysis data row
					if (rateNameTable.ContainsKey(dRow["RateName"]))
					{	
						inputRow = (DataRow) rateNameTable[dRow["RateName"]];	

						inputRow["Cases"] = Convert.ToDouble(inputRow["Cases"]) + 1;
						inputRow["Charges"] = Convert.ToDouble(inputRow["Charges"]) + Convert.ToDouble(dRow["Charges"]);
						inputRow["NetRev"] = Convert.ToDouble(inputRow["NetRev"]) + Convert.ToDouble(dRow["NetRev"]);
						inputRow["Model"] = Convert.ToDouble(inputRow["Model"]) + Convert.ToDouble(dRow["Model"]);
						inputRow["Cost"] = Convert.ToDouble(inputRow["Cost"]) + Convert.ToDouble(dRow["Cost"]);
					}
					else
					{	
						inputRow = dTable.NewRow(); 	

						inputRow["RateCategory"] = dRow["RateCategory"].ToString();
						inputRow["RateType"] = dRow["RateType"].ToString();
						inputRow["RateName"] = dRow["RateName"].ToString();

						inputRow["Cases"] = 1;
						inputRow["Charges"] = Convert.ToDouble(dRow["Charges"]);
						inputRow["NetRev"] = Convert.ToDouble(dRow["NetRev"]);
						inputRow["Model"] = Convert.ToDouble(dRow["Model"]);
						inputRow["Cost"] = Convert.ToDouble(dRow["Cost"]);

						rateNameTable.Add(dRow["RateName"].ToString(), inputRow);					
						dTable.Rows.Add(inputRow);	
					}
				}
			}

			return dTable;
		}

		#endregion

		#endregion
	}
}
