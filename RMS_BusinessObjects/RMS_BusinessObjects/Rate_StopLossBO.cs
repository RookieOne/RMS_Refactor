using System;
using System.Data;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for StopLossBO.
	/// </summary>
	public class Rate_StopLossBO : Rate_W_PassThruBO
	{
		#region "Variables"

		double var_Threshold;
		double var_POC;
		double var_DailyCap;
    
		#endregion

		#region "Constructors"

		public Rate_StopLossBO() : base()	{}
		public Rate_StopLossBO(int id) : base(id) {}
		public Rate_StopLossBO(int id, int in_RateSchedID) : base(id, in_RateSchedID) {}
		public Rate_StopLossBO(int id, int in_RateSchedID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes, double in_Threshold, double in_POC, double in_DailyCap) : base(id, in_RateSchedID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes)
	{
			this.Threshold = in_Threshold;
			this.POC = in_POC;
			this.DailyCap = in_DailyCap;
	}

	#endregion

	#region "Properties"

		public double Threshold
{
	get{return var_Threshold;}
	set{var_Threshold = value;}
}

		public double POC
{
	get{return var_POC;}
	set{var_POC = value;}
}

		public double DailyCap
{
	get{return var_DailyCap;}
	set{var_DailyCap = value;}
}


	#endregion


	#region "Methods"

	public void setRate(int id, int in_RateScheduleID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes, double in_Threshold, double in_POC, double in_DailyCap)
{
	this.setMainRate(id, in_RateScheduleID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes);
		this.Threshold = in_Threshold;
		this.POC = in_POC;
		this.DailyCap = in_DailyCap;
}
		#endregion

		#region "Overrides"

	public override System.Collections.ArrayList getDataColumns()
{
	ArrayList columnsList = base.getBaseDataColumns();

	columnsList.Add(new DataColumn("Threshold", Type.GetType("System.Double")));
	columnsList.Add(new DataColumn("POC", Type.GetType("System.Double")));
	columnsList.Add(new DataColumn("DailyCap", Type.GetType("System.Double")));

	columnsList.Add(new DataColumn("PassThrus", Type.GetType("System.String")));

	return columnsList;
}

	public override void fillRateRow(ref DataRow rateRow)
{
	base.fillBaseRateRow(ref rateRow);

	rateRow["Threshold"] = this.Threshold;
	rateRow["POC"] = this.POC;
	rateRow["DailyCap"] = this.DailyCap;

	rateRow["PassThrus"] = this.PassThrus.ToString();
}

		public override RateDataRow getRateAsRateDataRow()
		{
			RateDataRow rateRow = base.fillMainRateDataRow();

			rateRow.Threshold = this.POC;
			rateRow.Rate = this.Threshold;
			rateRow.AddtnlDayRate = this.DailyCap;

			return rateRow;
		}

		public override void increaseRateByValue(double valueToIncrease){}
		public override void increaseRateByPercent(double percentToIncrease){}


#endregion
	}
}
