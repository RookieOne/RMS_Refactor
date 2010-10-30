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
	/// Summary description for Edit_StopLoss.
	/// </summary>
	public class Edit_StopLoss_Ctrl : Edit_Rate_Ctrl
	{
		internal System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox thresholdBx;
		internal System.Windows.Forms.TextBox pocBx;
		internal System.Windows.Forms.Label addDayLbl;
		internal System.Windows.Forms.TextBox dailyCapBx;
		internal System.Windows.Forms.Label losLbl;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Edit_StopLoss_Ctrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#region "Methods"

		public override void loadRate(RateBO rateToLoad)
		{
			Rate_StopLossBO stopLoss = (Rate_StopLossBO) rateToLoad;

			thresholdBx.Text = Convert.ToString(stopLoss.Threshold);
			pocBx.Text = Convert.ToString(stopLoss.POC);
			dailyCapBx.Text = Convert.ToString(stopLoss.DailyCap);
		}

		public override RateBO getRate()
		{
			Rate_StopLossBO rateToReturn = new Rate_StopLossBO();

			rateToReturn.Threshold = Convert.ToDouble(thresholdBx.Text);
			rateToReturn.POC = Convert.ToDouble(pocBx.Text);
			rateToReturn.DailyCap = Convert.ToDouble(dailyCapBx.Text);

			return rateToReturn;
		}

		public override void Clear()
		{
			thresholdBx.Text = "";
			pocBx.Text = "";
			dailyCapBx.Text = "";
		}

		#endregion


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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.thresholdBx = new System.Windows.Forms.TextBox();
			this.pocBx = new System.Windows.Forms.TextBox();
			this.addDayLbl = new System.Windows.Forms.Label();
			this.dailyCapBx = new System.Windows.Forms.TextBox();
			this.losLbl = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.label1.Size = new System.Drawing.Size(112, 14);
			this.label1.TabIndex = 77;
			this.label1.Text = "Stop Loss Threshold";
			// 
			// thresholdBx
			// 
			this.thresholdBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.thresholdBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.thresholdBx.Location = new System.Drawing.Point(128, 8);
			this.thresholdBx.Name = "thresholdBx";
			this.thresholdBx.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.thresholdBx.Size = new System.Drawing.Size(76, 20);
			this.thresholdBx.TabIndex = 72;
			this.thresholdBx.Text = "0";
			// 
			// pocBx
			// 
			this.pocBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pocBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pocBx.Location = new System.Drawing.Point(136, 32);
			this.pocBx.Name = "pocBx";
			this.pocBx.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.pocBx.Size = new System.Drawing.Size(68, 20);
			this.pocBx.TabIndex = 74;
			this.pocBx.Text = "0";
			// 
			// addDayLbl
			// 
			this.addDayLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.addDayLbl.Location = new System.Drawing.Point(16, 32);
			this.addDayLbl.Name = "addDayLbl";
			this.addDayLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.addDayLbl.Size = new System.Drawing.Size(104, 18);
			this.addDayLbl.TabIndex = 76;
			this.addDayLbl.Text = "Percent of Charges";
			// 
			// dailyCapBx
			// 
			this.dailyCapBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dailyCapBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dailyCapBx.Location = new System.Drawing.Point(136, 68);
			this.dailyCapBx.Name = "dailyCapBx";
			this.dailyCapBx.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.dailyCapBx.Size = new System.Drawing.Size(68, 20);
			this.dailyCapBx.TabIndex = 73;
			this.dailyCapBx.Text = "0";
			// 
			// losLbl
			// 
			this.losLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.losLbl.Location = new System.Drawing.Point(16, 68);
			this.losLbl.Name = "losLbl";
			this.losLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.losLbl.Size = new System.Drawing.Size(104, 14);
			this.losLbl.TabIndex = 75;
			this.losLbl.Text = "Daily Cap";
			// 
			// Edit_StopLoss
			// 
			this.Controls.Add(this.label1);
			this.Controls.Add(this.thresholdBx);
			this.Controls.Add(this.pocBx);
			this.Controls.Add(this.addDayLbl);
			this.Controls.Add(this.dailyCapBx);
			this.Controls.Add(this.losLbl);
			this.Name = "Edit_StopLoss";
			this.Size = new System.Drawing.Size(224, 104);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
