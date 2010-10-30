using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections;

using RMS_BusinessObjects;
using RMS_DALObjects;


namespace RMS_v3
{
	/// <summary>
	/// Summary description for AnalysisObject.
	/// </summary>
	public class AnalysisObject
	{

		#region "Variables"

		DataSet analysisDS = new DataSet();
		DataSet dataDS = new DataSet();
		DataSet oDataSet;

		DataRow inputRow, foundRow, eRow, drgTypeRow, rateRow;
		DataRow[] detailRows;
		DataRow detailRow;

		string strSQL;

		int los, rateSchedSeqNum, datasetSeqNum;

		baseDALObject db = new baseDALObject();

		double chargeInc, inChrgInc, outChrgInc, costInc;
		double medRate, surgRate;

		DataSet drgDS, drgTypeDS, codesDS, limitCodesDS, ratesDS, passThruDS;

		Label progLabel;
		ProgressBar progBar;

		DateTime start_time, stop_time;
		TimeSpan elapsed_time;

		// NEW VARIABLES

		#region "General Variables"

		double tempModel;
		string inOut;

		DataRow[] codeRows;

		Hashtable genHashTable; // Hold Rate Seq Nums for General Rates ie. Stop Loss, Base Rate, etc
		ArrayList detailRowsList;

		#endregion

		#region "Inpatient Variables"

		Hashtable in_HasPerDiem, in_HasFFS;
		bool rateFound;
		string foundRateSeqNum, foundRateCatgry;

		#endregion

		#region "Limit Variables"

		double limitModel;

		#endregion

		#region "Base Rate Variables"

		DataRow baseRateRow, drgWeightRow;
		double hiTrimModel, difference;

		#endregion

		#region "Outpatient Variables"

		int rateToApply_Priority;
		string rateToApply_SeqNum;

		DataRow CBNARow;
		bool out_HasSDS, out_HasCBNA, out_HasFFS;
		Hashtable out_HasFFSCodeTypes;

		#endregion

		#region "ASC Rates Variables"

		DataSet ascGroupDS;
		DataTable ascTable;

		DataRow ascRow, ascRateRow;
		string ascCPT, ascGroup;
		DataRow[] cptRows, ascGroupList;

		int ascRateListIndex;
		bool ascPOCApplied;

		Hashtable rates, thresholds;
		ArrayList rateList = new ArrayList();

		#endregion

		#region "Pass Thru Variables"

		bool allPassThrus, nonePassThrus;
		DataRow[] validPassThrus;
		DataRow inputDetailRow, passThruRateRow;
		double passThruCharges, totalPassThruModel;
		ArrayList perVisitsApplied;
		ArrayList validPassThrusList;

		#endregion

		static string constLimitCategories = "'StopLoss','Floor','Ceiling','LessorOf'";

		#endregion

		#region "Constructors"

