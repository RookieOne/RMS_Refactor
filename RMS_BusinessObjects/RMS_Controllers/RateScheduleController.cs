using System;
using System.Data;
using System.Web.UI.WebControls;

using RMS_BusinessObjects;
using RMS_DALObjects;

using Microsoft.ApplicationBlocks.UIProcess;

namespace RMS_Controllers
{
	/// <summary>
	/// Summary description for RateScheduleController.
	/// </summary>
	public class RateScheduleController : ControllerBase
	{

		#region "Variables"

		#endregion

		#region "Constructors"

		public RateScheduleController( State controllerState ) : base( controllerState ){}
		

		#endregion

		#region "Methods"

		private RateScheduleBO getRateSchedule(string rateScheduleID)
		{
			RateScheduleDAL rateSchedDAL = new RateScheduleDAL();

			RateScheduleBO rateSchedule = rateSchedDAL.getRateSchedule(rateScheduleID);

			State["RateSchedule"] = rateSchedule;

			return rateSchedule;
		}

		public DataGrid getRateCategoryGrid(string rateScheduleID, string rateCategory)
		{
			RateScheduleBO rateSchedule = (RateScheduleBO) State["RateSchedule"];

			if (rateSchedule==null)	
			{ rateSchedule = getRateSchedule(rateScheduleID);	}

			return rateSchedule.Rates.getDataGrid(rateCategory);
		}

		#endregion
	}
}
