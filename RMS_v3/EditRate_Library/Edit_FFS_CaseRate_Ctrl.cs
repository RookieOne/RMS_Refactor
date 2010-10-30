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
	/// Summary description for Edit_FFS_CaseRate.
	/// </summary>
	public class Edit_FFS_CaseRate_Ctrl : Edit_Rate_Ctrl
	{
		internal System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox rateBx;
		internal System.Windows.Forms.TextBox addDayBx;
		internal System.Windows.Forms.Label addDayLbl;
		internal System.Windows.Forms.TextBox losBx;
		internal System.Windows.Forms.Label losLbl;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Edit_FFS_CaseRate_Ctrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}


		#region "Methods"

		public override void loadRate(RateBO rateToLoad)
		{
			Rate_FFS_In_CaseRateBO caseRate = (Rate_FFS_In_CaseRateBO) rateToLoad;

			rateBx.Text = Convert.ToString(caseRate.Rate);
			losBx.Text = Convert.ToString(caseRate.LOS);
			addDayBx.Text = Convert.ToString(caseRate.AddtnlDayRate);
		}

		public override RateBO getRate()
		{
			Rate_FFS_In_CaseRateBO rateToReturn = new Rate_FFS_In_CaseRateBO();

			rateToReturn.Rate = Convert.ToDouble(rateBx.Text);
			rateToReturn.LOS = Convert.ToInt16(losBx.Text);
			rateToReturn.AddtnlDayRate = Convert.ToDouble(addDayBx.Text);

			return rateToReturn;
		}

		public override void Clear()
		{
			rateBx.Text = "";
			losBx.Text = "";
			addDayBx.Text = "";
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
			this.rateBx = new System.Windows.Forms.TextBox();
			this.addDayBx = new System.Windows.Forms.TextBox();
			this.addDayLbl = new System.Windows.Forms.Label();
			this.losBx = new System.Windows.Forms.TextBox();
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
			this.label1.TabIndex = 71;
			this.label1.Text = "Case Rate";
			// 
			// rateBx
			// 
			this.rateBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rateBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateBx.Location = new System.Drawing.Point(128, 8);
			this.rateBx.Name = "rateBx";
			this.rateBx.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.rateBx.Size = new System.Drawing.Size(76, 20);
			this.rateBx.TabIndex = 66;
			this.rateBx.Text = "0";
			// 
			// addDayBx
			// 
			this.addDayBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.addDayBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.addDayBx.Location = new System.Drawing.Point(128, 56);
			this.addDayBx.Name = "addDayBx";
			this.addDayBx.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.addDayBx.Size = new System.Drawing.Size(68, 20);
			this.addDayBx.TabIndex = 68;
			this.addDayBx.Text = "0";
			// 
			// addDayLbl
			// 
			this.addDayLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.addDayLbl.Location = new System.Drawing.Point(16, 56);
			this.addDayLbl.Name = "addDayLbl";
			this.addDayLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.addDayLbl.Size = new System.Drawing.Size(104, 18);
			this.addDayLbl.TabIndex = 70;
			this.addDayLbl.Text = "Additional Day Rate";
			// 
			// losBx
			// 
			this.losBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.losBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.losBx.Location = new System.Drawing.Point(128, 32);
			this.losBx.Name = "losBx";
			this.losBx.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.losBx.Size = new System.Drawing.Size(36, 20);
			this.losBx.TabIndex = 67;
			this.losBx.Text = "0";
			// 
			// losLbl
			// 
			this.losLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.losLbl.Location = new System.Drawing.Point(16, 32);
			this.losLbl.Name = "losLbl";
			this.losLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.losLbl.Size = new System.Drawing.Size(104, 14);
			this.losLbl.TabIndex = 69;
			this.losLbl.Text = "LOS";
			// 
			// Edit_FFS_CaseRate
			// 
			this.Controls.Add(this.label1);
			this.Controls.Add(this.rateBx);
			this.Controls.Add(this.addDayBx);
			this.Controls.Add(this.addDayLbl);
			this.Controls.Add(this.losBx);
			this.Controls.Add(this.losLbl);
			this.Name = "Edit_FFS_CaseRate";
			this.Size = new System.Drawing.Size(216, 88);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
