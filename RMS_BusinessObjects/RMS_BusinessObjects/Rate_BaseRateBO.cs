using System;
using System.Data;
using System.Collections;

namespace RMS_BusinessObjects
{
	public struct WeightTableStruct
	{
		public int TableID;
		public string TableName;

		public WeightTableStruct(int in_TableID, string in_TableName)
		{	this.TableID = in_TableID; this.TableName = in_TableName;	}

		public override string ToString()
		{
			return TableName;
		}

	}

	/// <summary>
	/// Summary description for Rate_BaseRateBO.
	/// </summary>
	public class Rate_BaseRateBO: Rate_W_PassThruBO
	{
		#region "Variables"

		double var_Rate;

		WeightTableStruct var_WeightTable;
    
		#endregion

		#region "Constructors"

		public Rate_BaseRateBO() : base()	{}
		public Rate_BaseRateBO(int id) : base(id) {}
		public Rate_BaseRateBO(int id, int in_RateSchedID) : base(id, in_RateSchedID) {}
		public Rate_BaseRateBO(int id, int in_RateSchedID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes, double in_Rate, int in_DRG_WgtTableID, string in_DRG_WgtTableName) : base(id, in_RateSchedID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes)
	{
      this.Rate = in_Rate;
			this.WeightTable = new WeightTableStruct(in_DRG_WgtTableID, in_DRG_WgtTableName);
	}

	#endregion

	#region "Properties"

		public double Rate
{
	get{return var_Rate;}
	set{var_Rate = value;}
}

		public WeightTableStruct WeightTable
		{
			get{return var_WeightTable;}
			set{var_WeightTable = value;}
		}

	#endregion

	#region "Methods"

	public void setRate(int id, int in_RateScheduleID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes, double in_Rate, int in_DRG_WgtTableID, string in_DRG_WgtTableName)
{
	this.setMainRate(id, in_RateScheduleID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes);
	this.Rate = in_Rate;
		this.WeightTable = new WeightTableStruct(in_DRG_WgtTableID, in_DRG_WgtTableName);
}

		#endregion

		#region "Overrides"

	public override System.Collections.ArrayList getDataColumns()
{
		ArrayList columnsList = base.getBaseDataColumns();

		columnsList.Add(new DataColumn("Rate", Type.GetType("System.Double")));
		columnsList.Add(new DataColumn("Table", Type.GetType("System.String")));
		columnsList.Add(new DataColumn("PassThrus", Type.GetType("System.String")));

	return columnsList;
}

	public override void fillRateRow(ref DataRow rateRow)
{
		base.fillBaseRateRow(ref rateRow);

		rateRow["Rate"] = this.Rate;
		rateRow["Table"] = this.WeightTable.TableName;
		rateRow["PassThrus"] = this.PassThrus.ToString();
}

		public override RateDataRow getRateAsRateDataRow()
		{
			RateDataRow rateRow = base.fillMainRateDataRow();

			rateRow.Rate = this.Rate;

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
