using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for ASCRateBO.
	/// </summary>
	/// 

	public struct ASCRateStruct
	{
		#region "Variables"

		public string Group;
		public double Rate;
		public double Threshold;

		#endregion

		#region "Constructors"

		public ASCRateStruct(string in_Group, double in_Rate, double in_Threshold)	
		{
			this.Group = in_Group;
			this.Rate = in_Rate;
			this.Threshold = in_Threshold;
		}

		#endregion
	}


	public class Rate_ASCRateBO : RateBO
	{

		#region "Variables"

		int var_Priority;
		double var_Threshold;
		int var_GroupTableID;
		string var_GroupName;

		bool var_StandardCPTs;

		ArrayList var_RateReimbursement;
		ArrayList var_ASCRates;
    
		#endregion

		#region "Constructors"

		public Rate_ASCRateBO() : base()	
		{var_RateReimbursement = new ArrayList(); var_ASCRates = new ArrayList();}

		public Rate_ASCRateBO(int id) : base(id)
		{var_RateReimbursement = new ArrayList(); var_ASCRates = new ArrayList();}

		public Rate_ASCRateBO(int id, int in_RateSchedID) : base(id, in_RateSchedID)	
		{var_RateReimbursement = new ArrayList(); var_ASCRates = new ArrayList();}

		#endregion

		#region "Properties"

		public int Priority
		{
			get{return var_Priority;}
			set{var_Priority = value;}
		}

		public double Threshold
		{
			get{return var_Threshold;}
			set{var_Threshold = value;}
		}

		public int GroupTableID
		{
			get{return var_GroupTableID;}
			set{var_GroupTableID = value;}
		}

		public string GroupName
		{
			get{return var_GroupName;}
			set{var_GroupName = value;}
		}

		public bool StandardCPTs
		{
			get{return var_StandardCPTs;}
			set{var_StandardCPTs = value;}
		}

		public ArrayList RateReimbursement
		{
			get{return var_RateReimbursement;}
			set{var_RateReimbursement = value;}
		}

		public ArrayList ASCRates
		{
			get{return var_ASCRates;}
			set{var_ASCRates = value;}
		}


		#endregion

		#region "Methods"

		public void addRateReimbursement(double rateReimbursementPercent)
		{	this.RateReimbursement.Add(rateReimbursementPercent); this.RateReimbursement.Sort();	}

		public double RateReimbursementAt(int index)
		{	return Convert.ToDouble(this.RateReimbursement[index]);	}


		public void addASCRate(string in_Group, double in_Rate, double in_Threshold)
		{	this.ASCRates.Add(new ASCRateStruct(in_Group, in_Rate, in_Threshold));	}

		public ASCRateStruct ASCRateAt(int index)
		{	return (ASCRateStruct) this.ASCRates[index];	}

		public DataTable getASCRateTable()
		{
			DataTable ascRateTable = new DataTable("ASC Rates");

			ascRateTable.Columns.Add(new DataColumn("Group", Type.GetType("System.String")));
			ascRateTable.Columns.Add(new DataColumn("Rate", Type.GetType("System.String")));
			ascRateTable.Columns.Add(new DataColumn("Threshold", Type.GetType("System.String")));

			DataRow newRow;
			ASCRateStruct rate;

			for(int r=0; r<this.ASCRates.Count; r++)
			{
				rate = (ASCRateStruct) this.ASCRates[r];
				newRow = ascRateTable.NewRow();

				newRow["Group"] = rate.Group;

				if (rate.Rate<=1) // Percent POC
        {	newRow["Rate"] = string.Format("{0:P0}", rate.Rate);	}
				else	// Currency Rate
				{	newRow["Rate"] = string.Format("{0:C0}", rate.Rate);	}

				newRow["Threshold"] = string.Format("{0:C0}", rate.Threshold);
				
				ascRateTable.Rows.Add(newRow);
			}

			return ascRateTable;
		}

		#endregion

		#region "Override"

		public override System.Collections.ArrayList getDataColumns(){return null;}

		public override void fillRateRow(ref DataRow rateRow){}

		public override RateDataRow getRateAsRateDataRow(){ return base.fillMainRateDataRow();}

		public override void increaseRateByValue(double valueToIncrease)
		{

		}

		public override void increaseRateByPercent(double percentToIncrease)
		{

		}


		#endregion
	
	}
}
