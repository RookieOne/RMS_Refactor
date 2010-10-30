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
	/// Summary description for Edit_FFS_POC_OutpatientCtrl.
	/// </summary>
	public class Edit_FFS_POC_Outpatient_Ctrl : Edit_Rate_Ctrl
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox pocBx;
		private System.Windows.Forms.TextBox priorityBx;
		private System.Windows.Forms.Label label2;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Edit_FFS_POC_Outpatient_Ctrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		#region "Overrides"

		public override void loadRate(RateBO rateToLoad)
		{
			Rate_FFS_Out_CaseRateBO caseRate = (Rate_FFS_Out_CaseRateBO) rateToLoad;

			pocBx.Text = caseRate.Rate.ToString();
			priorityBx.Text = caseRate.Priority.ToString();

		}

		public override RateBO getRate()
		{
			Rate_FFS_Out_CaseRateBO rateToReturn = new Rate_FFS_Out_CaseRateBO();

			rateToReturn.Rate = Convert.ToDouble(pocBx.Text);
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
			this.pocBx = new System.Windows.Forms.TextBox();
			this.priorityBx = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "POC";
			// 
			// pocBx
			// 
			this.pocBx.Location = new System.Drawing.Point(64, 8);
			this.pocBx.Name = "pocBx";
			this.pocBx.TabIndex = 1;
			this.pocBx.Text = "";
			// 
			// priorityBx
			// 
			this.priorityBx.Location = new System.Drawing.Point(64, 40);
			this.priorityBx.Name = "priorityBx";
			this.priorityBx.Size = new System.Drawing.Size(40, 20);
			this.priorityBx.TabIndex = 3;
			this.priorityBx.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 24);
			this.label2.TabIndex = 2;
			this.label2.Text = "Priority";
			// 
			// Edit_FFS_POC_OutpatientCtrl
			// 
			this.Controls.Add(this.priorityBx);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pocBx);
			this.Controls.Add(this.label1);
			this.Name = "Edit_FFS_POC_OutpatientCtrl";
			this.Size = new System.Drawing.Size(176, 80);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
