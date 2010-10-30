using System;

using RMS_BusinessObjects;
using RMS_DALObjects;

namespace RMS_v3
{
	/// <summary>
	/// Summary description for RMS_Controller.
	/// </summary>
	public class RMS_Controller
	{
		#region "Variables"

		int var_ContractID;
		int var_RateScheduleID;
		int var_RateID;

		MainForm var_MainFrm;
		EditRateForm var_EditRateFrm;
		CoverageForm var_CoverageFrm;
		AdjustRatesForm var_AdjustRatesFrm;

		public CodesManager CodesMngr;

		#endregion

		#region "Constructor"

		public RMS_Controller()
		{
			CodesDAL codesData = new CodesDAL();
			CodesMngr = codesData.getCodesManager();		
		}

		#endregion

		#region "Properties"


		public event EventHandler RateScheduleIDChanged;

		public void reloadRateSchedule()
		{	RateScheduleIDChanged(this, EventArgs.Empty);	}

		public int RateScheduleID
		{
			get{return var_RateScheduleID;}
			set
			{
				if(this.var_RateScheduleID != value)
				{
					var_RateScheduleID = value;
					if(RateScheduleIDChanged != null)
					{
						RateScheduleIDChanged(this, EventArgs.Empty);
					}
				}
			}
		}


		public event EventHandler ContractIDChanged;

		public int ContractID
		{
			get{return var_ContractID;}
			set
			{
				if(this.var_ContractID != value)
				{
					var_ContractID = value;
					if(ContractIDChanged != null)
					{
						ContractIDChanged(this, EventArgs.Empty);
					}
				}
			}
		}


		public event EventHandler RateIDChanged;

		public int RateID
		{
			get{return var_RateID;}
			set
			{
				if(this.var_RateID != value)
				{
					var_RateID = value;
					if(RateIDChanged != null)
					{
						RateIDChanged(this, EventArgs.Empty);
					}
				}
			}
		}


		public MainForm MainFrm
		{
			get{return var_MainFrm;}
			set{ var_MainFrm = value; }
		}


		public event EventHandler RateScheduleChange;

		public void RateScheduleChanged()
		{
			RateScheduleChange(this, EventArgs.Empty);
		}


		#endregion

		#region "Window Form Methods"

		public void launchEditWindow()
		{
			this.RateID = 0;
			launchEditRateForm();
		}

		public void launchEditWindow(int in_RateID)
		{
			this.RateID = in_RateID;
			launchEditRateForm();
		}

		private void launchEditRateForm()
		{
			var_EditRateFrm = new EditRateForm(this);

			if (! (this.RateID==0) )
			{
				var_EditRateFrm.loadRate(this.RateID);
			}

			var_EditRateFrm.Show();
		}


		public void launchCoverageWindow()
		{
			var_CoverageFrm = new CoverageForm(this);

			var_CoverageFrm.Show();
		}

		public void launchAdjustRatesWindow()
		{
			var_AdjustRatesFrm = new AdjustRatesForm(this);

			var_AdjustRatesFrm.Show();
		}

		#endregion

	}
}
