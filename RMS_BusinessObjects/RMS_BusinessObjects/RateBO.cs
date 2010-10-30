using System;
using System.Data;
using System.Collections;


namespace RMS_BusinessObjects
{
	public struct RateDataRow
	{
		#region "Variables"

		private int var_RateID;
		private int var_RateScheduleID;

		private double var_AddtnlDayRate;
		private char var_InOut;
		private int var_LOS;
		private string var_RateCategory;
		private string var_RateType;
		private string var_Name;
		private double var_Rate;
		private double var_Threshold;

		#endregion


		#region "Properties"

		public int RateID
		{
			get{return var_RateID;}
			set{var_RateID = value;}
		}

		public int RateScheduleID
		{
			get{return var_RateScheduleID;}
			set{var_RateScheduleID = value;}
		}


		public double AddtnlDayRate
		{
			get{return var_AddtnlDayRate;}
			set{var_AddtnlDayRate = value;}
		}
		public char InOut
		{
			get{return var_InOut;}
			set{var_InOut = value;}
		}
		public int LOS
		{
			get{return var_LOS;}
			set{var_LOS = value;}
		}
		public string RateCategory
		{
			get{return var_RateCategory;}
			set{var_RateCategory = value;}
		}
		public string RateType
		{
			get{return var_RateType;}
			set{var_RateType = value;}
		}
		public string Name
		{
			get{return var_Name;}
			set{var_Name = value;}
		}

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
	}




	/// <summary>
	/// Summary description for rateBO.
	/// </summary>
	/// 


	public abstract class RateBO : baseBusinessObject
	{
		#region "Variables"

		int var_RateSchedID;

		string var_Name;
		string var_RateCategory;
		string var_RateType;

		char var_InOut;

		CodesBO var_RateCodes;

		private bool var_HasPassThrus;

		#endregion


		#region "Constructors"

		public RateBO() : base() {}
		public RateBO(int id) : base(id){}
		public RateBO(int id, int in_RateSchedID) : base(id){this.RateScheduleID = in_RateSchedID;}
		public RateBO(int id, int in_RateSchedID, string in_Name, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes) : base(id)
		{
			this.RateScheduleID = in_RateSchedID;
			this.Name = in_Name;
			this.RateCategory = in_RateCategory;
			this.RateType = in_RateType;
			this.InOut = in_InOut;
			this.Codes = in_RateCodes;
		}

	  #endregion


		#region "Properties"

		public int RateScheduleID
		{
			get{return var_RateSchedID;}
			set{var_RateSchedID = value;}
		}

		public string Name
		{
			get{return var_Name;}
			set{var_Name = value;}
		}

		public string RateCategory
		{
			get{return var_RateCategory;}
			set{var_RateCategory = value;}
		}

		public string RateType
		{
			get{return var_RateType;}
			set{var_RateType = value;}
		}

		public char InOut
		{
			get{return var_InOut;}
			set{var_InOut = value;}
		}

		public CodesBO Codes
		{
			get{return var_RateCodes;}
			set{var_RateCodes = value;}
		}

		public bool HasPassThrus
		{
			get{return var_HasPassThrus;}
			set{var_HasPassThrus = value;}
		}

		#endregion


		#region "Methods"

		public void setMainRate(int id, int in_RateScheduleID, string in_Name, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes)
		{
			this.ID = id;
			this.RateScheduleID = in_RateScheduleID;

			this.Name = in_Name;
			this.RateCategory = in_RateCategory;
			this.RateType = in_RateType;
			this.InOut = in_InOut;
			this.Codes = in_RateCodes;
		}

		public ArrayList getBaseDataColumns()
		{
      ArrayList columnsList = new ArrayList();

			columnsList.Add(new DataColumn("RateID", Type.GetType("System.Int16")));
			columnsList.Add(new DataColumn("RateScheduleID", Type.GetType("System.Int16")));

			columnsList.Add(new DataColumn("Name", Type.GetType("System.String")));
			columnsList.Add(new DataColumn("InOut", Type.GetType("System.String")));
			columnsList.Add(new DataColumn("RateCategory", Type.GetType("System.String")));
			columnsList.Add(new DataColumn("RateType", Type.GetType("System.String")));
			columnsList.Add(new DataColumn("Codes", Type.GetType("System.String")));

			return columnsList;
		}

		public abstract ArrayList getDataColumns();

		public void fillBaseRateRow(ref DataRow rateRow)
		{
			rateRow["RateID"] = this.ID;
			rateRow["RateScheduleID"] = this.RateScheduleID;

			rateRow["InOut"] = this.InOut;

			rateRow["Name"] = this.Name;
			rateRow["RateCategory"] = this.RateCategory;
			rateRow["RateType"] = this.RateType;

			rateRow["Codes"] = this.Codes.ToString();
		}

		public abstract void fillRateRow(ref DataRow rateRow);

		public RateDataRow fillMainRateDataRow()
		{
			RateDataRow rateRow = new RateDataRow();

			rateRow.RateID = this.ID;
			rateRow.RateScheduleID = this.RateScheduleID;

			rateRow.InOut = this.InOut;
			rateRow.RateCategory = this.RateCategory;
			rateRow.RateType = this.RateType;
			rateRow.Name = this.Name;

			return rateRow;
		}

		public abstract RateDataRow getRateAsRateDataRow();

		public abstract void increaseRateByValue(double valueToIncrease);
		public abstract void increaseRateByPercent(double percentToIncrease);

		#endregion


}
}
