using System;
using System.Data;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for RateSchedule_Collection.
	/// </summary>
	public class RateSchedule_Collection
	{
		#region "Variables"

		ArrayList RateSchedules;

		#endregion

		#region "Constructors"

		public RateSchedule_Collection()		{RateSchedules = new ArrayList();}

		#endregion

		#region "Methods"

		public void addRateSchedule(RateScheduleBO rateScheduleToAdd)
		{
			RateSchedules.Add(rateScheduleToAdd);
		}

		public DataTable toDataTable()
		{
			DataTable rateScheduleTable = new DataTable();

			rateScheduleTable.Columns.Add("RateScheduleID", Type.GetType("System.String"));
			rateScheduleTable.Columns.Add("RateScheduleName", Type.GetType("System.String"));
			

			DataRow rateScheduleRow;
			foreach (RateScheduleBO RateSchedule in RateSchedules)
			{
				rateScheduleRow = rateScheduleTable.NewRow();
				rateScheduleRow["RateScheduleID"] = RateSchedule.ID;
				rateScheduleRow["RateScheduleName"] = RateSchedule.RateScheduleName;
				rateScheduleTable.Rows.Add(rateScheduleRow);
			}

			return rateScheduleTable;
		}

		#endregion
	}
}