		public AnalysisObject(int iRateSchedSeqNum, int iDataSetSeqNum, ProgressBar pBar, Label pLbl)
		{
			rateSchedSeqNum = iRateSchedSeqNum;
			datasetSeqNum = iDataSetSeqNum;

			progBar = pBar;
			progLabel = pLbl;

			analysisDS = new DataSet();

			analysisDS.Tables.Add("RMS Encounter");

			analysisDS.Tables["RMS Encounter"].Columns.Add("EncNum", Type.GetType("System.String"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("InOut", Type.GetType("System.String"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("DRG", Type.GetType("System.String"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("PrincplProcdr", Type.GetType("System.String"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("PrincplDiag", Type.GetType("System.String"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("drgType", Type.GetType("System.String"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("Company", Type.GetType("System.String"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("InsPlan", Type.GetType("System.String"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("LOS", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("medSurg", Type.GetType("System.String"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("RateCategory", Type.GetType("System.String"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("RateType", Type.GetType("System.String"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("RateName", Type.GetType("System.String"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("Model", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("NetRev", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("Charges", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("Payment", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("Cost", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("passThruCharges", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("passThruModel", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("POC", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("cPOC", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("High", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("ALOS", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("Weight", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("HiTrim", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("ProcessTime", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("Period", Type.GetType("System.String"));
			analysisDS.Tables["RMS Encounter"].Columns.Add("Year", Type.GetType("System.String"));

			DataColumn[] key = new DataColumn[1];
			key[0] = analysisDS.Tables["RMS Encounter"].Columns["EncNum"];
			analysisDS.Tables["RMS Encounter"].PrimaryKey = key;


			analysisDS.Tables.Add("RMS Detail");

			analysisDS.Tables["RMS Detail"].Columns.Add("EncNum", Type.GetType("System.String"));
			analysisDS.Tables["RMS Detail"].Columns.Add("RevCode", Type.GetType("System.String"));
			analysisDS.Tables["RMS Detail"].Columns.Add("CPT", Type.GetType("System.String"));
			analysisDS.Tables["RMS Detail"].Columns.Add("Charges", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Detail"].Columns.Add("Quantity", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Detail"].Columns.Add("Model", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Detail"].Columns.Add("POC", Type.GetType("System.Double"));
			analysisDS.Tables["RMS Detail"].Columns.Add("RateCategory", Type.GetType("System.String"));
			analysisDS.Tables["RMS Detail"].Columns.Add("RateType", Type.GetType("System.String"));
			analysisDS.Tables["RMS Detail"].Columns.Add("RateName", Type.GetType("System.String"));

			analysisDS.Tables.Add("SDS");

			analysisDS.Tables["SDS"].Columns.Add("EncNum", Type.GetType("System.String"));
			analysisDS.Tables["SDS"].Columns.Add("Code", Type.GetType("System.String"));
			analysisDS.Tables["SDS"].Columns.Add("Sequence", Type.GetType("System.String"));
			analysisDS.Tables["SDS"].Columns.Add("Group", Type.GetType("System.String"));
			analysisDS.Tables["SDS"].Columns.Add("Rate", Type.GetType("System.Double"));
			analysisDS.Tables["SDS"].Columns.Add("Model", Type.GetType("System.Double"));
		}


		#endregion

		public void Dispose()
		{
			oDataSet.Dispose();
			dataDS.Dispose();
			analysisDS.Dispose();
			drgDS.Dispose();
			codesDS.Dispose();
			ratesDS.Dispose();
		}

		public void loadData(bool filterHospitals, bool filterInsPlans)
		{
			DataColumn[] key = new DataColumn[1];
			DataColumn[] cKey = new DataColumn[3];

			string entityFilter = "";
			string IPCFilter = "";

			progBar.Minimum = 0;
			progBar.Maximum = 3;
			progBar.Step = 1;

			progLabel.Text = "Loading Encounter...";
			progLabel.Refresh();

			DataTable dTable;

			strSQL = "SELECT EncntrNum as EncNum, ChrgAmt as Charges, CompanyCode as Company, CostAmt as Cost, DRGRateCode as DRG, InOutInd as InOut, InsurncPlanCode as InsPlan, LOSNum as LOS, NetRevenueAmt as NetRev, PmtAmt as Payment, PrincplProcdrCode as PrincplProcdrCode, PrincplDiagCode as PrincplDiagCode  FROM Encntr WHERE DatasetSeqNum=" + datasetSeqNum;

			if (filterHospitals)
			{
				oDataSet = db.getDataSet("SELECT * FROM Entity_RateSched, Entity WHERE Entity_RateSched.EntityCode=Entity.EntityCode AND RateSchedSeqNum=" + rateSchedSeqNum);

				if (oDataSet.Tables[0].Rows.Count == 0)
				{	filterHospitals = false;	}
				else
				{
					foreach(DataRow dRow in oDataSet.Tables[0].Rows)
					{
						if (entityFilter=="")
						{	entityFilter = "'" + dRow["CompanyCode"] + "'";	}
						else
						{	entityFilter += ",'" + dRow["CompanyCode"] + "'";	}
					}

					strSQL += " AND CompanyCode in (" + entityFilter + ")";
				}
			}

			if (filterInsPlans)
			{
				oDataSet = db.getDataSet("SELECT * FROM InsurncPlanCode WHERE RateSchedSeqNum=" + rateSchedSeqNum);

				if (oDataSet.Tables[0].Rows.Count == 0)
				{	filterInsPlans = false;	}
				else
				{
					foreach(DataRow dRow in oDataSet.Tables[0].Rows)
					{
						if (IPCFilter=="")
						{	IPCFilter = "'" + dRow["InsurncPlanCode"] + "'";	}
						else
						{	IPCFilter += ",'" + dRow["InsurncPlanCode"] + "'";	}
					}

					strSQL += " AND InsurncPlanCode In (" + IPCFilter + ")";
				}
			}

			oDataSet = db.getDataSet(strSQL);

			dTable = new DataTable();
			dTable = oDataSet.Tables[0].Copy();
			dTable.TableName = "Encounter";

			key[0] = dTable.Columns["EncNum"];

			dTable.PrimaryKey = key;

			dataDS.Tables.Add(dTable);

			progBar.PerformStep();
			progBar.Refresh();

			progLabel.Text = "Loading Detail...";
			progLabel.Refresh();


			strSQL = "SELECT Detl.EncntrNum as EncNum, Detl.ChrgAmt as Charges, Detl.DetlQty as Quantity, Detl.CPTRateCode as CPT, Detl.RevCodeRateCode as RevCode FROM Detl, Encntr WHERE Encntr.EncntrNum=Detl.EncntrNum AND Encntr.DatasetSeqNum=" + datasetSeqNum + " AND Detl.DatasetSeqNum=" + datasetSeqNum;

			if (filterHospitals)
			{	strSQL += " AND Encntr.CompanyCode In (" + entityFilter + ")";	}

			if (filterInsPlans)
			{	strSQL += " AND Encntr.InsurncPlanCode In (" + IPCFilter + ")";}

			oDataSet = db.getDataSet(strSQL);

			dTable = new DataTable();
			dTable = oDataSet.Tables[0].Copy();
			dTable.TableName = "Detail";
			dataDS.Tables.Add(dTable);
			
			dataDS.Relations.Add(new DataRelation("EncDetail", dataDS.Tables["Encounter"].Columns["EncNum"], dataDS.Tables["Detail"].Columns["EncNum"]));

			progBar.PerformStep();
			progBar.Refresh();
			progLabel.Text = "Loading EncCpt...";
			progLabel.Refresh();

			strSQL = "SELECT EncntrCPT.EncntrNum as EncNum, EncntrCPT.CPTCode as CPT, EncntrCPT.CPTSeq as CPT_Sequence FROM EncntrCPT, Encntr WHERE EncntrCPT.EncntrNum=Encntr.EncntrNum AND Encntr.DatasetSeqNum=" + datasetSeqNum + " AND EncntrCPT.DatasetSeqNum=" + datasetSeqNum;

			if (filterHospitals)
			{	strSQL += " AND Encntr.CompanyCode In (" + entityFilter + ")";	}

			if (filterInsPlans)
			{	strSQL += " AND Encntr.InsurncPlanCode In (" + IPCFilter + ")";}

			oDataSet = db.getDataSet(strSQL);

			dTable = new DataTable();
			dTable = oDataSet.Tables[0].Copy();
			dTable.TableName = "EncCPT";
			dataDS.Tables.Add(dTable);

			dataDS.Relations.Add(new DataRelation("EncCPT", dataDS.Tables["Encounter"].Columns["EncNum"], dataDS.Tables["EncCPT"].Columns["EncNum"]));

			progBar.PerformStep();
			progBar.Refresh();

			// Load DRG Info Table
			drgDS = db.getDataSet("SELECT * FROM DRGRate");
			key[0] = drgDS.Tables[0].Columns["DRGRateCode"];
			drgDS.Tables[0].PrimaryKey = key;

			// Load DRG Type Table
			drgTypeDS = db.getDataSet("SELECT * FROM DRGType WHERE DRGTypeGrpNum=1");
			key[0] = drgTypeDS.Tables[0].Columns["DRGTypeCode"];
			drgTypeDS.Tables[0].PrimaryKey = key;
		}


		private void setupGlobals()
		{
			DataColumn[] key = new DataColumn[1];

			// Load Rates DataSet
			ratesDS = db.getDataSet("SELECT * FROM Rate WHERE RateSchedSeqNum=" + rateSchedSeqNum);
			key[0] = ratesDS.Tables[0].Columns["RateSeqNum"];
			ratesDS.Tables[0].PrimaryKey = key;

			// Load Codes DataSet
			codesDS = db.getDataSet("SELECT Rate.RateSeqNum as RateSeqNum, InOutPatientInd, LOSNum, RateCatgryDescr, RateTypeCode, RateCode FROM Rate, RateCode WHERE Rate.RateSeqNum=RateCode.RateSeqNum AND Not RateTypeCode='PassThru' AND Not RateCatgryDescr IN (" + constLimitCategories + ") AND NOT (RateCode.RateCode='General') AND Rate.RateSchedSeqNum=" + rateSchedSeqNum);

			// Load Limits Codes Dataset
			limitCodesDS = db.getDataSet("SELECT Rate.RateSeqNum as RateSeqNum, InOutPatientInd, RateCatgryDescr, RateTypeCode, RateCode FROM Rate, RateCode WHERE Rate.RateSeqNum=RateCode.RateSeqNum AND Not RateTypeCode='PassThru' AND RateCatgryDescr IN (" + constLimitCategories + ") AND Rate.RateSchedSeqNum=" + rateSchedSeqNum);

			// Load General Hash Table
			genHashTable = new Hashtable();

			genHashTable.Add("I", new Hashtable());
			genHashTable.Add("O", new Hashtable());

			string inOut, rateCat;
			
			oDataSet = db.getDataSet("SELECT Rate.RateSeqNum as RateSeqNum, InOutPatientInd, RateCatgryDescr FROM Rate, RateCode WHERE Rate.RateSeqNum=RateCode.RateSeqNum AND RateCode='General' AND Rate.RateSchedSeqNum=" + rateSchedSeqNum);

			foreach(DataRow dRow in oDataSet.Tables[0].Rows)
			{
				inOut = dRow["InOutPatientInd"].ToString();
				rateCat = dRow["RateCatgryDescr"].ToString();
			
				if (! ( (Hashtable)genHashTable[inOut]).ContainsKey(rateCat))
				{
					( (Hashtable)genHashTable[inOut]).Add(rateCat, dRow["RateSeqNum"]);
				}
			}
	
			// Setup ASC Table
			ascTable = new DataTable();
      
			ascTable.Columns.Add("Code", Type.GetType("System.String"));
			ascTable.Columns.Add("Sequence", Type.GetType("System.String"));
			ascTable.Columns.Add("Group", Type.GetType("System.String"));

			// Load PassThrus Dataset
			passThruDS = db.getDataSet("SELECT * FROM RateCode WHERE RateTypeCode='PassThru' AND RateSchedSeqNum=" + rateSchedSeqNum);

			// Get Medical and Surgical
			oDataSet = db.getDataSet("SELECT * FROM Rate, RateCode WHERE Rate.RateSeqNum=RateCode.RateSeqNum AND Rate.RateSchedSeqNum=" + rateSchedSeqNum + " AND (RateCode='Medical' OR RateCode='Surgical')");

			foreach(DataRow dRow in oDataSet.Tables[0].Rows)
			{
				switch (dRow["RateCode"].ToString())
				{
					case "Medical" : medRate = Convert.ToDouble(dRow["RateValue"]); break;
					case "Surgical" : surgRate = Convert.ToDouble(dRow["RateValue"]); break;
					default: break;
				}
			}

			setup_InpatientHas();
			setup_OutpatientHas();
		}


		public DataSet Analyze(double input_inChrgInc, double input_outChrgInc, double input_costInc)
		{
			inChrgInc = input_inChrgInc;
			outChrgInc = input_outChrgInc;
			costInc = input_costInc;

			analysisDS.Clear();
			setupGlobals();

			progBar.Minimum = 0;
			progBar.Maximum = dataDS.Tables["Encounter"].Rows.Count;
			progBar.Step = 150;

			progLabel.Text = "0 of " + dataDS.Tables["Encounter"].Rows.Count;
			progLabel.Refresh();

			int count = 0;
			int barCount = 0;

			inputRow = analysisDS.Tables[0].NewRow();

			for(int e=0; e<dataDS.Tables["Encounter"].Rows.Count; e++)
			{
				eRow = dataDS.Tables["Encounter"].Rows[e];

				if (analyzeEncounter())
				{
					count += 1;
					barCount += 1;

					if (barCount >= 150)
					{
						barCount = 0;
						progBar.PerformStep();
						progBar.Refresh();
					
						progLabel.Text = count + " of " + dataDS.Tables["Encounter"].Rows.Count;
						progLabel.Refresh();
					}

					stop_time = DateTime.Now;

					elapsed_time = stop_time.Subtract(start_time);

					inputRow["ProcessTime"] = Math.Round(elapsed_time.TotalMilliseconds, 4);
					analysisDS.Tables[0].Rows.Add(inputRow);
				}
			}

			progBar.PerformStep();
			progBar.Refresh();

			progLabel.Text = count + " of " + dataDS.Tables["Encounter"].Rows.Count;
			progLabel.Refresh();

			analysisDS.Relations.Add(new DataRelation("Detail", analysisDS.Tables["RMS Encounter"].Columns["EncNum"], analysisDS.Tables["RMS Detail"].Columns["EncNum"]));

			return analysisDS;
		}


		private bool analyzeEncounter()
		{
			drgTypeRow = drgTypeDS.Tables[0].Rows.Find(eRow["DRG"]);

			if (drgTypeRow == null)
			{
				drgTypeRow = drgTypeDS.Tables[0].NewRow();
				drgTypeRow["DRGTypeDescr"] = "n/a";
			}

			// Check to see if rate schedule ignores Psych/CD and if the encounter is Psych/CD

			if (! ignoreEnc())
			{
				inputRow = analysisDS.Tables[0].NewRow();

				start_time = DateTime.Now;

				if (eRow["InOut"].ToString()=="I")
				{
					chargeInc = inChrgInc;
				}
				else if (eRow["InOut"].ToString()=="O")
				{
					chargeInc = outChrgInc;
				}


				// Setup initial encounter modeling information
				inputRow["EncNum"] = eRow["EncNum"];
				inputRow["InOut"] = eRow["InOut"];
				inputRow["NetRev"] = eRow["NetRev"];
				inputRow["Charges"] = Convert.ToDouble(eRow["Charges"]) * chargeInc;
				inputRow["Model"] = 0;
				inputRow["Payment"] = eRow["Payment"];
				inputRow["Cost"] = Convert.ToDouble(eRow["Cost"]) * costInc;
				inputRow["LOS"] = eRow["LOS"];
				inputRow["Company"] = eRow["Company"];
				inputRow["InsPlan"] = eRow["InsPlan"];

				inputRow["RateCategory"] = "null";
				inputRow["RateType"] = "null";
				inputRow["RateName"] = "null";
				inputRow["PassThruModel"] = 0;
				inputRow["PassThruCharges"] = 0;

				inputRow["ALOS"] = 0;
				inputRow["Weight"] = 0;
				inputRow["High"] = 0;
				inputRow["hiTrim"] = 0;

				if ( (eRow["InOut"].ToString()=="I") && (drgTypeRow["DRGTypeDescr"].ToString() == "n/a") )
				{	inputRow["drgType"] = "All Other";	}
				else
				{	inputRow["drgType"] = drgTypeRow["DRGTypeDescr"];	}

				if ( ! (eRow["DRG"]==null) )
				{	inputRow["DRG"] = eRow["DRG"];	}
				else
				{	inputRow["DRG"] = "NULL";	}
		
				if ( ! (eRow["PrincplProcdrCode"]==null) )
				{	inputRow["PrincplProcdr"] = eRow["PrincplProcdrCode"];	}
				else
				{	inputRow["PrincplProcdr"] = "NULL";	}

				if ( ! (eRow["PrincplDiagCode"]==null) )
				{	inputRow["PrincplDiag"] = eRow["PrincplDiagCode"];	}
				else
				{	inputRow["PrincplDiag"] = "NULL";	}

				detailRows = eRow.GetChildRows(dataDS.Relations["EncDetail"]);
				detailRowsList = new ArrayList();

				for(int d=0; d<detailRows.Length; d++)
				{
					detailRow = detailRows[d];
					inputDetailRow = analysisDS.Tables["RMS Detail"].NewRow();

					inputDetailRow["EncNum"] = detailRow["EncNum"];

					if (detailRow["RevCode"] == null)
					{	inputDetailRow["RevCode"] = "null";	}
					else
					{	inputDetailRow["RevCode"] = detailRow["RevCode"];	}

					if (detailRow["CPT"] == null)
					{	inputDetailRow["CPT"] = "null";	}
					else
					{	inputDetailRow["CPT"] = detailRow["CPT"];	}

					inputDetailRow["Charges"] = Convert.ToDouble(detailRow["Charges"]) * chargeInc;
					inputDetailRow["Quantity"] = detailRow["Quantity"];
					inputDetailRow["RateName"] = "null";
					inputDetailRow["RateType"] = "null";
					inputDetailRow["RateCategory"] = "null";
					inputDetailRow["Model"] = 0;
					inputDetailRow["POC"] = 0;

					detailRowsList.Add(inputDetailRow);
				}

				if (eRow["EncNum"].ToString()=="4601851216")
				{
					int a = 2;
					a += 3;
				}

				switch (eRow["InOut"].ToString())
				{
					case "I" : handleInpatient(); break;
					case "O" : handleOutpatient(); break;
					default: break;
				}

				for(int d=0; d<detailRowsList.Count; d++)
				{
					analysisDS.Tables["RMS Detail"].Rows.Add((DataRow) detailRowsList[d]);
				}

				return true;
			}
			
			return false;
		}

	
		private bool ignoreEnc()
		{
			codeRows = codesDS.Tables[0].Select("InOutPatientInd='" + eRow["InOut"] + "' AND RateCatgryDescr='Ignore' AND RateTypeCode='DRG' AND RateCode='" + eRow["DRG"] + "'");

			if (codeRows.Length>0)
			{	return true;	}
			else
			{	return false;	}
		}



		#region "Rate Limits ie Stop Loss, Floor, Ceiling, LessorOf"

		private void handleLimits()
		{
			inOut = eRow["InOut"].ToString();

			// Stop Loss

			codeRows = limitCodesDS.Tables[0].Select("InOutPatientInd='" + inOut + "' AND RateTypeCode='DRG' AND RateCatgryDescr='StopLoss' AND RateCode='" + eRow["DRG"] + "'");
			if (codeRows.Length>0)
			{
				handleStopLoss(codeRows[0]["RateSeqNum"].ToString());
			}
			else if ( ((Hashtable)genHashTable[inOut]).ContainsKey("StopLoss") )
			{
				handleStopLoss(((Hashtable)genHashTable[inOut])["StopLoss"].ToString());
			}

			// Floor

			codeRows = limitCodesDS.Tables[0].Select("InOutPatientInd='" + inOut + "' AND RateTypeCode='DRG' AND RateCatgryDescr='Floor' AND RateCode='" + eRow["DRG"] + "'");
			if (codeRows.Length > 0)
			{
				handleFloor(codeRows[0]["RateSeqNum"].ToString());
			}
			else if ( ((Hashtable)genHashTable[inOut]).ContainsKey("Floor") )
			{
				handleStopLoss(((Hashtable)genHashTable[inOut])["Floor"].ToString());
			}

			// Ceiling

			codeRows = limitCodesDS.Tables[0].Select("InOutPatientInd='" + inOut + "' AND RateTypeCode='DRG' AND RateCatgryDescr='Ceiling' AND RateCode='" + eRow["DRG"] + "'");
			if (codeRows.Length > 0)
			{
				handleCeiling(codeRows[0]["RateSeqNum"].ToString());
			}
			else if ( ((Hashtable)genHashTable[inOut]).ContainsKey("Ceiling") )
			{
				handleStopLoss(((Hashtable)genHashTable[inOut])["Ceiling"].ToString());
			}

			// Lessor Of

			codeRows = limitCodesDS.Tables[0].Select("InOutPatientInd='" + inOut + "' AND RateTypeCode='DRG' AND RateCatgryDescr='LessorOf' AND RateCode='" + eRow["DRG"] + "'");
			if ( (codeRows.Length > 0) || (((Hashtable)genHashTable[inOut]).ContainsKey("LessorOf") ) )
			{
				if ( Convert.ToDouble(inputRow["Model"]) > Convert.ToDouble(inputRow["Charges"]) )
				{
					inputRow["RateCategory"] = "LessorOf";
					inputRow["RateType"] = "LessorOf";
					inputRow["RateName"] = "LessorOf";
					inputRow["Model"] = inputRow["Charges"];
				}
			}
		}


		private void handleStopLoss(string rateSeqNum)
		{
			rateRow = ratesDS.Tables[0].Rows.Find(rateSeqNum);

			switch (rateRow["RateTypeDescr"].ToString())
			{
				case "Type I" :

					if (Convert.ToDouble(inputRow["Charges"]) > Convert.ToDouble(rateRow["RateValue"]))
					{
						inputRow["RateName"] = rateRow["RateName"];
						inputRow["RateType"] = rateRow["RateTypeDescr"];
						inputRow["RateCategory"] = rateRow["RateCatgryDescr"];

						inputRow["passThruModel"] = getPassThrus(inOut, rateRow["RateSeqNum"].ToString());
						inputRow["passThruCharges"] = passThruCharges;

						limitModel = (Convert.ToDouble(inputRow["Charges"]) - Convert.ToDouble(inputRow["passThruCharges"])) * Convert.ToDouble(rateRow["ThreshldNum"]) + Convert.ToDouble(inputRow["passThruModel"]);

						if ( (Convert.ToDouble(rateRow["AddtnlDayRate"])>0) && ((limitModel / Convert.ToDouble(eRow["LOS"])) > Convert.ToDouble(rateRow["AddtnlDayRate"])) )
						{
							inputRow["RateType"] = "Daily Cap";
							limitModel = Convert.ToDouble(rateRow["AddtnlDayRate"]) * Convert.ToDouble(eRow["LOS"]);
						}

						inputRow["Model"] = limitModel;
						inputRow["POC"] = Convert.ToDouble(inputRow["Model"]) / Convert.ToDouble(inputRow["Charges"]);
					}
					break;

				case "Type II" :
					if (Convert.ToDouble(inputRow["Charges"]) > Convert.ToDouble(rateRow["RateValue"]))
					{
						inputRow["RateName"] = rateRow["RateName"];
						inputRow["RateType"] = rateRow["RateTypeDescr"];
						inputRow["RateCategory"] = rateRow["RateCatgryDescr"];

						inputRow["passThruModel"] = getPassThrus(inOut, rateRow["RateSeqNum"].ToString());
						inputRow["passThruCharges"] = passThruCharges;

						limitModel = (Convert.ToDouble(inputRow["Charges"]) - Convert.ToDouble(inputRow["passThruCharges"]) - Convert.ToDouble(rateRow["RateValue"])) * Convert.ToDouble(rateRow["ThreshldNum"]) + Convert.ToDouble(inputRow["passThruModel"]);

						if ( (Convert.ToDouble(rateRow["AddtnlDayRate"]) > 0) && ( (limitModel / Convert.ToDouble(eRow["LOS"])) > Convert.ToDouble(rateRow["AddtnlDayRate"])) )
						{
							inputRow["RateType"] = "Daily Cap";
							limitModel = Convert.ToDouble(rateRow["AddtnlDayRate"]) * Convert.ToDouble(eRow["LOS"]);
						}

						inputRow["Model"] = limitModel;
						inputRow["POC"] = Convert.ToDouble(inputRow["Model"]) / Convert.ToDouble(inputRow["Charges"]);
					}
					break;

				default: break;
			}
		}


		private void handleFloor(string rateSeqNum)
		{
			rateRow = ratesDS.Tables[0].Rows.Find(rateSeqNum);

			switch(rateRow["RateTypeDescr"].ToString())
			{
				case "CaseRate" :
					if (Convert.ToDouble(inputRow["Model"]) < Convert.ToDouble(rateRow["RateValue"]))
					{
						inputRow["RateName"] = rateRow["RateName"];
						inputRow["RateType"] = rateRow["RateTypeDescr"];
						inputRow["RateCategory"] = rateRow["RateCatgryDescr"];

						inputRow["passThruModel"] = 0;
						inputRow["passThruCharges"] = 0;
						inputRow["Model"] = rateRow["RateValue"];
						inputRow["POC"] = Convert.ToDouble(inputRow["Model"]) / Convert.ToDouble(inputRow["Charges"]);
					}
					break;

				case "POC" :
					if (Convert.ToDouble(inputRow["POC"]) < Convert.ToDouble(rateRow["RateValue"]))
					{
						inputRow["RateName"] = rateRow["RateName"];
						inputRow["RateType"] = rateRow["RateTypeDescr"];
						inputRow["RateCategory"] = rateRow["RateCatgryDescr"];

						inputRow["passThruModel"] = 0;
						inputRow["passThruCharges"] = 0;
						inputRow["Model"] = Convert.ToDouble(inputRow["Charges"]) * Convert.ToDouble(rateRow["RateValue"]);
						inputRow["POC"] = Convert.ToDouble(inputRow["Model"]) / Convert.ToDouble(inputRow["Charges"]);
					}
					break;

				default: break;
			}
		}


		private void handleCeiling(string rateSeqNum)
		{
			rateRow = ratesDS.Tables[0].Rows.Find(rateSeqNum);

			switch (rateRow["RateTypeDescr"].ToString())
			{
				case "CaseRate" :
					if (Convert.ToDouble(inputRow["Model"]) > Convert.ToDouble(rateRow["RateValue"]))
					{
						inputRow["RateName"] = rateRow["RateName"];
						inputRow["RateType"] = rateRow["RateType"];
						inputRow["RateCategory"] = rateRow["RateCatgryDescr"];

						inputRow["passThruModel"] = 0;
						inputRow["passThryCharges"] = 0;
						inputRow["Model"] = rateRow["RateValue"];
					}
					break;

				default: break;
			}
		}


		#endregion


		#region "Inpatient"

		private void setup_InpatientHas()
		{
			in_HasPerDiem = new Hashtable();
			in_HasPerDiem.Add("DRG", false);
			in_HasPerDiem.Add("RevCode", false);

			oDataSet = db.getDataSet("SELECT RateTypeCode FROM Rate, RateCode WHERE Rate.RateSeqNum=RateCode.RateSeqNum AND InOutPatientInd='I' AND RateCatgryDescr='PerDiem' AND Rate.RateSchedSeqNum=" + rateSchedSeqNum + " GROUP BY RateTypeCode");

			foreach(DataRow dRow in oDataSet.Tables[0].Rows)
			{
				in_HasPerDiem[dRow["RateTypeCode"]] = true;
			}

			in_HasFFS = new Hashtable();
			in_HasFFS.Add("DRG", false);
			in_HasFFS.Add("RevCode", false);
			in_HasFFS.Add("CPT", false);
			in_HasFFS.Add("ICD9", false);
			in_HasFFS.Add("ICD9D", false);

			oDataSet = db.getDataSet("SELECT RateTypeCode FROM Rate, RateCode WHERE Rate.RateSeqNum=RateCode.RateSeqNum AND InOutPatientInd='I' AND RateCatgryDescr='FFS' AND Rate.RateSchedSeqNum=" + rateSchedSeqNum + " GROUP BY RateTypeCode");

			foreach(DataRow dRow in oDataSet.Tables[0].Rows)
			{
				in_HasFFS[dRow["RateTypeCode"]] = true;
			}
		}


		private void handleInpatient()
		{
			rateFound = false;

			codeRows = codesDS.Tables[0].Select("InOutPatientInd='I' AND RateTypeCode='DRG' AND RateCode='" + eRow["DRG"] + "'");

			if (codeRows.Length > 0)
			{
				rateFound = true;
				foundRateSeqNum = codeRows[0]["RateSeqNum"].ToString();
				foundRateCatgry = codeRows[0]["RateCatgryDescr"].ToString();
			}

			if ( ((bool)in_HasFFS["ICD9"]) && (rateFound == false))
			{
				codeRows = codesDS.Tables[0].Select("InOutPatientInd='I' AND RateTypeCode='ICD9' AND RateCode='" + eRow["PrincplProcdrCode"] + "'");

				if (codeRows.Length > 0)
				{
					rateFound = true;
					foundRateSeqNum = codeRows[0]["RateSeqNum"].ToString();
					foundRateCatgry = codeRows[0]["RateCatgryDescr"].ToString();
				}
			}

			if ( ((bool)in_HasFFS["ICD9D"]) && (rateFound==false))
			{
				codeRows = codesDS.Tables[0].Select("InOutPatientInd='I' AND RateTypeCode='ICD9D' AND RateCode='" + eRow["PrincplDiagCode"] + "'");

				if (codeRows.Length > 0)
				{
					rateFound = true;
					foundRateSeqNum = codeRows[0]["RateSeqNum"].ToString();
					foundRateCatgry = codeRows[0]["RateCatgryDescr"].ToString();
				}
			}

			if ( ((bool)in_HasFFS["RevCode"]) && (rateFound == false))
			{
				foreach(DataRow detailRow in detailRows)
				{
					codeRows = codesDS.Tables[0].Select("InOutPatientInd='I' AND RateTypeCode='RevCode' AND RateCatgryDescr='FFS' AND RateCode='" + detailRow["RevCode"] + "'");

					if (codeRows.Length > 0)
					{
						rateFound = true;
						foundRateSeqNum = codeRows[0]["RateSeqNum"].ToString();
						foundRateCatgry = codeRows[0]["RateCatgryDescr"].ToString();
					}
				}
			}


			if (!rateFound) // No rate found therefore handle modeling as a regular Per Diem
			{

				if (  ((Hashtable)genHashTable["I"]).ContainsKey("FFS")  )
				{	handlePOC("I", ((Hashtable)genHashTable["I"])["FFS"].ToString());	}
				else if (  ((Hashtable)genHashTable["I"]).ContainsKey("BaseRate")  )
				{	handleBaseRate(((Hashtable)genHashTable["I"])["BaseRate"].ToString());	}
				else
				{	handlePerDiem();	}
			}
			else
			{
				switch (foundRateCatgry)
				{
					case "FFS" : handleFFS(foundRateSeqNum); break;
					case "BaseRate" : handleBaseRate(foundRateSeqNum); break;
					case "PerDiem" : handlePerDiem(); break;
					default: break;
				}
			}

			// Check limits *************************

			inputRow["POC"] = Convert.ToDouble(inputRow["Model"]) / Convert.ToDouble(inputRow["Charges"]);
			handleLimits();
		}


		private void handlePOC(string inOut, string rateSeqNum)
		{
			rateRow = ratesDS.Tables[0].Rows.Find(rateSeqNum);

			inputRow["RateName"] = rateRow["RateName"];
			inputRow["RateType"] = rateRow["RateTypeDescr"];
			inputRow["RateCategory"] = rateRow["RateCatgryDescr"];

			inputRow["passThruModel"] = getPassThrus(inOut, rateSeqNum);
			inputRow["passThruCharges"] = passThruCharges;
			inputRow["Model"]  = ( (  Convert.ToDouble(inputRow["Charges"]) - Convert.ToDouble(inputRow["passThruCharges"]) ) * Convert.ToDouble(rateRow["RateValue"]) ) + Convert.ToDouble(inputRow["passThruModel"]);
		}


		private void handleFFS(string rateSeqNum)
		{
			rateRow = ratesDS.Tables[0].Rows.Find(rateSeqNum);

			inputRow["RateName"] = rateRow["RateName"];
			inputRow["RateType"] = rateRow["RateTypeDescr"];
			inputRow["RateCategory"] = rateRow["RateCatgryDescr"];

			switch (rateRow["RateTypeDescr"].ToString())
			{
				case "CaseRate" :
					inputRow["Model"] = rateRow["RateValue"];
					los = Convert.ToInt16(eRow["LOS"]) - Convert.ToInt16(rateRow["LOSNum"]);  // Check to see if LOS exceeds allocated limit

					if (los>0)
					{
						if (Convert.ToDouble(rateRow["AddtnlDayRate"])==0)
						{
							inputRow["Model"] = addPerDiems(detailRows, Convert.ToDouble(inputRow["Model"]), los);	// Apply Applicable Additional Day Rate
						}
						else
						{
							inputRow["Model"] = Convert.ToDouble(inputRow["Model"]) + los * Convert.ToDouble(rateRow["AddtnlDayRate"]); // Apply FFS Additional Day Rate
						}
					}

					inputRow["passThruModel"] = getPassThrus("I", rateRow["RateSeqNum"].ToString());
					inputRow["passThruCharges"] = passThruCharges;
					inputRow["Model"] = Convert.ToDouble(inputRow["Model"]) + Convert.ToDouble(inputRow["passThruModel"]);
					break;

				case "POC" :
					inputRow["passThruModel"] = getPassThrus("I", rateRow["RateSeqNum"].ToString());
					inputRow["passThruCharges"] = passThruCharges;
					inputRow["Model"] = (Convert.ToDouble(inputRow["Charges"]) - Convert.ToDouble(inputRow["passThruCharges"])) * Convert.ToDouble(rateRow["RateValue"]) + Convert.ToDouble(inputRow["passThruModel"]);
					break;

				default: break;
			}
		}


		private void handleBaseRate(string rateSeqNum)
		{
			baseRateRow = ratesDS.Tables[0].Rows.Find(rateSeqNum);

			// Find out which Wgt Table to use
			string drgWgtID;
		
			oDataSet = db.getDataSet("SELECT * FROM RateCode WHERE RateSchedSeqNum=" + rateSchedSeqNum + " AND RateSeqNum=" + rateSeqNum + " AND RateTypeCode='Table'");

			if (oDataSet.Tables[0].Rows.Count > 0)
			{	drgWgtID = oDataSet.Tables[0].Rows[0]["RateCode"].ToString();	}
			else
			{	drgWgtID = "1";	}

			oDataSet = db.getDataSet("SELECT * FROM DRGWgt WHERE DRGWgtIDSeqNum=" + drgWgtID + " AND DRGCode='" + eRow["DRG"] + "'");

			if (oDataSet.Tables[0].Rows.Count > 0)
			{	
				drgWeightRow = oDataSet.Tables[0].Rows[0];

				inputRow["RateName"] = baseRateRow["RateName"];
				inputRow["RateType"] = baseRateRow["RateTypeDescr"];
				inputRow["RateCategory"] = baseRateRow["RateCatgryDescr"];

				hiTrimModel = 0;
				difference = Convert.ToDouble(eRow["LOS"]) - Convert.ToDouble(drgWeightRow["HighTrim"]);

				if (difference>0)
				{
					if (Convert.ToDouble(drgWeightRow["ALOS"]) > 0)
					{
						hiTrimModel = ((Convert.ToDouble(baseRateRow["RateValue"]) * Convert.ToDouble(drgWeightRow["DRGWgt"])) / Convert.ToDouble(drgWeightRow["ALOS"])) * 0.7 * difference;
					}
				}

				inputRow["ALOS"] = drgWeightRow["ALOS"];
				inputRow["Weight"] = drgWeightRow["DRGWgt"];
				inputRow["High"] = drgWeightRow["HighTrim"];
				inputRow["hiTrim"] = hiTrimModel;

				inputRow["passThruModel"] = getPassThrus("I", rateSeqNum);
				inputRow["passThruCharges"] = passThruCharges;

				inputRow["Model"] = Convert.ToDouble(drgWeightRow["DRGWgt"]) * Convert.ToDouble(baseRateRow["RateValue"]) + Convert.ToDouble(inputRow["passThruModel"]) + hiTrimModel;
			}
		}

																									
		private void handlePerDiem()
		{
			// **************************** PER DIEM
			inputRow["RateName"] = "PerDiem";
			inputRow["RateType"] = "PerDiem";
			inputRow["RateCategory"] = "PerDiem";
			inputRow["Model"] = addPerDiems(detailRows, Convert.ToDouble(inputRow["Model"]), Convert.ToInt16(eRow["LOS"]));
      
			inputRow["passThruModel"] = getPassThrus("I", "");
			inputRow["passThruCharges"] = passThruCharges;

			inputRow["Model"] = Convert.ToDouble(inputRow["Model"]) + Convert.ToDouble(inputRow["passThruModel"]);
		}


		#region "addPerDiems"

		double surgicalCharges;
		string revCode;

		private double addPerDiems(DataRow[] detailRows, double model, double iLOS)
		{
			if ( ((bool)in_HasPerDiem["DRG"]) )
			{
				codeRows = codesDS.Tables[0].Select("InOutPatientInd='I' AND RateCatgryDescr='PerDiem' AND RateTypeCode='DRG' AND RateCode='" + eRow["DRG"] + "'");

				if (codeRows.Length > 0) // '*** Found DRG Per Diem rate
				{
					rateRow = ratesDS.Tables[0].Rows.Find(codeRows[0]["rateseqNum"]);

					model = model + Convert.ToDouble(rateRow["RateValue"]) * iLOS;

					return model;
				}
			}

			if ( ((bool)in_HasPerDiem["RevCode"]) )
			{
				surgicalCharges = 0;

				foreach(DataRow detailRow in detailRowsList)
				{
					if (detailRow["RevCode"]==null)
					{
						revCode = "";
					}
					else
					{
						revCode = detailRow["RevCode"].ToString().Trim();
					}

					if (revCode == "360" || revCode == "361" || revCode == "369" || revCode == "362" || revCode == "367" || revCode == "481" || revCode == "750" || revCode == "759" || revCode == "790" || revCode == "799")
					{
						surgicalCharges = surgicalCharges + Convert.ToDouble(detailRow["Charges"]);
					}
						
					codeRows = codesDS.Tables[0].Select("InOutPatientInd='I' AND RateCatgryDescr='PerDiem' AND RateTypeCode='RevCode' AND RateCode='" + detailRow["RevCode"] + "'");

					if ( (codeRows.Length > 0) && (iLOS > 0) )
					{
						rateRow = ratesDS.Tables[0].Rows.Find(codeRows[0]["rateSeqNum"]);

						detailRow["RateName"] = rateRow["RateName"];
						detailRow["RateType"] = rateRow["RateTypeDescr"];
						detailRow["RateCategory"] = rateRow["RateCatgryDescr"];
						detailRow["Model"] = Convert.ToDouble(rateRow["RateValue"]) * Convert.ToDouble(detailRow["Quantity"]);

						model = model + Convert.ToDouble(detailRow["Model"]);
						iLOS = iLOS - Convert.ToDouble(detailRow["Quantity"]);
					}
				}
			}

			if ( (iLOS > 0) && (! (inputRow["drgType"].ToString()== "Neonate")) && (! (inputRow["drgType"].ToString()== "Normal Newborn")) ) // If DRG is Neonate then do not calculate a rate for leftover days
			{
				if (surgicalCharges > 0)
				{
					inputRow["RateName"] = "Surgical";
					model = model + iLOS * surgRate;
				}
				else
				{
					inputRow["RateName"] = "Medical";
					model = model + iLOS * medRate;
				}
			}

			return model;
		}


		#endregion

		#endregion


		#region "Outpatient"
    
		private void setup_OutpatientHas()
		{
			// **** SETUP FFS Code Has
			out_HasFFSCodeTypes = new Hashtable();
			out_HasFFSCodeTypes.Add("DRG", false);
			out_HasFFSCodeTypes.Add("RevCode", false);
			out_HasFFSCodeTypes.Add("CPT", false);
			out_HasFFSCodeTypes.Add("ICD9", false);
			out_HasFFS = false;

			oDataSet = db.getDataSet("SELECT RateTypeCode FROM Rate, RateCode WHERE Rate.RateSeqNum=RateCode.RateSeqNum AND InOutPatientInd='O' AND RateCatgryDescr='FFS' AND NOT (RateCode.RateCode='General') AND Rate.RateSchedSeqNum=" + rateSchedSeqNum + " GROUP BY RateTypeCode");

			foreach(DataRow dRow in oDataSet.Tables[0].Rows)
			{
				out_HasFFS = true;
				out_HasFFSCodeTypes[dRow["RateTypeCode"]] = true;
			}
				
			// **** CBNA Check
			// **** I used this for one particular rate schedule and I think it is just a one case issue. It probably can be removed.
			oDataSet = db.getDataSet("SELECT * FROM Rate, RateCode WHERE Rate.RateSeqNum=RateCode.RateSeqNum AND RateCatgryDescr='FFS' AND InOutPatientInd='O' AND RateTypeCode='RevCode' AND RateCode='CBNA' AND Rate.RateSchedSeqNum=" + rateSchedSeqNum);

			if (oDataSet.Tables[0].Rows.Count > 0)
			{
				out_HasCBNA = true;
				CBNARow = oDataSet.Tables[0].Rows[0];
			}
			else
			{
				out_HasCBNA = false;
			}

			// ******** SETUP ASC Has and Other Variables
			oDataSet = db.getDataSet("SELECT * FROM Rate WHERE RateCatgryDescr='ASC' AND RateTypeDescr='Main' AND InOutPatientInd='O' and RateSchedSeqNum=" + rateSchedSeqNum);

			if (oDataSet.Tables[0].Rows.Count == 0)
			{
				out_HasSDS = false;
			}
			else
			{
				out_HasSDS = true;
				ascRateRow = oDataSet.Tables[0].Rows[0];
			

				// **** Load ASC Group Table
				ascGroupDS = db.getDataSet("SELECT * FROM ASCCode WHERE ASCGrpSeqNum=" + ascRateRow["RateValue"]);
				DataColumn[] key = new DataColumn[1];
				key[0] = ascGroupDS.Tables[0].Columns["ASCCode"];
				ascGroupDS.Tables[0].PrimaryKey = key;

				// **** Load Rates and Thresholds for groups
				oDataSet = db.getDataSet("SELECT * FROM Rate WHERE RateCatgryDescr='ASC' AND RateTypeDescr='Group' AND InOutPatientInd='O' AND RateSchedSeqNum=" + rateSchedSeqNum);
				rates = new Hashtable();
				thresholds = new Hashtable();

				foreach(DataRow dRow in oDataSet.Tables[0].Rows)
				{
					rates.Add(dRow["RateName"].ToString().Trim(), dRow["RateValue"].ToString());
					thresholds.Add(dRow["RateName"].ToString().Trim(), dRow["ThreshldNum"].ToString());
				}

				// **** Load Reimbursement Percentages
				oDataSet = db.getDataSet("SELECT * FROM Rate WHERE RateCatgryDescr='ASC' AND RateTypeDescr='Rate' AND InOutPatientInd='O' AND RateSchedSeqNum=" + rateSchedSeqNum + " ORDER BY RateName ASC");
				rateList.Clear();

				foreach(DataRow dRow in oDataSet.Tables[0].Rows)
				{
					rateList.Add(dRow["RateValue"]);
				}
			}
		}
	

		private void handleOutpatient()
		{
			rateToApply_SeqNum = "-1";
			rateToApply_Priority = -1;		// Checks outpatient FFS priority

			// Priority -1 is default for the variable. The default priority for a FFS rate is 0. Zero has the last priority otherwise the lower the priority number the higher the priority. Maybe order is a better name but this is just a work around until a better outpatient design can be constructed.

			if (out_HasFFS)
			{
				// ***** Encounter Level Checks
				if ( ((bool)out_HasFFSCodeTypes["DRG"]) )
				{
					codeRows = codesDS.Tables[0].Select("InOutPatientInd='O' AND RateCatgryDescr IN ('FFS','ASC') AND RateTypeCode='DRG' AND RateCode='" + eRow["DRG"] + "'");

					if (codeRows.Length>0)
					{
						setRateToApply(codeRows[0]["RateSeqNum"].ToString(), Convert.ToInt16(codeRows[0]["LOSNum"]));
					}
				}

				if ( ((bool)out_HasFFSCodeTypes["ICD9"]) )
				{
					codeRows = codesDS.Tables[0].Select("InOutPatientInd='O' AND RateCatgryDescr IN ('FFS','ASC') AND RateTypeCode='ICD9' AND RateCode='" + eRow["PrincipalProc"].ToString() + "'");

					if (codeRows.Length > 0)
					{
						setRateToApply(codeRows[0]["RateSeqNum"].ToString(), Convert.ToInt16(codeRows[0]["LOSNum"]));
					}
				}

				// ***** Detail Level Checks

				foreach(DataRow detailRow in detailRows)
				{
					// *** Check for FFS
					if ( ((bool)out_HasFFSCodeTypes["RevCode"]) )
					{
						codeRows = codesDS.Tables[0].Select("InOutPatientInd='O' AND RateCatgryDescr IN ('FFS','ASC') AND RateTypeCode='RevCode' AND RateCode='" + detailRow["RevCode"].ToString() + "'");

						if (codeRows.Length > 0)
						{
							setRateToApply(codeRows[0]["RateSeqNum"].ToString(), Convert.ToInt16(codeRows[0]["LOSNum"]));
						}
					}

					if ( ((bool)out_HasFFSCodeTypes["CPT"]) )
					{
						codeRows = codesDS.Tables[0].Select("InOutPatientInd='O' AND RateCatgryDescr IN ('FFS','ASC') AND RateTypeCode='CPT' AND RateCode='" + detailRow["CPT"].ToString() + "'");

						if (codeRows.Length > 0)
						{
							setRateToApply(codeRows[0]["RateSeqNum"].ToString(), Convert.ToInt16(codeRows[0]["LOSNum"]));
						}
					}

					if (out_HasSDS)
					{
						if (Convert.ToInt16(ascRateRow["AddtnlDayRate"])== 1)
						{
							if (! (detailRow["CPT"]==null) )
							{
								if ( (Convert.ToInt64(detailRow["CPT"]) >= 10000) && (Convert.ToInt64(detailRow["CPT"]) <= 69999) )
								{
									setRateToApply(ascRateRow["RateSeqNum"].ToString(), Convert.ToInt16(ascRateRow["LOSNum"]));
								}
							}
						}
					}

				}
			}


			if (! (rateToApply_SeqNum == "-1")	)	// *** Rate found
			{
				rateRow = ratesDS.Tables[0].Rows.Find(rateToApply_SeqNum);

				switch (rateRow["RateCatgryDescr"].ToString())
				{
					case "FFS" :	handleOutpatientFFS(rateRow); break;
					case "ASC" : handleASC(); break;
				}
			}
			else
			{
				if ( ((Hashtable)genHashTable["O"]).ContainsKey("FFS"))
				{
					handlePOC("O", ((Hashtable)genHashTable["O"])["FFS"].ToString());
				}
			}

			handleLimits();

			if (out_HasCBNA)
			{
				if (eRow["Admit_Service"].ToString().Trim() == "OJ")
				{
					inputRow["RateName"] = CBNARow["RateName"];
					inputRow["RateCategory"] = "CBNA";
					inputRow["RateType"] = CBNARow["RateTypeDescr"];

					switch (CBNARow["RateTypeDescr"].ToString())
					{
						case "CaseRate" : inputRow["Model"] = CBNARow["RateValue"]; break;
						case "POC" : inputRow["Model"] = Convert.ToDouble(CBNARow["RateValue"]) * Convert.ToDouble(inputRow["Charges"]); break;
						default : break;
					}

					inputRow["PassThruCharges"] = 0;
					inputRow["PassThruModel"] = 0;
				}
			}

			inputRow["POC"] = Convert.ToDouble(inputRow["Model"]) / Convert.ToDouble(inputRow["Charges"]);
		}


		private void setRateToApply(string newSeqNum, int newPriority)
		{
			if 
				( ( (rateToApply_Priority== -1) || (newPriority < rateToApply_Priority ) )
				&&
				( (newPriority > 0) || (rateToApply_Priority == 0 && newPriority > 0) ) )
			{
				rateToApply_SeqNum = newSeqNum;
				rateToApply_Priority = newPriority;
			}
		}


		private void handleOutpatientFFS(DataRow rateRow)
		{
			inputRow["RateName"] = rateRow["RateName"];
			inputRow["RateType"] = rateRow["RateTypeDescr"];
			inputRow["RateCategory"] = rateRow["RateCatgryDescr"];

			switch(rateRow["RateTypeDescr"].ToString())
			{
				case "CaseRate" :
					inputRow["Model"] = rateRow["RateValue"];

					inputRow["passThruModel"] = getPassThrus("O", rateRow["RateSeqNum"].ToString());
					inputRow["passThruCharges"] = passThruCharges;
					inputRow["Model"] = Convert.ToDouble(inputRow["Model"]) + Convert.ToDouble(inputRow["passThruModel"]);
					break;

				case "POC" :
					inputRow["passThruModel"] = getPassThrus("O", rateRow["RateSeqNum"].ToString());
					inputRow["passThruCharges"] = passThruCharges;
					inputRow["Model"] = (Convert.ToDouble(inputRow["Charges"]) - Convert.ToDouble(inputRow["passThruCharges"])) * Convert.ToDouble(rateRow["RateValue"]) + Convert.ToDouble(inputRow["passThruModel"]);
					break;
				
				default: break;
			}
		}


		private void handleASC()
		{
			ascTable.Clear();

			cptRows = eRow.GetChildRows(dataDS.Relations["EncCpt"]);

			if (cptRows.Length == 0)
			{
				ascRow = ascTable.NewRow();
        
				ascRow["Code"] = "";
				ascRow["Sequence"] = 1;
				ascRow["Group"] = "Unlisted";

				ascTable.Rows.Add(ascRow);
			}
			else
			{
				foreach(DataRow dRow in cptRows)
				{
					ascRow = ascTable.NewRow();

					if (dRow["CPT"] == null)
					{
						ascCPT = "";
					}
					else
					{
						ascCPT = dRow["CPT"].ToString().Trim();
					}

					ascRow["Code"] = ascCPT;

					if (dRow["Cpt_Sequence"] == null)
					{
						ascRow["Sequence"] = "";
					}
					else
					{
						ascRow["Sequence"] = dRow["Cpt_Sequence"].ToString().Trim();
					}

					foundRow = ascGroupDS.Tables[0].Rows.Find(ascCPT);

					if (! (foundRow==null) )
					{
						ascRow["Group"] = foundRow["ASCGrpNum"];
					}
					else
					{
						ascRow["Group"] = "Unlisted";
					}

					ascTable.Rows.Add(ascRow);
				}
			}

			ascGroupList = ascTable.Select("", "Sequence ASC");
			tempModel = 0;
			ascRateListIndex = 0;
			ascPOCApplied = false;

			inputRow["RateType"] = "CaseRate";
			inputRow["RateCategory"] = "SDS";

			foreach(DataRow dRow in ascGroupList)
			{
				ascRow = analysisDS.Tables["SDS"].NewRow();

				ascRow["EncNum"] = eRow["EncNum"];
				ascRow["Code"] = dRow["Code"];
				ascRow["Sequence"] = dRow["Sequence"];
				ascRow["Group"] = dRow["Group"];
				ascRow["Rate"] = 0;
				ascRow["Model"] = 0;

				if (!ascPOCApplied)
				{
					ascGroup = dRow["Group"].ToString().Trim();

					if ( (Convert.ToInt16(rates[ascGroup]) > 1) || (Convert.ToDouble(rates[ascGroup])==0) ) // Check to see if the rate is a Case Rate or Zero
					{
						ascRow["Rate"] = rates[ascGroup];
						ascRow["Model"] = Convert.ToDouble(rates[ascGroup]) * Convert.ToDouble(rateList[ascRateListIndex]);

						tempModel = tempModel + Convert.ToDouble(rates[ascGroup]) * Convert.ToDouble(rateList[ascRateListIndex]);

						if (ascRateListIndex == 0)
						{
							inputRow["RateName"] = ascGroup;
						}
						
						if ( (ascRateListIndex < rateList.Count - 1) && (Convert.ToDouble(rateList[ascRateListIndex]) > 0) )
						{
							ascRateListIndex = ascRateListIndex + 1;
						}
					}
					else				// The rate is a POC
					{
						inputRow["passThruModel"] = getPassThrus("O", ascRateRow["RateSeqNum"].ToString());
						inputRow["RateName"] = ascGroup;
						inputRow["RateType"] = "POC";

						ascRow["Rate"] = rates[ascGroup];
						ascRow["Model"] = (Convert.ToDouble(inputRow["Charges"]) - Convert.ToDouble(passThruCharges)) * Convert.ToDouble(rates[ascGroup]);

						inputRow["Model"] = Convert.ToDouble(ascRow["Model"]) + Convert.ToDouble(inputRow["passThruModel"]);

						tempModel = Convert.ToDouble(inputRow["Model"]);
						ascPOCApplied = true;
					}
				}

				analysisDS.Tables["SDS"].Rows.Add(ascRow);
			}

			/*
			'If Not ascPOCApplied Then
		'inputRow.Item("passThruModel") = getPassThrus("O", ASCRateRow.Item("RateSeqNum"))
		'inputRow.Item("passThruCharges") = passThruCharges
		'inputRow.Item("Model") = tempModel + inputRow.Item("passThruModel")
		'End If
		*/

			if (Convert.ToDouble(ascRateRow["ThreshldNum"]) > 0)
			{
				if ( (tempModel > Convert.ToDouble(ascRateRow["ThreshldNum"])) && (Convert.ToDouble(ascRateRow["ThreshldNum"]) > 1) )
				{
					inputRow["RateName"] = "SDS Ceiling";
					inputRow["passThruModel"] = getPassThrus("O", ascRateRow["RateSeqNum"].ToString());

					inputRow["passThruCharges"] = passThruCharges;
					inputRow["Model"] = Convert.ToDouble(ascRateRow["ThreshldNum"]) + Convert.ToDouble(inputRow["passThruModel"]);
				}
				else if ( ( (tempModel / Convert.ToDouble(inputRow["Charges"])) > Convert.ToDouble(ascRateRow["ThreshldNum"]) ) && (Convert.ToDouble(ascRateRow["ThreshldNum"]) <= 1) )
				{
					inputRow["RateName"] = "SDS POC Ceiling";
					inputRow["passThruModel"] = getPassThrus("O", ascRateRow["RateSeqNum"].ToString());
					inputRow["passThruCharges"] = passThruCharges;

					inputRow["Model"] = ((Convert.ToDouble(inputRow["Charges"]) - Convert.ToDouble(inputRow["passThruCharges"])) * Convert.ToDouble(ascRateRow["ThreshldNum"])) + Convert.ToDouble(inputRow["passThruModel"]);
				}
			}

			if (! ascPOCApplied)
			{
				inputRow["passThruModel"] = getPassThrus("O", ascRateRow["RateSeqNum"].ToString());
				inputRow["passThruCharges"] = passThruCharges;

				inputRow["Model"] = tempModel + Convert.ToDouble(inputRow["passThruModel"]);
			}

		}


		#endregion


		#region "PassThrus"

		// **** TODO: Add other Code checks for PassThrus... mainly CPTs. Currently it only checks for RevCode PassThrus

		private double getPassThrus(string inOut, string rateSeqNum)
		{
			allPassThrus = false;
			nonePassThrus = false;
			passThruCharges = 0;

			totalPassThruModel = 0;
			perVisitsApplied = new ArrayList();
			validPassThrusList = new ArrayList();

			if (rateSeqNum == "")
			{
				allPassThrus = true;
			}
			else
			{
				validPassThrus = passThruDS.Tables[0].Select("RateSeqNum=" + rateSeqNum);

				foreach(DataRow dRow in validPassThrus)
				{
					if (dRow["RateCode"].ToString() == "All")
					{
						allPassThrus = true;
					}
					else if (dRow["RateCode"].ToString() == "None")
					{
						nonePassThrus = true;
					}
					else
					{
						validPassThrusList.Add(dRow["RateCode"].ToString().Trim());
					}
				}
			}

			foreach(DataRow detailRow in detailRowsList)
			{
				if (nonePassThrus)
				{
					detailRow["RateName"] = "null";
					detailRow["RateType"] = "null";
					detailRow["RateCategory"] = "null";
					detailRow["Model"] = 0;
				}
				else
				{
					codeRows = codesDS.Tables[0].Select("InOutPatientInd='" + inOut + "' AND RateCatgryDescr='PassThru' AND RateTypeCode='RevCode' AND RateCode='" + detailRow["RevCode"] + "'");

					if (codeRows.Length == 0)			// *** If no rate found for RevCode then check CPT
					{
						codeRows = codesDS.Tables[0].Select("InOutPatientInd='" + inOut + "' AND RateCatgryDescr='PassThru' AND RateTypeCode='CPT' AND RateCode='" + detailRow["CPT"] + "'");
					}

					if (codeRows.Length > 0)	// *** If rate found for RevCode or CPT
					{
						passThruRateRow = ratesDS.Tables[0].Rows.Find(codeRows[0]["RateSeqNum"]);

						if ( allPassThrus || validPassThrusList.Contains(codeRows[0]["RateSeqNum"].ToString()) )
						{
							detailRow["RateName"] = passThruRateRow["RateName"];
							detailRow["RateType"] = passThruRateRow["RateTypeDescr"];
							detailRow["RateCategory"] = passThruRateRow["RateCatgryDescr"];

							if ( (Convert.ToDouble(detailRow["Charges"]) >= Convert.ToDouble(passThruRateRow["ThreshldNum"])) || (Convert.ToDouble(passThruRateRow["ThreshldNum"]) == 0) )
							{
								passThruCharges = passThruCharges + Convert.ToDouble(detailRow["Charges"]);

								switch(passThruRateRow["RateTypeDescr"].ToString())
								{
									case "PerUnit" :	detailRow["Model"] = Convert.ToDouble(passThruRateRow["RateValue"]) * Convert.ToDouble(detailRow["Quantity"]); break;
									case "POC" : detailRow["Model"] = Convert.ToDouble(passThruRateRow["RateValue"]) * Convert.ToDouble(detailRow["Charges"]); break;
									case "PerVisit" :
										// '*** This is time consuming, but from a big picture perspective, not many cases/rates will have PerVisists. at least in theory
										if ( (! perVisitsApplied.Contains(passThruRateRow["RateName"]) && Convert.ToDouble(detailRow["Quantity"]) > 0) )
										{
											perVisitsApplied.Add(passThruRateRow["RateName"]);
											detailRow["Model"] = passThruRateRow["RateValue"];
										}
										break;
									default: break;
								}

								totalPassThruModel = totalPassThruModel + Convert.ToDouble(detailRow["Model"]);
							}
						}
					}

				}
			}

			return totalPassThruModel;
		}

		#endregion


	}
}

