using System;
using System.Data;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for Rate_CeilingBO.
	/// </summary>
	public class Rate_CeilingBO: RateBO
	{
		#region "Variables"

		double var_Rate;
    
		#endregion

		#region "Constructors"

		public Rate_CeilingBO() : base()	{}
		public Rate_CeilingBO(int id) : base(id) {}
		public Rate_CeilingBO(int id, int in_RateSchedID) : base(id, in_RateSchedID) {}
		public Rate_CeilingBO(int id, int in_RateSchedID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes, double in_Rate) : base(id, in_RateSchedID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes)
	{
		this.Rate = in_Rate;
	}

	#endregion

	#region "Properties"

	public double Rate
{
	get{return var_Rate;}
	set{var_Rate = value;}
}

	#endregion

	#region "Methods"

	public void setRate(int id, int in_RateScheduleID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes, double in_Rate)
{
	this.setMainRate(id, in_RateScheduleID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes);
	this.Rate = in_Rate;
}

		#endregion

		#region "Overrides"
    
	public override System.Collections.ArrayList getDataColumns()
{
	ArrayList columnsList = base.getBaseDataColumns();

	columnsList.Add(new DataColumn("Rate", Type.GetType("System.Double")));

	return columnsList;
}

	public override void fillRateRow(ref DataRow rateRow)
{
	base.fillBaseRateRow(ref rateRow);

	rateRow["Rate"] = this.Rate;
}

		public override RateDataRow getRateAsRateDataRow()
		{
			RateDataRow rateRow = base.fillMainRateDataRow();

			rateRow.Rate = this.Rate;

			return rateRow;
		}

		public override void increaseRateByValue(double valueToIncrease){}

		public override void increaseRateByPercent(double percentToIncrease){}

#endregion

}
}
