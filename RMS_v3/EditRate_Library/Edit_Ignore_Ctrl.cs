using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using RMS_BusinessObjects;

namespace EditRate_Library
{
	/// <summary>
	/// Edit Control for Ignore Rates
	/// </summary>
	public class Edit_Ignore_Ctrl : Edit_Rate_Ctrl
	{
		#region "Variables"

		private System.ComponentModel.Container components = null;

		#endregion

		#region "Constructors"

		public Edit_Ignore_Ctrl()
		{
			InitializeComponent();	// This call is required by the Windows.Forms Form Designer.
			this.Height = 0;
		}

		#endregion


		#region "Overrides"

		public override void loadRate(RateBO rateToLoad){}

		public override RateBO getRate()
		{
			Rate_IgnoreBO rateToReturn = new Rate_IgnoreBO();

			return rateToReturn;
		}

		public override void Clear()	{	}


		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
