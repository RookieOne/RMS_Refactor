using System;
using System.Data;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for FFS_CaseRateBO.
	/// </summary>
	public class Rate_FFS_In_CaseRateBO : Rate_W_PassThruBO
	{
		#region "Variables"

		double var_Rate;
		double var_AddtnlDayRate;
		int var_LOS;
    
		#endregion

		#region "Constructors"

		public Rate_FFS_In_CaseRateBO() : base()	{}
		public Rate_FFS_In_CaseRateBO(int id) : base(id) {}
		public Rate_FFS_In_CaseRateBO(int id, int in_RateSchedID) : base(id, in_RateSchedID) {}
		public Rate_FFS_In_CaseRateBO(int id, int in_RateSchedID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes, double in_Rate, double in_AddtnlDayRate, int in_LOS) : base(id, in_RateSchedID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes)
	{
      this.Rate = in_Rate;
			this.AddtnlDayRate = in_AddtnlDayRate;
			this.LOS = in_LOS;		
	}

	#endregion

	#region "Properties"

		public double Rate
		{
			get{return var_Rate;}
			set{var_Rate = value;}
		}

		public double AddtnlDayRate
		{
			get{return var_AddtnlDayRate;}
			set{var_AddtnlDayRate = value;}
		}

		public int LOS
		{
			get{return var_LOS;}
			set{var_LOS = value;}
		}



	#endregion


	#region "Methods"

	public void setRate(int id, int in_RateScheduleID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes, double in_Rate, double in_AddtnlDayRate, int in_LOS)
{
		this.setMainRate(id, in_RateScheduleID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes);
		this.Rate = in_Rate;
		this.AddtnlDayRate = in_AddtnlDayRate;
		this.LOS = in_LOS;	
}

		#endregion

		#region "Overrides"

	public override System.Collections.ArrayList getDataColumns()
{
	ArrayList columnsList = base.getBaseDataColumns();

		columnsList.Add(new DataColumn("Rate", Type.GetType("System.Double")));
		columnsList.Add(new DataColumn("AddtnlDayRate", Type.GetType("System.Double")));
		columnsList.Add(new DataColumn("LOS", Type.GetType("System.Int16")));
		columnsList.Add(new DataColumn("PassThrus", Type.GetType("System.String")));

	return columnsList;
}

	public override void fillRateRow(ref DataRow rateRow)
{
	base.fillBaseRateRow(ref rateRow);

	rateRow["Rate"] = this.Rate;
		rateRow["AddtnlDayRate"] = this.AddtnlDayRate;
		rateRow["LOS"] = this.LOS;
		rateRow["PassThrus"] = this.PassThrus.ToString();
}

		public override RateDataRow getRateAsRateDataRow()
		{
			RateDataRow rateRow = base.fillMainRateDataRow();

			rateRow.AddtnlDayRate = this.AddtnlDayRate;
			rateRow.Rate = this.Rate;
			rateRow.LOS = this.LOS;

			return rateRow;
		}

		public override void increaseRateByValue(double valueToIncrease)
		{
			this.Rate += valueToIncrease;
		}

		public override void increaseRateByPercent(double percentToIncrease)
		{
			this.Rate = System.Math.Round(this.Rate * percentToIncrease, 0);
			this.AddtnlDayRate = System.Math.Round(this.AddtnlDayRate * percentToIncrease, 0);
		}

#endregion
	}
}
