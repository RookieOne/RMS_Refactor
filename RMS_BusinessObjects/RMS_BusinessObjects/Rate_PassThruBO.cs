using System;
using System.Collections;
using System.Data;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for PassThruRateBO.
	/// </summary>
	public class Rate_PassThruBO : RateBO
	{
		#region "Variables"

		double var_Rate;
		double var_Threshold;
    
		#endregion

		#region "Constructors"

		public Rate_PassThruBO() : base()	{}
		public Rate_PassThruBO(int id) : base(id) {}
		public Rate_PassThruBO(int id, int in_RateSchedID) : base(id, in_RateSchedID) {}
		public Rate_PassThruBO(int id, int in_RateSchedID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes, double in_Rate, double in_Threshold) : base(id, in_RateSchedID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes)
	{
		this.Rate = in_Rate;
			this.Threshold = in_Threshold;
	}

	#endregion

	#region "Properties"

	public double Rate
{
	get{return var_Rate;}
	set{var_Rate = value;}
}

		public double Threshold
		{
			get{return var_Threshold;}
			set{var_Threshold = value;}
		}


	#endregion

	#region "Methods"

	public void setRate(int id, int in_RateScheduleID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes, double in_Rate, double in_Threshold)
{
	this.setMainRate(id, in_RateScheduleID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes);
	this.Rate = in_Rate;
		this.Threshold = in_Threshold;
}

		#endregion

		#region "Overrides"

	public override System.Collections.ArrayList getDataColumns()
{
	ArrayList columnsList = base.getBaseDataColumns();

	columnsList.Add(new DataColumn("Rate", Type.GetType("System.Double")));
		columnsList.Add(new DataColumn("Threshold", Type.GetType("System.Double")));

	return columnsList;
}

	public override void fillRateRow(ref DataRow rateRow)
{
	base.fillBaseRateRow(ref rateRow);

	rateRow["Rate"] = this.Rate;
		rateRow["Threshold"] = this.Threshold;
}

		public override RateDataRow getRateAsRateDataRow()
		{
			RateDataRow rateRow = base.fillMainRateDataRow();

			rateRow.Threshold = this.Threshold;
			rateRow.Rate = this.Rate;

			return rateRow;
		}

		public override void increaseRateByValue(double valueToIncrease)
		{

		}

		public override void increaseRateByPercent(double percentToIncrease)
		{
			this.Rate = System.Math.Round(this.Rate * percentToIncrease, 2);
			this.Threshold = System.Math.Round(this.Threshold * percentToIncrease, 0);
		}

#endregion
}
}
