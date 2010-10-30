using System;
using System.Data;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for LessorOfBO.
	/// </summary>
	public class Rate_LessorOfBO : RateBO
	{
		#region "Variables"
    
		#endregion

		#region "Constructors"

		public Rate_LessorOfBO() : base()	{}
		public Rate_LessorOfBO(int id) : base(id) {}
		public Rate_LessorOfBO(int id, int in_RateSchedID) : base(id, in_RateSchedID) {}
		public Rate_LessorOfBO(int id, int in_RateSchedID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes) : base(id, in_RateSchedID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes)
	{	}

	#endregion

	#region "Properties"


	#endregion


	#region "Methods"

	public void setRate(int id, int in_RateScheduleID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes)
{
	this.setMainRate(id, in_RateScheduleID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes);
}

		#endregion

		#region "Overrides"

	public override System.Collections.ArrayList getDataColumns()
{
	ArrayList columnsList = base.getBaseDataColumns();

	return columnsList;
}

	public override void fillRateRow(ref DataRow rateRow)
{
	base.fillBaseRateRow(ref rateRow);
}

		public override RateDataRow getRateAsRateDataRow()
		{
			return base.fillMainRateDataRow();
		}

		public override void increaseRateByValue(double valueToIncrease){}
		public override void increaseRateByPercent(double percentToIncrease){}

		#endregion

}
}
