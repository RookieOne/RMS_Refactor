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
	/// Simple UI to retrieve a name for a new contract
	/// </summary>
	public class NewContractForm : System.Windows.Forms.Form
	{
		#region "Variables"

		private System.Windows.Forms.TextBox contractNameBx;
		private System.Windows.Forms.Button setBtn;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region "Constructors"

		public NewContractForm()
		{
			InitializeComponent();	// Required for Windows Form Designer support
		}

		#endregion

		#region "Override"

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
			this.contractNameBx = new System.Windows.Forms.TextBox();
			this.setBtn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// contractNameBx
			// 
			this.contractNameBx.Location = new System.Drawing.Point(92, 8);
			this.contractNameBx.Name = "contractNameBx";
			this.contractNameBx.Size = new System.Drawing.Size(200, 20);
			this.contractNameBx.TabIndex = 0;
			this.contractNameBx.Text = "";
			// 
			// setBtn
			// 
			this.setBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.setBtn.Location = new System.Drawing.Point(304, 8);
			this.setBtn.Name = "setBtn";
			this.setBtn.TabIndex = 1;
			this.setBtn.Text = "Add";
			this.setBtn.Click += new System.EventHandler(this.setBtn_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Contract Name";
			// 
			// NewContractForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(386, 40);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.setBtn);
			this.Controls.Add(this.contractNameBx);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "NewContractForm";
			this.Text = "New Contract";
			this.ResumeLayout(false);

		}
		#endregion


		#region "Events"

		public event EventHandler ContractAdded;

		#endregion


		#region "Buttons"

		private void setBtn_Click(object sender, System.EventArgs e)
		{
			ContractBO newContract = new ContractBO();

			newContract.ContractName = contractNameBx.Text;

			ContractDAL contractData = new ContractDAL();

			int contractID = contractData.insertContract(newContract);

			RateScheduleBO rateSchedule = new RateScheduleBO();

			rateSchedule.ContractID = contractID;
			rateSchedule.RateScheduleName = contractNameBx.Text;
			rateSchedule.Coverage.StartDate = System.DateTime.Today;
			rateSchedule.Coverage.EndDate = System.DateTime.Today;

			// Add by default to Development
			rateSchedule.Status = "3";

			RateScheduleDAL rateScheduleData = new RateScheduleDAL();
			rateScheduleData.insertRateSchedule(rateSchedule);



			ContractAdded(this, EventArgs.Empty);

			this.Close();
		}

		#endregion

	}
}
