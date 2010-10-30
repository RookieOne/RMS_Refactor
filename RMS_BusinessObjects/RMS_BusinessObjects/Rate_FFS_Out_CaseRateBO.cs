using System;
using System.Data;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for Rate_FFS_Out_CaseRateBO.
	/// </summary>
	public class Rate_FFS_Out_CaseRateBO : Rate_W_PassThruBO
	{
		#region "Variables"

		double var_Rate;
		int var_Priority;
    
		#endregion

		#region "Constructors"

		public Rate_FFS_Out_CaseRateBO() : base()	{}
		public Rate_FFS_Out_CaseRateBO(int id) : base(id) {}
		public Rate_FFS_Out_CaseRateBO(int id, int in_RateSchedID) : base(id, in_RateSchedID) {}
		public Rate_FFS_Out_CaseRateBO(int id, int in_RateSchedID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes, double in_Rate, int in_Priority) : base(id, in_RateSchedID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes)
	{
		this.Rate = in_Rate;
		this.Priority = in_Priority;
	}

	#endregion

	#region "Properties"

	public double Rate
{
	get{return var_Rate;}
	set{var_Rate = value;}
}

	public int Priority
{
	get{return var_Priority;}
	set{var_Priority = value;}
}



	#endregion


	#region "Methods"

	public void setRate(int id, int in_RateScheduleID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes, double in_Rate, int in_Priority)
{
		this.setMainRate(id, in_RateScheduleID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes);
		this.Rate = in_Rate;
		this.Priority = in_Priority;
}

	#endregion

	#region "Overrides"

	public override System.Collections.ArrayList getDataColumns()
{
	ArrayList columnsList = base.getBaseDataColumns();

	columnsList.Add(new DataColumn("Rate", Type.GetType("System.Double")));
	columnsList.Add(new DataColumn("Priority", Type.GetType("System.Double")));
	columnsList.Add(new DataColumn("PassThrus", Type.GetType("System.String")));

	return columnsList;
}

	public override void fillRateRow(ref DataRow rateRow)
{
	base.fillBaseRateRow(ref rateRow);

	rateRow["Rate"] = this.Rate;
	rateRow["Priority"] = this.Priority;
	rateRow["PassThrus"] = this.PassThrus.ToString();
}

public override RateDataRow getRateAsRateDataRow()
{
RateDataRow rateRow = base.fillMainRateDataRow();

rateRow.Rate = this.Rate;
rateRow.LOS = this.Priority;

return rateRow;
}

public override void increaseRateByValue(double valueToIncrease)
{
this.Rate += valueToIncrease;
}

public override void increaseRateByPercent(double percentToIncrease)
{
this.Rate = System.Math.Round(this.Rate * percentToIncrease, 0);
}

#endregion
}
}
