using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using RMS_BusinessObjects;
using RMS_DALObjects;
using RMSCtrl_Library;

namespace RMS_v3
{
	/// <summary>
	/// Summary description for AdjustRatesForm.
	/// </summary>
	public class AdjustRatesForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.Panel barPanel;
		private System.Windows.Forms.Panel panel3;
		internal System.Windows.Forms.TextBox chgIncBx;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.CheckBox adjustPOCsBx;
		internal System.Windows.Forms.TextBox rateIncBx;
		internal System.Windows.Forms.Button rateIncBtn;
		private System.Windows.Forms.Label titleLbl;
		private System.Windows.Forms.Panel chargePanel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private RMSCtrl_Library.RMS_ProgressBar progBar;

		private RMS_Controller rmsController;

		public AdjustRatesForm(RMS_Controller in_rmsController)
		{
			rmsController = in_rmsController;

			InitializeComponent();	// Required for Windows Form Designer support


			barPanel.Controls.Add(new RMS_ProgressBar());
		}

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AdjustRatesForm));
			this.topPanel = new System.Windows.Forms.Panel();
			this.barPanel = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.chgIncBx = new System.Windows.Forms.TextBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.adjustPOCsBx = new System.Windows.Forms.CheckBox();
			this.rateIncBx = new System.Windows.Forms.TextBox();
			this.rateIncBtn = new System.Windows.Forms.Button();
			this.titleLbl = new System.Windows.Forms.Label();
			this.chargePanel = new System.Windows.Forms.Panel();
			this.progBar = new RMSCtrl_Library.RMS_ProgressBar();
			this.topPanel.SuspendLayout();
			this.barPanel.SuspendLayout();
			this.panel3.SuspendLayout();
			this.chargePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// topPanel
			// 
			this.topPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("topPanel.BackgroundImage")));
			this.topPanel.Controls.Add(this.titleLbl);
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPanel.Location = new System.Drawing.Point(0, 0);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(338, 24);
			this.topPanel.TabIndex = 49;
			// 
			// barPanel
			// 
			this.barPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.barPanel.Controls.Add(this.progBar);
			this.barPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.barPanel.Location = new System.Drawing.Point(0, 24);
			this.barPanel.Name = "barPanel";
			this.barPanel.Size = new System.Drawing.Size(338, 40);
			this.barPanel.TabIndex = 50;
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
			this.panel3.Controls.Add(this.chargePanel);
			this.panel3.Controls.Add(this.Label1);
			this.panel3.Controls.Add(this.adjustPOCsBx);
			this.panel3.Controls.Add(this.rateIncBx);
			this.panel3.Controls.Add(this.rateIncBtn);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 64);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(338, 88);
			this.panel3.TabIndex = 52;
			// 
			// chgIncBx
			// 
			this.chgIncBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.chgIncBx.Location = new System.Drawing.Point(160, 8);
			this.chgIncBx.Name = "chgIncBx";
			this.chgIncBx.Size = new System.Drawing.Size(96, 20);
			this.chgIncBx.TabIndex = 2;
			this.chgIncBx.Text = "";
			// 
			// Label1
			// 
			this.Label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label1.Location = new System.Drawing.Point(16, 8);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Label1.Size = new System.Drawing.Size(90, 16);
			this.Label1.TabIndex = 3;
			this.Label1.Text = "Rate Increase";
			// 
			// Label2
			// 
			this.Label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label2.Location = new System.Drawing.Point(8, 8);
			this.Label2.Name = "Label2";
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Label2.Size = new System.Drawing.Size(146, 14);
			this.Label2.TabIndex = 4;
			this.Label2.Text = "Charge Master Increase";
			// 
			// adjustPOCsBx
			// 
			this.adjustPOCsBx.Location = new System.Drawing.Point(104, 32);
			this.adjustPOCsBx.Name = "adjustPOCsBx";
			this.adjustPOCsBx.Size = new System.Drawing.Size(104, 16);
			this.adjustPOCsBx.TabIndex = 28;
			this.adjustPOCsBx.Text = "Adjust POCs";
			this.adjustPOCsBx.CheckedChanged += new System.EventHandler(this.adjustPOCsBx_CheckedChanged);
			// 
			// rateIncBx
			// 
			this.rateIncBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rateIncBx.Location = new System.Drawing.Point(112, 8);
			this.rateIncBx.Name = "rateIncBx";
			this.rateIncBx.Size = new System.Drawing.Size(96, 20);
			this.rateIncBx.TabIndex = 0;
			this.rateIncBx.Text = "";
			// 
			// rateIncBtn
			// 
			this.rateIncBtn.BackColor = System.Drawing.Color.White;
			this.rateIncBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rateIncBtn.Location = new System.Drawing.Point(216, 8);
			this.rateIncBtn.Name = "rateIncBtn";
			this.rateIncBtn.Size = new System.Drawing.Size(108, 24);
			this.rateIncBtn.TabIndex = 1;
			this.rateIncBtn.Text = "Increase Rates";
			this.rateIncBtn.Click += new System.EventHandler(this.rateIncBtn_Click);
			// 
			// titleLbl
			// 
			this.titleLbl.BackColor = System.Drawing.Color.Transparent;
			this.titleLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.titleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.titleLbl.ForeColor = System.Drawing.Color.White;
			this.titleLbl.Location = new System.Drawing.Point(0, 0);
			this.titleLbl.Name = "titleLbl";
			this.titleLbl.Size = new System.Drawing.Size(338, 24);
			this.titleLbl.TabIndex = 0;
			this.titleLbl.Text = "Adjust Rates";
			// 
			// chargePanel
			// 
			this.chargePanel.Controls.Add(this.chgIncBx);
			this.chargePanel.Controls.Add(this.Label2);
			this.chargePanel.Location = new System.Drawing.Point(8, 48);
			this.chargePanel.Name = "chargePanel";
			this.chargePanel.Size = new System.Drawing.Size(264, 32);
			this.chargePanel.TabIndex = 29;
			this.chargePanel.Visible = false;
			// 
			// progBar
			// 
			this.progBar.BackColor = System.Drawing.Color.WhiteSmoke;
			this.progBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progBar.Increment = 0;
			this.progBar.Location = new System.Drawing.Point(0, 0);
			this.progBar.Max = 100;
			this.progBar.Min = 0;
			this.progBar.Name = "progBar";
			this.progBar.Size = new System.Drawing.Size(338, 40);
			this.progBar.TabIndex = 0;
			this.progBar.Title = "";
			// 
			// AdjustRatesForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(338, 152);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.barPanel);
			this.Controls.Add(this.topPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AdjustRatesForm";
			this.topPanel.ResumeLayout(false);
			this.barPanel.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.chargePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void adjustPOCsBx_CheckedChanged(object sender, System.EventArgs e)
		{
			if (adjustPOCsBx.Checked)
			{	chargePanel.Visible = true;	}
			else
			{	chargePanel.Visible = false; }
		}

		private void rateIncBtn_Click(object sender, System.EventArgs e)
		{
			BaseDALObject data = new BaseDALObject();
			RateScheduleDAL rateScheduleData = new RateScheduleDAL();

			RateScheduleBO rateSchedule = rateScheduleData.getRateSchedule(ref rmsController.CodesMngr, rmsController.RateScheduleID);
			if (adjustPOCsBx.Checked)
			{
				rateSchedule.Rates.increaseRatesByPercent(Convert.ToDouble(rateIncBx.Text), adjustPOCsBx.Checked, Convert.ToDouble(chgIncBx.Text));
			}
			else
			{
				rateSchedule.Rates.increaseRatesByPercent(Convert.ToDouble(rateIncBx.Text), adjustPOCsBx.Checked, 0);
			}
				
			rateScheduleData.updateRateSchedule(rateSchedule);
			rmsController.reloadRateSchedule();
		}


	}
}
