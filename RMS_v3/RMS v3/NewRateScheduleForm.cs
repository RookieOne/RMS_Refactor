using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using RMS_BusinessObjects;
using RMS_DALObjects;

namespace RMS_v3
{
	/// <summary>
	/// Simple UI to retrieve a name for a new rate schedule
	/// </summary>
	public class NewRateScheduleForm : System.Windows.Forms.Form
	{
		#region "Variables"

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button setBtn;
		private System.Windows.Forms.TextBox rateScheduleNameBx;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		/// 
		private System.ComponentModel.Container components = null;

		private RMS_Controller rmsController;
		private string statusTypeCode;

		#endregion

		#region "Constructors"

		public NewRateScheduleForm(RMS_Controller in_RMSController, string in_StatusTypeCode)
		{
			InitializeComponent();

			rmsController = in_RMSController;
			statusTypeCode = in_StatusTypeCode;
		}

		#endregion

		#region "Overrides"

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.setBtn = new System.Windows.Forms.Button();
			this.rateScheduleNameBx = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Rate Schedule Name";
			// 
			// setBtn
			// 
			this.setBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.setBtn.Location = new System.Drawing.Point(336, 8);
			this.setBtn.Name = "setBtn";
			this.setBtn.TabIndex = 4;
			this.setBtn.Text = "Add";
			this.setBtn.Click += new System.EventHandler(this.setBtn_Click);
			// 
			// rateScheduleNameBx
			// 
			this.rateScheduleNameBx.Location = new System.Drawing.Point(128, 8);
			this.rateScheduleNameBx.Name = "rateScheduleNameBx";
			this.rateScheduleNameBx.Size = new System.Drawing.Size(200, 20);
			this.rateScheduleNameBx.TabIndex = 3;
			this.rateScheduleNameBx.Text = "";
			// 
			// NewRateScheduleForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(418, 40);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.setBtn);
			this.Controls.Add(this.rateScheduleNameBx);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "NewRateScheduleForm";
			this.Text = "New Rate Schedule";
			this.ResumeLayout(false);

		}
		#endregion


		#region "Events"

		public event EventHandler RateScheduleAdded;

		#endregion


		#region "Buttons"

		private void setBtn_Click(object sender, System.EventArgs e)
		{
			RateScheduleDAL rateScheduleData = new RateScheduleDAL();

			RateScheduleBO rateSchedule = new RateScheduleBO();

			rateSchedule.ContractID = rmsController.ContractID;

			rateSchedule.Status = statusTypeCode;

			rateSchedule.RateScheduleName = rateScheduleNameBx.Text;
			rateSchedule.Coverage.StartDate = System.DateTime.Today;
			rateSchedule.Coverage.EndDate = System.DateTime.Today;

			rateScheduleData.insertRateSchedule(rateSchedule);


			RateScheduleAdded(this, EventArgs.Empty);

			this.Close();
		}

		#endregion

	}
}
