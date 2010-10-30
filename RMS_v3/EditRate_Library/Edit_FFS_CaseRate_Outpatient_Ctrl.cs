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
	/// Summary description for Edit_FFS_CaseRate_Outpatient.
	/// </summary>
	public class Edit_FFS_CaseRate_Outpatient_Ctrl : Edit_Rate_Ctrl
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox caseRateBx;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox priorityBx;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Edit_FFS_CaseRate_Outpatient_Ctrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#region "Overrides"

		public override void loadRate(RateBO rateToLoad)
		{
			Rate_FFS_Out_CaseRateBO caseRate = (Rate_FFS_Out_CaseRateBO) rateToLoad;

			caseRateBx.Text = caseRate.Rate.ToString();
			priorityBx.Text = caseRate.Priority.ToString();

		}

		public override RateBO getRate()
		{
			Rate_FFS_Out_CaseRateBO rateToReturn = new Rate_FFS_Out_CaseRateBO();

			rateToReturn.Rate = Convert.ToDouble(caseRateBx.Text);
			rateToReturn.Priority = Convert.ToInt16(priorityBx.Text);

			return rateToReturn;
		}

		public override void Clear()
		{

		}


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
			this.label1 = new System.Windows.Forms.Label();
			this.caseRateBx = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.priorityBx = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "CaseRate";
			// 
			// caseRateBx
			// 
			this.caseRateBx.Location = new System.Drawing.Point(72, 8);
			this.caseRateBx.Name = "caseRateBx";
			this.caseRateBx.TabIndex = 1;
			this.caseRateBx.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Priority";
			// 
			// priorityBx
			// 
			this.priorityBx.Location = new System.Drawing.Point(72, 40);
			this.priorityBx.Name = "priorityBx";
			this.priorityBx.Size = new System.Drawing.Size(40, 20);
			this.priorityBx.TabIndex = 3;
			this.priorityBx.Text = "";
			// 
			// Edit_FFS_CaseRate_Outpatient
			// 
			this.Controls.Add(this.priorityBx);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.caseRateBx);
			this.Controls.Add(this.label1);
			this.Name = "Edit_FFS_CaseRate_Outpatient";
			this.Size = new System.Drawing.Size(192, 72);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
